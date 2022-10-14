using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Models
{
    public class BusinessProductModel
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string Date { set; get; }
        /// <summary>
        /// 項目名稱
        /// </summary>
        public string Item { set; get; }
        /// <summary>
        /// 金額
        /// </summary>
        public int Cost { set; get; }
        /// <summary>
        /// 備註
        /// </summary>
        public string Remark { set; get; }
    }
}
