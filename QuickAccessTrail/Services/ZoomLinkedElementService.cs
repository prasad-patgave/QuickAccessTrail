using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using QuickAccessTrail.Request;
using QuickAccessTrail.FIlters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickAccessTrail.Result;
using QuickAccessTrail.Helper;

namespace QuickAccessTrail.Services
{
    public class ZoomLinkedElementService
    {
        private readonly UIDocument _uidoc;
        private readonly Document _doc;
        public ZoomLinkedElementService(UIDocument uidoc)
        {
            _uidoc = uidoc;
            _doc = uidoc.Document;
        }
        public void zoom(ZoomRequest request)
        {
            LinkElementFinder finder = new LinkElementFinder(_doc);
            LinkedElementResult result = finder.FindLinkedElement(request.ElementId);
            if (result == null)
            {
                TaskDialog.Show("Zoom", "Linked Element Not Found!");
                return;
            }
            BoundingBoxHelper helper = new BoundingBoxHelper();
            BoundingBoxXYZ sectionBox = helper.GetTransformedBoundingBox(result.Element, result.LinkInstance, request.Padding);
            if (sectionBox == null)
            {
                TaskDialog.Show("Zoom", "Bounding Box Not Available.");
                return;
            }
            View3D view = _doc.ActiveView as View3D;
            if (view == null)
            {
                TaskDialog.Show("Zoom", "Active view is not a 3D view.");
                return;
            }
            using(Transaction tx = new Transaction(_doc, "Zoom Linked Element"))
            {
                tx.Start();
                if (!view.IsSectionBoxActive)
                {
                    view.IsSectionBoxActive = true;
                }
                view.SetSectionBox(sectionBox);
                tx.Commit();
            }
            _uidoc.RefreshActiveView();
        }
    }
}
