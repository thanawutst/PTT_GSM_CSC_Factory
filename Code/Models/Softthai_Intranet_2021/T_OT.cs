using System;
using System.Collections.Generic;

#nullable disable

namespace SoftthaiIntranet.Models.Softthai_Intranet_2021
{
    public partial class T_OT
    {
        public int nOTID { get; set; }
        public DateTime dDate { get; set; }
        public TimeSpan? tStart { get; set; }
        public TimeSpan? tEnd { get; set; }
        public decimal? nPlan { get; set; }
        public decimal? nActual { get; set; }
        public int? nProjectID { get; set; }
        public int? nTaskID { get; set; }
        public int? nTaskSubID { get; set; }
        public string sDetail { get; set; }
        public string sCause { get; set; }
        public int nStatusOTID { get; set; }
        public bool IsDel { get; set; }
        public int nCreateBy { get; set; }
        public DateTime dCreate { get; set; }
        public int nUpdateBy { get; set; }
        public DateTime dUpdate { get; set; }
        public int? nApproverLevel1ID { get; set; }
        public int? nApproverLevel2ID { get; set; }
    }
}
