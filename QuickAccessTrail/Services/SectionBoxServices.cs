using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using QuickAccessTrail.Helper;
using QuickAccessTrail.Request;
using QuickAccessTrail.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuickAccessTrail.Services
{
    
    public class SectionBoxServices
    {
        private UIDocument _uidoc;
        private Autodesk.Revit.DB.Document _doc;

        public SectionBoxServices(UIDocument uidoc)
        {
            _uidoc = uidoc;
            _doc = uidoc.Document;
        }
        public void zoom(LinkedElementResult result, SectionBox3DRequest request)
        {
            BoundingBoxHelper helper = new BoundingBoxHelper();
            BoundingBoxXYZ sectionBox = helper.GetTransformedBoundingBox(result.Element, result.LinkInstance, request.paddiing);
            if (sectionBox == null)
            {
                MessageBox.Show("Bounding Box Not Available");
                return;
            }
            string viewName = request.Last3DViewName;
            View3D targetView = new FilteredElementCollector(_doc).OfClass(typeof(View3D)).Cast<View3D>().First(x => !x.IsTemplate && x.Name.Equals(viewName, StringComparison.OrdinalIgnoreCase));
            if (targetView == null)
            {
                MessageBox.Show("Write 3D view name properly!");
                return;
            }
            _uidoc.RequestViewChange(targetView);

            using(Transaction tx = new Transaction(_doc,"Zoom Linked Element"))
            {
                tx.Start();
                if (!targetView.IsSectionBoxActive)
                {
                    targetView.IsSectionBoxActive = true;
                }
                targetView.SetSectionBox(sectionBox);
                _uidoc.ShowElements(result.Element);
                tx.Commit();
            }
            _uidoc.RefreshActiveView();

        }
    }
}
