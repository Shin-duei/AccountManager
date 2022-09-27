using AccountManager.Models;
using AccountManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace AccountManager.Views
{
    /// <summary>
    /// StorgeValueView.xaml 的互動邏輯
    /// </summary>
    public partial class StorgeValueView : UserControl
    {
        public StorgeViewModel ViewModel
        {
            get
            {
                return this.DataContext as StorgeViewModel;
            }
        }
        public StorgeValueView()
        {
            InitializeComponent();
            var viewModel = new StorgeViewModel();
            DataContext = viewModel;
            ViewModel.StorgeDate = DateTime.Now;
            ViewModel.InitialDate = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
            ViewModel.FinalDate = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.AddMonths(1).Day);
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
                ViewModel.StaffList = new List<string>();
                staffList.ForEach(s=>ViewModel.StaffList.Add(s.ID));
                ComboBox_Staff.Items.Refresh();
            }
        }
    }
}
