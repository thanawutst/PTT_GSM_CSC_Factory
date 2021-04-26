using System;
using System.Collections.Generic;

#nullable disable

namespace PTT_GSM_Gas_Quality.Models.FactoryDB
{
    public partial class TM_CMS
    {
        public int nMenuID { get; set; }
        public int? nHeadMenuID { get; set; }
        public int? nMenuTypeID { get; set; }
        public int? nOrder { get; set; }
        public string sMenuNameTH { get; set; }
        public string sMenuNameEN { get; set; }
        public string cNewTab { get; set; }
        public string sURL { get; set; }
        public string sShortContentTH { get; set; }
        public string sShortContentEN { get; set; }
        public string sFullContentTH { get; set; }
        public string sFullContentEN { get; set; }
        public string cStatus { get; set; }
        public string cPRMS { get; set; }
        public int? nAddBy { get; set; }
        public DateTime? dAddDate { get; set; }
        public int? nUpdateBy { get; set; }
        public DateTime? dUpdate { get; set; }
        public int? nDeleteBy { get; set; }
        public DateTime? dDelete { get; set; }
        public string cDel { get; set; }
    }
}
