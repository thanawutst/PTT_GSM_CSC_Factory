using System;
using System.Collections.Generic;

#nullable disable

namespace SoftthaiIntranet.Models.Softthai_Intranet_2021
{
    public partial class T_Request_Travel_Item
    {
        public int nReqTravelID { get; set; }
        public int nItemNo { get; set; }
        public decimal? nPriceKilometer { get; set; }
        public int? nProjectID { get; set; }
        public int? nLocationFromID { get; set; }
        public int? nLocationToID { get; set; }
        public bool? IsRoundTrip { get; set; }
        public int? nTravelID { get; set; }
        public decimal? nDistance { get; set; }
        public decimal? nTollway { get; set; }
        public decimal? nAmount { get; set; }
        public DateTime? dTravelDate { get; set; }
    }
}
