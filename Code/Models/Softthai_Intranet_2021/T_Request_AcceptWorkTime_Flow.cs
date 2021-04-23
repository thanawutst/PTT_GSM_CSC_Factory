using System;
using System.Collections.Generic;

#nullable disable

namespace SoftthaiIntranet.Models.Softthai_Intranet_2021
{
    public partial class T_Request_AcceptWorkTime_Flow
    {
        public int nFlowID { get; set; }
        public int nAcceptWorkTimeID { get; set; }
        public int? nFlowProcessID { get; set; }
        public string sTypeFlow { get; set; }
        public int? nRoleID { get; set; }
        public int? nApproveID { get; set; }
        public string cApprove { get; set; }
        public DateTime? dApprove { get; set; }
        public string sComment { get; set; }
        public int? nStep { get; set; }
        public int? nCreate { get; set; }
        public DateTime? dCreate { get; set; }
        public int? nUpdate { get; set; }
        public DateTime? dUpdate { get; set; }
        public int? nDelete { get; set; }
        public DateTime? dDelete { get; set; }
        public bool IsDelete { get; set; }

        public virtual T_Request_AcceptWorkTime nAcceptWorkTime { get; set; }
    }
}
