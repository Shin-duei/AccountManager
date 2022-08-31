using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Models
{
    [Table("BillDetails")]//表單名稱
    public class EssentialModel
    {
        /// <summary>
        /// 主鍵
        /// </summary>
        [DataMember(Name = "主鍵")]
        [PrimaryKey]
        [AutoIncrement]
        public int PrimaryKey { set; get; }
        /// <summary>
        /// 消費日期
        /// </summary>
        public int ConsumptionDate { set; get; }
        /// <summary>
        /// 消費單號(年月日xxxx)
        /// </summary>
        public string ConsumptionNumber { set; get; }
        /// <summary>
        /// 紅單單號
        /// </summary>
        public string RedBill { set; get; }
        /// <summary>
        /// 指定設計師
        /// </summary>
        public bool IsAssignDesigner { set; get; }
      
        /// <summary>
        /// 顧客名稱
        /// </summary>
        public string CustomerName { set; get; }
        /// <summary>
        /// 會員編號
        /// </summary>
        public string MembershipNumber { set; get; }

        /// <summary>
        /// 單中流水編號
        /// </summary>
        public int NumberInBill { set; get; }
        /// <summary>
        /// 消費項目
        /// </summary>
        public string ConsumptionItem { set; get; }
        /// <summary>
        /// 設計師
        /// </summary>
        public string Designer { set; get; }
        /// <summary>
        /// 單價
        /// </summary>
        public int UnitPrice { set; get; }
        /// <summary>
        /// 數量
        /// </summary>
        public int Count { set; get; }
        /// <summary>
        /// 總額
        /// </summary>
        public int TotalPrice { get { return UnitPrice * Count; } }
        /// <summary>
        /// 消費別
        /// </summary>
        public string PaymentType { set; get; }
        /// <summary>
        /// 助理1
        /// </summary>
        public string Assistant1{ set; get; }
        /// <summary>
        /// 助理2
        /// </summary>
        public string Assistant2 { set; get; }
        /// <summary>
        /// 助理3
        /// </summary>
        public string Assistant3 { set; get; }
        /// <summary>
        /// 備註
        /// </summary>
        public string Remark { set; get; }
        /// <summary>
        /// 登錄日期時間
        /// </summary>
        public int LoginDateTime { set; get; }
        /// <summary>
        /// 登錄者帳號
        /// </summary>
        public string LoginID { set; get; }
    }
}
