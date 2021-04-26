using System;
using System.Collections.Generic;

#nullable disable

namespace PTT_GSM_CSC_Factory.Models.FactoryDB
{
    public partial class TH_District
    {
        public TH_District()
        {
            TH_Subdistrict = new HashSet<TH_Subdistrict>();
        }

        public string sDistrictID { get; set; }
        public string sDistrictName { get; set; }
        public string cActive { get; set; }
        public string sProvinceID { get; set; }

        public virtual ICollection<TH_Subdistrict> TH_Subdistrict { get; set; }
    }
}
