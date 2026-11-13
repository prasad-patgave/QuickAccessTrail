using QuickAccessTrail.Model;
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
    /// Interaction logic for EqTextWindow.xaml
    /// </summary>
    public partial class EqTextWindow : Window
    {
        public EqTextModel EqData { get; private set; }
        public EqTextWindow()
        {
            InitializeComponent();
        }

        private void btnReplace_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(txtTolerence.Text, out double TolerenceValue))
            {
                MessageBox.Show("Please Enter Valid Tolerence!");
                return;
            }
            if (string.IsNullOrEmpty(txtReplacedText.Text))
            {
                MessageBox.Show("Please Enter Replacement Text!");
                return;
            }
            EqData = new EqTextModel()
            {
                SetEqText = txtReplacedText.Text,
                SetTolerence = TolerenceValue
            };
            DialogResult = true;
            Close();
        }
    }
}
