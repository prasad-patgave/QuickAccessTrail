using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using QuickAccessTrail.Request;
using QuickAccessTrail.Result;
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
    /// Interaction logic for SectionBoxLinkedElement.xaml
    /// </summary>
    public partial class SectionBoxLinkedElement : Window

    {
        private UIDocument _uidoc;
        private Document _doc;
        public SectionBox3DRequest request { get; private set; }
        public LinkedElementResult result { get; private set; }
        public SectionBoxLinkedElement(UIDocument uidoc, LinkedElementResult Result)
        {
            InitializeComponent();
            _uidoc = uidoc;
            _doc = uidoc.Document;
            result = Result;
            txtThreeD.Text = Properties.Settings.Default.Last3DViewName;
        }
        private void btnZoom_click(object sender, RoutedEventArgs e)
        {
            double.TryParse(txtPadding.Text, out double pad);
            Properties.Settings.Default.Last3DViewName = txtThreeD.Text;
            Properties.Settings.Default.Save();
            request = new SectionBox3DRequest()
            {
                paddiing = pad,
                Last3DViewName = txtThreeD.Text
            };
            DialogResult = true;
            Close();
        }
    }
}
