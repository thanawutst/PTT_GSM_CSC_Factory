using System;
using System.Collections.Generic;

#nullable disable

namespace PTT_GSM_CSC_Factory.Models.FactoryDB
{
    public partial class TM_FactoryProduct
    {
        public int nID { get; set; }
        public int nDistributorID { get; set; }
        public int nFactoryID { get; set; }
        public string sMaterialCode { get; set; }
        public string cStatus { get; set; }
        public DateTime? dUpdate { get; set; }
        public int? nUpdateBy { get; set; }
        public DateTime? dAdd { get; set; }
        public int? nAddBy { get; set; }
        public string cDel { get; set; }
    }
}
