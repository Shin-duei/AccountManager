using AccountManager.Common;
using AccountManager.Models;
using CommunityToolkit.Mvvm.Input;
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
        public ObservableCollection<EssentialModel> BillDisplayList { set; get; }
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

                    RaisePropertyChanged(() => _billNumber);
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
        private List<string> _assistantList = new List<string>()
        {
            "Tom Chen",
            "Leo Liu",
            "",
        };
        public List<string> AssistantList
        {
            get
            {
                return _assistantList;
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
            Initialize();
        }
        public RelayCommand AddNewBillCommand { get; }
        public RelayCommand AddNewStatementCommand { get; }
        public RelayCommand DeleteStatementCommand { get; }
        public RelayCommand EditStatementCommand { get; }
        public RelayCommand DeleteOneBillCommand { get; }
        public RelayCommand DeleteAllBillCommand { get; }
        /// <summary>
        /// 增加訂單
        /// </summary>
        private void ExecuteAddNewBillCommand()
        {
            if (string.IsNullOrWhiteSpace(BillNumber))//沒有訂單號
                return;

            if (_billListDictionary.ContainsKey(BillNumber))//訂單重複 應該要跳提示
                return;

            _billListDictionary.Add(BillNumber, new List<EssentialModel>());
            RaisePropertyChanged(() => TotalBillCount);
        }
        /// <summary>
        /// 增加明細
        /// </summary>
        private void ExecuteAddNewStatementCommand()
        {
            if (string.IsNullOrEmpty(BillNumber))
                return;

            _billListDictionary.TryGetValue(BillNumber, out var bill);

            if (bill == null)
                return;

            var numberInBill = bill.Count;
            bill.Add(new EssentialModel()
            {
                NumberInBill = numberInBill,
                ConsumptionNumber = BillNumber,
                ConsumptionItem = SeletedOrderItem,
                CustomerName = CustomerName,
                MembershipNumber = MembershipNumber,
                UnitPrice = UnitPrice,
                Count = Count,
                PaymentType = PaymentType,
                Designer = SeletedDesigner
            }); ;
            BillDisplayList.Clear();

            bill.ForEach(s => BillDisplayList.Add(s));
        }

        /// <summary>
        /// 刪除明細
        /// </summary>
        private void ExecuteDeleteStatementCommand()
        {
            if (string.IsNullOrEmpty(BillNumber))
                return;

            if (SeletedStatement.Count == 0)
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
            BillDisplayList.Clear();

            var billCount = bill.Count;
            int i = 0;
            foreach (var item in bill)
            {
                item.NumberInBill = i;
                i++;
                BillDisplayList.Add(item);
            }
        }
        private void ExecuteEditStatementCommand()
        {
            if (SeletedStatement.Count != 1)
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
                }
            });

            BillDisplayList.Clear();

            foreach (var item in bill)
            {
                BillDisplayList.Add(item);
            }


        }
        private void ExecuteDeleteOneBillCommand()
        {
            if (_billListDictionary.Count <= 0)
                return;

            _billListDictionary.Remove(BillNumber);
            RaisePropertyChanged(() => TotalBillCount);
        }
        private void ExecuteDeleteAllBillCommand()
        {
            _billListDictionary.Clear();
            RaisePropertyChanged(() => TotalBillCount);
        }
        public ICommand SelectionChangedCommand => _selectionChangedCommand ?? (_selectionChangedCommand = new RelayCommand<IList>(OnChanged));

        private void OnChanged(IList dataset)
        {
            SeletedStatement = dataset.OfType<EssentialModel>().ToList();
        }

        private ICommand _selectionChangedCommand;

        private void Initialize()
        {
            BillDisplayList = new ObservableCollection<EssentialModel>();
            BillNumber = "202208260001";
            CustomerName = "陳XX";
            MembershipNumber = "M124423";
            Count = 1;
            CashPay = true;
        }
    }
}
