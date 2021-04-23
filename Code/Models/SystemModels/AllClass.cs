using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftthaiIntranet.Models;

namespace SoftthaiIntranet.Models.SystemModels.AllClass
{
    public class AllClass
    {
        public class FilterReferencePrice
        {
            public string Type { get; set; }
            public string sType { get; set; }
            public string sYearID { get; set; }
            public int? nYearID { get; set; }
            public int? nType_Data { get; set; }
            public string sModeID { get; set; }
            public int? nDecimal { get; set; }
        }

        public class FilterRevenue
        {
            public string sType { get; set; }
            public int? nMode { get; set; }
            public int? nYearID { get; set; }
            public int? nDecimal { get; set; }
            public List<string> sType_R { get; set; }
            public List<string> sType_M { get; set; }
        }

        public class CResutlWebMethod
        {
            public string sStatus { get; set; }
            public string sMsg { get; set; }
            public string sUsername { get; set; }
            public string sContent { get; set; }
            public string lstError { get; set; }
        }

        public class Attahfile
        {
            public FileContentResult lstfile { get; set; }
            public string sName { get; set; }
        }

        public class FilterProductGroup
        {
            public string sSearch { get; set; }
            public string sStatus { get; set; }
            public string sProductGroupID { get; set; }
        }

        public class FilterCheck
        {
            public string sCheck { get; set; }
            public int? nID { get; set; }
        }

        public class FilterCheckProduct
        {
            public int? nProductID { get; set; }
            public int? nProductGroupID { get; set; }
            public string sProductCodeID { get; set; }
            public string sProductName { get; set; }
            public string sProductCode { get; set; }
            public int? nUnitID { get; set; }
        }

        public class FilterCustomer
        {
            public string sSearch { get; set; }
            public string sStatus { get; set; }
            public string sProductGroupID { get; set; }
            public string sProductID { get; set; }
        }

        public class FilterRollingPRISM
        {
            public string sType { get; set; }
            public string sYear { get; set; }
            public string nDecimal { get; set; }
            public int nMode { get; set; }
        }

        public class FilterGSPCostRolling
        {
            public string sYear { get; set; }
            public string nDecimal { get; set; }
        }

        public class FilterCheckCustomer
        {
            public int? nCustomerID { get; set; }
            public int? nProductID { get; set; }
            public string sCustomerName { get; set; }
        }
        public class UserSession
        {
            public string sTitle { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string nUserId { get; set; }
            public string sEmpCode { get; set; }
            public string sComCode { get; set; }
            public string nRole { get; set; }
            public string ExpireDay { get; set; }

        }

        public class Dropdown
        {
            public string label { get; set; }
            public string value { get; set; }
        }
    }


}
