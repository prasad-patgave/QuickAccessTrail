using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick_Access.Command
{
    [Transaction(TransactionMode.Manual)]
    public class PreviousSheet : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            View activeView = doc.ActiveView;
            if (!(activeView is ViewSheet))
            {
                TaskDialog.Show("Info", "You are not on a sheet!");
                return Result.Failed;
            }
            ViewSheet currentSheet = activeView as ViewSheet;
            var sheets = new FilteredElementCollector(doc).OfClass(typeof(ViewSheet)).Cast<ViewSheet>().OrderBy(x => x.SheetNumber).ToList();
            //List<ViewSheet> vieShe = sheets.Cast<ViewSheet>().OrderBy(x => x.SheetNumber).ToList();
            int index = sheets.FindIndex(x => x.Id == currentSheet.Id);
            if (index <= 0)
            {
                TaskDialog.Show("Info", "No Previous Sheet Is Available!");
                return Result.Failed;
            }
            ViewSheet nextSheet = sheets[index - 1];
            uidoc.RequestViewChange(nextSheet);
            return Result.Succeeded;

        }
    }
}
