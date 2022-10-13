using AccountManager.Enums;
using AccountManager.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.ViewModels
{
    public class StatisticsViewModel : ObservableRecipient
    {

        /// <summary>
        /// 顯示用列表
        /// </summary>
        public ObservableCollection<IncomeExpenditureModel> IncomeExpenditureList { set; get; } = new ObservableCollection<IncomeExpenditureModel>();

        public SQLiteHelper _sqliteHelper;

        private DateTime _initialDate;
        /// <summary>
        /// 開始日期
        /// </summary>
        public DateTime InitialDate
        {
            get { return _initialDate; }
            set
            {
                if (_initialDate != value)
                {
                    _initialDate = value;

                    OnPropertyChanged(nameof(InitialDate));
                }
            }
        }
        private DateTime _finalDate;
        /// <summary>
        /// 結束日期
        /// </summary>
        public DateTime FinalDate
        {
            get { return _finalDate; }
            set
            {
                if (_finalDate != value)
                {
                    _finalDate = value;

                    OnPropertyChanged(nameof(FinalDate));
                }
            }
        }
        private int _totalExpenditure;
        /// <summary>
        /// 支出
        /// </summary>
        public int TotalExpenditure
        {
            get { return _totalExpenditure; }
            set
            {
                if (_totalExpenditure != value)
                {
                    _totalExpenditure = value;
                    OnPropertyChanged(nameof(TotalExpenditure));
                }
            }
        }
        private int _totalIncome;
        /// <summary>
        /// 收入
        /// </summary>
        public int TotalIncome
        {
            get { return _totalIncome; }
            set
            {
                if (_totalIncome != value)
                {
                    _totalIncome = value;
                    OnPropertyChanged(nameof(TotalIncome));
                }
            }
        }
        private int _storge;
        /// <summary>
        /// 儲值金
        /// </summary>
        public int Storge
        {
            get { return _storge; }
            set
            {
                if (_storge != value)
                {
                    _storge = value;
                    OnPropertyChanged(nameof(Storge));
                }
            }

        }
        private int _prodcut;
        /// <summary>
        /// 產品銷售
        /// </summary>
        public int Product
        {
            get { return _prodcut; }
            set
            {
                if (_prodcut != value)
                {
                    _prodcut = value;
                    OnPropertyChanged(nameof(Product));
                }
            }

        }
        private int _business;
        /// <summary>
        /// 營業收入
        /// </summary>
        public int Business
        {
            get { return _business; }
            set
            {
                if (_business != value)
                {
                    _business = value;
                    OnPropertyChanged(nameof(Business));
                }
            }

        }
        private int _total;
        /// <summary>
        /// 淨利
        /// </summary>
        public int Total
        {
            get { return _total; }
            set
            {
                if (_total != value)
                {
                    _total = value;
                    OnPropertyChanged(nameof(Total));
                }
            }

        }
        public StatisticsViewModel()
        {
            SearchCommand = new RelayCommand(ExecuteSearchCommand);
            _sqliteHelper = new SQLiteHelper();
            InitialDate = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
            FinalDate = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.AddMonths(1).Day);
            ChartInitialize();
        }
        public RelayCommand SearchCommand { get; }


        /// <summary>
        /// 搜尋
        /// </summary>
        private void ExecuteSearchCommand()
        {
            IncomeExpenditureList.Clear();
            var initialDate = InitialDate.ToString("yyyyMMdd");
            var finalDate = FinalDate.ToString("yyyyMMdd");
            var incomeExpenditureList = _sqliteHelper.Query<IncomeExpenditureModel>($"select * from IncomeExpenditure WHERE Date BETWEEN '{initialDate}' AND '{finalDate}'");//收支
            var allStatement = _sqliteHelper.Query<EssentialModel>($"select * from BillDetails WHERE ConsumptionDate BETWEEN '{initialDate}' AND '{finalDate}'");//營業消費明細
            var allStorgeHistory = _sqliteHelper.Query<StorgeValueModel>($"select * from StorgeValue WHERE Date BETWEEN '{initialDate}' AND '{finalDate}'");//儲值金

            int totalBusinessIncome = 0;
            int totalProductIncome = 0;
            int totalStorge = 0;

            if (allStatement != null)
            {
                allStatement.ForEach(s =>
                {

                    if (s.ConsumptionItem != "產品購買")
                        totalBusinessIncome += s.TotalPrice;
                    else
                        totalProductIncome += s.TotalPrice;
                });
            }
            Business = totalBusinessIncome;
            Product = totalProductIncome;
            if (allStorgeHistory != null)
            {
                allStorgeHistory.ForEach(s =>
                {
                    totalStorge += s.ImportExport;
                });
            }
            Storge = totalStorge;
            if (incomeExpenditureList != null)
                incomeExpenditureList.ForEach(s => IncomeExpenditureList.Add(s));

            UpdateValue();
        }
        public void ChartInitialize()
        {
            List<DateValue> incomeChartValues = new List<DateValue>();

            Mapper = Mappers.Xy<DateValue>().X(s=>s.Date.ToOADate()).Y(s => s.Value);
            Formatter = value =>DateTime.FromOADate(value).ToString("yyyy/MM/dd");
            incomeChartValues.Add(new DateValue {  Date = InitialDate, Value =109 });
            incomeChartValues.Add(new DateValue { Date = FinalDate, Value = 30 });
            IncomeChartValues = incomeChartValues.AsChartValues();
            
            List<DateValue> totalChartValues = new List<DateValue>();
            totalChartValues.Add(new DateValue { Date = InitialDate, Value = 109 });
            totalChartValues.Add(new DateValue { Date = FinalDate, Value = 30 });
            TotalChartValues = totalChartValues.AsChartValues();

        }
        public object Mapper { get; set; }
        public ChartValues<DateValue> IncomeChartValues { get; set; }
        public ChartValues<DateValue> TotalChartValues { get; set; }
        public ObservableCollection<string> Labels { get; set; } = new ObservableCollection<string>();
        public Func<double, string> Formatter { set; get; }

        public SeriesCollection SeriesCollection { set; get; }

        /// <summary>
        /// 刷新資料
        /// </summary>
        private void UpdateValue()
        {
            TotalExpenditure = IncomeExpenditureList.Where(s => s.ItemType == IncomeExpenditure.Expenditure).Select(s => s.Cost).Sum();
            TotalIncome = IncomeExpenditureList.Where(s => s.ItemType == IncomeExpenditure.Income).Select(s => s.Cost).Sum();
            Total = TotalIncome - TotalExpenditure + Business + Product;
        }
    }
    public class DateValue
    {
        public double Value { get; set; }
        public DateTime Date { get; set; }
    }
}
