using AccountManager.Common;
using AccountManager.Models;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccountManager.ViewModels
{
    public class OrderViewModel : ViewModel
    {
        /// <summary>
        /// 顯示用列表
        /// </summary>
        public ObservableCollection<EssentialModel> BillDisplay { set; get; }
        /// <summary>
        /// 當前訂單
        /// </summary>
        private Tuple<string, List<EssentialModel>> _currentBillDisplay { set; get; }
        /// <summary>
        /// 紀錄訂單順序
        /// </summary>
        private List<string> _billSequence { set; get; } = new List<string>();
        /// <summary>
        /// 所有訂單
        /// </summary>
        private Dictionary<string, List<EssentialModel>> _billListDictionary = new Dictionary<string, List<EssentialModel>>();

        private string _billNumber;
        public string BillNumber
        {
            get { return _billNumber; }
            set
            {
                if (_billNumber != value)
                {
                    _billNumber = value;

                    RaisePropertyChanged(() => BillNumber                                   );
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

                    RaisePropertyChanged(() => SelectedBill);
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

                    RaisePropertyChanged(() => ConsumptionDate);
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

                    RaisePropertyChanged(() => IsAssignDesigner);
                }
            }
        }
        public uint TotalBillCount
        {
            get => (uint)_billListDictionary.Count;
        }
        private List<string> _designerList = new List<string>()
        {
            "Amy Chen",
            "Cart Lin",
        };
        public List<string> DesignerList
        {
            get
            {
                return _designerList;
            }
        }
        private string _seletedDesigner;
        public string SeletedDesigner
        {
            get { return _seletedDesigner; }
            set
            {
                if (_seletedDesigner != value)
                {
                    _seletedDesigner = value;

                    RaisePropertyChanged(() => SeletedDesigner);
                }
            }
        }
        private List<string> _assistantList;
        public List<string> AssistantList
        {
            get
            {
                return _assistantList;
            }
            set
            {
                _assistantList = value;
                RaisePropertyChanged(() => AssistantList);
            }
        }
        private List<string> _assistantList2;
        public List<string> AssistantList2
        {
            get
            {
                return _assistantList2;
            }
            set
            {
                _assistantList2 = value;
                RaisePropertyChanged(() => AssistantList2);
            }
        }
        private List<string> _assistantList3;
        public List<string> AssistantList3
        {
            get
            {
                return _assistantList3;
            }
            set
            {
                _assistantList3 = value;
                RaisePropertyChanged(() => AssistantList3);
            }
        }
        private string _seletedAssistant1;
        public string SeletedAssistant1
        {
            get { return _seletedAssistant1; }
            set
            {
                if (_seletedAssistant1 != value)
                {
                    _seletedAssistant1 = value;

                    RaisePropertyChanged(() => SeletedAssistant1);
                    RaisePropertyChanged(() => CanSelectAssistant2);
                    AssistantList2 = AssistantList.Select(s => s).Where(s => s != SeletedAssistant1).ToList();
                }
            }
        }
        public bool CanSelectAssistant2
        {
            get
            {
                var status = !string.IsNullOrWhiteSpace(SeletedAssistant1);
                if (!status)
                    SeletedAssistant2 = "";
                return status;
            }
        }
        private string _seletedAssistant2;
        public string SeletedAssistant2
        {
            get { return _seletedAssistant2; }
            set
            {
                if (_seletedAssistant2 != value)
                {
                    _seletedAssistant2 = value;

                    RaisePropertyChanged(() => SeletedAssistant2);
                    RaisePropertyChanged(() => CanSelectAssistant3);
                    AssistantList3 = AssistantList.Select(s => s).Where(s => s != SeletedAssistant1 && s != SeletedAssistant2).ToList();
                }
            }
        }
        public bool CanSelectAssistant3
        {
            get
            {
                var status = !string.IsNullOrWhiteSpace(SeletedAssistant2);
                if (!status)
                    SeletedAssistant3 = "";
                return status;
            }
        }
        private string _seletedAssistant3;
        public string SeletedAssistant3
        {
            get { return _seletedAssistant3; }
            set
            {
                if (_seletedAssistant3 != value)
                {
                    _seletedAssistant3 = value;

                    RaisePropertyChanged(() => SeletedAssistant3);
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

                    RaisePropertyChanged(() => CustomerName);
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

                    RaisePropertyChanged(() => MembershipNumber);
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

                    RaisePropertyChanged(() => UnitPrice);
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

                    RaisePropertyChanged(() => Count);
                }
            }
        }
        private bool _cashPay;
        public bool CashPay
        {
            get { return _cashPay; }
            set
            {
                if (_cashPay != value)
                {
                    _cashPay = value;

                    RaisePropertyChanged(() => CashPay);

                    PaymentType = ChangePaymentType();
                    RaisePropertyChanged(() => PaymentType);
                }
            }
        }
        private bool _storedValue;
        public bool StoredValue
        {
            get { return _storedValue; }
            set
            {
                if (_storedValue != value)
                {
                    _storedValue = value;

                    RaisePropertyChanged(() => StoredValue);

                    PaymentType = ChangePaymentType();

                    RaisePropertyChanged(() => PaymentType);
                }
            }
        }
        private bool _creditCard;
        public bool CreditCard
        {
            get { return _creditCard; }
            set
            {
                if (_creditCard != value)
                {
                    _creditCard = value;

                    RaisePropertyChanged(() => CreditCard);

                    PaymentType = ChangePaymentType();
                    RaisePropertyChanged(() => PaymentType);
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
                    RaisePropertyChanged(() => PaymentType);
                }
            }
        }
        private string ChangePaymentType()
        {
            if (CashPay)
                return "現金";
            else if (StoredValue)
                return "儲值金";
            else if (CreditCard)
                return "信用卡";
            else
                return "";
        }
        private List<string> _orderItemList = new List<string>()
        {
            "洗髮",
            "剪髮",
            "染髮",
            "燙髮",
            "護髮",
            "頭皮SPA",
            "產品購買"
        };
        public List<string> OrderItemList
        {
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

                    RaisePropertyChanged(() => SeletedOrderItem);
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

                    RaisePropertyChanged(() => SeletedStatement);
                }
            }
        }
        public OrderViewModel()
        {
            AddNewBillCommand = new RelayCommand(ExecuteAddNewBillCommand);
            AddNewStatementCommand = new RelayCommand(ExecuteAddNewStatementCommand);
            DeleteStatementCommand = new RelayCommand(ExecuteDeleteStatementCommand);
            EditStatementCommand = new RelayCommand(ExecuteEditStatementCommand);
            DeleteOneBillCommand = new RelayCommand(ExecuteDeleteOneBillCommand);
            DeleteAllBillCommand = new RelayCommand(ExecuteDeleteAllBillCommand);
            InsertAllBillCommand = new RelayCommand(ExecuteInsertAllBillCommand);

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
        public RelayCommand InsertAllBillCommand { get; }

        public RelayCommand PreviousBillCommand { get; }
        public RelayCommand NextBillCommand { get; }

        public RelayCommand FirstBillCommand { get; }

        public RelayCommand LastBillCommand { get; }


        /// <summary>
        /// 增加訂單
        /// </summary>
        private void ExecuteAddNewBillCommand()
        {
            if (string.IsNullOrWhiteSpace(BillNumber))//沒有訂單號
                return;

            if (_billListDictionary.ContainsKey(BillNumber))//訂單重複 應該要跳提示
                return;

            var addedBill = new List<EssentialModel>();

            _billListDictionary.Add(BillNumber, addedBill);
            _billSequence.Add(BillNumber);

            _currentBillDisplay = new Tuple<string, List<EssentialModel>>(BillNumber, addedBill);

            RefreshDataGrid(addedBill);
            RaisePropertyChanged(() => TotalBillCount);
            RaisePropertyChanged(() => CanExecuteAddNewStatementCommand);
            SelectedBill = TotalBillCount;
            RaisePropertyChanged(() => SelectedBill);
        }
        /// <summary>
        /// 增加明細
        /// </summary>
        private void ExecuteAddNewStatementCommand()
        {
            //if (string.IsNullOrEmpty(BillNumber))
            //    return;

            if (_currentBillDisplay == null || _currentBillDisplay.Item1 == null)
                return;

            var billNumber = _currentBillDisplay.Item1;

            _billListDictionary.TryGetValue(billNumber, out var bill);

            if (bill == null)
                return;

            var numberInBill = bill.Count;
            bill.Add(new EssentialModel()
            {
                NumberInBill = numberInBill,
                ConsumptionNumber = billNumber,
                ConsumptionDate= Int32.Parse(ConsumptionDate.ToString("yyyyMMdd")),
                IsAssignDesigner =IsAssignDesigner,
                CustomerName = CustomerName,
                MembershipNumber = MembershipNumber,

                ConsumptionItem = SeletedOrderItem,
                UnitPrice = UnitPrice,
                Count = Count,
                PaymentType = PaymentType,
                Designer = SeletedDesigner,
                Assistant1 = SeletedAssistant1,
                Assistant2 = SeletedAssistant2,
                Assistant3 = SeletedAssistant3

            });

            RefreshDataGrid(bill);
        }
        public bool CanExecuteAddNewStatementCommand
        {
            get
            {
                if (_currentBillDisplay == null)
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// 刪除明細
        /// </summary>
        private void ExecuteDeleteStatementCommand()
        {
            if (SeletedStatement == null || SeletedStatement.Count == 0)
                return;

            _billListDictionary.TryGetValue(BillNumber, out var bill);

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
        public bool CanExecuteDeleteStatementCommand
        {
            get
            {
                if (SeletedStatement == null || SeletedStatement.Count == 0)
                    return false;
                else
                    return true;
            }
        }
        /// <summary>
        /// 編輯明細
        /// </summary>
        private void ExecuteEditStatementCommand()
        {
            if (SeletedStatement == null || SeletedStatement.Count != 1)
                return;

            _billListDictionary.TryGetValue(BillNumber, out var bill);

            bill.ForEach(s =>
            {
                if (s.NumberInBill == SeletedStatement[0].NumberInBill)
                {
                    s.ConsumptionNumber = BillNumber;
                    s.ConsumptionItem = SeletedOrderItem;
                    s.CustomerName = CustomerName;
                    s.MembershipNumber = MembershipNumber;
                    s.UnitPrice = UnitPrice;
                    s.Count = Count;
                    s.Designer = SeletedDesigner;
                    s.PaymentType = PaymentType;
                    s.Designer = SeletedDesigner;
                    s.Assistant1 = SeletedAssistant1;
                    s.Assistant2 = SeletedAssistant2;
                    s.Assistant3 = SeletedAssistant3;
                }
            });

            RefreshDataGrid(bill);
        }

        public bool CanExecuteEditStatementCommand
        {
            get
            {
                if (SeletedStatement == null || SeletedStatement.Count != 1)
                    return false;
                else
                    return true;
            }
        }
        /// <summary>
        /// 刪除單筆訂單
        /// </summary>
        private void ExecuteDeleteOneBillCommand_()
        {
            if (_currentBillDisplay == null || _currentBillDisplay.Item1 == null)
                return;

            var billNumber = _currentBillDisplay.Item1;

            _billListDictionary.Remove(billNumber);
            //顯示前一筆訂單資料如果沒有就清空(還未完成)
            var previousBillIndex = _billSequence.IndexOf(billNumber) - 1;
            _billSequence.Remove(billNumber);

            List<EssentialModel> previousBill = null;
            if (previousBillIndex >= 0)
            {
                var previousBillNumber = _billSequence[previousBillIndex];
                _billListDictionary.TryGetValue(previousBillNumber, out previousBill);

                _currentBillDisplay = new Tuple<string, List<EssentialModel>>(previousBillNumber, previousBill);
                SelectedBill = (uint)previousBillIndex + 1;
            }
            else
                _currentBillDisplay = null;


            RefreshDataGrid(previousBill);

            RaisePropertyChanged(() => CanExecuteAddNewStatementCommand);
            RaisePropertyChanged(() => TotalBillCount);
        }
        /// <summary>
        /// 刪除單筆訂單
        /// </summary>
        private void ExecuteDeleteOneBillCommand()
        {
            if (_currentBillDisplay == null || _currentBillDisplay.Item1 == null)
                return;

            List<EssentialModel> previousBill = null;
            var currentBillNumber = _currentBillDisplay.Item1;

            _billListDictionary.Remove(currentBillNumber);

            var currentBillIndex = _billSequence.IndexOf(currentBillNumber);
            _billSequence.Remove(currentBillNumber);

            if (currentBillIndex == 0 && _billSequence.Count == 0)//刪除第一筆後完全沒訂單
            {
                _currentBillDisplay = null;
                SelectedBill = 0;
            }
            else if (currentBillIndex >= 0 && _billSequence.Count != currentBillIndex)//刪除當前訂單 顯示後一筆訂單
            {
                var nextBillNumber = _billSequence[currentBillIndex];
                _billListDictionary.TryGetValue(nextBillNumber, out previousBill);
                _currentBillDisplay = new Tuple<string, List<EssentialModel>>(nextBillNumber, previousBill);
                SelectedBill = (uint)currentBillIndex + 1;
            }
            else if (currentBillIndex == _billSequence.Count)//刪除當前訂單(末筆) 因為後面沒訂單 所以顯示前一筆訂單
            {
                var previousBillNumber = _billSequence[currentBillIndex - 1];
                _billListDictionary.TryGetValue(previousBillNumber, out previousBill);
                _currentBillDisplay = new Tuple<string, List<EssentialModel>>(previousBillNumber, previousBill);
                SelectedBill = (uint)currentBillIndex;
            }

            RefreshDataGrid(previousBill);
            RaisePropertyChanged(() => CanExecuteAddNewStatementCommand);
            RaisePropertyChanged(() => TotalBillCount);
        }
        /// <summary>
        /// 刪除全部訂單
        /// </summary>
        private void ExecuteDeleteAllBillCommand()
        {
            _billListDictionary.Clear();
            _currentBillDisplay = null;
            _billSequence.Clear();
            BillDisplay.Clear();
            RaisePropertyChanged(() => TotalBillCount);
        }
        /// <summary>
        /// 入賬(寫到資料庫)
        /// </summary>
        private void ExecuteInsertAllBillCommand()
        {

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
            RaisePropertyChanged(() => CanExecuteEditStatementCommand);
            RaisePropertyChanged(() => CanExecuteDeleteStatementCommand);
        }

        private ICommand _selectionChangedCommand;

        private void Initialize()
        {
            BillDisplay = new ObservableCollection<EssentialModel>();
            BillNumber = "202208260001";
            //ConsumptionDate =Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));
            ConsumptionDate = DateTime.Now;
            CustomerName = "陳XX";
            MembershipNumber = "M124423";
            Count = 1;
            CashPay = true;
            AssistantList = new List<string>(){
                                        "",
                                    "Tom Chen",
                                    "Leo Liu",
                                    "Peter",
                                    "Wang Pi",
                                       };
        }
        /// <summary>
        /// 刷新顯示列表
        /// </summary>
        private void RefreshDataGrid(List<EssentialModel> bill)
        {
            BillDisplay.Clear();

            if (bill != null)
                bill.ForEach(statement => BillDisplay.Add(statement));
        }
    }
}
