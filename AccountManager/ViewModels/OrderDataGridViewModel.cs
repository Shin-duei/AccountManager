
using AccountManager.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AccountManager.ViewModels
{
    public class OrderDataGridViewModel : ObservableRecipient
    {
        public OrderDataGridViewModel(EssentialModel essentialModel)
        {
            NumberInBill = essentialModel.NumberInBill;
            ConsumptionNumber = essentialModel.ConsumptionNumber;
            ConsumptionItem = essentialModel.ConsumptionItem;
            Designer = essentialModel.Designer;
            Count = essentialModel.Count;
            MembershipNumber = essentialModel.MembershipNumber;
            CustomerName = essentialModel.CustomerName;
            UnitPrice = essentialModel.UnitPrice;
        }

        private int _numberInBill;
        /// <summary>
        /// 單中流水編號
        /// </summary>
        public int NumberInBill
        {
            get { return _numberInBill; }
            set
            {
                if (_numberInBill != value)
                {
                    _numberInBill = value;
                    OnPropertyChanged(nameof(NumberInBill));
                }
            }
        }

        private int _consumptionDate;
        public int ConsumptionDate
        {
            get { return _consumptionDate; }
            set
            {
                if (_consumptionDate != value)
                {
                    _consumptionDate = value;

                    OnPropertyChanged(nameof(ConsumptionDate));
                }
            }
        }

        private string _consumptionNumber;
        /// <summary>
        /// 消費單號(年月日xxxx)
        /// </summary>
        public string ConsumptionNumber
        {
            get { return _consumptionNumber; }
            set
            {
                if (_consumptionNumber != value)
                {
                    _consumptionNumber = value;

                    OnPropertyChanged(nameof(ConsumptionNumber));
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

        private string _customerName;
        /// <summary>
        /// 顧客姓名
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
        private string _consumptionItem;
        /// <summary>
        /// 消費項目
        /// </summary>
        public string ConsumptionItem
        {
            get { return _consumptionItem; }
            set
            {
                if (_consumptionItem != value)
                {
                    _consumptionItem = value;

                    OnPropertyChanged(nameof(ConsumptionItem));
                }
            }
        }

        private string _designer;
        /// <summary>
        /// 設計師
        /// </summary>
        public string Designer
        {
            get { return _designer; }
            set
            {
                if (_designer != value)
                {
                    _designer = value;

                    OnPropertyChanged(nameof(Designer));
                }
            }
        }
        private int _unitPrice;
        /// <summary>
        /// 單價
        /// </summary>
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
        /// <summary>
        /// 數量
        /// </summary>
        public int Count
        {
            get { return _count; }
            set
            {
                if (_count != value)
                {
                    _count = value;

                    OnPropertyChanged(nameof(Count));
                }
            }
        }
    }
}
