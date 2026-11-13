using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickAccessTrail.Result
{
    public class LinkedElementResult
    {
        public Element Element { get; set; }
        public Document LinkedDocument { get; set; }
        public RevitLinkInstance LinkInstance { get; set; }
    }
}
