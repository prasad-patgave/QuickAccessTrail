using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using QuickAccessTrail.Helper;
using QuickAccessTrail.Result;
using QuickAccessTrail.Services;
using QuickAccessTrail.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuickAccessTrail.Command
{
    [Transaction(TransactionMode.Manual)]
    public class SectionBoxLinkedCommand : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Reference linkedElement = uidoc.Selection.PickObject(ObjectType.LinkedElement, "Select Linked Element");
            RevitLinkInstance linkInstance = doc.GetElement(linkedElement.ElementId) as RevitLinkInstance;
            if (linkInstance == null)
            {
                MessageBox.Show("No Revit Link Found!");
                return Autodesk.Revit.UI.Result.Cancelled;
            }
            Document linkDoc = linkInstance.GetLinkDocument();
            ElementId linkedElementId = linkedElement.LinkedElementId;
            Element linkEle = linkDoc.GetElement(linkedElementId);
            LinkedElementResult result = new LinkedElementResult()
            {
                Element = linkEle,
                LinkInstance = linkInstance,
                LinkedDocument = linkDoc
            };
            SectionBoxLinkedElement window = new SectionBoxLinkedElement(uidoc, result);
            bool? resulta = window.ShowDialog();
            if (resulta == true)
            {
                SectionBoxServices sevicess = new SectionBoxServices(uidoc);
                sevicess.zoom(result, window.request);
                return Autodesk.Revit.UI.Result.Succeeded;
            }
            return Autodesk.Revit.UI.Result.Cancelled;
        }
    }
}
