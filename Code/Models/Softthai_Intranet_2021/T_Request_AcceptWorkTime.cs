using System;
using System.Collections.Generic;

#nullable disable

namespace SoftthaiIntranet.Models.Softthai_Intranet_2021
{
    public partial class T_Request_AcceptWorkTime
    {
        public T_Request_AcceptWorkTime()
        {
            T_Request_AcceptWorkTime_Flow = new HashSet<T_Request_AcceptWorkTime_Flow>();
        }

        public int nAcceptWorkTimeID { get; set; }
        public int? nProjectID { get; set; }
        public DateTime? dStartOverTime { get; set; }
        public string sStartTime { get; set; }
        public DateTime? dEndOverTime { get; set; }
        public string sEndTime { get; set; }
        public int? nActualMinute { get; set; }
        public int? nAwtConditionID { get; set; }
        public int? nExpenseID { get; set; }
        public string sDetailOfWork { get; set; }
        public bool? IsCertification { get; set; }
        public DateTime? dCertification { get; set; }
        public int? nFlowProcessID { get; set; }
        public int? nStep { get; set; }
        public DateTime? dRequest { get; set; }
        public int? nCreate { get; set; }
        public DateTime? dCreate { get; set; }
        public int? nUpdate { get; set; }
        public DateTime? dUpdate { get; set; }
        public bool IsActive { get; set; }
        public int? nDelete { get; set; }
        public DateTime? dDelete { get; set; }
        public bool IsDelete { get; set; }

        public virtual ICollection<T_Request_AcceptWorkTime_Flow> T_Request_AcceptWorkTime_Flow { get; set; }
    }
}
