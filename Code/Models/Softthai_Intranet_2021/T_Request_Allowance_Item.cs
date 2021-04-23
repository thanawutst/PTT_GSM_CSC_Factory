using System;
using System.Collections.Generic;

#nullable disable

namespace SoftthaiIntranet.Models.Softthai_Intranet_2021
{
    public partial class T_Request_Allowance_Item
    {
        public int nItemAllowanceID { get; set; }
        public int nAllowanceID { get; set; }
        public DateTime? dMeetingt { get; set; }
        public bool? IsRoundtrip { get; set; }
        public bool? IsOvernight { get; set; }
        public string sStartTime { get; set; }
        public string sEndTime { get; set; }
        public int? nActualMinute { get; set; }
        public int? nExpenseID { get; set; }
        public decimal? nAmount { get; set; }
        public int? nCreate { get; set; }
        public DateTime? dCreate { get; set; }
        public int? nUpdate { get; set; }
        public DateTime? dUpdate { get; set; }
        public int? nDelete { get; set; }
        public DateTime? dDelete { get; set; }
        public bool IsDelete { get; set; }

        public virtual T_Request_Allowance nAllowance { get; set; }
    }
}
