using System;
using System.Collections.Generic;

#nullable disable

namespace SoftthaiIntranet.Models.Softthai_Intranet_2021
{
    public partial class T_Request_Travel
    {
        public int nReqTravelID { get; set; }
        public DateTime? dRequest { get; set; }
        public int? nFlowProcessID { get; set; }
        public int? nStep { get; set; }
        public int? nCreate { get; set; }
        public DateTime? dCreate { get; set; }
        public int? nUpdate { get; set; }
        public DateTime? dUpdate { get; set; }
        public int? nDelete { get; set; }
        public DateTime? dDelete { get; set; }
        public bool IsDelete { get; set; }
    }
}
