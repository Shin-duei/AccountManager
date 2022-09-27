using AccountManager.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public StorgeViewModel()
        {
            SearchCommand = new RelayCommand(ExecuteSearchCommand);
            InsertCommand = new RelayCommand(ExecuteInsertCommand);
            _sqliteHelper = new SQLiteHelper();
        }
        public RelayCommand SearchCommand { get; }
        public RelayCommand InsertCommand { get; }

        private void ExecuteSearchCommand()
        {

        }
        private void ExecuteInsertCommand()
        {
            var unitStorge = new StorgeValueModel()
            {
                CustomerName = CustomerName,
                CustomerID = MembershipNumber,
                Date = Int32.Parse(StorgeDate.ToString("yyyyMMdd")),
                ImportExport = Value,
                StaffID = Handler,
                WorkDataTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            StorgeHistoryList.Add(unitStorge);
        }
    }
}
