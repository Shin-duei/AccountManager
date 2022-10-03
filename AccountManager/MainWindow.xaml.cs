using AccountManager.ViewModels;
using AccountManager.Views;
using MahApps.Metro.Controls;

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
        public MainWindow()
        {
            InitializeComponent();
            var mainWindowViewModel = new MainWindowViewModel();
            DataContext = mainWindowViewModel;

            var signInView = new SignInView();
            bool checkValidity=(bool)signInView.ShowDialog();

            if (checkValidity)
            {
                ViewModel.ID = signInView.textBox_ID.Text;
                SettingView.ViewModel.ID = ViewModel.ID;
            }
            else
            {
                System.Environment.Exit(0);
            }
        }
    }
}
