using System;
using System.Collections.Generic;

#nullable disable

namespace SoftthaiIntranet.Models.Softthai_Intranet_2021
{
    public partial class TM_ProjectProgress
    {
        public int nProjectProgressID { get; set; }
        public string sProjectProgressName { get; set; }
        public int? nOrder { get; set; }
        public string cStatus { get; set; }
    }
}
