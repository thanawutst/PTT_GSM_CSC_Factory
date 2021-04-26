using System;
using System.Collections.Generic;

#nullable disable

namespace PTT_GSM_Gas_Quality.Models.FactoryDB
{
    public partial class TM_AD_Connection
    {
        public string ServerName { get; set; }
        public string Userdomain { get; set; }
        public string ldap { get; set; }
        public string Server { get; set; }
        public string DC { get; set; }
        public int? ORD { get; set; }
        public string SubDomain { get; set; }
    }
}
