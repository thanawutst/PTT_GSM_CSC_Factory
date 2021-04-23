using System;
using System.Collections.Generic;

#nullable disable

namespace SoftthaiIntranet.Models.Softthai_Intranet_2021
{
    public partial class TM_Condition_AcceptWorkTime
    {
        public int nAwtConditionID { get; set; }
        public string sAwtConditionName { get; set; }
        public string sCertificateTitle { get; set; }
        public int? nOverHour { get; set; }
        public int? nOverMinute { get; set; }
        public int? nCertificateHour { get; set; }
        public int? nCertificateMinute { get; set; }
        public int? nConditionOverMinute { get; set; }
        public int? nConditionCertificateMinute { get; set; }
        public int? nOrder { get; set; }
        public int? nCreate { get; set; }
        public DateTime? dCreate { get; set; }
        public int? nUpdate { get; set; }
        public DateTime? dUpdate { get; set; }
        public bool IsActive { get; set; }
        public int? nDelete { get; set; }
        public DateTime? dDelete { get; set; }
        public bool IsDelete { get; set; }
        public int? nVersion { get; set; }
    }
}
