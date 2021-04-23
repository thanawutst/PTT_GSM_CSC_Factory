using System;
using System.Collections.Generic;

#nullable disable

namespace SoftthaiIntranet.Models.Softthai_Intranet_2021
{
    public partial class TM_CategoryKM
    {
        public int nCategoryKMID { get; set; }
        public string sCategoryName { get; set; }
        public string sDescription { get; set; }
        public decimal? nOrder { get; set; }
        public bool IsActive { get; set; }
        public bool IsDel { get; set; }
        public int nCreateBy { get; set; }
        public DateTime dCreate { get; set; }
        public int nUpdateBy { get; set; }
        public DateTime dUpdate { get; set; }
    }
}
