﻿using AccountManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// StatisticsView.xaml 的互動邏輯
    /// </summary>
    public partial class StatisticsView : UserControl
    {
        public StatisticsViewModel ViewModel
        {
            get
            {
                return this.DataContext as StatisticsViewModel;
            }
        }
        public StatisticsView()
        {
            InitializeComponent();
            var viewModel = new StatisticsViewModel();
            DataContext = viewModel;
            
        }

        private void button_Search_Click(object sender, RoutedEventArgs e)
        {
            ChartSeriesVisibilty();
        }
        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            ChartSeriesVisibilty();
        }
        void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
        /// <summary>
        /// 數列顯示控制
        /// </summary>
        private void ChartSeriesVisibilty()
        {
            if (ColumnSeriesIncome != null)
            {
                if ((bool)checkBoxIncome.IsChecked)
                    ColumnSeriesIncome.Visibility = Visibility.Visible;
                else
                    ColumnSeriesIncome.Visibility = Visibility.Hidden;
            }

            if (ColumnSeriesExpenditure != null)
            {
                if ((bool)checkBoxExpenditure.IsChecked)
                    ColumnSeriesExpenditure.Visibility = Visibility.Visible;
                else
                    ColumnSeriesExpenditure.Visibility = Visibility.Hidden;
            }

            if (ColumnSeriesBusiness != null)
            {
                if ((bool)checkBoxBusiness.IsChecked)
                    ColumnSeriesBusiness.Visibility = Visibility.Visible;
                else
                    ColumnSeriesBusiness.Visibility = Visibility.Hidden;
            }
            if (ColumnSeriesProduct != null)
            {
                if ((bool)checkBoxProduct.IsChecked)
                    ColumnSeriesProduct.Visibility = Visibility.Visible;
                else
                    ColumnSeriesProduct.Visibility = Visibility.Hidden;
            }
            if (ColumnSeriesTotal != null)
            {
                if ((bool)checkBoxTotal.IsChecked)
                    ColumnSeriesTotal.Visibility = Visibility.Visible;
                else
                    ColumnSeriesTotal.Visibility = Visibility.Hidden;
            }
        }
    }
    /// <summary>
    /// 轉換器 讓DataGrid 編號從1開始
    /// </summary>
    public class NumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,object parameter,CultureInfo cultureInfo)
        {
            if (value == null)
                return 0;
            return System.Convert.ToInt32(value) + 1;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }
}
