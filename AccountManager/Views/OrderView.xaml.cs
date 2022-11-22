using AccountManager.Models;
using AccountManager.ViewModels;
using MahApps.Metro.Controls;
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
    /// OrderView.xaml 的互動邏輯
    /// </summary>
    public partial class OrderView : UserControl
    {
        public OrderViewModel ViewModel
        {
            get
            {
                return this.DataContext as OrderViewModel;
            }
        }
        public OrderView()
        {
            InitializeComponent();
            var viewModel = new OrderViewModel();
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
        /// <summary>
        /// 只能輸入數字和大寫英文
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_previewTextInput_IncludeAlphabet(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^A-Z0-9]").IsMatch(e.Text);
        }
        /// <summary>
        /// 更新下拉表單
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxDesigner_DropDownOpened(object sender, EventArgs e)
        {
            var staffList = ViewModel._sqliteHelper.Query<StaffModel>("select * from Staff WHERE ResignationDate is NULL");

            if (staffList != null && staffList.Count != 0)
            {
                var staffGroup = staffList.GroupBy(s => s.Position).ToList();

                var designer = staffGroup.FirstOrDefault(s => s.Key == "設計師");
                if (designer != null)
                {
                    ViewModel.DesignerList = new List<ComboBoxItemTuple>();
                    designer.ToList().ForEach(s => ViewModel.DesignerList.Add(new ComboBoxItemTuple { Value = s.ID, DisplayName = $"{ s.Alias} [{s.ID}]" }));
                }

                ComboBoxDesigner.Items.Refresh();
            }
        }
        /// <summary>
        /// 更新下拉表單
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxAssistant1_DropDownOpened(object sender, EventArgs e)
        {
             var staffList = ViewModel._sqliteHelper.Query<StaffModel>("select * from Staff WHERE ResignationDate is NULL");//所有在職員工列表

            ViewModel.AssistantList = new List<ComboBoxItemTuple>();
            staffList.ToList().ForEach(s =>
            {
                if (ViewModel.SeletedDesigner.Value != s.ID)//設計師不能用自己當助理
                    ViewModel.AssistantList.Add(new ComboBoxItemTuple { Value = s.ID, DisplayName = $"{ s.Alias} [{s.ID}]" });
            });
            ComboBoxAssistant1.Items.Refresh();
        }
    }
}
