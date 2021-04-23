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
    public class DestinationController : ControllerBase
    {
        [HttpGet("GetDataTable")]
        public ActionResult GetDataTable()
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            var lst = DB.TM_Destination.Where(w => w.IsDelete != true).OrderByDescending(o => o.dUpdate).Select(s =>
            new lstData
            {
                nDestinationID = s.nDestinationID,
                sDestinationName = s.sDestinationName,
                // nOrder = s.nOrder,
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
                var UPT_Destination = DB.TM_Destination.FirstOrDefault(f => f.nDestinationID == Obj.nDestinationID);
                if (UPT_Destination != null)
                {
                    UPT_Destination.sDestinationName = Obj.sDestinationName;
                    UPT_Destination.nUpdate = 1;
                    UPT_Destination.dUpdate = DateTime.Now;
                    UPT_Destination.IsActive = Obj.sActive == "1" ? true : false;
                    UPT_Destination.IsDelete = false;
                    DB.SaveChanges();
                }
                else
                {
                    TM_Destination CRT_Destination = new TM_Destination();
                    CRT_Destination.sDestinationName = Obj.sDestinationName;
                    var MaxOrder = DB.TM_Destination.Max(m => m.nOrder);
                    if (MaxOrder.HasValue)
                    {
                        CRT_Destination.nOrder = MaxOrder + 1;
                    }
                    CRT_Destination.nCreate = 1;
                    CRT_Destination.dCreate = DateTime.Now;
                    CRT_Destination.nUpdate = 1;
                    CRT_Destination.dUpdate = DateTime.Now;
                    CRT_Destination.IsActive = Obj.sActive == "1" ? true : false;
                    CRT_Destination.IsDelete = false;
                    DB.TM_Destination.Add(CRT_Destination);
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
                        var lstDelete = DB.TM_Destination.FirstOrDefault(f => f.nDestinationID == item.nDestinationID);
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
            public int nDestinationID { get; set; }
            public string sDestinationName { get; set; }
            public int nOrder { get; set; }
            public string sActive { get; set; }
            public bool IsDelete { get; set; }
        }
        public class lstSaveData
        {
            public int? nDestinationID { get; set; }
            public string sDestinationName { get; set; }
            public string sActive { get; set; }
        }
        #endregion
    }
}
