using System;
using System.Collections.Generic;

#nullable disable

namespace PTT_GSM_CSC_Factory.Models.FactoryDB
{
    public partial class TM_Distributor
    {
        public int nDistributorID { get; set; }
        public string cPTT { get; set; }
        public string sComapanyCode { get; set; }
        public int? nTitleID { get; set; }
        public string sTitleName { get; set; }
        public string sDistributorCode { get; set; }
        public string sDistributorName { get; set; }
        public string sCode { get; set; }
        public int? nHeadDistributor { get; set; }
        public string nCustomerGroupID { get; set; }
        public string sEmail { get; set; }
        public string sABDistributorName { get; set; }
        public string sWebsite { get; set; }
        public string sSoldTo { get; set; }
        public string sAddressSoldTo { get; set; }
        public string sLongitude { get; set; }
        public string sLatitude { get; set; }
        public string sMap { get; set; }
        public string sShipTo { get; set; }
        public string sAddressShipTo { get; set; }
        public string sDistrictDesc { get; set; }
        public string sMeterName { get; set; }
        public string sDistrict { get; set; }
        public string sSysLogoName { get; set; }
        public string sLogoName { get; set; }
        public string sLogoPath { get; set; }
        public string sSysOrgChartName { get; set; }
        public string sOrgChartName { get; set; }
        public string sOrgChartPath { get; set; }
        public string sDetail { get; set; }
        public int? nOrder { get; set; }
        public string cStatus { get; set; }
        public DateTime? dUpdate { get; set; }
        public int? nUpdateBy { get; set; }
        public DateTime? dAdd { get; set; }
        public int? nAddBy { get; set; }
        public string cDel { get; set; }
    }
}
