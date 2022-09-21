using AccountManager.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Models
{
    public class InternalSystemModel
    {
        /// <summary>
        /// 消費明細系統
        /// </summary>
        public Authority OrderSystem { set; get; } = Authority.Non;
        /// <summary>
        /// 收支系統
        /// </summary>
        public Authority IncomeExpenditureSystem { set; get; } = Authority.Non;
        /// <summary>
        /// 儲值系統
        /// </summary>
        public Authority StorgeValueSystem { set; get; } = Authority.Non;
        /// <summary>
        /// 薪資系統
        /// </summary>
        public Authority SalarySystem { set; get; } = Authority.Non;
        /// <summary>
        /// 統計系統
        /// </summary>
        public Authority StatisticsSystem { set; get; } = Authority.Non;
        /// <summary>
        /// 員工系統
        /// </summary>
        public Authority StaffSystem { set; get; } = Authority.Non;
        /// <summary>
        /// 系統設定
        /// </summary>
        public Authority SettingSystem { set; get; } = Authority.Non;
    }
}
