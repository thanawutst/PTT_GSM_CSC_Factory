using System;
using System.Collections.Generic;

#nullable disable

namespace PTT_GSM_Gas_Quality.Models.FactoryDB
{
    public partial class TM_News
    {
        public int nNewsID { get; set; }
        public string sHeadTH { get; set; }
        public string sHeadEN { get; set; }
        public int? nOrder { get; set; }
        public string sLink { get; set; }
        public string sSource { get; set; }
        public DateTime? dStartDate { get; set; }
        public DateTime? dEndDate { get; set; }
        public string cEndless { get; set; }
        public int? nNewsType { get; set; }
        public string sShortDesTH { get; set; }
        public string sShortDesEN { get; set; }
        public string sDetailTH { get; set; }
        public string sDetailEN { get; set; }
        public string sThumbnailPathTH { get; set; }
        public string sSysFileThumbnailNameTH { get; set; }
        public string sFileThumbnailNameTH { get; set; }
        public string sSysThumbnailFileNameEN { get; set; }
        public string sFileThumbnailNameEN { get; set; }
        public string sFileImagePathTH { get; set; }
        public string sSysFileImageNameTH { get; set; }
        public string sFileImageNameTH { get; set; }
        public string sSysImageFileNameEN { get; set; }
        public string sFileImageNameEN { get; set; }
        public string cStatus { get; set; }
        public string cPin { get; set; }
        public DateTime? dAdd { get; set; }
        public int? nAddBy { get; set; }
        public DateTime? dUpdate { get; set; }
        public int? nUpdateBy { get; set; }
        public string cDel { get; set; }
    }
}
