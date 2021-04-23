using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftthaiIntranet.Models.Softthai_Intranet_2021;
using Newtonsoft.Json;
using SoftthaiIntranet.Models.SystemModels.AllClass;
using SoftthaiIntranet.Models;
using SoftthaiIntranet.Extensions;
namespace SoftthaiIntranet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        [HttpGet("GetFloor")]
        public ActionResult GetFloor()
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            var lst = DB.TM_Floor.Where(w => w.IsActive == true && w.IsDelete != true).OrderBy(o => o.nOrder).Select(s =>
            new lstddl
            {
                value = s.nFloorID + "",
                label = s.sFloorName
            }).ToList();
            return Ok(lst);
        }

        [HttpGet("GetDataTable")]
        public ActionResult GetDataTable()
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            var lst = DB.TM_MeetingRoom.Where(w => w.IsDelete != true).OrderByDescending(o => o.dUpdate).Select(s =>
            new lstData
            {
                nMeetingRoomID = s.nMeetingRoomID,
                nFloorID = s.nFloorID,
                sMeetingRoomName = s.sMeetingRoomName,
                sActive = s.IsActive ? "Active" : "Inactive",
                IsDelete = s.IsDelete
            }).ToList();
            return Ok(lst);
        }

        [HttpPost("SaveData")]
        public AllClass.CResutlWebMethod SaveData([FromBody] lstSaveData Obj)
        {
            AllClass.CResutlWebMethod Result = new AllClass.CResutlWebMethod();
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            try
            {
                #region Save Data to DB TM_MeetingRoom
                var UPT_MeetingRoom = DB.TM_MeetingRoom.FirstOrDefault(f => f.nMeetingRoomID == Obj.nMeetingRoomID);
                if (UPT_MeetingRoom != null)
                {
                    UPT_MeetingRoom.nFloorID = Systemfunction.ParseIntToZero(Obj.sFloorID);
                    UPT_MeetingRoom.sMeetingRoomName = Obj.sMeetingRoomName;
                    UPT_MeetingRoom.nUpdate = 1;
                    UPT_MeetingRoom.dUpdate = DateTime.Now;
                    UPT_MeetingRoom.IsActive = Obj.sActive == "1" ? true : false;
                    UPT_MeetingRoom.IsDelete = false;
                    DB.SaveChanges();
                }
                else
                {
                    TM_MeetingRoom CRT_MeetingRoom = new TM_MeetingRoom();
                    CRT_MeetingRoom.nFloorID = Systemfunction.ParseIntToZero(Obj.sFloorID);
                    CRT_MeetingRoom.sMeetingRoomName = Obj.sMeetingRoomName;
                    CRT_MeetingRoom.nCreate = 1;
                    CRT_MeetingRoom.dCreate = DateTime.Now;
                    CRT_MeetingRoom.nUpdate = 1;
                    CRT_MeetingRoom.dUpdate = DateTime.Now;
                    CRT_MeetingRoom.IsActive = Obj.sActive == "1" ? true : false;
                    CRT_MeetingRoom.IsDelete = false;
                    DB.TM_MeetingRoom.Add(CRT_MeetingRoom);
                    DB.SaveChanges();
                }
                Result.sStatus = Systemfunction.process_Success();
                #endregion
            }
            catch (Exception ex)
            {
                Result.sStatus = Systemfunction.process_Failed();
                Result.sMsg = ex.Message;
            }
            return Result;
        }

        [HttpPost("DeleteData")]
        public AllClass.CResutlWebMethod DeleteData([FromBody] List<lstData> lstData)
        {
            AllClass.CResutlWebMethod Result = new AllClass.CResutlWebMethod();
            try
            {
                #region Check จำนวนข้อมูลที่ทำการ Select
                if (lstData.Count > 0)
                {
                    foreach (var item in lstData)
                    {
                        Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
                        var lstDelete = DB.TM_MeetingRoom.FirstOrDefault(f => f.nMeetingRoomID == item.nMeetingRoomID);
                        if (lstDelete != null)
                        {
                            lstDelete.nDelete = 1;
                            lstDelete.dDelete = DateTime.Now;
                            lstDelete.IsDelete = true;
                            DB.SaveChanges();
                        }
                    }
                    Result.sStatus = "Success";
                }
                #endregion
            }
            catch (Exception ex)
            {
                Result.sMsg = ex.Message;
                Result.sStatus = "Failed";
            }
            return Result;
        }

        #region Class
        public class lstddl
        {
            public string label { get; set; }
            public string value { get; set; }
        }
        public class lstData
        {
            public int nMeetingRoomID { get; set; }
            public int nFloorID { get; set; }
            public string sMeetingRoomName { get; set; }
            public string sActive { get; set; }
            public bool IsDelete { get; set; }
        }
        public class lstSaveData
        {
            public int? nMeetingRoomID { get; set; }
            public string sFloorID { get; set; }
            public string sMeetingRoomName { get; set; }
            public string sActive { get; set; }
        }

      
        #endregion
    }
}
