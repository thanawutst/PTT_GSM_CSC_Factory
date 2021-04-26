using System;
using System.Collections.Generic;

#nullable disable

namespace PTT_GSM_Gas_Quality.Models.FactoryDB
{
    public partial class SAP_CUST_GENERAL_VIEW
    {
        public string CUST_NUMBER { get; set; }
        public string CUST_NAME { get; set; }
        public string HOUSE_NUMBER { get; set; }
        public string STREET4 { get; set; }
        public string DISTRICT { get; set; }
        public string DIFFERENT_CITY { get; set; }
        public string CITY { get; set; }
        public string POSTAL_CODE { get; set; }
        public string SEARCH_TERM2 { get; set; }
        public DateTime? dUpdate { get; set; }
    }
}
