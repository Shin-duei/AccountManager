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
    public class StaffViewModel : ObservableRecipient
    {
        private SQLiteHelper _sqliteHelper;
        /// <summary>
        /// 顯示用列表
        /// </summary>
        public ObservableCollection<StaffModel> StaffListDisplay { set; get; } = new ObservableCollection<StaffModel>();

        private int _maxId;
        public string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;

                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        public string _alias;
        public string Alias
        {
            get { return _alias; }
            set
            {
                if (_alias != value)
                {
                    _alias = value;

                    OnPropertyChanged(nameof(Alias));
                }
            }
        }
        public string _id;
        public string ID
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;

                    OnPropertyChanged(nameof(ID));
                }
            }
        }
        public string _position;
        public string Position
        {
            get { return _position; }
            set
            {
                if (_position != value)
                {
                    _position = value;

                    OnPropertyChanged(nameof(Position));
                }
            }
        }
        public string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;

                    OnPropertyChanged(nameof(Password));
                }
            }
        }
        public string _passwordConfirm;
        public string Passwordconfirm
        {
            get { return _passwordConfirm; }
            set
            {
                if (_passwordConfirm != value)
                {
                    _passwordConfirm = value;

                    OnPropertyChanged(nameof(Passwordconfirm));
                }
            }
        }
        public DateTime _onBoardDate = DateTime.Now;
        public DateTime OnBoardDate
        {
            get { return _onBoardDate; }
            set
            {
                if (_onBoardDate != value)
                {
                    _onBoardDate = value;

                    OnPropertyChanged(nameof(OnBoardDate));
                }
            }
        }

        private bool _isValidAddAccount = true;
        public bool IsValidAddAccount
        {
            get { return _isValidAddAccount; }
            set
            {
                if (_isValidAddAccount != value)
                {
                    _isValidAddAccount = value;

                    OnPropertyChanged(nameof(IsValidAddAccount));
                }
            }
        }
        private bool _isReadBill;
        public bool IsReadBill
        {
            get { return _isReadBill; }
            set
            {
                if (_isReadBill != value)
                {
                    _isReadBill = value;

                    OnPropertyChanged(nameof(IsReadBill));
                }
            }
        }
        private bool _isEditBill;
        public bool IsEditBill
        {
            get { return _isEditBill; }
            set
            {
                if (_isEditBill != value)
                {
                    _isEditBill = value;

                    OnPropertyChanged(nameof(IsEditBill));
                }
            }
        }
        private bool _isReadStatistics;
        public bool IsReadStatistics
        {
            get { return _isReadStatistics; }
            set
            {
                if (_isReadStatistics != value)
                {
                    _isReadStatistics = value;

                    OnPropertyChanged(nameof(IsReadStatistics));
                }
            }
        }
        private bool _isEditStatistics;
        public bool IsEditStatistics
        {
            get { return _isEditStatistics; }
            set
            {
                if (_isEditStatistics != value)
                {
                    _isEditStatistics = value;

                    OnPropertyChanged(nameof(IsEditStatistics));
                }
            }
        }
        private bool _isReadStorge;
        public bool IsReadStorge
        {
            get { return _isReadStorge; }
            set
            {
                if (_isReadStorge != value)
                {
                    _isReadStorge = value;

                    OnPropertyChanged(nameof(IsReadStorge));
                }
            }
        }
        private bool _isEditStorge;
        public bool IsEditStorge
        {
            get { return _isEditStorge; }
            set
            {
                if (_isEditStorge != value)
                {
                    _isEditStorge = value;

                    OnPropertyChanged(nameof(IsEditStorge));
                }
            }
        }
        private bool _isReadSalary;
        public bool IsReadSalary
        {
            get { return _isReadSalary; }
            set
            {
                if (_isReadSalary != value)
                {
                    _isReadSalary = value;

                    OnPropertyChanged(nameof(IsReadSalary));
                }
            }
        }
        private bool _isEditSalary;
        public bool IsEditSalary
        {
            get { return _isEditSalary; }
            set
            {
                if (_isEditSalary != value)
                {
                    _isEditSalary = value;

                    OnPropertyChanged(nameof(IsEditSalary));
                }
            }
        }
        private bool _isReadStaff;
        public bool IsReadStaff
        {
            get { return _isReadStaff; }
            set
            {
                if (_isReadStaff != value)
                {
                    _isReadStaff = value;

                    OnPropertyChanged(nameof(IsReadStaff));
                }
            }
        }
        private bool _isEditStaff;
        public bool IsEditStaff
        {
            get { return _isEditStaff; }
            set
            {
                if (_isEditStaff != value)
                {
                    _isEditStaff = value;

                    OnPropertyChanged(nameof(IsEditStaff));
                }
            }
        }


        public StaffViewModel()
        {
            AddNewStaffCommand = new RelayCommand(ExecuteAddNewStaffCommand);
            _sqliteHelper = new SQLiteHelper();
            _sqliteHelper.db.CreateTable<StaffModel>();//表已存在不會重覆創建
            var staffList = _sqliteHelper.Query<StaffModel>("select * from Staff");

            if (staffList.Count > 0)
                _maxId = (staffList.Max(s => Int32.Parse(s.ID)) + 1);
            else
                _maxId = 1;

            ID = _maxId.ToString("000");

            staffList.ForEach(s =>
            {
                if (string.IsNullOrEmpty(s.ResignationDate))//離職的不加進來
                    StaffListDisplay.Add(s);
            });
        }
        public RelayCommand AddNewStaffCommand { get; }

        private void ExecuteAddNewStaffCommand()
        {
            if (!IsValidAddAccount)
            {
                IsValidAddAccount = true;
                return;
            }

            var newStaff = new StaffModel()
            {
                Name = Name,
                Alias = Alias,
                ID = ID,
                Position = Position,
                Password = Password,
                OnBoardDate = OnBoardDate.ToString("yyyyMMdd"),
                RegisterDateTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                Authority = GetAuthority()
            };
            StaffListDisplay.Add(newStaff);
            _sqliteHelper.Add(newStaff);         
            ID = (++_maxId).ToString("000");
        }
        private void ExecuteSearchCommand()
        {

        }
        private string GetAuthority()
        {
            int[] authority = new int[5] { 0, 0, 0, 0, 0 };
            if (IsReadBill)
                authority[0] = 1;
            else if (IsEditBill)
                authority[0] = 2;

            if (IsReadStatistics)
                authority[1] = 1;
            else if (IsEditStatistics)
                authority[1] = 2;

            if (IsReadStorge)
                authority[2] = 1;
            else if (IsEditStorge)
                authority[2] = 2;

            if (IsReadSalary)
                authority[3] = 1;
            else if (IsEditSalary)
                authority[3] = 2;

            if (IsReadStaff)
                authority[4] = 1;
            else if (IsEditStaff)
                authority[4] = 2;

            string result = "";
            authority.ToList().ForEach(s => result += s);

            return result;
        }
    }
}
