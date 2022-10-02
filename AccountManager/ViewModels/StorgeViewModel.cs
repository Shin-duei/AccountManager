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
    public class StorgeViewModel : ObservableRecipient
    {
        public SQLiteHelper _sqliteHelper;

        /// <summary>
        /// 顯示用列表
        /// </summary>
        public ObservableCollection<StorgeValueModel> StorgeHistoryList { set; get; } = new ObservableCollection<StorgeValueModel>();

        private DateTime _storgeDate;
        /// <summary>
        /// 儲值日期
        /// </summary>
        public DateTime StorgeDate
        {
            get { return _storgeDate; }
            set
            {
                if (_storgeDate != value)
                {
                    _storgeDate = value;

                    OnPropertyChanged(nameof(StorgeDate));
                }
            }
        }
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
        private string _customerName;
        /// <summary>
        /// 會員姓名
        /// </summary>
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                if (_customerName != value)
                {
                    _customerName = value;

                    OnPropertyChanged(nameof(CustomerName));
                }
            }
        }
        private string _membershipNumber;
        /// <summary>
        /// 會員編號
        /// </summary>
        public string MembershipNumber
        {
            get { return _membershipNumber; }
            set
            {
                if (_membershipNumber != value)
                {
                    _membershipNumber = value;

                    OnPropertyChanged(nameof(MembershipNumber));
                }
            }
        }
        private int _value;
        /// <summary>
        /// 儲值金額
        /// </summary>
        public int Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;

                    OnPropertyChanged(nameof(_value));
                }
            }
        }
        private string _handler;
        /// <summary>
        /// 經手人ID(員工ID)
        /// </summary>
        public string Handler
        {
            get { return _handler; }
            set
            {
                if (_handler != value)
                {
                    _handler = value;

                    OnPropertyChanged(nameof(Handler));
                }
            }
        }
        private List<string> _staffList;
        public List<string> StaffList
        {
            get { return _staffList; }
            set
            {
                _staffList = value;
                OnPropertyChanged(nameof(StaffList));
            }
        }
        private List<StorgeValueModel> _seletedItems;
        public List<StorgeValueModel> SeletedItems
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

        public StorgeViewModel()
        {
            SearchCommand = new RelayCommand(ExecuteSearchCommand);
            InsertCommand = new RelayCommand(ExecuteInsertCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            _sqliteHelper = new SQLiteHelper();
        }
        public RelayCommand SearchCommand { get; }
        public RelayCommand InsertCommand { get; }
        public RelayCommand DeleteCommand { get; }


        /// <summary>
        /// 搜尋
        /// </summary>
        private void ExecuteSearchCommand()
        {
            StorgeHistoryList.Clear();
            var initialDate = InitialDate.ToString("yyyyMMdd");
            var finalDate = FinalDate.ToString("yyyyMMdd");
            var unitStorge = _sqliteHelper.Query<StorgeValueModel>($"select * from StorgeValue WHERE Date BETWEEN '{initialDate}' AND '{finalDate}'");

            if (unitStorge != null)
                unitStorge.ForEach(s => StorgeHistoryList.Add(s));
        }
        /// <summary>
        /// 輸入
        /// </summary>
        private void ExecuteInsertCommand()
        {
            if (!CheckValidity())
                return;

            var unitStorge = new StorgeValueModel()
            {
                CustomerName = CustomerName,
                CustomerID = MembershipNumber,
                Date = Int32.Parse(StorgeDate.ToString("yyyyMMdd")),
                ImportExport = Value,
                StaffID = Handler,
                WorkDataTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            //StorgeHistoryList.Add(unitStorge);

            var result = MessageBox.Show("即將完成入賬，是否已確認所有項目?!", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _sqliteHelper.db.CreateTable<StorgeValueModel>();//表已存在不會重覆創建
                _sqliteHelper.Add(unitStorge);
                MessageBox.Show("完成入賬!", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                ExecuteSearchCommand();
                SetDefaultTextBox();
            }
        }
        /// <summary>
        /// 刪除
        /// </summary>
        private void ExecuteDeleteCommand()
        {
            if (SeletedItems != null && SeletedItems.Count > 0)
            {
                var result = MessageBox.Show("確定要從資料庫中刪除?!", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    foreach (StorgeValueModel item in SeletedItems)
                    {
                        _sqliteHelper.Delete(item);
                    }
                    ExecuteSearchCommand();
                    MessageBox.Show("已刪除該資料!", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        public bool CanExecuteDeleteCommand()
        {
            if (SeletedItems == null || SeletedItems.Count == 0)
                return false;
            else
                return true;
        }
        public ICommand SelectionChangedCommand => _selectionChangedCommand ?? (_selectionChangedCommand = new RelayCommand<IList>(OnChanged));

        private ICommand _selectionChangedCommand;
        private void OnChanged(IList dataset)
        {
            SeletedItems = dataset.OfType<StorgeValueModel>().ToList();
            DeleteCommand.NotifyCanExecuteChanged();
        }

        private void SetDefaultTextBox()
        {
            CustomerName = "";
            MembershipNumber = "";
            Value = 0;
        }
        private bool CheckValidity()
        {

            if (string.IsNullOrEmpty(CustomerName))
            {
                MessageBox.Show("會員姓名不能為空", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (string.IsNullOrEmpty(MembershipNumber))
            {
                MessageBox.Show("會員編號不能為空", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (Value == 0)
            {
                MessageBox.Show("金額不能為零", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (string.IsNullOrEmpty(Handler))
            {
                MessageBox.Show("請選擇經手人", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }
    }
}
