using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick_Access.Command
{
    [Transaction(TransactionMode.Manual)]
    public class ToLower : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            try
            {
                var eleId = uidoc.Selection.PickObjects(ObjectType.Element, new TextNoteSelectionFilter(), "Select Text Note");
                Transaction tran = new Transaction(doc, "To Lower");
                tran.Start();
                foreach (var el in eleId)
                {
                    TextElement ele = doc.GetElement(el.ElementId) as TextElement;
                    if (ele != null)
                    {
                        string lower = ele.Text.ToLower();
                        ele.Text = lower;
                    }
                }
                tran.Commit();
                return Result.Succeeded;
            }
            catch(Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Result.Cancelled;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }  
        }

        public class TextNoteSelectionFilter : ISelectionFilter
        {
            public bool AllowElement(Element elem)
            {
                return elem is TextElement;
            }

            public bool AllowReference(Reference reference, XYZ position)
            {
                return true;
            }
        }
    }

}
