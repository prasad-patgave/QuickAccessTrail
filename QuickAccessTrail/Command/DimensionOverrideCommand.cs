using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using QuickAccessTrail.WPF;
using QuickAccessTrail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickAccessTrail.FIlters;
using Autodesk.Revit.UI.Selection;

namespace QuickAccessTrail.Command
{
    [Transaction(TransactionMode.Manual)]
    public class DimensionOverrideCommand : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            OverrideModel data;
            try
            {
                DimensionOverride window = new DimensionOverride();
                if (window.ShowDialog() != true)
                {
                    return Autodesk.Revit.UI.Result.Cancelled;
                }
                data = window.model;
            }
            catch (Exception ex)
            {

                TaskDialog.Show("Dedug Error", ex.ToString());
                return Autodesk.Revit.UI.Result.Failed;
            }
            DImensionFilter filter = new DImensionFilter();
            IList<Reference> refs;
            try
            {
                refs = uidoc.Selection.PickObjects(ObjectType.Element, filter, "Select Dimension");
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Autodesk.Revit.UI.Result.Cancelled;
            }
            string ReplacementText = data.TextOverride;
            using (Transaction tran = new Transaction(doc, "Apply EQ Text"))
            {
                tran.Start();
                foreach (Reference re in refs)
                {
                    Dimension dim = doc.GetElement(re.ElementId) as Dimension;
                    if (dim == null)
                    {
                        continue;
                    }
                    DimensionSegmentArray segments = dim.Segments;
                    foreach (DimensionSegment seg in segments)
                    {
                        seg.ValueOverride = ReplacementText;
                    }
                }
                tran.Commit();
            }
            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}
