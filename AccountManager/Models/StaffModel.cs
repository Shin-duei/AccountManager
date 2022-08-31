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
    [Table("Staff")]//表單名稱
    public class StaffModel
    {
        /// <summary>
        /// 主鍵
        /// </summary>
        [DataMember(Name = "主鍵")]
        [PrimaryKey]
        [AutoIncrement]
        public int PrimaryKey { set; get; }

        /// <summary>
        /// 員工姓名
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 別名
        /// </summary>
        public string Alias { set; get; }
        /// <summary>
        /// 員工編號
        /// </summary>
        public uint ID { set; get; }
        /// <summary>
        /// 系統登入密碼
        /// </summary>
        public string Password { set; get; }
        /// <summary>
        /// 職稱
        /// </summary>
        public string Position { set; get; }
    }
}
