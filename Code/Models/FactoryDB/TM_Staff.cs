using System;
using System.Collections.Generic;

#nullable disable

namespace PTT_GSM_CSC_Factory.Models.FactoryDB
{
    public partial class TM_Staff
    {
        public int? nStaffID { get; set; }
        public int? nDistributorID { get; set; }
        public int? nFactoryID { get; set; }
        public int? nStaffLevel { get; set; }
        public string sAvatarName { get; set; }
        public string sAvatarPath { get; set; }
        public string sAvatarSysFileName { get; set; }
        public int? nTitleID { get; set; }
        public string sFirstname { get; set; }
        public string sSurName { get; set; }
        public string sUserName { get; set; }
        public string sPassword { get; set; }
        public string sEmail { get; set; }
        public bool? cStatus { get; set; }
    }
}
