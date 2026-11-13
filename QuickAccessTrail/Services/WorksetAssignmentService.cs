using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Macros;
using QuickAccessTrail.Request;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickAccessTrail.Services
{
    public class WorksetAssignmentService
    {
        public AssignmentResult Assign(Autodesk.Revit.DB.Document doc, View view, WorksetAssignmentRequest request)
        {
            AssignmentResult result = new AssignmentResult();
            Dictionary<string, WorksetId> worksetMap = new Dictionary<string, WorksetId>(StringComparer.OrdinalIgnoreCase);
            FilteredWorksetCollector collector = new FilteredWorksetCollector(doc);
            foreach (Workset workset in collector.OfKind(WorksetKind.UserWorkset))
            {
                worksetMap[workset.Name] = workset.Id;
            }
            using (Transaction transaction = new Transaction(doc, "Assign Worksets"))
            {
                transaction.Start();
                foreach (var item in request.CategoryWorksets)
                {
                    string categoryName = item.Key;
                    string worksetName = item.Value;
                    if (!worksetMap.ContainsKey(worksetName))
                    {
                        result.FailedCount++;
                        continue;
                    }
                    WorksetId targetWorksetId = worksetMap[worksetName];
                    Category category = GetCategory(doc, categoryName);
                    if (category == null)
                    {
                        result.FailedCount++;
                        continue;
                    }
                    ElementCategoryFilter categoryFilter = new ElementCategoryFilter(category.Id);
                    IList<Element> elements = new FilteredElementCollector(doc, view.Id).WherePasses(categoryFilter).WhereElementIsNotElementType().ToElements();
                    foreach (Element element in elements)
                    {
                        if (element.IsHidden(view))
                            continue;
                        Parameter parameter = element.get_Parameter(BuiltInParameter.ELEM_PARTITION_PARAM);
                        if (parameter == null || parameter.IsReadOnly)
                        {
                            result.FailedCount++;
                            continue;
                        }
                        if (parameter.AsInteger() == targetWorksetId.IntegerValue)
                        {
                            result.SkippedCount++;
                            continue;
                        }
                        parameter.Set(targetWorksetId.IntegerValue);
                        result.AssignedCount++;
                    }
                }
                transaction.Commit();
            }
            return result;
        }
        private Category GetCategory(Autodesk.Revit.DB.Document doc, string categoryName)
        {
            foreach (Category category in doc.Settings.Categories)
            {
                if (category.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase))
                {
                    return category;
                }
            }

            return null;
        }
        public class AssignmentResult
        {
            public int AssignedCount { get; set; }

            public int SkippedCount { get; set; }

            public int FailedCount { get; set; }
        }


    }
}
