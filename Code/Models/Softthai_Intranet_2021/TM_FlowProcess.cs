using System;
using System.Collections.Generic;

#nullable disable

namespace SoftthaiIntranet.Models.Softthai_Intranet_2021
{
    public partial class TM_FlowProcess
    {
        public int nFlowProcessID { get; set; }
        public string sFlowProcessName { get; set; }
        public int? nOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
