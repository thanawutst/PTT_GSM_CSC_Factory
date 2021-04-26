using System;
using System.Collections.Generic;

#nullable disable

namespace PTT_GSM_Gas_Quality.Models.FactoryDB
{
    public partial class TH_Subdistrict
    {
        public string sSubdistrictID { get; set; }
        public string sProvinceID { get; set; }
        public string sDistrictID { get; set; }
        public string sSubdistrictName { get; set; }
        public string cActive { get; set; }

        public virtual TH_District sDistrict { get; set; }
        public virtual TH_Province sProvince { get; set; }
    }
}
