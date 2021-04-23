using System;
using System.Collections.Generic;

#nullable disable

namespace SoftthaiIntranet.Models.Softthai_Intranet_2021
{
    public partial class T_Leave
    {
        public int nLeaveID { get; set; }
        public int? nLeaveTypeID { get; set; }
        public DateTime dStart { get; set; }
        public DateTime dEnd { get; set; }
        public bool IsEmergency { get; set; }
        public string sRemark { get; set; }
        public int nLeaveHour { get; set; }
        public int? nApproverLevel1ID { get; set; }
        public int? nApproverLevel2ID { get; set; }
        public int nStatusID { get; set; }
        public bool IsDel { get; set; }
        public int nEvent { get; set; }
        public int nCreateBy { get; set; }
        public DateTime dCreate { get; set; }
        public int nUpdateBy { get; set; }
        public DateTime dUpdate { get; set; }
    }
}
