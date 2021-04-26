using System;
using System.Collections.Generic;

#nullable disable

namespace PTT_GSM_Gas_Quality.Models.FactoryDB
{
    public partial class SAP_CUST_SALE_VIEW
    {
        public string DIST_CHANNEL { get; set; }
        public string DIVISION { get; set; }
        public string SALES_GR { get; set; }
        public string SALES_OFFICE { get; set; }
        public string CUST_NUMBER { get; set; }
        public string CUST_NAME { get; set; }
        public string CUST_GR { get; set; }
        public string CUST_GR_DESC { get; set; }
        public string SALES_DISTRICT { get; set; }
        public string SALES_DISTRICT_DESC { get; set; }
        public DateTime? dUpdate { get; set; }
    }
}
