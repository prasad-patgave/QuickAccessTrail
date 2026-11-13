using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using QuickAccessTrail.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickAccessTrail.Command
{
    [Transaction(TransactionMode.Manual)]
    public class WorksetManagementCommand : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            WorksetManagementWindow window = new WorksetManagementWindow(uidoc);
            window.ShowDialog();

            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}
