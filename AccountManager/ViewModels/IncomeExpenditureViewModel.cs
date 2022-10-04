using AccountManager.Enums;
using AccountManager.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AccountManager.ViewModels
{
    public class IncomeExpenditureViewModel : ObservableRecipient
    {
        public SQLiteHelper _sqliteHelper;
        private DateTime _date;
        /// <summary>
        /// 顯示用列表
        /// </summary>
        public ObservableCollection<IncomeExpenditureModel> IncomeExpenditureList { set; get; } = new ObservableCollection<IncomeExpenditureModel>();
        /// <summary>
        /// 日期
        /// </summary>
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
        private int _cost;
        /// <summary>
        /// 金額
        /// </summary>
        public int Cost
        {
            get { return _cost; }
            set
            {
                if (_cost != value)
                {
                    _cost = value;

                    OnPropertyChanged(nameof(Cost));
                }
            }
        }
        private string _remark;
        /// <summary>
        /// 備註
        /// </summary>
        public string Remark
        {
            get { return _remark; }
            set
            {
                if (_remark != value)
                {
                    _remark = value;

                    OnPropertyChanged(nameof(Remark));
                }
            }
        }
        private string _item;
        /// <summary>
        /// 項目
        /// </summary>
        public string Item
        {
            get { return _item; }
            set
            {
                if (_item != value)
                {
                    _item = value;

                    OnPropertyChanged(nameof(Item));
                }
            }
        }
        private bool _isIncome;
        public bool IsIncome
        {
            get { return _isIncome; }
            set
            {
                if (_isIncome != value)
                {
                    _isIncome = value;

                    OnPropertyChanged(nameof(IsIncome));
                    IncomeExpenditureType = ChangeIncomeExpenditureType();
                    OnPropertyChanged(nameof(IncomeExpenditureType));
                }
            }
        }
        private bool _isExpenditure=true;
        public bool IsExpenditure
        {
            get { return _isExpenditure; }
            set
            {
                if (_isExpenditure != value)
                {
                    _isExpenditure = value;

                    OnPropertyChanged(nameof(IsExpenditure));
                    IncomeExpenditureType = ChangeIncomeExpenditureType();
                    OnPropertyChanged(nameof(IncomeExpenditureType));
                }
            }
        }
        private IncomeExpenditure _incomeExpenditureType;
        public IncomeExpenditure IncomeExpenditureType
        {
            get { return _incomeExpenditureType; }
            set
            {
                if (_incomeExpenditureType != value)
                {
                    _incomeExpenditureType = value;
                    OnPropertyChanged(nameof(IncomeExpenditureType));
                }
            }
        }
        private IncomeExpenditure ChangeIncomeExpenditureType()
        {
            if (IsExpenditure)
                return IncomeExpenditure.Expenditure;
            else if (IsIncome)
                return IncomeExpenditure.Income;
            else
                return IncomeExpenditure.Expenditure;
        }
        private List<IncomeExpenditureModel> _seletedItems;
        public List<IncomeExpenditureModel> SeletedItems
        {
            get { return _seletedItems; }
            set
            {
                if (_seletedItems != value)
                {
                    _seletedItems = value;

                    OnPropertyChanged(nameof(SeletedItems));
                }
            }
        }
        private int _totalExpenditure;
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
        private int _total;
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
        public IncomeExpenditureViewModel()
        {
            _sqliteHelper = new SQLiteHelper();
            Date = DateTime.Now;
            AddNewItemCommand = new RelayCommand(ExecuteAddNewItemCommand);
            CancelItemCommand = new RelayCommand(ExecuteCancelItemCommand, CanExecuteCancelItemCommand);
            InsertToDBCommand = new RelayCommand(ExecuteInsertToDBCommand, CanExecuteInsertToDBCommand);
        }

        public RelayCommand AddNewItemCommand { get; }
        public RelayCommand CancelItemCommand { get; }
        public RelayCommand InsertToDBCommand { get; }

        /// <summary>
        /// 增加
        /// </summary>
        private void ExecuteAddNewItemCommand()
        {
            var newItem = new IncomeExpenditureModel()
            {
                Date = Date.ToString("yyyyMMdd"),
                Item = Item,
                Cost = Cost,
                ItemType = IncomeExpenditureType,
                Remark = Remark
            };

            IncomeExpenditureList.Add(newItem);
            SetDefaultTextBox();
            InsertToDBCommand.NotifyCanExecuteChanged();
            UpdateValue();
        }
        /// <summary>
        /// 刪除
        /// </summary>
        private void ExecuteCancelItemCommand()
        {
            if (SeletedItems != null && SeletedItems.Count > 0)
            {
                var result = IncomeExpenditureList.ToList().Except(SeletedItems).ToList();
                IncomeExpenditureList.Clear();
                result.ForEach(s => IncomeExpenditureList.Add(s));
                UpdateValue();
            }
            InsertToDBCommand.NotifyCanExecuteChanged();
        }
        private bool CanExecuteCancelItemCommand()
        {
            if (SeletedItems == null || SeletedItems.Count == 0)
                return false;
            else
                return true;
        }
        public ICommand SelectionChangedCommand => _selectionChangedCommand ?? (_selectionChangedCommand = new RelayCommand<IList>(OnChanged));

        private ICommand _selectionChangedCommand;
        private void OnChanged(System.Collections.IList dataset)
        {
            SeletedItems = dataset.OfType<IncomeExpenditureModel>().ToList();
            CancelItemCommand.NotifyCanExecuteChanged();
        }
        /// <summary>
        /// 入賬
        /// </summary>
        private void ExecuteInsertToDBCommand()
        {
            var result = MessageBox.Show("即將完成入賬，是否已確認所有明細?!", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _sqliteHelper.db.CreateTable<IncomeExpenditureModel>();//表已存在不會重覆創建

                foreach (var item in IncomeExpenditureList)
                {
                    item.LoginDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    _sqliteHelper.Add(item);
                }

                MessageBox.Show("完成入賬!", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                SetDefaultTextBox();
                IncomeExpenditureList.Clear();
                CancelItemCommand.NotifyCanExecuteChanged();
                InsertToDBCommand.NotifyCanExecuteChanged();
                UpdateValue();
            }
        }
        private bool CanExecuteInsertToDBCommand()
        {
            if (IncomeExpenditureList.Count > 0)
                return true;
            else
                return false;
        }
        private void SetDefaultTextBox()
        {
            Item = "";
            Remark = "";
            Cost = 0;
        }
        private void UpdateValue()
        {
            TotalExpenditure=IncomeExpenditureList.Where(s => s.ItemType == IncomeExpenditure.Expenditure).Select(s => s.Cost).Sum();
            TotalIncome = IncomeExpenditureList.Where(s => s.ItemType == IncomeExpenditure.Income).Select(s => s.Cost).Sum();
            Total=TotalIncome - TotalExpenditure;
        }
    }
}
