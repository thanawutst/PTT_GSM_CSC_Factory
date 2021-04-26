using System;
using System.Collections.Generic;

#nullable disable

namespace PTT_GSM_Gas_Quality.Models.FactoryDB
{
    public partial class TM_ContactUs
    {
        public int nID { get; set; }
        public string sSubject { get; set; }
        public string sUserName { get; set; }
        public string sEmail { get; set; }
        public string sTel { get; set; }
        public string sAdminResponse { get; set; }
        public string sUserDetail { get; set; }
        public string sSysFileNameAdmin { get; set; }
        public string sFileNameAdmin { get; set; }
        public string sSysFilePathAdmin { get; set; }
        public DateTime? dAdd { get; set; }
        public DateTime? dUpdateBy { get; set; }
        public int? nUpdateBy { get; set; }
        public string cReplied { get; set; }
    }
}
