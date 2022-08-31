

using SQLite;
using System.Runtime.Serialization;

namespace AccountManager.Models
{

    /// <summary>
    /// 會員資料
    /// </summary>
    [Table("Customer")]//表單名稱
    public class CustomerModel
    {
        /// <summary>
        /// 主鍵
        /// </summary>
        [DataMember(Name = "主鍵")]
        [PrimaryKey]
        [AutoIncrement]
        public int PrimaryKey { set; get; }
        /// <summary>
        /// 會員姓名
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 會員編號
        /// </summary>
        public string ID { set; get; }
        /// <summary>
        /// 會員生日
        /// </summary>
        public string Birthday { set; get; }
        /// <summary>
        /// 聯絡電話
        /// </summary>
        public string PhoneNumber { set; get; }
        /// <summary>
        /// 當前儲值卡金額
        /// </summary>
        public int StoredValue { set; get; }
        /// <summary>
        /// 備註
        /// </summary>
        public string Remark { set; get; }
    }
}
