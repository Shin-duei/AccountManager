using AccountManager.ViewModels;
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
    /// StatisticsView.xaml 的互動邏輯
    /// </summary>
    public partial class StatisticsView : UserControl
    {
        public StatisticsViewModel ViewModel
        {
            get
            {
                return this.DataContext as StatisticsViewModel;
            }
        }
        public StatisticsView()
        {
            InitializeComponent();
            var viewModel = new StatisticsViewModel();
            DataContext = viewModel;
        }
    }
}
