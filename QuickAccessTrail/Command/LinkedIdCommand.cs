using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickAccessTrail.Command
{
    [Transaction(TransactionMode.Manual)]
    public class LinkedIdCommand : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            try
            {
                Reference pickedRef = uidoc.Selection.PickObject(ObjectType.LinkedElement, "Select a linked element");
                RevitLinkInstance linkInstance = doc.GetElement(pickedRef.ElementId) as RevitLinkInstance;
                if (linkInstance == null)
                {
                    TaskDialog.Show("Error", "Selected reference is not a linked element");
                    return Autodesk.Revit.UI.Result.Failed;
                }
                Document linkDoc = linkInstance.GetLinkDocument();
                if (linkDoc == null)
                {
                    TaskDialog.Show("Error", "Could not get the linked document");
                    return Autodesk.Revit.UI.Result.Failed;
                }
                ElementId linkedElementId = pickedRef.LinkedElementId;
                Element linkedElement = linkDoc.GetElement(linkedElementId);
                string idText = linkedElementId.Value.ToString();
                TaskDialog.Show("Linked element Id", $"Element: {linkedElement.Name}\nId: {idText}");
                return Autodesk.Revit.UI.Result.Succeeded;
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Autodesk.Revit.UI.Result.Cancelled;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Autodesk.Revit.UI.Result.Failed;
            }
        }
    }
}
