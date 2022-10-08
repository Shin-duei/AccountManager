using AccountManager.Models;
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
using System.Windows.Shapes;

namespace AccountManager.Views
{
    /// <summary>
    /// SignInView.xaml 的互動邏輯
    /// </summary>
    public partial class SignInView : MetroWindow
    {
        public SQLiteHelper _sqliteHelper;
        public SignInViewModel ViewModel
        {
            get
            {
                return this.DataContext as SignInViewModel;
            }
        }
        public SignInView()
        {
            var viewModel = new SignInViewModel();
            DataContext = viewModel;
            InitializeComponent();
            _sqliteHelper = new SQLiteHelper();
            _sqliteHelper.db.CreateTable<StaffModel>();//表已存在不會重覆創建

        }
        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            ViewModel.Password = passwordBox.Password;
        }
        private bool IsPasswordShow = false;
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

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            SignIn();
        }
        private bool CheckIdentity()
        {
            var id = ViewModel.ID;
            var pw = ViewModel.Password;

            if (id == "Admin" && pw == "1234")
                return true;

            var staffList = _sqliteHelper.Query<StaffModel>("select * from Staff WHERE ResignationDate is NULL");//員工總表
            if (staffList != null && staffList.Count > 0)
            {
                var staff = staffList.FirstOrDefault(s => s.ID == id);

                if (staff != null)
                {
                    if (staff.Password == pw)
                        return true;
                    else
                        return false;
                }
                return false;
            }
            return false;
        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SignIn();
        }
        private void SignIn()
        {
            if (!CheckIdentity())
            {
                MessageBox.Show("帳號或密碼錯誤!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            this.DialogResult = true;
            this.Close();
        }
    }
}
