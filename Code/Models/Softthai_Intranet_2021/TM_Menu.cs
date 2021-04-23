using System;
using System.Collections.Generic;

#nullable disable

namespace SoftthaiIntranet.Models.Softthai_Intranet_2021
{
    public partial class TM_Menu
    {
        public int nMenuID { get; set; }
        public int? nMenuHeadID { get; set; }
        public string sMenuName { get; set; }
        public string sIcon { get; set; }
        public int? nLevel { get; set; }
        public string sRounter { get; set; }
        public int? nOrder { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsPRMS { get; set; }
    }
}
