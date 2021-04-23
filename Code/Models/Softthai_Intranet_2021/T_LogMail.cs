using System;
using System.Collections.Generic;

#nullable disable

namespace SoftthaiIntranet.Models.Softthai_Intranet_2021
{
    public partial class T_LogMail
    {
        public int nLogMailID { get; set; }
        public int? nMenuID { get; set; }
        public string sDescription { get; set; }
        public string sFrom { get; set; }
        public string sTo { get; set; }
        public string sCC { get; set; }
        public string sBCC { get; set; }
        public string sSubject { get; set; }
        public string sContent { get; set; }
        public string sStatus { get; set; }
        public string sErrorMessage { get; set; }
        public DateTime? dSend { get; set; }
    }
}
