using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Models
{
   public  class BillBaseModel
    {
        /// <summary>
        /// 消費日期
        /// </summary>
        public int ConsumptionDate { set; get; }
        /// <summary>
        /// 消費單號(年月日xxxx)
        /// </summary>
        public string ConsumptionNumber { set; get; }
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
        /// 備註
        /// </summary>
        public string Remark { set; get; }

    }
}
