using QuickAccessTrail.Model;
using QuickAccessTrail.ViewModels;
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
    /// Interaction logic for FloorLevelWindow.xaml
    /// </summary>
    public partial class FloorLevelWindow : Window
    {
        internal FloorTypeViewModel _model;
        public FloorLevelWindow(FloorTypeViewModel viewModel)
        {
            InitializeComponent();
            _model = viewModel;
            this.DataContext = viewModel;
        }

        private void btnExport_click(object sender, RoutedEventArgs e)
        {
            if (_model.FilteredFloors.Count == 0)
            {
                MessageBox.Show("No Floor are model!");
                return;
            }
            MessageBox.Show($"Total floor = {_model.FilteredFloors.Count.ToString()} Modeled");
            this.DialogResult = true;
            Close();
        }
    }
}
