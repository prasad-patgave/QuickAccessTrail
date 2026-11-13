using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using QuickAccessTrail.Model;
using QuickAccessTrail.ViewModels;
using QuickAccessTrail.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickAccessTrail.Command
{
    [Transaction(TransactionMode.Manual)]
    public class FloorTypesCommnd : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UIDocument uidoc = commandData.Application.ActiveUIDocument;
                Document doc = uidoc.Document;
                List<Floor> floorTypes = new FilteredElementCollector(doc).OfClass(typeof(Floor)).Cast<Floor>().ToList();
                if (floorTypes.Count == 0)
                {
                    TaskDialog.Show("Info", "loor is not modelled yet");
                    return Autodesk.Revit.UI.Result.Cancelled;
                }
                List<FloorTypeItem> floorType = floorTypes.Select(x =>
                {
                    Parameter areaParam = x.get_Parameter(BuiltInParameter.HOST_AREA_COMPUTED);
                    return new FloorTypeItem
                    {
                        FloorName = x.Name,
                        LevelName = doc.GetElement(x.LevelId)?.Name ?? "None",
                        FloorArea = areaParam?.AsDouble() ?? 0.0
                    };
                }).ToList();
                FloorTypeViewModel vm = new FloorTypeViewModel(floorType);
                FloorLevelWindow window = new FloorLevelWindow(vm);
                bool? result = window.ShowDialog();
                return result == true ? Autodesk.Revit.UI.Result.Succeeded : Autodesk.Revit.UI.Result.Cancelled;
            }
            catch (Exception ex)
            {
                message = ex.ToString();
                return Autodesk.Revit.UI.Result.Cancelled;
                
            }
        }
    }
}
