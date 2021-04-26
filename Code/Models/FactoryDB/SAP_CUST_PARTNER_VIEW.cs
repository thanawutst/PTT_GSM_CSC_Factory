using System;
using System.Collections.Generic;

#nullable disable

namespace PTT_GSM_Gas_Quality.Models.FactoryDB
{
    public partial class SAP_CUST_PARTNER_VIEW
    {
        public string CUST_NUMBER { get; set; }
        public string CUST_NAME { get; set; }
        public string PARTNER_FUNC { get; set; }
        public string BUS_PART_NO { get; set; }
        public string SEARCH_TERM2 { get; set; }
        public DateTime? dUpdate { get; set; }
    }
}
