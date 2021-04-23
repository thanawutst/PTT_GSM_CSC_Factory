using System;
using System.Collections.Generic;

#nullable disable

namespace SoftthaiIntranet.Models.Softthai_Intranet_2021
{
    public partial class T_ReserveMeetingRoom
    {
        public int nReserveID { get; set; }
        public int? nMeetingRoomID { get; set; }
        public string sMeetingDetail { get; set; }
        public DateTime? dDateMeeting { get; set; }
        public bool? IsAllDay { get; set; }
        public string sStartTime { get; set; }
        public string sEndTime { get; set; }
        public int? nCreate { get; set; }
        public DateTime? dCreate { get; set; }
        public int? nUpdate { get; set; }
        public DateTime? dUpdate { get; set; }
        public int? nDelete { get; set; }
        public DateTime? dDelete { get; set; }
        public bool? IsDelete { get; set; }
    }
}
