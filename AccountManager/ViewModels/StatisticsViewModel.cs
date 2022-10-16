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

        private List<DateValue> _incomeChartValuesDay = new List<DateValue>();
        private List<DateValue> _expenditureChartValuesDay = new List<DateValue>();
        private List<DateValue> _productChartValuesDay = new List<DateValue>();
        private List<DateValue> _businessChartValuesDay = new List<DateValue>();
        private List<DateValue> _totalChartValuesDay = new List<DateValue>();

        private List<DateValue> _incomeChartValuesMonth = new List<DateValue>();
        private List<DateValue> _expenditureChartValuesMonth = new List<DateValue>();
        private List<DateValue> _productChartValuesMonth = new List<DateValue>();
        private List<DateValue> _businessChartValuesMonth = new List<DateValue>();
        private List<DateValue> _totalChartValuesMonth = new List<DateValue>();

        private List<DateValue> _incomeChartValuesYear = new List<DateValue>();
        private List<DateValue> _expenditureChartValuesYear = new List<DateValue>();
        private List<DateValue> _productChartValuesYear = new List<DateValue>();
        private List<DateValue> _businessChartValuesYear = new List<DateValue>();
        private List<DateValue> _totalChartValuesYear = new List<DateValue>();
        /// <summary>
        /// 收支顯示用列表
        /// </summary>
        public ObservableCollection<IncomeExpenditureModel> IncomeExpenditureList { set; get; } = new ObservableCollection<IncomeExpenditureModel>();

        /// <summary>
        /// 營業及產品顯示用列表
        /// </summary>
        public ObservableCollection<BusinessProductModel> BusinessProductList { set; get; } = new ObservableCollection<BusinessProductModel>();

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

        public ChartValues<DateValue> IncomeChartValues { get; set; } = new ChartValues<DateValue>();
        public ChartValues<DateValue> ExpenditureChartValues { get; set; } = new ChartValues<DateValue>();
        public ChartValues<DateValue> ProductChartValues { get; set; } = new ChartValues<DateValue>();
        public ChartValues<DateValue> BusinessChartValues { get; set; } = new ChartValues<DateValue>();
        public ChartValues<DateValue> TotalChartValues { get; set; } = new ChartValues<DateValue>();

        private Func<double, string> _formatter;
        public Func<double, string> Formatter
        {
            get { return _formatter; }
            set
            {
                if (_formatter != value)
                {
                    _formatter = value;
                    OnPropertyChanged(nameof(Formatter));
                }
            }
        }

        private object _mapper;
        public object Mapper
        {
            get { return _mapper; }
            set
            {
                if (_mapper != value)
                {
                    _mapper = value;
                    OnPropertyChanged(nameof(Mapper));
                }
            }
        }
        private double? _step;
        public double? Step
        {
            get { return _step; }
            set
            {
                if (_step != value)
                {
                    _step = value;
                    OnPropertyChanged(nameof(Step));
                }
            }
        }
        public StatisticsViewModel()
        {
            SearchCommand = new RelayCommand(ExecuteSearchCommand);
            DayViewCommand = new RelayCommand(ExecuteDayViewCommand);
            MonthViewCommand = new RelayCommand(ExecuteMonthViewCommand);
            YearViewCommand = new RelayCommand(ExecuteYearViewCommand);
            _sqliteHelper = new SQLiteHelper();
            InitialDate = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
            FinalDate = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.AddMonths(1).Day);
            Mapper = Mappers.Xy<DateValue>().X(s => s.Date.ToOADate()).Y(s => s.Value);
            Formatter = value => DateTime.FromOADate(value).ToString("yyyy/MM/dd");
        }
        public RelayCommand SearchCommand { get; }
        public RelayCommand DayViewCommand { get; }

        public RelayCommand MonthViewCommand { get; }

        public RelayCommand YearViewCommand { get; }
        /// <summary>
        /// 日檢視
        /// </summary>
        private void ExecuteDayViewCommand()
        {
            ChartSeriesClear();
            Mapper = Mappers.Xy<DateValue>().X(s => s.Date.ToOADate()).Y(s => s.Value);
            Formatter = value => DateTime.FromOADate(value).ToString("yyyy/MM/dd");
            Step = null;

            IncomeChartValues.AddRange(_incomeChartValuesDay);
            ExpenditureChartValues.AddRange(_expenditureChartValuesDay);
            BusinessChartValues.AddRange(_businessChartValuesDay);
            ProductChartValues.AddRange(_productChartValuesDay); 
            TotalChartValues.AddRange(_totalChartValuesDay);
        }
        /// <summary>
        /// 月檢視
        /// </summary>
        private void ExecuteMonthViewCommand()
        {
            ChartSeriesClear();
            Step = 1;
            Mapper = Mappers.Xy<DateValue>().X(s => Double.Parse(s.Date.ToString("yyyyMM"))).Y(s => s.Value);
            Formatter = value => $"{((int)value / 100)}/{((int)value % 100)}";

            IncomeChartValues.AddRange(_incomeChartValuesMonth);
            ExpenditureChartValues.AddRange(_expenditureChartValuesMonth);
            BusinessChartValues.AddRange(_businessChartValuesMonth);
            ProductChartValues.AddRange(_productChartValuesMonth); 
            TotalChartValues.AddRange(_totalChartValuesMonth);
        }
        /// <summary>
        /// 年檢視
        /// </summary>
        private void ExecuteYearViewCommand()
        {
            ChartSeriesClear();
            Step = 1;
            Mapper = Mappers.Xy<DateValue>().X(s => Double.Parse(s.Date.ToString("yyyy"))).Y(s => s.Value);
            Formatter = value => value.ToString();

            IncomeChartValues.AddRange(_incomeChartValuesYear);
            ExpenditureChartValues.AddRange(_expenditureChartValuesYear);
            BusinessChartValues.AddRange(_businessChartValuesYear); 
            ProductChartValues.AddRange(_productChartValuesYear);
            TotalChartValues.AddRange(_totalChartValuesYear);
        }
        /// <summary>
        /// 搜尋
        /// </summary>
        private void ExecuteSearchCommand()
        {
            var initialDate = InitialDate.ToString("yyyyMMdd");
            var finalDate = FinalDate.ToString("yyyyMMdd");
            var incomeExpenditureList = _sqliteHelper.Query<IncomeExpenditureModel>($"select * from IncomeExpenditure WHERE Date BETWEEN '{initialDate}' AND '{finalDate}'");//收支
            var allStatement = _sqliteHelper.Query<EssentialModel>($"select * from BillDetails WHERE ConsumptionDate BETWEEN '{initialDate}' AND '{finalDate}'");//營業消費明細
            var allStorgeHistory = _sqliteHelper.Query<StorgeValueModel>($"select * from StorgeValue WHERE Date BETWEEN '{initialDate}' AND '{finalDate}'");//儲值金

            TotalDataCalculate(incomeExpenditureList, allStatement, allStorgeHistory);//計算總額

            IncomeExpenditureList.Clear();
            if (incomeExpenditureList != null)//添加到收支列表
                incomeExpenditureList.ForEach(s => IncomeExpenditureList.Add(s));
            UpdateValue();//更新數據

            BusinessProductCalculate(allStatement);//計算營業收入和產品銷售

            Mapper = Mappers.Xy<DateValue>().X(s => s.Date.ToOADate()).Y(s => s.Value);
            Formatter = value => DateTime.FromOADate(value).ToString("yyyy/MM/dd");
            Step = null;
            ChartGetData(incomeExpenditureList, allStatement);//更新圖數據
        }
        /// <summary>
        /// 統計各項總額
        /// </summary>
        /// <param name="incomeExpenditureList"></param>
        /// <param name="allStatement"></param>
        /// <param name="allStorgeHistory"></param>
        private void TotalDataCalculate(List<IncomeExpenditureModel> incomeExpenditureList, List<EssentialModel> allStatement, List<StorgeValueModel> allStorgeHistory)
        {
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
        }
        /// <summary>
        /// 計算營業收入DataGrid
        /// </summary>
        /// <param name="allStatement"></param>
        private void BusinessProductCalculate(List<EssentialModel> allStatement)
        {
            var dayBills = allStatement.GroupBy(s => s.ConsumptionDate).ToList();
            BusinessProductList.Clear();
            foreach (var item in dayBills)
            {
                var day = DateTime.ParseExact(item.First().ConsumptionDate.ToString(), "yyyyMMdd", null);
                var dayBusiness = item.Where(s => s.ConsumptionItem != "產品購買").Sum(s => s.TotalPrice);
                var dayProduct = item.Where(s => s.ConsumptionItem == "產品購買").Sum(s => s.TotalPrice);

                if (dayBusiness != 0)
                    BusinessProductList.Add(new BusinessProductModel { Date = day.ToString("yyyyMMdd"), Item = "營業收入", Cost = dayBusiness });
                if (dayProduct != 0)
                    BusinessProductList.Add(new BusinessProductModel { Date = day.ToString("yyyyMMdd"), Item = "產品銷售", Cost = dayProduct });
            }
        }
        /// <summary>
        /// 計算圖表需要的數據集合
        /// </summary>
        /// <param name="incomeExpenditureList"></param>
        /// <param name="allStatement"></param>
        private void ChartGetData(List<IncomeExpenditureModel> incomeExpenditureList, List<EssentialModel> allStatement)
        {
            DataListClear();
            ChartSeriesClear();

            var timeSpanDay = FinalDate - InitialDate;

            for (int i = 0; i <= timeSpanDay.Days; i++)
            {
                DateTime day = InitialDate.AddDays(i);
                var valueIncome = incomeExpenditureList.ToList()
                    .Where(s => s.Date.ToString() == day.ToString("yyyyMMdd") && s.ItemType == IncomeExpenditure.Income)
                    .Sum(s => s.Cost);
                _incomeChartValuesDay.Add(new DateValue { Date = day, Value = valueIncome });

                var valueExpenditure = incomeExpenditureList.ToList()
                    .Where(s => s.Date.ToString() == day.ToString("yyyyMMdd") && s.ItemType == IncomeExpenditure.Expenditure)
                    .Sum(s => s.Cost);
                _expenditureChartValuesDay.Add(new DateValue { Date = day, Value = valueExpenditure });

                var valueBusiness = allStatement.ToList()
                    .Where(s => s.ConsumptionDate.ToString() == day.ToString("yyyyMMdd") && s.ConsumptionItem != "產品購買")
                    .Sum(s => s.TotalPrice);
                _businessChartValuesDay.Add(new DateValue { Date = day, Value = valueBusiness });

                var valueProduct = allStatement.ToList()
                    .Where(s => s.ConsumptionDate.ToString() == day.ToString("yyyyMMdd") && s.ConsumptionItem == "產品購買")
                    .Sum(s => s.TotalPrice);
                _productChartValuesDay.Add(new DateValue { Date = day, Value = valueProduct });

                _totalChartValuesDay.Add(new DateValue { Date = day, Value = (valueIncome - valueExpenditure + valueBusiness + valueProduct) });
            }

            var timeSpanMonth=((FinalDate.Year - InitialDate.Year) * 12) + FinalDate.Month - InitialDate.Month;
            for (int i = 0; i <= timeSpanMonth; i++)
            {
                DateTime day = InitialDate.AddMonths(i);
                var valueIncome = incomeExpenditureList.ToList()
                    .Where(s => s.Date.ToString().Contains(day.ToString("yyyyMM")) && s.ItemType == IncomeExpenditure.Income)
                    .Sum(s => s.Cost);
                _incomeChartValuesMonth.Add(new DateValue { Date = day, Value = valueIncome });

                var valueExpenditure = incomeExpenditureList.ToList()
                    .Where(s => s.Date.ToString().Contains(day.ToString("yyyyMM")) && s.ItemType == IncomeExpenditure.Expenditure)
                    .Sum(s => s.Cost);
                _expenditureChartValuesMonth.Add(new DateValue { Date = day, Value = valueExpenditure });

                var valueBusiness = allStatement.ToList()
                    .Where(s => s.ConsumptionDate.ToString().Contains(day.ToString("yyyyMM")) && s.ConsumptionItem != "產品購買")
                    .Sum(s => s.TotalPrice);
                _businessChartValuesMonth.Add(new DateValue { Date = day, Value = valueBusiness });

                var valueProduct = allStatement.ToList()
                    .Where(s => s.ConsumptionDate.ToString().Contains(day.ToString("yyyyMM")) && s.ConsumptionItem == "產品購買")
                    .Sum(s => s.TotalPrice);
                _productChartValuesMonth.Add(new DateValue { Date = day, Value = valueProduct });

                _totalChartValuesMonth.Add(new DateValue { Date = day, Value = (valueIncome - valueExpenditure + valueBusiness + valueProduct) });
            }
            var timeSpanYear = (FinalDate.Year - InitialDate.Year);
            for (int i = 0; i <= timeSpanYear; i++)
            {
                DateTime day = InitialDate.AddYears(i);
                var valueIncome = incomeExpenditureList.ToList()
                    .Where(s => s.Date.ToString().Contains(day.ToString("yyyy")) && s.ItemType == IncomeExpenditure.Income)
                    .Sum(s => s.Cost);
                _incomeChartValuesYear.Add(new DateValue { Date = day, Value = valueIncome });

                var valueExpenditure = incomeExpenditureList.ToList()
                    .Where(s => s.Date.ToString().Contains(day.ToString("yyyy")) && s.ItemType == IncomeExpenditure.Expenditure)
                    .Sum(s => s.Cost);
                _expenditureChartValuesYear.Add(new DateValue { Date = day, Value = valueExpenditure });

                var valueBusiness = allStatement.ToList()
                    .Where(s => s.ConsumptionDate.ToString().Contains(day.ToString("yyyy")) && s.ConsumptionItem != "產品購買")
                    .Sum(s => s.TotalPrice);
                _businessChartValuesYear.Add(new DateValue { Date = day, Value = valueBusiness });

                var valueProduct = allStatement.ToList()
                    .Where(s => s.ConsumptionDate.ToString().Contains(day.ToString("yyyy")) && s.ConsumptionItem == "產品購買")
                    .Sum(s => s.TotalPrice);
                _productChartValuesYear.Add(new DateValue { Date = day, Value = valueProduct });

                _totalChartValuesYear.Add(new DateValue { Date = day, Value = (valueIncome - valueExpenditure + valueBusiness + valueProduct) });
            }

            IncomeChartValues.AddRange(_incomeChartValuesDay);
            ExpenditureChartValues.AddRange(_expenditureChartValuesDay);
            BusinessChartValues.AddRange(_businessChartValuesDay);
            ProductChartValues.AddRange(_productChartValuesDay); 
            TotalChartValues.AddRange(_totalChartValuesDay);
        }

        private void ChartSeriesClear()
        {
            IncomeChartValues.Clear();
            ExpenditureChartValues.Clear();
            ProductChartValues.Clear();
            BusinessChartValues.Clear();
            TotalChartValues.Clear();
        }
        private void DataListClear()
        {
            _incomeChartValuesDay.Clear();
            _expenditureChartValuesDay.Clear();
            _productChartValuesDay.Clear();
            _businessChartValuesDay.Clear();
            _totalChartValuesDay.Clear();

            _incomeChartValuesMonth.Clear();
            _expenditureChartValuesMonth.Clear();
            _productChartValuesMonth.Clear();
            _businessChartValuesMonth.Clear();
            _totalChartValuesMonth.Clear();

            _incomeChartValuesYear.Clear();
            _expenditureChartValuesYear.Clear();
            _productChartValuesYear.Clear();
            _businessChartValuesYear.Clear();
            _totalChartValuesYear.Clear();

        }
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
    public class DateValue : ObservableRecipient
    {

        private double _value;
        public double Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }

        }
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }

        }
    }
}
