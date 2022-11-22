using AccountManager.ViewModels;
using Microsoft.Win32;
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

            InitializeComponent();
            var viewModel = new SalaryViewModel();
            DataContext = viewModel;
        }


        private void dataGrid_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            ViewModel.TotalSalary = (int)ViewModel.PerformanceList.Sum(s => s.FinalSalary);
        }
        /// <summary>
        /// 存圖片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPEG(*.jpg;*.jpeg)|*.jpg;*.jpeg";
            saveFileDialog.FileName = $"{ViewModel.InitialDate.ToString("MM")}月薪資";
            if (saveFileDialog.ShowDialog()==true)
            {
                var fileName = saveFileDialog.FileName;
                RenderTargetBitmap rtbmp = new RenderTargetBitmap((int)MainGrid.ActualWidth, (int)MainGrid.ActualHeight, 96, 96, PixelFormats.Default);
                rtbmp.Render(MainGrid);
                var encode = new JpegBitmapEncoder();
                encode.Frames.Add(BitmapFrame.Create(rtbmp));
                var ms = new System.IO.MemoryStream();
                encode.Save(ms);
                System.Drawing.Image MyImage = System.Drawing.Image.FromStream(ms);
                MyImage.Save(fileName);
            }
        }
    }
}
