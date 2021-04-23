using System;
using System.Collections.Generic;

#nullable disable

namespace SoftthaiIntranet.Models.Softthai_Intranet_2021
{
    public partial class TM_ProjectType
    {
        public int nProjectTypeID { get; set; }
        public string sProjectTypeName { get; set; }
        public int? nOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
