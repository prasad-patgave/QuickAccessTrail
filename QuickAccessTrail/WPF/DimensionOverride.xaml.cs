using QuickAccessTrail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for DimensionOverride.xaml
    /// </summary>
    public partial class DimensionOverride : Window
    {
        public OverrideModel model { get; private set; }
        public DimensionOverride()
        {
            InitializeComponent();
        }

        private void btnReplaced_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtReplacementText.Text))
            {
                MessageBox.Show("Please Enter Replacement Text");
                return;
            }
            model = new OverrideModel()
            {
                TextOverride = txtReplacementText.Text
            };
            DialogResult = true;
            Close();
        }
    }
}
