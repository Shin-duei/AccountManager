using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AccountManager.ViewModels
{
    public class SalaryDataGridViewModel : ObservableRecipient
    {
        public SalaryDataGridViewModel()
        {

        }
        private string _name;
        /// <summary>
        /// 姓名
        /// </summary>
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
        private string _id;
        /// <summary>
        /// 員工編號
        /// </summary>
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
        private string _position;
        /// <summary>
        /// 職稱
        /// </summary>
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
        private int _assign;
        /// <summary>
        /// 指定
        /// </summary>
        public int Assign
        {
            get { return _assign; }
            set
            {
                if (_assign != value)
                {
                    _assign = value;
                    OnPropertyChanged(nameof(Assign));
                }
            }
        }
        private int _unassign;
        /// <summary>
        /// 不指定
        /// </summary>
        public int Unassign
        {
            get { return _unassign; }
            set
            {
                if (_unassign != value)
                {
                    _unassign = value;
                    OnPropertyChanged(nameof(Unassign));
                }
            }
        }

        private int _totalCustomer;
        /// <summary>
        /// 總客數
        /// </summary>
        public int TotalCustomer
        {
            get { return _totalCustomer; }
            set
            {
                if (_totalCustomer != value)
                {
                    _totalCustomer = value;
                    OnPropertyChanged(nameof(TotalCustomer));
                }
            }
        }
        private int _wash;
        /// <summary>
        /// 洗
        /// </summary>
        public int Wash
        {
            get { return _wash; }
            set
            {
                if (_wash != value)
                {
                    _wash = value;
                    OnPropertyChanged(nameof(Wash));
                }
            }
        }
        private int _cut;
        /// <summary>
        /// 剪
        /// </summary>
        public int Cut
        {
            get { return _cut; }
            set
            {
                if (_cut != value)
                {
                    _cut = value;
                    OnPropertyChanged(nameof(Cut));
                }
            }
        }
        private int _dye;
        /// <summary>
        /// 染
        /// </summary>
        public int Dye
        {
            get { return _dye; }
            set
            {
                if (_dye != value)
                {
                    _dye = value;
                    OnPropertyChanged(nameof(Dye));
                }
            }
        }
        private int _perm;
        /// <summary>
        /// 燙
        /// </summary>
        public int Perm
        {
            get { return _perm; }
            set
            {
                if (_perm != value)
                {
                    _perm = value;
                    OnPropertyChanged(nameof(Perm));
                }
            }
        }
        private int _protect;
        /// <summary>
        /// 護
        /// </summary>
        public int Protect
        {
            get { return _protect; }
            set
            {
                if (_protect != value)
                {
                    _protect = value;
                    OnPropertyChanged(nameof(Protect));
                }
            }
        }
        private int _product;
        /// <summary>
        /// 產品
        /// </summary>
        public int Product
        {
            get { return _product; }
            set
            {
                if (_product != value)
                {
                    _product = value;
                    OnPropertyChanged(nameof(Product));
                }
            }
        }
        private int _spa;
        /// <summary>
        ///頭皮SPA
        /// </summary>
        public int SPA
        {
            get { return _spa; }
            set
            {
                if (_spa != value)
                {
                    _spa = value;
                    OnPropertyChanged(nameof(SPA));
                }
            }
        }
        private int _storge;
        /// <summary>
        /// 儲值
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
        private int _totalSale;
        /// <summary>
        /// 總業績
        /// </summary>
        public int TotalSale
        {
            get { return _totalSale; }
            set
            {
                if (_totalSale != value)
                {
                    _totalSale = value;
                    OnPropertyChanged(nameof(TotalSale));
                }
            }
        }
        private int _percentCompleteSale;
        /// <summary>
        /// 業績分潤比例
        /// </summary>
        public int PercentCompleteSale
        {
            get { return _percentCompleteSale; }
            set
            {
                if (_percentCompleteSale != value)
                {
                    _percentCompleteSale = value;

                    OnPropertyChanged(nameof(PercentCompleteSale));
                    OnPropertyChanged(nameof(SaleProfit));
                    OnPropertyChanged(nameof(FinalSalary));
                }
            }
        }
        private int _percentCompleteProduct;
        /// <summary>
        /// 產品分潤比例
        /// </summary>
        public int PercentCompleteProduct
        {
            get { return _percentCompleteProduct; }
            set
            {
                if (_percentCompleteProduct != value)
                {
                    _percentCompleteProduct = value;

                    OnPropertyChanged(nameof(PercentCompleteProduct));
                    OnPropertyChanged(nameof(ProductProfit));
                    OnPropertyChanged(nameof(FinalSalary));
                }
            }
        }

        /// <summary>
        /// 業績分潤
        /// </summary>
        public double SaleProfit
        {
            get {          
                var  result=TotalSale* PercentCompleteSale*0.01;
                if (result % 0.5 == 0)
                    result += 0.1;
                return Math.Round(result); }
        }
        /// <summary>
        /// 產品分潤
        /// </summary>
        public double ProductProfit
        {
            get
            {
                var result = Product * PercentCompleteProduct * 0.01;
                if (result % 0.5 == 0)
                    result += 0.1;
                return Math.Round(result);
            }
        }
        /// <summary>
        /// 薪資
        /// </summary>
        public double FinalSalary
        {
            get { return SaleProfit + ProductProfit; }
        }
    }
}
