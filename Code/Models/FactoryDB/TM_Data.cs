using System;
using System.Collections.Generic;

#nullable disable

namespace PTT_GSM_CSC_Factory.Models.FactoryDB
{
    public partial class TM_Data
    {
        public int nDataID { get; set; }
        public int nTypeID { get; set; }
        public string sDataTH { get; set; }
        public string sDataEN { get; set; }
        public int? nOrder { get; set; }
        public string cRequired { get; set; }
        public string cActive { get; set; }
        public string cDel { get; set; }
        public DateTime? dUpdate { get; set; }
        public int? nUpdateBy { get; set; }
        public DateTime? dAdd { get; set; }
        public int? nAddBy { get; set; }
        public string cCanDelete { get; set; }
    }
}
