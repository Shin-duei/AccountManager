using AccountManager.Enums;
using AccountManager.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AccountManager.ViewModels
{
    public class SalaryViewModel : ObservableRecipient
    {
        public SQLiteHelper _sqliteHelper;

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
        private int _totalSale;
        /// <summary>
        /// 業績總額
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
        private int _totalProduct;
        /// <summary>
        /// 產品銷售總額
        /// </summary>
        public int TotalProduct
        {
            get { return _totalProduct; }
            set
            {
                if (_totalProduct != value)
                {
                    _totalProduct = value;
                    OnPropertyChanged(nameof(TotalProduct));
                }
            }
        }
        private int _totalSalary;
        /// <summary>
        /// 薪資支出總額
        /// </summary>
        public int TotalSalary
        {
            get { return _totalSalary; }
            set
            {
                if (_totalSalary != value)
                {
                    _totalSalary = value;
                    OnPropertyChanged(nameof(TotalSalary));
                }
            }
        }


        public SalaryViewModel()
        {
            SearchCommand = new RelayCommand(ExecuteSearchCommand);
            InsertToDBCommand = new RelayCommand(ExecuteInsertToDBCommand);
            _sqliteHelper = new SQLiteHelper();
            InitialDate = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
            FinalDate = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.AddMonths(1).Day);
        }
        public RelayCommand SearchCommand { get; }

        public RelayCommand InsertToDBCommand { get; }
        /// <summary>
        /// 執行查詢功能
        /// </summary>
        private void ExecuteSearchCommand()
        {
            PerformanceList.Clear();
            var initialDate = InitialDate.ToString("yyyyMMdd");
            var finalDate = FinalDate.ToString("yyyyMMdd");

            var staffList = _sqliteHelper.Query<StaffModel>("select * from Staff WHERE ResignationDate is NULL");//員工總表

            var allStatement = _sqliteHelper.Query<EssentialModel>($"select * from BillDetails WHERE ConsumptionDate BETWEEN '{initialDate}' AND '{finalDate}'");//消費明細

            var allStorgeHistory = _sqliteHelper.Query<StorgeValueModel>($"select * from StorgeValue WHERE Date BETWEEN '{initialDate}' AND '{finalDate}'");//儲值金

            DesignerSalary(staffList, allStatement, allStorgeHistory);
            AssistantSalary(staffList, allStatement);
            
            TotalSale = PerformanceList.Sum(s => s.SettlementSale)+ PerformanceList.Sum(s => s.Cooperation);
            TotalProduct = PerformanceList.Sum(s => s.Product);
            TotalSalary = (int)PerformanceList.Sum(s => s.FinalSalary);
        }
        /// <summary>
        /// 設計師薪資統計
        /// </summary>
        /// <param name="staffs">員工總表</param>
        /// <param name="statements">明細總表</param>
        /// <param name="allStorgeHistory">儲值金總表</param>
        private void DesignerSalary(List<StaffModel> staffs, List<EssentialModel> statements, List<StorgeValueModel> allStorgeHistory)
        {
            Dictionary<StaffModel, List<List<EssentialModel>>> everyDesignerBill = new Dictionary<StaffModel, List<List<EssentialModel>>>();//每個設計師員工的訂單
            Dictionary<StaffModel, int> designerCoorporation = new Dictionary<StaffModel, int>();//每個設計師員工的互助業績
            var allBill = statements.GroupBy(s => s.ConsumptionNumber).ToList();//按照訂單編號分類

            //設計師訂單統計
            foreach (var staff in staffs)
            {
                if (staff.Position != "設計師")
                    continue;

                var staffAllBill = new List<List<EssentialModel>>();
                foreach (var oneBill in allBill)
                {
                    var staffOneBill = oneBill.ToList().Select(s => s).Where(s => s.Designer == staff.ID).ToList();//指定設計師的所有訂單
                    if (staffOneBill != null && staffOneBill.Count != 0)
                        staffAllBill.Add(staffOneBill);
                }
                everyDesignerBill.Add(staff, staffAllBill);

                //設計師互助業績統計
                var coorporationStatement = statements.Select(s => s).Where(s => s.Assistant1 == staff.ID || s.Assistant2 == staff.ID || s.Assistant3 == staff.ID);
                int coorporationSale = 0;
                foreach (var statement in coorporationStatement)
                {
                    int coorporationNumber = 0;
                    if (!string.IsNullOrEmpty(statement.Assistant1))
                        coorporationNumber++;
                    if (!string.IsNullOrEmpty(statement.Assistant2))
                        coorporationNumber++;
                    if (!string.IsNullOrEmpty(statement.Assistant3))
                        coorporationNumber++;

                    if (coorporationNumber != 0)
                        coorporationSale += (int)(statement.TotalPrice * 0.1 / coorporationNumber);
                }
                designerCoorporation.Add(staff, coorporationSale);
            }

            //訂單內容統計(設計師)
            foreach (var staff in everyDesignerBill)
            {
                SalaryDataGridViewModel dataGridRow = new SalaryDataGridViewModel();
                int totalIncome = 0;
                int totalWash = 0;
                int totalCut = 0;
                int totalDye = 0;
                int totalPerm = 0;
                int totalProtect = 0;
                int totalExtension = 0;
                int totalSPA = 0;
                int totalUnassign = 0;
                int totalAssign = 0;
                int totalProductIncome = 0;
                int totalStorge = 0;
                int totalAssistanceFee = 0;

                if (allStorgeHistory != null && allStorgeHistory.Count > 0)//顯示經手的儲值金
                {
                    var totalStaffStorge = allStorgeHistory.Where(s => s.StaffID == staff.Key.ID).Select(s => s.ImportExport);

                    if (totalStaffStorge != null)
                        totalStorge = totalStaffStorge.Sum();
                }

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
                            else if (statement.ConsumptionItem == "接髮")
                                totalExtension++;
                            else if (statement.ConsumptionItem == "頭皮SPA")
                                totalSPA++;

                            if (string.IsNullOrEmpty(statement.Assistant1))//沒有助理
                                totalIncome += statement.TotalPrice;
                            else
                            {//有助理
                                totalIncome += (int)(statement.TotalPrice * 0.9);//扣一成助理費
                                totalAssistanceFee += (int)(statement.TotalPrice * 0.1);
                            }
                        }
                    }
                }

                dataGridRow.ID = staff.Key.ID;
                dataGridRow.Name = staff.Key.Name;
                dataGridRow.Alias = staff.Key.Alias;
                dataGridRow.Position = staff.Key.Position;
                dataGridRow.TotalCustomer = staff.Value.Count;
                dataGridRow.SettlementSale = totalIncome;
                dataGridRow.AssistanceFee = totalAssistanceFee;
                dataGridRow.Cooperation = designerCoorporation[staff.Key];
                dataGridRow.Wash = totalWash;
                dataGridRow.Cut = totalCut;
                dataGridRow.Dye = totalDye;
                dataGridRow.Perm = totalPerm;
                dataGridRow.Protect = totalProtect;
                dataGridRow.Extension = totalExtension;
                dataGridRow.SPA = totalSPA;
                dataGridRow.Product = totalProductIncome;
                dataGridRow.Storge = 0;
                dataGridRow.Unassign = totalUnassign;
                dataGridRow.Assign = totalAssign;
                dataGridRow.PercentCompleteSale = 50;
                dataGridRow.PercentCompleteProduct = 10;
                dataGridRow.Storge = totalStorge;
                PerformanceList.Add(dataGridRow);
            }
        }
        /// <summary>
        /// 助理薪資統計
        /// </summary>
        /// <param name="staffs">員工總表</param>
        /// <param name="statements">明細總表</param>
        private void AssistantSalary(List<StaffModel> staffs, List<EssentialModel> statements)
        {
            var assistants = staffs.Select(s => s).Where(s => s.Position == "助理");

            if (assistants == null || assistants.Count() == 0)
                return;


            foreach (var assistant in assistants)
            {
                SalaryDataGridViewModel dataGridRow = new SalaryDataGridViewModel();
                var assistantStatements = statements.Select(s => s).Where(s => s.Assistant1 == assistant.ID || s.Assistant2 == assistant.ID || s.Assistant3 == assistant.ID);
                
                int totalIncome = 0;
                int totalWash = 0;
                int totalCut = 0;
                int totalDye = 0;
                int totalPerm = 0;
                int totalProtect = 0;
                int totalExtension = 0;
                int totalSPA = 0;

                foreach (var assistantStatement in assistantStatements)
                {
                    int assistantNumber = 0;
                    if (!string.IsNullOrEmpty(assistantStatement.Assistant1))
                        assistantNumber++;
                    if (!string.IsNullOrEmpty(assistantStatement.Assistant2))
                        assistantNumber++;
                    if (!string.IsNullOrEmpty(assistantStatement.Assistant3))
                        assistantNumber++;

                    totalIncome += (int) (assistantStatement.TotalPrice * 0.1 / assistantNumber);

                    if (assistantStatement.ConsumptionItem == "洗髮")
                        totalWash++;
                    else if (assistantStatement.ConsumptionItem == "剪髮")
                        totalCut++;
                    else if (assistantStatement.ConsumptionItem == "染髮")
                        totalDye++;
                    else if (assistantStatement.ConsumptionItem == "燙髮")
                        totalPerm++;
                    else if (assistantStatement.ConsumptionItem == "護髮")
                        totalProtect++;
                    else if (assistantStatement.ConsumptionItem == "接髮")
                        totalExtension++;
                    else if (assistantStatement.ConsumptionItem == "頭皮SPA")
                        totalSPA++;

                }
                dataGridRow.ID = assistant.ID;
                dataGridRow.Name = assistant.Name;
                dataGridRow.Alias = assistant.Alias;
                dataGridRow.Position = assistant.Position;
                dataGridRow.TotalCustomer = assistantStatements.Count();//助理客數用一個工作項來計算
                dataGridRow.SettlementSale = totalIncome;
                dataGridRow.AssistanceFee = 0;
                dataGridRow.Cooperation = 0;
                dataGridRow.Wash = totalWash;
                dataGridRow.Cut = totalCut;
                dataGridRow.Dye = totalDye;
                dataGridRow.Perm = totalPerm;
                dataGridRow.Protect = totalProtect;
                dataGridRow.Extension = totalExtension;
                dataGridRow.SPA = totalSPA;
                dataGridRow.Product = 0;
                dataGridRow.Storge = 0;
                dataGridRow.Unassign = 0;
                dataGridRow.Assign = 0;
                dataGridRow.PercentCompleteSale = 100;
                dataGridRow.PercentCompleteProduct = 0;
                dataGridRow.Storge = 0;
                PerformanceList.Add(dataGridRow);
            }
        }
        /// <summary>
        /// 入賬
        /// </summary>
        private void ExecuteInsertToDBCommand()
        {
            var result = MessageBox.Show("是否將薪資支出登錄到收支列表?", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                IncomeExpenditureModel item = new IncomeExpenditureModel()
                {
                    Item = $"{InitialDate.ToString("MM")}月薪資",
                    Cost = TotalSalary,
                    Date = DateTime.Now.ToString("yyyyMMdd"),
                    ItemType = IncomeExpenditure.Expenditure,
                    LoginDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                _sqliteHelper.db.CreateTable<IncomeExpenditureModel>();//表已存在不會重覆創建
                _sqliteHelper.Add(item);
                MessageBox.Show("完成入賬!", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
