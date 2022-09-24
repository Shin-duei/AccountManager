using AccountManager.Models;
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
    /// StaffView.xaml 的互動邏輯
    /// </summary>
    public partial class StaffView : UserControl
    {
        public StaffViewModel ViewModel
        {
            get
            {
                return this.DataContext as StaffViewModel;
            }
        }
        public StaffView()
        {
            InitializeComponent();
            var viewModel = new StaffViewModel();
            DataContext = viewModel;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (!IsPasswordShow)
            {
                passwordTxtBox.Text = passwordBox.Password;
                passwordBox.Visibility = Visibility.Collapsed;
                passwordTxtBox.Visibility = Visibility.Visible;

                var button = (Button)sender;
                button.Content = new MaterialDesignThemes.Wpf.PackIcon { Kind = MaterialDesignThemes.Wpf.PackIconKind.EyeOff };
                IsPasswordShow = true;
            }
            else
            {
                passwordBox.Password = passwordTxtBox.Text;
                passwordTxtBox.Visibility = Visibility.Collapsed;
                passwordBox.Visibility = Visibility.Visible;
                var button = (Button)sender;
                button.Content = new MaterialDesignThemes.Wpf.PackIcon { Kind = MaterialDesignThemes.Wpf.PackIconKind.Eye };
                IsPasswordShow = false;
            }
        }
        private bool IsPasswordShow = false;

        /// <summary>
        /// 添加員工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckAccountData())
            {
                return;
            }
            passwordBox.Password = "";
            passwordBox_confirm.Password = "";
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            ViewModel.Password = passwordBox.Password;
        }
        /// <summary>
        /// 雙擊編輯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var result = MessageBox.Show("是否要編輯該員工資料?!", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                button_EditStaff.IsEnabled = false;
                return;
            }
            else
            {
                button_EditStaff.IsEnabled = true;
                button_Add.IsEnabled = false;
                button_Cancel.IsEnabled = false;
                DataGrid grid = sender as DataGrid;

                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    var rowItem = grid.SelectedItem as StaffModel;

                    ViewModel.Name = rowItem.Name;
                    ViewModel.ID = rowItem.ID;
                    ViewModel.Position = rowItem.Position;
                    ViewModel.Alias = rowItem.Alias;
                    ViewModel.Password = rowItem.Password;
                    passwordBox.Password = rowItem.Password;
                    ViewModel.OnBoardDate = DateTime.ParseExact(rowItem.OnBoardDate, "yyyyMMdd", null);
                    ViewModel.ReloadAuthority(rowItem.Authority);
                }
            }
        }

        private void button_EditStaff_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckAccountData())
            {
                ViewModel.IsValidAddAccount = false;
            }
            else
            {
                ViewModel.IsValidAddAccount = true;

                button_EditStaff.IsEnabled = false;
                button_Add.IsEnabled = true;
                button_Cancel.IsEnabled = true;
                ViewModel.TempPassword = passwordBox.Password;
                ViewModel.Authority = ViewModel.GetAuthority();
                passwordBox.Password = "";
                passwordBox_confirm.Password = "";
                radioButton_Non.IsChecked = true;
                radioButton_Non1.IsChecked = true;
                radioButton_Non2.IsChecked = true;
                radioButton_Non3.IsChecked = true;
                radioButton_Non4.IsChecked = true;
                radioButton_Non5.IsChecked = true;
                radioButton_Non6.IsChecked = true;
            }
        }
        private bool CheckAccountData()
        {
            if (string.IsNullOrEmpty(ViewModel.Name))
            {
                ViewModel.IsValidAddAccount = false;
                MessageBox.Show("請輸入姓名", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //if (ViewModel.StaffListDisplay.Any(s => s.ID == ViewModel.ID))
            //{
            //    ViewModel.IsValidAddAccount = false;
            //    MessageBox.Show("員工編號已存在，請給予新編號", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return false;
            //}

            if (string.IsNullOrEmpty(ViewModel.Position))
            {
                ViewModel.IsValidAddAccount = false;
                MessageBox.Show("請輸入職稱", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(ViewModel.Password))
            {
                ViewModel.IsValidAddAccount = false;
                MessageBox.Show("請輸入密碼", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (passwordBox.Password != passwordBox_confirm.Password)
            {
                ViewModel.IsValidAddAccount = false;
                MessageBox.Show("密碼與再次確認不相符", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("是否要移除該員工資料?!", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
                ViewModel.IsConfirmCancelStaff = false;
            else
                ViewModel.IsConfirmCancelStaff = true;
        }
    }
}
