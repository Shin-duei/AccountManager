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
using System.Windows.Input;

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
        public int MaxId
        {
            get { return _maxId; }
            set
            {
                if (_maxId != value)
                {
                    _maxId = value;

                    OnPropertyChanged(nameof(MaxId));
                }
            }
        }
        private string _tempPassword;
        public string TempPassword
        {
            get { return _tempPassword; }
            set
            {
                if (_tempPassword != value)
                {
                    _tempPassword = value;

                    OnPropertyChanged(nameof(TempPassword));
                }
            }
        }

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
        public string PasswordConfirm
        {
            get { return _passwordConfirm; }
            set
            {
                if (_passwordConfirm != value)
                {
                    _passwordConfirm = value;

                    OnPropertyChanged(nameof(PasswordConfirm));
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
        private bool _isConfirmCancelStaff = false;
        public bool IsConfirmCancelStaff
        {
            get { return _isConfirmCancelStaff; }
            set
            {
                if (_isConfirmCancelStaff != value)
                {
                    _isConfirmCancelStaff = value;

                    OnPropertyChanged(nameof(IsConfirmCancelStaff));
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
        private List<StaffModel> _seletedStaff;
        public List<StaffModel> SeletedStaff
        {
            get { return _seletedStaff; }
            set
            {
                if (_seletedStaff != value)
                {
                    _seletedStaff = value;

                    OnPropertyChanged(nameof(SeletedStaff));
                }
            }
        }
        private string _authority;
        public string Authority
        {
            get { return _authority; }
            set
            {
                if (_authority != value)
                {
                    _authority = value;

                    OnPropertyChanged(nameof(Authority));
                }
            }
        }

        public StaffViewModel()
        {
            AddNewStaffCommand = new RelayCommand(ExecuteAddNewStaffCommand);
            CancelStaffCommand = new RelayCommand(ExecuteCancelStaffCommand, CanExecuteCancelStaffCommand);
            EditStaffCommand = new RelayCommand(ExecuteEditStaffCommand);

            _sqliteHelper = new SQLiteHelper();
            _sqliteHelper.db.CreateTable<StaffModel>();//表已存在不會重覆創建
            var staffList = _sqliteHelper.Query<StaffModel>("select * from Staff");

            if (staffList.Count > 0)
                MaxId = (staffList.Max(s => Int32.Parse(s.ID)) + 1);
            else
                MaxId = 1;

            ID = MaxId.ToString("000");

            staffList.ForEach(s =>
            {
                if (string.IsNullOrEmpty(s.ResignationDate))//離職的不加進來
                    StaffListDisplay.Add(s);
            });
        }
        public RelayCommand AddNewStaffCommand { get; }
        public RelayCommand CancelStaffCommand { get; }
        public RelayCommand EditStaffCommand { get; }

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
                Authority = Authority=GetAuthority()
            };
            StaffListDisplay.Add(newStaff);
            _sqliteHelper.Add(newStaff);
            ID = (++MaxId).ToString("000");
        }
        /// <summary>
        /// 執行編輯
        /// </summary>
        private void ExecuteEditStaffCommand()
        {
            if (!IsValidAddAccount)
                return;

            var targetId = SeletedStaff.First().ID;

            List<StaffModel> _temList = new List<StaffModel>(StaffListDisplay.ToList());

            StaffListDisplay.Clear();

            _temList.ForEach(s =>
            {
                if (s.ID == targetId)
                {
                    s.Name = Name;
                    s.Alias = Alias;
                    s.Position = Position;
                    s.Password = TempPassword;
                    s.OnBoardDate = OnBoardDate.ToString("yyyyMMdd");
                    s.ModifyDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                    s.Authority = Authority;
                    _sqliteHelper.Update(s);
                }
                StaffListDisplay.Add(s);
            });

            RestInputUI();
        }
        /// <summary>
        /// 移除離職人員
        /// </summary>
        private void ExecuteCancelStaffCommand()
        {
            if (!IsConfirmCancelStaff)
                return;

            var seletedStaff = SeletedStaff.First();
            seletedStaff.ResignationDate = DateTime.Now.ToString("yyyyMMdd");
            StaffListDisplay.Remove(StaffListDisplay.Where(s => s.ID == seletedStaff.ID).Single());
            _sqliteHelper.Update(seletedStaff);
        }
        public bool CanExecuteCancelStaffCommand()
        {
            if (SeletedStaff == null || SeletedStaff.Count != 1)
                return false;
            else
                return true;
        }
        public ICommand SelectionChangedCommand => _selectionChangedCommand ?? (_selectionChangedCommand = new RelayCommand<IList>(OnChanged));

        private void OnChanged(IList dataset)
        {
            SeletedStaff = dataset.OfType<StaffModel>().ToList();
            EditStaffCommand.NotifyCanExecuteChanged();
            CancelStaffCommand.NotifyCanExecuteChanged();
        }

        private ICommand _selectionChangedCommand;

        public  string GetAuthority()
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
        public void ReloadAuthority(string authority)
        {
            var authorityArrary = authority.ToArray();

            if (authorityArrary[0].ToString() == "1")
                IsReadBill = true;
            else if (authorityArrary[0].ToString() == "2")
                IsEditBill = true;
            else
            {
                IsReadBill = false;
                IsEditBill = false;
            }

            if (authorityArrary[1].ToString() == "1")
                IsReadStatistics = true;
            else if (authorityArrary[1].ToString() == "2")
                IsEditStatistics = true;
            else
            {
                IsReadStatistics = false;
                IsEditStatistics = false;
            }

            if (authorityArrary[2].ToString() == "1")
                IsReadStorge = true;
            else if (authorityArrary[2].ToString() == "2")
                IsEditStorge = true;
            else
            {
                IsReadStorge = false;
                IsEditStorge = false;
            }

            if (authorityArrary[3].ToString() == "1")
                IsReadSalary = true;
            else if (authorityArrary[3].ToString() == "2")
                IsEditSalary = true;
            else
            {
                IsReadSalary = false;
                IsEditSalary = false;
            }

            if (authorityArrary[4].ToString() == "1")
                IsReadStaff = true;
            else if (authorityArrary[4].ToString() == "2")
                IsEditStaff = true;
            else
            {
                IsReadStaff = false;
                IsEditStaff = false;
            }
        }
        private void RestInputUI()
        {
            Name = "";
            ID = MaxId.ToString("000");
            Position = "";
            Alias = "";
            OnBoardDate = DateTime.Now;
        }
    }
}
