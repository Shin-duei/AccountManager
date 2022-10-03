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
    /// SettingView.xaml 的互動邏輯
    /// </summary>
    public partial class SettingView : UserControl
    {
        public SettingViewModel ViewModel
        {
            get
            {
                return this.DataContext as SettingViewModel;
            }
        }
        public SettingView()
        {
            InitializeComponent();
            var viewModel = new SettingViewModel();
            DataContext = viewModel;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var signInView = new SignInView();
            bool checkValidity = (bool)signInView.ShowDialog();

            if (checkValidity)
            {
                ViewModel.ID = signInView.textBox_ID.Text;//MainWindowViewModel 的ID沒有被更新 要注意
            }
            else
            {
                System.Environment.Exit(0);
            }
        }
    }
}
