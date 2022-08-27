using AccountManager.ViewModels;
using MahApps.Metro.Controls;
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

namespace AccountManager.Views
{
    /// <summary>
    /// OrderView.xaml 的互動邏輯
    /// </summary>
    public partial class OrderView : UserControl
    {
        OrderViewModel ViewModel => DataContext as OrderViewModel;
        public OrderView()
        {
            InitializeComponent();
            var viewModel = new OrderViewModel() { ViewObject = this };
            DataContext = viewModel;
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
