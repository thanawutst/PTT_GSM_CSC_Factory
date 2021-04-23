﻿using System;
using System.Collections.Generic;

#nullable disable

namespace SoftthaiIntranet.Models.Softthai_Intranet_2021
{
    public partial class TM_Floor
    {
        public int nFloorID { get; set; }
        public string sFloorName { get; set; }
        public int? nOrder { get; set; }
        public int? nCreate { get; set; }
        public DateTime? dCreate { get; set; }
        public int? nUpdate { get; set; }
        public DateTime? dUpdate { get; set; }
        public bool IsActive { get; set; }
        public int? nDelete { get; set; }
        public DateTime? dDelete { get; set; }
        public bool IsDelete { get; set; }
    }
}