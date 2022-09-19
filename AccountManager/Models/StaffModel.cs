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
        public string ID { set; get; }
        /// <summary>
        /// 系統登入密碼
        /// </summary>
        public string Password { set; get; }
        /// <summary>
        /// 職稱
        /// </summary>
        public string Position { set; get; }
        /// <summary>
        /// 到職日
        /// </summary>
        public string OnBoardDate { set; get; }
        /// <summary>
        /// 離職日
        /// </summary>
        public string ResignationDate { set; get; }
        /// <summary>
        /// 登錄日期時間
        /// </summary>
        public string RegisterDateTime { set; get; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public string ModifyDateTime { set; get; }
        /// <summary>
        /// 修改人帳號
        /// </summary>
        public string ModifyPerson { set; get; }

        /// <summary>
        /// 權限
        /// </summary>
        public string Authority { set; get; }
    }
}
