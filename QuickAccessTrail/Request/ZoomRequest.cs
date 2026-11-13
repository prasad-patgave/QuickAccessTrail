using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickAccessTrail.Request
{
    public class ZoomRequest
    {
        public int ElementId { get; set; }
        public double Padding { get; set; }
    }
}
