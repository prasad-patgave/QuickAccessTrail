using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using QuickAccessTrail.Model;
using QuickAccessTrail.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuickAccessTrail.WPF
{
    /// <summary>
    /// Interaction logic for BatchCreationWindow.xaml
    /// </summary>
    public partial class BatchCreationWindow : Window
    {
        private UIDocument _uidoc;
        private Document _doc;
        public BatchCreationWindow(UIDocument uidoc)
        {
            InitializeComponent();
            _uidoc = uidoc;
            _doc = uidoc.Document;
            LoadTitleBlock();
        }

        private void LoadTitleBlock()
        {
            List<FamilySymbol> titleBlocks = new FilteredElementCollector(_doc).OfCategory(BuiltInCategory.OST_TitleBlocks).WhereElementIsElementType()
                .Cast<FamilySymbol>().OrderBy(x => x.FamilyName).ThenBy(x => x.Name).ToList();
            cmbTitleBlock.ItemsSource = titleBlocks;
            if (titleBlocks.Any())
            {
                cmbTitleBlock.SelectedIndex = 0;
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (cmbTitleBlock.SelectedItem == null)
            {
                MessageBox.Show("Please select a Title Block.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSheetData.Text))
            {
                MessageBox.Show("Please enter sheet information.");
                return;
            }

            List<SheetModel> sheets = ParseSheetData(txtSheetData.Text);

            if (sheets.Count == 0)
            {
                MessageBox.Show("No valid sheet data found.");
                return;
            }
            FamilySymbol titleBlock = (FamilySymbol)cmbTitleBlock.SelectedItem;

            SheetCreationService service = new SheetCreationService();

            service.CreateSheets(_doc, titleBlock.Id, sheets);

            MessageBox.Show($"{sheets.Count} sheet(s) created successfully.");

            Close();
        }
        /// <summary>
        /// Convert pasted text into List&lt;SheetEntry&gt;
        /// </summary>
        private List<SheetModel> ParseSheetData(string input)
        {
            List<SheetModel> sheets = new List<SheetModel>();

            string[] lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                string[] parts = line.Split('\t');

                if (parts.Length < 2)
                    continue;

                sheets.Add(new SheetModel
                {
                    SheetNumber = parts[0].Trim(),
                    SheetName = parts[1].Trim()
                });
            }
            return sheets;
        }
    }
}
