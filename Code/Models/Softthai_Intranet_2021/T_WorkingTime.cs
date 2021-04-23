using System;
using System.Collections.Generic;

#nullable disable

namespace SoftthaiIntranet.Models.Softthai_Intranet_2021
{
    public partial class T_WorkingTime
    {
        public int nAttendID { get; set; }
        public int? nEmployeeID { get; set; }
        public string sEmployeeCode { get; set; }
        public DateTime? dDateWork { get; set; }
        public DateTime? dSignIn { get; set; }
        public DateTime? dSignOut { get; set; }
        public int? nMinuteDelay { get; set; }
        public int? nLeaveID { get; set; }
        public string sRemark { get; set; }
        public string nAttendType { get; set; }
    }
}
