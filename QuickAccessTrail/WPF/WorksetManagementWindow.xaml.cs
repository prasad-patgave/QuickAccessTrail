using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using QuickAccessTrail.FIlters;
using QuickAccessTrail.Request;
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
using static QuickAccessTrail.Services.WorksetAssignmentService;

namespace QuickAccessTrail.WPF
{
    /// <summary>
    /// Interaction logic for WorksetManagementWindow.xaml
    /// </summary>
    public partial class WorksetManagementWindow : Window
    {
        private readonly UIDocument _uidoc;
        private readonly Document _doc;
        public WorksetManagementWindow(UIDocument uidoc)
        {
            InitializeComponent();
            _uidoc = uidoc;
            _doc = uidoc.Document;
        }

        private void btnAssign_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WorksetAssignmentRequest request = InputParser.Parse(txtInput.Text);
                WorksetAssignmentService service = new WorksetAssignmentService();
                AssignmentResult result = service.Assign(_doc, _uidoc.ActiveView, request);
                TaskDialog.Show("Workset Management",
                    $"Assigned : {result.AssignedCount}\n" +
                    $"Skipped : {result.SkippedCount}\n" +
                    $"Failed : {result.FailedCount}");

                Close();
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
