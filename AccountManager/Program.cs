using AccountManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AccountManager
{
    class Program
    {
        [STAThread]
        public static void Main()
        {
            SQLiteHelper sqliteHelper = new SQLiteHelper();
            //var list = sqliteHelper.Query<EssentialModel>("select * from Stock");
            //sqliteHelper.Add(new Valuation() { Price = 100, StockId = 1, Time = DateTime.Now });
            //var list2 = sqliteHelper.Query<Valuation>("select *  from Valuation");
            //var list3 = sqliteHelper.Query<Valuation>("select *   from Valuation INDEXED BY ValuationStockId2 WHERE StockId > 2");//使用索引ValuationStockId2查詢
            try
            {
                sqliteHelper.Execute("drop index ValuationStockId");//刪除索引，因為該索引已被刪除，所以拋異常
            }
            catch (Exception ex)
            {

            }
        }
    }
}
