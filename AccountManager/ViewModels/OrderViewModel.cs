using AccountManager.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccountManager.ViewModels
{
    public class OrderViewModel : ObservableRecipient
    {
        /// <summary>
        /// 顯示用列表
        /// </summary>
        public ObservableCollection<EssentialModel> BillDisplay { set; get; }
        /// <summary>
        /// 訂單基礎資料
        /// </summary>
        private Dictionary<string, BillBaseModel> _billBaseDictionary { set; get; } = new Dictionary<string, BillBaseModel>();
        /// <summary>
        /// 當前訂單
        /// </summary>
        private Tuple<string, List<EssentialModel>> _currentBillDisplay;



        public Tuple<string, List<EssentialModel>> CurrentBillDisplay
        {
            get { return _currentBillDisplay; }
            set
            {
                SetProperty(ref _currentBillDisplay, value);
                AddNewStatementCommand.NotifyCanExecuteChanged();
            }
        }
        public SQLiteHelper _sqliteHelper;
        /// <summary>
        /// 紀錄訂單順序
        /// </summary>
        private List<string> _billSequence { set; get; } = new List<string>();

        private Dictionary<string, List<EssentialModel>> _billListDictionary = new Dictionary<string, List<EssentialModel>>();
        /// <summary>
        /// 所有訂單
        /// </summary>
        public Dictionary<string, List<EssentialModel>> BillListDictionary
        {
            get
            {
                return _billListDictionary;
            }
        }

        public string _currentBillNumber;
        public string CurrentBillNumber
        {
            get
            {
                if (_currentBillDisplay == null || _currentBillDisplay.Item1 == null)
                    return "";

                return _currentBillDisplay.Item1;
            }
        }
        public Visibility VisibleDisplayCurrentBillNumber
        {
            get
            {
                if (string.IsNullOrEmpty(CurrentBillNumber))
                    return Visibility.Hidden;
                else
                    return Visibility.Visible;
            }
        }
        public int DynamicRowHeight
        {
            get
            {
                if (VisibleDisplayCurrentBillNumber == Visibility.Hidden)
                    return 0;
                else
                    return 35;
            }
        }


        private string _billNumber;
        public string BillNumber
        {
            get { return _billNumber; }
            set
            {
                if (_billNumber != value)
                {
                    _billNumber = value;

                    OnPropertyChanged(nameof(BillNumber));
                }
            }
        }
        private uint _selectedBill;
        public uint SelectedBill
        {
            get { return _selectedBill; }
            set
            {
                if (_selectedBill != value)
                {
                    _selectedBill = value;

                    OnPropertyChanged(nameof(SelectedBill));
                }
            }
        }
        private DateTime _consumptionDate;
        /// <summary>
        /// 消費日期
        /// </summary>
        public DateTime ConsumptionDate
        {
            get { return _consumptionDate; }
            set
            {
                if (_consumptionDate != value)
                {
                    _consumptionDate = value;
                    BillNumber = SearchBillNumberInCurrentListAndDB(ConsumptionDate);
                    OnPropertyChanged(nameof(ConsumptionDate));
                }
            }
        }
        private bool _isAssignDesigner;
        /// <summary>
        /// 指定設計師
        /// </summary>
        public bool IsAssignDesigner
        {
            get { return _isAssignDesigner; }
            set
            {
                if (_isAssignDesigner != value)
                {
                    _isAssignDesigner = value;

                    OnPropertyChanged(nameof(IsAssignDesigner));
                }
            }
        }
        public uint TotalBillCount
        {
            get => (uint)BillListDictionary.Count;
        }
        private List<ComboBoxItemTuple> _designerList;
        public List<ComboBoxItemTuple> DesignerList
        {
            set
            {
                _designerList = value;
                OnPropertyChanged(nameof(DesignerList));
            }
            get { return _designerList; }
        }
        private ComboBoxItemTuple _seletedDesigner;
        public ComboBoxItemTuple SeletedDesigner
        {
            get { return _seletedDesigner; }
            set
            {
                if (_seletedDesigner != value)
                {
                    _seletedDesigner = value;

                    OnPropertyChanged(nameof(SeletedDesigner));
                }
            }
        }
        private List<ComboBoxItemTuple> _assistantList;
        public List<ComboBoxItemTuple> AssistantList
        {
            get
            {
                return _assistantList;
            }
            set
            {
                _assistantList = value;
                OnPropertyChanged(nameof(AssistantList));
            }
        }
        private List<ComboBoxItemTuple> _assistantList2;
        public List<ComboBoxItemTuple> AssistantList2
        {
            get
            {
                return _assistantList2;
            }
            set
            {
                _assistantList2 = value;
                OnPropertyChanged(nameof(AssistantList2));
            }
        }
        private List<ComboBoxItemTuple> _assistantList3;
        public List<ComboBoxItemTuple> AssistantList3
        {
            get
            {
                return _assistantList3;
            }
            set
            {
                _assistantList3 = value;
                OnPropertyChanged(nameof(AssistantList3));
            }
        }
        private ComboBoxItemTuple _seletedAssistant1;
        public ComboBoxItemTuple SeletedAssistant1
        {
            get { return _seletedAssistant1; }
            set
            {
                if (_seletedAssistant1 != value)
                {
                    _seletedAssistant1 = value;

                    OnPropertyChanged(nameof(SeletedAssistant1));
                    OnPropertyChanged(nameof(CanSelectAssistant2));
                    AssistantList2 = AssistantList.Select(s => s).Where(s => s != SeletedAssistant1).ToList();
                }
            }
        }
        public bool CanSelectAssistant2
        {
            get
            {
                if (SeletedAssistant1 == null)
                    return false;

                var status = !string.IsNullOrWhiteSpace(SeletedAssistant1.Value);
                if (!status)
                    SeletedAssistant2.Value = "";
                return status;
            }
        }
        private ComboBoxItemTuple _seletedAssistant2;
        public ComboBoxItemTuple SeletedAssistant2
        {
            get { return _seletedAssistant2; }
            set
            {
                if (_seletedAssistant2 != value)
                {
                    _seletedAssistant2 = value;

                    OnPropertyChanged(nameof(SeletedAssistant2));
                    OnPropertyChanged(nameof(CanSelectAssistant3));
                    AssistantList3 = AssistantList.Select(s => s).Where(s => s != SeletedAssistant1 && s != SeletedAssistant2).ToList();
                }
            }
        }
        public bool CanSelectAssistant3
        {
            get
            {
                if (SeletedAssistant2 == null)
                    return false;

                var status = !string.IsNullOrWhiteSpace(SeletedAssistant2.Value);
                if (!status)
                    SeletedAssistant3.Value = "";
                return status;
            }
        }
        private ComboBoxItemTuple _seletedAssistant3 = new ComboBoxItemTuple { DisplayName = "", Value = "" };
        public ComboBoxItemTuple SeletedAssistant3
        {
            get { return _seletedAssistant3; }
            set
            {
                if (_seletedAssistant3 != value)
                {
                    _seletedAssistant3 = value;

                    OnPropertyChanged(nameof(SeletedAssistant3));
                }
            }
        }
        private string _remark;
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
        private string _customerName;
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
        private int _unitPrice;
        public int UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                if (_unitPrice != value)
                {
                    _unitPrice = value;

                    OnPropertyChanged(nameof(UnitPrice));
                }
            }
        }
        private int _count;
        public int Count
        {
            get { return _count; }
            set
            {
                if (Count != value)
                {
                    _count = value;

                    OnPropertyChanged(nameof(Count));
                }
            }
        }
        private bool _IsCashPay;
        public bool IsCashPay
        {
            get { return _IsCashPay; }
            set
            {
                if (_IsCashPay != value)
                {
                    _IsCashPay = value;

                    OnPropertyChanged(nameof(IsCashPay));

                    PaymentType = ChangePaymentType();
                    OnPropertyChanged(nameof(PaymentType));
                }
            }
        }
        private bool _isStoredValue;
        public bool IsStoredValue
        {
            get { return _isStoredValue; }
            set
            {
                if (_isStoredValue != value)
                {
                    _isStoredValue = value;

                    OnPropertyChanged(nameof(IsStoredValue));

                    PaymentType = ChangePaymentType();

                    OnPropertyChanged(nameof(PaymentType));
                }
            }
        }
        private bool _isCreditCard;
        public bool IsCreditCard
        {
            get { return _isCreditCard; }
            set
            {
                if (_isCreditCard != value)
                {
                    _isCreditCard = value;

                    OnPropertyChanged(nameof(IsCreditCard));

                    PaymentType = ChangePaymentType();
                    OnPropertyChanged(nameof(PaymentType));
                }
            }
        }
        private string _paymentType = "";
        public string PaymentType
        {
            get { return _paymentType; }
            set
            {
                if (_paymentType != value)
                {
                    _paymentType = value;
                    OnPropertyChanged(nameof(PaymentType));
                }
            }
        }
        private string ChangePaymentType()
        {
            if (IsCashPay)
                return "現金";
            else if (IsStoredValue)
                return "儲值金";
            else if (IsCreditCard)
                return "信用卡";
            else
                return "";
        }
        private int _cash;
        public int Cash
        {
            get { return _cash; }
            set
            {
                if (_cash != value)
                {
                    _cash = value;
                    OnPropertyChanged(nameof(Cash));
                }
            }
        }
        private int _storgedValue;
        public int StorgedValue
        {
            get { return _storgedValue; }
            set
            {
                if (_storgedValue != value)
                {
                    _storgedValue = value;
                    OnPropertyChanged(nameof(StorgedValue));
                }
            }
        }
        private int _creditCard;
        public int CreditCard
        {
            get { return _creditCard; }
            set
            {
                if (_creditCard != value)
                {
                    _creditCard = value;
                    OnPropertyChanged(nameof(CreditCard));
                }
            }
        }

        public int TotalCost
        {
            get { return Cash + StorgedValue + CreditCard; }
        }
        private List<string> _orderItemList;
        public List<string> OrderItemList
        {
            set
            {
                if (_orderItemList != value)
                {
                    _orderItemList = value;

                    OnPropertyChanged(nameof(OrderItemList));
                }
            }
            get
            {
                return _orderItemList;
            }
        }
        private string _seletedOrderItem;
        public string SeletedOrderItem
        {
            get { return _seletedOrderItem; }
            set
            {
                if (_seletedOrderItem != value)
                {
                    _seletedOrderItem = value;

                    OnPropertyChanged(nameof(SeletedOrderItem));
                }
            }
        }
        private List<EssentialModel> _seletedStatement;
        public List<EssentialModel> SeletedStatement
        {
            get { return _seletedStatement; }
            set
            {
                if (_seletedStatement != value)
                {
                    _seletedStatement = value;

                    OnPropertyChanged(nameof(SeletedStatement));
                }
            }
        }
        public OrderViewModel()
        {
            _sqliteHelper = new SQLiteHelper();
            _sqliteHelper.db.CreateTable<EssentialModel>();//表已存在不會重覆創建
            _sqliteHelper.db.CreateTable<StaffModel>();//表已存在不會重覆創建

            AddNewBillCommand = new RelayCommand(ExecuteAddNewBillCommand);
            AddNewStatementCommand = new RelayCommand(ExecuteAddNewStatementCommand, CanExecuteAddNewStatementCommand);
            EditStatementCommand = new RelayCommand(ExecuteEditStatementCommand, CanExecuteEditStatementCommand);
            DeleteStatementCommand = new RelayCommand(ExecuteDeleteStatementCommand, CanExecuteDeleteStatementCommand);
            DeleteOneBillCommand = new RelayCommand(ExecuteDeleteOneBillCommand, CanExecuteDeleteOneBillCommand);
            DeleteAllBillCommand = new RelayCommand(ExecuteDeleteAllBillCommand, CanExecuteDeleteAllBillCommand);
            InsertAllBillCommand = new RelayCommand(ExecuteInsertAllBillCommand, CanExecuteInsertAllBillCommand);

            PreviousBillCommand = new RelayCommand(ExecutePreviousBillCommand);
            NextBillCommand = new RelayCommand(ExecuteNextBillCommand);
            FirstBillCommand = new RelayCommand(ExecuteFirstBillCommand);
            LastBillCommand = new RelayCommand(ExecuteLastBillCommand);
            Initialize();
        }
        public RelayCommand AddNewBillCommand { get; }
        public RelayCommand AddNewStatementCommand { get; }
        public RelayCommand DeleteStatementCommand { get; }
        public RelayCommand EditStatementCommand { get; }
        public RelayCommand DeleteOneBillCommand { get; }
        public RelayCommand DeleteAllBillCommand { get; }
        public RelayCommand InsertAllBillCommand { get; private set; }

        public RelayCommand PreviousBillCommand { get; }
        public RelayCommand NextBillCommand { get; }
        public RelayCommand FirstBillCommand { get; }
        public RelayCommand LastBillCommand { get; }


        /// <summary>
        /// 增加訂單
        /// </summary>
        private void ExecuteAddNewBillCommand()
        {
            if (!IsBillExistedInDB(BillNumber))//判斷訂單是否重複
                return;

            if (string.IsNullOrWhiteSpace(BillNumber))//沒有訂單號
                return;

            if (BillListDictionary.ContainsKey(BillNumber))//訂單重複 應該要跳提示
                return;

            var addedBill = new List<EssentialModel>();

            BillListDictionary.Add(BillNumber, addedBill);
            _billSequence.Add(BillNumber);

            CurrentBillDisplay = new Tuple<string, List<EssentialModel>>(BillNumber, addedBill);
            _billBaseDictionary.Add(BillNumber, new BillBaseModel()
            {

                ConsumptionNumber = BillNumber,
                ConsumptionDate = Int32.Parse(ConsumptionDate.ToString("yyyyMMdd")),
                IsAssignDesigner = IsAssignDesigner,
                CustomerName = CustomerName,
                MembershipNumber = MembershipNumber,
            });

            RefreshDataGrid(addedBill);
            OnPropertyChanged(nameof(TotalBillCount));

            InsertAllBillCommand.NotifyCanExecuteChanged();
            DeleteAllBillCommand.NotifyCanExecuteChanged();
            DeleteOneBillCommand.NotifyCanExecuteChanged();
            SelectedBill = TotalBillCount;
            BillNumber = SearchBillNumberInCurrentListAndDB(ConsumptionDate);

            OnPropertyChanged(nameof(VisibleDisplayCurrentBillNumber));
            OnPropertyChanged(nameof(CurrentBillNumber));
            OnPropertyChanged(nameof(DynamicRowHeight));
        }
        /// <summary>
        /// 增加明細
        /// </summary>
        private void ExecuteAddNewStatementCommand()
        {
            if (_currentBillDisplay == null || _currentBillDisplay.Item1 == null)
                return;

            var billNumber = _currentBillDisplay.Item1;

            _billListDictionary.TryGetValue(billNumber, out var bill);
            _billBaseDictionary.TryGetValue(billNumber, out var billBase);

            if (bill == null)
                return;

            if (billBase == null)
                return;

            if (SeletedDesigner == null || string.IsNullOrEmpty(SeletedDesigner.Value))
            {
                MessageBox.Show("設計師欄位不能空白", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var numberInBill = bill.Count;
            bill.Add(new EssentialModel()
            {
                ConsumptionNumber = billBase.ConsumptionNumber,
                ConsumptionDate = billBase.ConsumptionDate,
                IsAssignDesigner = billBase.IsAssignDesigner,
                CustomerName = billBase.CustomerName,
                MembershipNumber = billBase.MembershipNumber,

                NumberInBill = numberInBill,
                ConsumptionItem = SeletedOrderItem,
                UnitPrice = UnitPrice,
                Count = Count,
                PaymentType = PaymentType,
                Designer = SeletedDesigner.Value,
                Assistant1 = SeletedAssistant1 == null ? "" : SeletedAssistant1.Value,
                Assistant2 = SeletedAssistant2 == null ? "" : SeletedAssistant2.Value,
                Assistant3 = SeletedAssistant3 == null ? "" : SeletedAssistant3.Value,
                Remark = Remark

            }); ;
            Remark = "";
            RefreshDataGrid(bill);
        }
        public bool CanExecuteAddNewStatementCommand()
        {
            if (CurrentBillDisplay == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 刪除明細
        /// </summary>
        private void ExecuteDeleteStatementCommand()
        {
            if (SeletedStatement == null || SeletedStatement.Count == 0)
                return;

            var billNumber = _currentBillDisplay.Item1;

            _billListDictionary.TryGetValue(billNumber, out var bill);

            foreach (var statement in SeletedStatement)
            {
                for (int k = 0; k < bill.Count; k++)
                {
                    if (bill[k].NumberInBill == statement.NumberInBill)
                        bill.RemoveAt(k);
                }
            }

            var billCount = bill.Count;
            int i = 0;
            foreach (var item in bill)
            {
                item.NumberInBill = i;
                i++;
            }
            RefreshDataGrid(bill);
        }
        public bool CanExecuteDeleteStatementCommand()
        {

            if (SeletedStatement == null || SeletedStatement.Count == 0)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 編輯明細
        /// </summary>
        private void ExecuteEditStatementCommand()
        {
            if (SeletedStatement == null || SeletedStatement.Count != 1)
                return;

            var billNumber = _currentBillDisplay.Item1;
            _billBaseDictionary.TryGetValue(billNumber, out var billBase);
            _billListDictionary.TryGetValue(billNumber, out var bill);

            if (billBase == null || bill == null)
                return;

            bill.ForEach(s =>
            {
                if (s.NumberInBill == SeletedStatement[0].NumberInBill)
                {
                    s.ConsumptionNumber = billBase.ConsumptionNumber;
                    s.ConsumptionDate = billBase.ConsumptionDate;
                    s.IsAssignDesigner = billBase.IsAssignDesigner;
                    s.CustomerName = billBase.CustomerName;
                    s.MembershipNumber = billBase.MembershipNumber;
                    s.ConsumptionItem = SeletedOrderItem;
                    s.UnitPrice = UnitPrice;
                    s.Count = Count;
                    s.Designer = SeletedDesigner.Value;
                    s.PaymentType = PaymentType;
                    s.Assistant1 = SeletedAssistant1 == null ? "" : SeletedAssistant1.Value;
                    s.Assistant2 = SeletedAssistant2 == null ? "" : SeletedAssistant2.Value;
                    s.Assistant3 = SeletedAssistant3 == null ? "" : SeletedAssistant3.Value;
                    s.Remark = Remark;
                }
            });
            RefreshDataGrid(bill);
        }
        public bool CanExecuteEditStatementCommand()
        {
            if (SeletedStatement == null || SeletedStatement.Count != 1)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 刪除單筆訂單
        /// </summary>
        private void ExecuteDeleteOneBillCommand()
        {
            if (CurrentBillDisplay == null || CurrentBillDisplay.Item1 == null)
                return;

            List<EssentialModel> previousBill = null;
            var currentBillNumber = CurrentBillDisplay.Item1;

            _billListDictionary.Remove(currentBillNumber);
            _billBaseDictionary.Remove(currentBillNumber);

            var currentBillIndex = _billSequence.IndexOf(currentBillNumber);
            _billSequence.Remove(currentBillNumber);

            if (currentBillIndex == 0 && _billSequence.Count == 0)//刪除第一筆後完全沒訂單
            {
                CurrentBillDisplay = null;
                SelectedBill = 0;
            }
            else if (currentBillIndex >= 0 && _billSequence.Count != currentBillIndex)//刪除當前訂單 顯示後一筆訂單
            {
                var nextBillNumber = _billSequence[currentBillIndex];
                _billListDictionary.TryGetValue(nextBillNumber, out previousBill);
                CurrentBillDisplay = new Tuple<string, List<EssentialModel>>(nextBillNumber, previousBill);
                SelectedBill = (uint)currentBillIndex + 1;
            }
            else if (currentBillIndex == _billSequence.Count)//刪除當前訂單(末筆) 因為後面沒訂單 所以顯示前一筆訂單
            {
                var previousBillNumber = _billSequence[currentBillIndex - 1];
                _billListDictionary.TryGetValue(previousBillNumber, out previousBill);
                CurrentBillDisplay = new Tuple<string, List<EssentialModel>>(previousBillNumber, previousBill);
                SelectedBill = (uint)currentBillIndex;
            }

            RefreshDataGrid(previousBill);
            OnPropertyChanged(nameof(TotalBillCount));
            OnPropertyChanged(nameof(VisibleDisplayCurrentBillNumber));
            OnPropertyChanged(nameof(CurrentBillNumber));
            OnPropertyChanged(nameof(DynamicRowHeight));

            InsertAllBillCommand.NotifyCanExecuteChanged();
            DeleteAllBillCommand.NotifyCanExecuteChanged();
            AddNewStatementCommand.NotifyCanExecuteChanged();
            DeleteOneBillCommand.NotifyCanExecuteChanged();
            BillNumber = SearchBillNumberInCurrentListAndDB(ConsumptionDate);
        }
        public bool CanExecuteDeleteOneBillCommand()
        {

            if (_currentBillDisplay == null || _currentBillDisplay.Item1 == null)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 刪除全部訂單
        /// </summary>
        private void ExecuteDeleteAllBillCommand()
        {
            BillListDictionary.Clear();
            _billBaseDictionary.Clear();
            _currentBillDisplay = null;
            _billSequence.Clear();
            BillDisplay.Clear();
            SelectedBill = 0;

            OnPropertyChanged(nameof(TotalBillCount));
            OnPropertyChanged(nameof(VisibleDisplayCurrentBillNumber));
            OnPropertyChanged(nameof(CurrentBillNumber));
            OnPropertyChanged(nameof(DynamicRowHeight));

            InsertAllBillCommand.NotifyCanExecuteChanged();
            DeleteAllBillCommand.NotifyCanExecuteChanged();
            DeleteOneBillCommand.NotifyCanExecuteChanged();
            AddNewStatementCommand.NotifyCanExecuteChanged();
            BillNumber = SearchBillNumberInCurrentListAndDB(ConsumptionDate);
        }
        public bool CanExecuteDeleteAllBillCommand()
        {

            if (BillListDictionary == null || BillListDictionary.Count == 0)
                return false;
            else
                return true;

        }
        /// <summary>
        /// 入賬(寫到資料庫)
        /// </summary>
        private void ExecuteInsertAllBillCommand()
        {
            var result = MessageBox.Show("即將完成入賬，是否已確認所有明細?!", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                foreach (var bill in _billListDictionary)
                {
                    bill.Value.ForEach(s =>
                    {
                        s.LoginDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                        _sqliteHelper.Add(s);
                    });
                }
                MessageBox.Show("完成入賬!", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                ExecuteDeleteAllBillCommand();//清空當前訂單資料
            }
        }
        public bool CanExecuteInsertAllBillCommand()
        {

            if (BillListDictionary == null || BillListDictionary.Count == 0)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 翻頁(0最前 1前 2後 3最後)
        /// </summary>
        /// <param name=""></param>
        private void turnBill(int flag)
        {
            if (_currentBillDisplay == null || _currentBillDisplay.Item1 == null)
                return;

            var currentBillNumber = _currentBillDisplay.Item1;

            var currentIndex = _billSequence.IndexOf(currentBillNumber);

            if (currentIndex != -1)
            {
                int targetIndex = 0;

                if (flag == 0)
                    targetIndex = 0;
                else if (flag == 1)
                    targetIndex = currentIndex - 1;
                else if (flag == 2)
                    targetIndex = currentIndex + 1;
                else if (flag == 3)
                    targetIndex = _billSequence.Count - 1;

                if (targetIndex >= 0 && targetIndex < _billSequence.Count)
                {
                    var targetBillNumber = _billSequence[targetIndex];
                    _billListDictionary.TryGetValue(targetBillNumber, out var previousBill);
                    _currentBillDisplay = new Tuple<string, List<EssentialModel>>(targetBillNumber, previousBill);
                    RefreshDataGrid(previousBill);
                    SelectedBill = (uint)targetIndex + 1;
                }
            }
            OnPropertyChanged(nameof(CurrentBillNumber));
        }
        /// <summary>
        /// 第一筆
        /// </summary>
        private void ExecuteFirstBillCommand()
        {
            turnBill(0);
        }
        /// <summary>
        /// 前一筆
        /// </summary>
        private void ExecutePreviousBillCommand()
        {
            turnBill(1);
        }
        /// <summary>
        /// 下一筆
        /// </summary>
        private void ExecuteNextBillCommand()
        {
            turnBill(2);
        }

        /// <summary>
        /// 最後一筆
        /// </summary>
        private void ExecuteLastBillCommand()
        {
            turnBill(3);
        }

        public ICommand SelectionChangedCommand => _selectionChangedCommand ?? (_selectionChangedCommand = new RelayCommand<IList>(OnChanged));

        private void OnChanged(IList dataset)
        {
            SeletedStatement = dataset.OfType<EssentialModel>().ToList();
            EditStatementCommand.NotifyCanExecuteChanged();
            DeleteStatementCommand.NotifyCanExecuteChanged();
        }

        private ICommand _selectionChangedCommand;

        private void Initialize()
        {
            BillDisplay = new ObservableCollection<EssentialModel>();
            ConsumptionDate = DateTime.Now;
            Count = 1;
            IsCashPay = true;
            AssistantList = new List<ComboBoxItemTuple>();

            var staffList = _sqliteHelper.Query<StaffModel>("select * from Staff WHERE ResignationDate is NULL");//在職員工

            if (staffList != null && staffList.Count != 0)
            {
                var staffGroup = staffList.GroupBy(s => s.Position).ToList();

                var designer = staffGroup.FirstOrDefault(s => s.Key == "設計師");
                if (designer != null)
                {
                    DesignerList = new List<ComboBoxItemTuple>();
                    designer.ToList().ForEach(s => DesignerList.Add(new ComboBoxItemTuple { Value = s.ID, DisplayName = $"{ s.Alias} [{s.ID}]" }));
                }

                var assistant = staffGroup.FirstOrDefault(s => s.Key == "助理");
                if (assistant != null)
                {
                    assistant.ToList().ForEach(s => AssistantList.Add(new ComboBoxItemTuple { Value = s.ID, DisplayName = $"{ s.Alias} [{s.ID}]" }));
                }
            }

            OrderItemList = new List<string>(){
                                        "洗髮",
                                        "剪髮",
                                        "染髮",
                                        "燙髮",
                                        "護髮",
                                        "接髮",
                                        "頭皮SPA",
                                        "產品購買"};
        }
        /// <summary>
        /// 刷新顯示列表
        /// </summary>
        private void RefreshDataGrid(List<EssentialModel> bill)
        {
            BillDisplay.Clear();

            if (bill != null)
                bill.ForEach(statement => BillDisplay.Add(statement));

            Cash = BillDisplay.ToList()
                              .Where(s => s.PaymentType == "現金")
                              .Select(s => s.TotalPrice)
                              .Sum();
            StorgedValue = BillDisplay.ToList()
                              .Where(s => s.PaymentType == "儲值金")
                              .Select(s => s.TotalPrice)
                              .Sum();
            CreditCard = BillDisplay.ToList()
                              .Where(s => s.PaymentType == "信用卡")
                              .Select(s => s.TotalPrice)
                              .Sum();

            OnPropertyChanged(nameof(TotalCost));
        }
        /// <summary>
        /// 判斷資料庫中訂單(沒重複回傳true,)
        /// </summary>
        /// <param name="ConsumptionNumber"></param>
        private bool IsBillExistedInDB(string ConsumptionNumber)
        {
            var existedBills = _sqliteHelper.Query<EssentialModel>($"SELECT * FROM BillDetails WHERE ConsumptionNumber='{ConsumptionNumber}'");

            if (existedBills != null && existedBills.Count() != 0)
            {
                var result = MessageBox.Show("訂單編號已存在，是否要從資料庫清除後重新提交訂單", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)//清除該訂單
                {
                    existedBills.ForEach(s => _sqliteHelper.Delete<EssentialModel>(s));
                    return true;
                }
                else
                    return false;
            }
            else
                return true;
        }
        /// <summary>
        /// 從DB中找出該日最大訂單號碼
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private string SearchBillNumberInCurrentListAndDB(DateTime dateTime)
        {
            int consumptionDate = Int32.Parse(dateTime.ToString("yyyyMMdd"));

            var existedBills = _sqliteHelper.Query<EssentialModel>($"SELECT * FROM BillDetails WHERE ConsumptionDate='{consumptionDate}'");
            var existedBillNumber = BillListDictionary.Keys.Select(s => s).Where(s => s.Substring(0, 8) == dateTime.ToString("yyyyMMdd"));
            string maxBillNumberInDB = "0";
            string maxBillNumberInList = "0";
            try
            {
                if (existedBills != null && existedBills.Count() != 0)
                    maxBillNumberInDB = existedBills.Select(s => s.ConsumptionNumber.Substring(8)).Max();

                if (existedBillNumber != null && existedBillNumber.Count() != 0)
                    maxBillNumberInList = existedBillNumber.Select(s => s.Substring(8)).Max();

                if (Int32.Parse(maxBillNumberInDB) <= Int32.Parse(maxBillNumberInList))
                    return dateTime.ToString("yyyyMMdd") + string.Format("{0:000}", Int32.Parse(maxBillNumberInList) + 1);
                else if (Int32.Parse(maxBillNumberInDB) > Int32.Parse(maxBillNumberInList))
                    return dateTime.ToString("yyyyMMdd") + string.Format("{0:000}", Int32.Parse(maxBillNumberInDB) + 1);
            }
            catch (Exception ex)
            {

            }
            return Int32.Parse(dateTime.ToString("yyyyMMdd")) + string.Format("{0:000}", 1);

        }
    }
}
