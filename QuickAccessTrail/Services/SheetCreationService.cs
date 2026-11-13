using Autodesk.Revit.DB;
using QuickAccessTrail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickAccessTrail.Services
{
    public class SheetCreationService
    {
        public int CreateSheets(Document doc, ElementId titleBlockTypeId, List<SheetModel> sheets)
        {
            int createdCount = 0;
            // Collect existing sheet numbers
            HashSet<string> existingSheetNumbers = new FilteredElementCollector(doc).OfClass(typeof(ViewSheet)).Cast<ViewSheet>()
                .Select(x => x.SheetNumber).ToHashSet();
            using (Transaction transaction = new Transaction(doc, "Batch Create Sheets"))
            {
                transaction.Start();
                foreach (SheetModel sheet in sheets)
                {
                    // Skip duplicate sheet numbers
                    if (existingSheetNumbers.Contains(sheet.SheetNumber))
                        continue;
                    ViewSheet viewSheet = ViewSheet.Create(doc, titleBlockTypeId);
                    viewSheet.SheetNumber = sheet.SheetNumber;
                    viewSheet.Name = sheet.SheetName;
                    existingSheetNumbers.Add(sheet.SheetNumber);
                    createdCount++;
                }
                transaction.Commit();
            }
            return createdCount;
        }
    }
}
