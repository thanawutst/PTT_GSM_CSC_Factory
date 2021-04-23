using System;
using System.Collections.Generic;

#nullable disable

namespace SoftthaiIntranet.Models.Softthai_Intranet_2021
{
    public partial class TM_Condition_Config
    {
        public int nConfigID { get; set; }
        public string sDescription { get; set; }
        public int? nValue { get; set; }
        public string sValue { get; set; }
        public DateTime? dValue { get; set; }
        public int? nCreate { get; set; }
        public DateTime? dCreate { get; set; }
        public int? nUpdate { get; set; }
        public DateTime? dUpdate { get; set; }
        public bool IsActive { get; set; }
    }
}
