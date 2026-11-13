using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using QuickAccessTrail.FIlters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace QuickAccessTrail.Command
{
    [Transaction(TransactionMode.Manual)]
    public class EqValueCommand : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            IList<Reference> refs;
            try
            {
                refs = uidoc.Selection.PickObjects(ObjectType.Element, new DImensionFilter(), "Select Dimension");
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {

                return Autodesk.Revit.UI.Result.Cancelled;
            }
            using (Transaction trans = new Transaction(doc, "Dimension Value"))
            {
                trans.Start();
                foreach (Reference r in refs)
                {
                    Dimension dim = doc.GetElement(r.ElementId) as Dimension;
                    if (dim == null || dim.NumberOfSegments <=1)
                    {
                        continue;
                    }
                    DimensionSegmentArray segments = dim.Segments;
                    DimensionSegment firsSegment = segments.get_Item(0);
                    DimensionSegment lastSegment = segments.get_Item(segments.Size - 1);
                    firsSegment.ValueOverride = null;
                    lastSegment.ValueOverride = null;
                }
                trans.Commit();

            }
            
            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}
