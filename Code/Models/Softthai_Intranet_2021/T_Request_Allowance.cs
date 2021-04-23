using System;
using System.Collections.Generic;

#nullable disable

namespace SoftthaiIntranet.Models.Softthai_Intranet_2021
{
    public partial class T_Request_Allowance
    {
        public T_Request_Allowance()
        {
            T_Request_Allowance_Flow = new HashSet<T_Request_Allowance_Flow>();
            T_Request_Allowance_Item = new HashSet<T_Request_Allowance_Item>();
        }

        public int nAllowanceID { get; set; }
        public int nProjectID { get; set; }
        public int? nLocationID { get; set; }
        public string sMeetingDetail { get; set; }
        public DateTime? dMeetingStart { get; set; }
        public DateTime? dMeetingEnd { get; set; }
        public int? nFlowProcessID { get; set; }
        public int? nStep { get; set; }
        public int? nCreate { get; set; }
        public DateTime? dCreate { get; set; }
        public int? nUpdate { get; set; }
        public DateTime? dUpdate { get; set; }
        public int? nDelete { get; set; }
        public DateTime? dDelete { get; set; }
        public bool IsDelete { get; set; }

        public virtual ICollection<T_Request_Allowance_Flow> T_Request_Allowance_Flow { get; set; }
        public virtual ICollection<T_Request_Allowance_Item> T_Request_Allowance_Item { get; set; }
    }
}
