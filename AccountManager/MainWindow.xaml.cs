using AccountManager.ViewModels;
using AccountManager.Views;
using MahApps.Metro.Controls;
using System;
using System.Threading;
using System.Windows;

namespace AccountManager
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindowViewModel ViewModel
        {
            get
            {
                return this.DataContext as MainWindowViewModel;
            }
        }
        static Mutex mutex = null;
        public MainWindow()
        {
            mutex = new System.Threading.Mutex(true, "OnlyRun");
            if (!mutex.WaitOne(0, false))
            {
                MessageBox.Show("程序執行中，不可重複開啟","提示" ,MessageBoxButton.OK, MessageBoxImage.Warning);
                Environment.Exit(1);
            }

            InitializeComponent();
            var mainWindowViewModel = new MainWindowViewModel();
            DataContext = mainWindowViewModel;
#if(!DEBUG)
            var signInView = new SignInView();
            bool checkValidity = (bool)signInView.ShowDialog();

            if (checkValidity)
            {
                ViewModel.ID = signInView.textBox_ID.Text;
                SettingView.ViewModel.ID = ViewModel.ID;
            }
            else
            {
                System.Environment.Exit(0);
            }
#endif
        }
    }
}
