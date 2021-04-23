using System;
using System.Collections.Generic;

#nullable disable

namespace SoftthaiIntranet.Models.Softthai_Intranet_2021
{
    public partial class T_Employee
    {
        public int nEmployeeID { get; set; }
        public string sEmployeeCode { get; set; }
        public string sUsername { get; set; }
        public string sPassword { get; set; }
        public string sFirstname { get; set; }
        public string sLastname { get; set; }
        public string sNickname { get; set; }
        public bool? IsHR { get; set; }
        public int? nPositionID { get; set; }
        public DateTime? dStartWork { get; set; }
        public string sEmail { get; set; }
        public string sLineID { get; set; }
        public string sMobile { get; set; }
        public string sAddress { get; set; }
        public string sContactName { get; set; }
        public string sContactMobile { get; set; }
        public string sPath { get; set; }
        public string sFileName { get; set; }
        public string sSysFileName { get; set; }
        public int? nApproverLevel1ID { get; set; }
        public bool IsActive { get; set; }
        public bool IsDel { get; set; }
        public int? nCreateBy { get; set; }
        public DateTime? dCreate { get; set; }
        public int? nUpdateBy { get; set; }
        public DateTime? dUpdate { get; set; }
        public DateTime? dDisplacement { get; set; }
        public int? nRoleID { get; set; }
        public string sGender { get; set; }
    }
}
