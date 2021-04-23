using System;
using System.Collections.Generic;

#nullable disable

namespace SoftthaiIntranet.Models.Softthai_Intranet_2021
{
    public partial class T_Emp_Token
    {
        public int nEmpTokenID { get; set; }
        public int? nEmployeeID { get; set; }
        public string sDeviceToken { get; set; }
        public string sPlatform { get; set; }
        public string sSessionID { get; set; }
        public DateTime? dLastLogin { get; set; }
        public string cActive { get; set; }
    }
}
