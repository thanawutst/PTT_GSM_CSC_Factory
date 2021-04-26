using System;
using System.Collections.Generic;

#nullable disable

namespace PTT_GSM_Gas_Quality.Models.FactoryDB
{
    public partial class TH_Province
    {
        public TH_Province()
        {
            TH_Subdistrict = new HashSet<TH_Subdistrict>();
        }

        public string sProvinceID { get; set; }
        public string sProvinceCode { get; set; }
        public string sProvinceName { get; set; }
        public string cActive { get; set; }

        public virtual ICollection<TH_Subdistrict> TH_Subdistrict { get; set; }
    }
}
