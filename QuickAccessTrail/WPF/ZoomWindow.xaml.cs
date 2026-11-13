using Autodesk.Revit.UI;
using QuickAccessTrail.Request;
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
    /// Interaction logic for ZoomWindow.xaml
    /// </summary>
    public partial class ZoomWindow : Window
    {
        private readonly UIDocument _uidoc;
        public ZoomRequest Request { get; private set; }
        public ZoomWindow(Autodesk.Revit.UI.UIDocument uidoc)
        {
            InitializeComponent();
            _uidoc = uidoc;
        }

        private void btnZoom_click(object sender, RoutedEventArgs e)
        {
            int.TryParse(txtElementId.Text, out int elementId);
            double.TryParse(txtPadding.Text, out double padding);
            Request = new ZoomRequest()
            {
                ElementId = elementId,
                Padding = padding
            };
            DialogResult = true;
            Close();
        }

        private void btnCancel_click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
