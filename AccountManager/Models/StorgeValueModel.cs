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
     /// 儲值紀錄
     /// </summary>
    [Table("StorgeValue")]//表單名稱
    public class StorgeValueModel
    {
        /// <summary>
        /// 主鍵
        /// </summary>
        [DataMember(Name = "主鍵")]
        [PrimaryKey]
        [AutoIncrement]
        public int PrimaryKey { set; get; }
        /// <summary>
        /// 日期
        /// </summary>
        public int Date { set; get; }
        /// <summary>
        /// 出入金額
        /// </summary>
        public int ImportExport { set; get; }
        /// <summary>
        /// 會員姓名
        /// </summary>
        public string CustomerName { set; get; }
        /// <summary>
        /// 會員編號
        /// </summary>
        public string CustomerID { set; get; }
        /// <summary>
        /// 員工編號
        /// </summary>
        public string StaffID { set; get; }
        /// <summary>
        /// 操作時間
        /// </summary>
        public string WorkDataTime { set; get; }
    }
}
