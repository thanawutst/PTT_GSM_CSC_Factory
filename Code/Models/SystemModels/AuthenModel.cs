using System;
using System.Collections.Generic;

namespace PTT_GSM_CSC_Factory.Models.SystemModels.AuthenModel
{

    public class CAppMenu : CMenu
    {
        public List<CMenu> lstSubMenu { get; set; }
    }
    public class CMenu
    {
        public bool isHeadMenu { get; set; }
        public string sPath { get; set; }
        public bool isExact { get; set; }
        public string sIcon { get; set; }
        public string sLabel { get; set; }
    }
}
