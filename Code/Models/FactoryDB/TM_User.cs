using System;
using System.Collections.Generic;

#nullable disable

namespace PTT_GSM_Gas_Quality.Models.FactoryDB
{
    public partial class TM_User
    {
        public int nUserID { get; set; }
        public string sUserCode { get; set; }
        public int? nDistributorID { get; set; }
        public int? nFactoryID { get; set; }
        public int? nTitleNameID { get; set; }
        public string sFirstNameTH { get; set; }
        public string sSurNameTH { get; set; }
        public string sFirstNameEN { get; set; }
        public string sSurNameEN { get; set; }
        public string sPosition { get; set; }
        public string sEmail { get; set; }
        public string sTel { get; set; }
        public string sMobile { get; set; }
        public string sFax { get; set; }
        public int? nGroupID { get; set; }
        public int? nUserTypeID { get; set; }
        public string sUsername { get; set; }
        public string sPassword { get; set; }
        public string sPathFile { get; set; }
        public string sFileName { get; set; }
        public string sSysFileName { get; set; }
        public string cStatus { get; set; }
        public DateTime? dAdd { get; set; }
        public int? nAddBy { get; set; }
        public DateTime? dUpdate { get; set; }
        public int? nUpdateBy { get; set; }
        public DateTime? dLastLogin { get; set; }
        public string cDel { get; set; }
        public DateTime? dDelete { get; set; }
        public int? nDeleteBy { get; set; }
    }
}
