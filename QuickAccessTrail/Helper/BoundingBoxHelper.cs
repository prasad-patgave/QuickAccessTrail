using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickAccessTrail.Helper
{
    public class BoundingBoxHelper
    {
        public BoundingBoxXYZ GetTransformedBoundingBox(Element element, RevitLinkInstance linkInstance,double padding)
        {
            BoundingBoxXYZ bbox = element.get_BoundingBox(null);
            if (bbox == null)
            {
                return null;
            }
            Transform transform = linkInstance.GetTotalTransform();
            List<XYZ> corners = GetCorners(bbox);
            List<XYZ> transformedCorners = corners.Select(x => transform.OfPoint(x)).ToList();
            double minX = transformedCorners.Min(p=>p.X);
            double minY = transformedCorners.Min(p => p.Y);
            double minZ = transformedCorners.Min(p => p.Z);

            double maxX = transformedCorners.Max(p => p.X);
            double maxY = transformedCorners.Max(p => p.Y);
            double maxZ = transformedCorners.Max(p => p.Z);
            double pad = UnitUtils.ConvertToInternalUnits(padding,UnitTypeId.Millimeters);
            BoundingBoxXYZ result = new BoundingBoxXYZ();
            result.Min = new XYZ(minX - pad, minY - pad, minZ - pad);
            result.Max = new XYZ(maxX + pad, maxY + pad, maxZ + pad);
            return result;
        }

        private List<XYZ> GetCorners(BoundingBoxXYZ bbox)
        {
            XYZ min = bbox.Min;
            XYZ max = bbox.Max;
            List<XYZ> corners = new List<XYZ>
            {
                new XYZ(min.X, min.Y, min.Z),
                new XYZ(max.X, min.Y, min.Z),
                new XYZ(min.X, max.Y, min.Z),
                new XYZ(max.X, max.Y, min.Z),

                new XYZ(min.X, min.Y, max.Z),
                new XYZ(max.X, min.Y, max.Z),
                new XYZ(min.X, max.Y, max.Z),
                new XYZ(max.X, max.Y, max.Z)
            };
            return corners;
        }
    }
}
