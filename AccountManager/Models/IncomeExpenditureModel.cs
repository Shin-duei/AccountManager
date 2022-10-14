using AccountManager.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Models
{
    /// <summary>
    /// 員工資料
    /// </summary>
    [Table("IncomeExpenditure")]//表單名稱
    public class IncomeExpenditureModel
    {
        /// <summary>
        /// 主鍵
        /// </summary>
        [DataMember(Name = "主鍵")]
        [PrimaryKey]
        [AutoIncrement]
        public int PrimaryKey { set; get; }
        /// <summary>
        /// 登錄日期
        /// </summary>
        public string Date{ set; get; }
        /// <summary>
        /// 收入或支出
        /// </summary>
        public IncomeExpenditure ItemType { set; get; }
        /// <summary>
        /// 項目名稱
        /// </summary>
        public string Item { set; get; }
        /// <summary>
        /// 金額
        /// </summary>
        public int Cost { set; get; }
        /// <summary>
        /// 登錄日期時間
        /// </summary>
        public string LoginDateTime { set; get; }
        /// <summary>
        /// 登錄者帳號
        /// </summary>
        public string LoginID { set; get; }
        /// <summary>
        /// 備註
        /// </summary>
        public string Remark { set; get; }
    }
}
