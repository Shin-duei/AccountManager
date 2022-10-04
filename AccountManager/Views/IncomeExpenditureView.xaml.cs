using AccountManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// IncomeExpenditureView.xaml 的互動邏輯
    /// </summary>
    public partial class IncomeExpenditureView : UserControl
    {
        public IncomeExpenditureViewModel ViewModel
        {
            get
            {
                return this.DataContext as IncomeExpenditureViewModel;
            }
        }
        public IncomeExpenditureView()
        {
            InitializeComponent();
            var viewModel = new IncomeExpenditureViewModel();
            DataContext = viewModel;
        }
        /// <summary>
        /// 只能輸入數字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
