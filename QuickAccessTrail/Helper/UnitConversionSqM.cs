using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickAccessTrail.Helper
{
    public static class UnitConversionSqM
    {
        public static double ToSqM(double value)
        {
            return UnitUtils.ConvertFromInternalUnits(value, UnitTypeId.SquareMeters);
        }
    }
}
