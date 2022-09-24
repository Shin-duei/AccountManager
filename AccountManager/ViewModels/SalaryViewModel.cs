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
    public class SalaryViewModel : ObservableRecipient
    {
        private SQLiteHelper _sqliteHelper;

        /// <summary>
        /// 顯示用列表
        /// </summary>
        public ObservableCollection<SalaryDataGridViewModel> PerformanceList { set; get; } = new ObservableCollection<SalaryDataGridViewModel>();

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

        
        public SalaryViewModel()
        {
            SearchCommand = new RelayCommand(ExecuteSearchCommand);
            _sqliteHelper = new SQLiteHelper();
            InitialDate = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
            FinalDate = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.AddMonths(1).Day);
        }
        public RelayCommand SearchCommand { get; }

        private void ExecuteSearchCommand()
        {
            PerformanceList.Clear();
            var initialDate = InitialDate.ToString("yyyyMMdd");
            var finalDate = FinalDate.ToString("yyyyMMdd");

            var staffList = _sqliteHelper.Query<StaffModel>("select * from Staff WHERE ResignationDate is NULL");//員工總表

            var allStatement = _sqliteHelper.Query<EssentialModel>($"select * from BillDetails WHERE ConsumptionDate BETWEEN '{initialDate}' AND '{finalDate}'");//消費明細

            var allBill = allStatement.GroupBy(s => s.ConsumptionNumber).ToList();//按照訂單編號分類

            Dictionary<StaffModel, List<List<EssentialModel>>> everyStaffBill = new Dictionary<StaffModel, List<List<EssentialModel>>>();//每個員工的訂單
            foreach (var staff in staffList)
            {
                var staffAllBill = new List<List<EssentialModel>>();
                foreach (var oneBill in allBill)
                {
                    var staffOneBill = oneBill.ToList().Select(s => s).Where(s => s.Designer == staff.ID).ToList();

                    if (staffOneBill != null && staffOneBill.Count != 0)
                        staffAllBill.Add(staffOneBill);
                }
                everyStaffBill.Add(staff, staffAllBill);
            }

            //訂單內容統計
            foreach (var staff in everyStaffBill)
            {
                SalaryDataGridViewModel dataGridRow = new SalaryDataGridViewModel();
                int totalIncome = 0;
                int totalWash = 0;
                int totalCut = 0;
                int totalDye = 0;
                int totalPerm = 0;
                int totalProtect = 0;
                int totalSPA = 0;
                int totalUnassign = 0;
                int totalAssign = 0;

                int totalStorge = 0;
                int totalProductIncome = 0;

                foreach (var bill in staff.Value)
                {
                    if (bill.Any(s => s.IsAssignDesigner))
                        totalAssign++;
                    else
                        totalUnassign++;

                    foreach (var statement in bill)
                    {
                        if (statement.ConsumptionItem == "產品購買")
                        {
                            totalProductIncome += statement.TotalPrice;
                        }
                        else
                        {
                            if (statement.ConsumptionItem == "洗髮")
                                totalWash++;
                            else if (statement.ConsumptionItem == "剪髮")
                                totalCut++;
                            else if (statement.ConsumptionItem == "染髮")
                                totalDye++;
                            else if (statement.ConsumptionItem == "燙髮")
                                totalPerm++;
                            else if (statement.ConsumptionItem == "護髮")
                                totalProtect++;
                            else if (statement.ConsumptionItem == "頭皮SPA")
                                totalSPA++;

                            totalIncome += statement.TotalPrice;
                        }
                    }
                }

                dataGridRow.ID = staff.Key.ID;
                dataGridRow.Name = staff.Key.Name;
                dataGridRow.Position = staff.Key.Position;
                dataGridRow.TotalCustomer = staff.Value.Count;
                dataGridRow.TotalSale = totalIncome;
                dataGridRow.Wash = totalWash;
                dataGridRow.Cut = totalCut;
                dataGridRow.Dye = totalDye;
                dataGridRow.Perm = totalPerm;
                dataGridRow.Protect = totalProtect;
                dataGridRow.SPA=totalSPA;
                dataGridRow.Product = totalProductIncome;
                dataGridRow.Storge = 0;
                dataGridRow.Unassign = totalUnassign;
                dataGridRow.Assign = totalAssign;
                dataGridRow.PercentCompleteSale = 50;
                dataGridRow.PercentCompleteProduct = 10;
                PerformanceList.Add(dataGridRow);
            }
        }
    }
}
