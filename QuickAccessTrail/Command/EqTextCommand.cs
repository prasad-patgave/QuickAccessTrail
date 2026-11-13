using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using QuickAccessTrail.FIlters;
using QuickAccessTrail.Model;
using QuickAccessTrail.WPF;
using QuickAccessTrail.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickAccessTrail.Command
{
    [Transaction(TransactionMode.Manual)]
    public class EqTextCommand : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            EqTextModel data;
            try
            {
                EqTextWindow mainWindow = new EqTextWindow();
                if (mainWindow.ShowDialog() != true)
                {
                    return Autodesk.Revit.UI.Result.Cancelled;
                }
                data = mainWindow.EqData;
            }
            catch (Exception ex)
            {

                TaskDialog.Show("Debug Error", ex.ToString());
                return Autodesk.Revit.UI.Result.Failed;
            }

            DImensionFilter filter = new DImensionFilter();
            IList<Reference> refs;
            try
            {
                refs = uidoc.Selection.PickObjects(ObjectType.Element, filter, "Select Dimension");

            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {

                return Autodesk.Revit.UI.Result.Cancelled;
            }

            string ReplacementText = data.SetEqText;
            double tolerenValue = data.SetTolerence;
            int updatedCount = 0;
            int skippedCount = 0;



            using (Transaction tran = new Transaction(doc, "Apply EQ Text"))
            {
                tran.Start();
                foreach (Reference r in refs)
                {
                    Dimension dim = doc.GetElement(r.ElementId) as Dimension;
                    if (dim == null || dim.NumberOfSegments <= 1)
                    {
                        skippedCount++;
                        continue;
                    }
                    DimensionSegmentArray segments = dim.Segments;
                    DimensionSegment firstseg = segments.get_Item(0);
                    DimensionSegment lastSeg = segments.get_Item(segments.Size - 1);
                    double firstVal = Math.Abs(UnitConverter.ToMm(firstseg.Value.Value));
                    double lastVal = Math.Abs(UnitConverter.ToMm(lastSeg.Value.Value));
                    double diff = Math.Abs(firstVal - lastVal);
                    double tolere = tolerenValue;
                    if (diff <= tolere || firstVal == lastVal)
                    {
                        firstseg.ValueOverride = ReplacementText;
                        lastSeg.ValueOverride = ReplacementText;
                        updatedCount++;
                    }
                    else
                    {
                        skippedCount++;
                    }

                }

                tran.Commit();
            }
            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}
