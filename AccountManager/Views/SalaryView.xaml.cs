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
    /// SalaryView.xaml 的互動邏輯
    /// </summary>
    public partial class SalaryView : UserControl
    {       

        public SalaryViewModel ViewModel
        {
            get
            {
                return this.DataContext as SalaryViewModel;
            }
        }
        public SalaryView()
        {
            var viewModel = new SalaryViewModel();
            DataContext = viewModel;
            InitializeComponent();
        }

}
}
