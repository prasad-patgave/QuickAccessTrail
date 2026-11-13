using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using QuickAccessTrail.Services;
using QuickAccessTrail.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickAccessTrail.Command
{
    [Transaction(TransactionMode.Manual)]
    public class ZoomLinkedElementCommand : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            try
            {
                ZoomWindow window = new ZoomWindow(uidoc);
                bool? result = window.ShowDialog();
                if (result == true)
                {
                    ZoomLinkedElementService service = new ZoomLinkedElementService(uidoc);
                    service.zoom(window.Request);
                    return Autodesk.Revit.UI.Result.Succeeded;
                }
                return Autodesk.Revit.UI.Result.Cancelled;
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {

                return Autodesk.Revit.UI.Result.Cancelled;
            }
            catch(Exception ex)
            {
                message = ex.Message;
                return Autodesk.Revit.UI.Result.Cancelled;
            }

        }
        
    }
}
