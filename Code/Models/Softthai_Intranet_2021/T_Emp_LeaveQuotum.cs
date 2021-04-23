using System;
using System.Collections.Generic;

#nullable disable

namespace SoftthaiIntranet.Models.Softthai_Intranet_2021
{
    public partial class T_Emp_LeaveQuotum
    {
        public int nEmployeeID { get; set; }
        public int nLeaveID { get; set; }
        public int nYear { get; set; }
        public int? nQuota { get; set; }
        public int? nUsed { get; set; }
    }
}
