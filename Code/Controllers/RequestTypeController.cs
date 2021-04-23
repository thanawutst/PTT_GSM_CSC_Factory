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
    public class RequestTypeController : ControllerBase
    {
        [HttpGet("GetOrder")]
        public ActionResult GetOrder()
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            var lst = DB.TM_RequestType.Where(w => w.IsDelete != true).OrderBy(o => o.nOrder).Select(s =>
            new Droupdown
            {
                label = s.nOrder + "",
                value = s.nOrder + ""
            }).ToList();
            return Ok(lst);
        }

        [HttpGet("GetDataTable")]
        public ActionResult GetDataTable()
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            var lst = DB.TM_RequestType.Where(w => w.IsDelete != true).OrderBy(o => o.nOrder).Select(s =>
            new lstData
            {
                nRequestTypeID = s.nRequestTypeID,
                sRequestTypeName = s.sRequestTypeName,
                nOrder = s.nOrder,
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
                #region Save Data to DB TM_RequestType
                var UPT_RequestType = DB.TM_RequestType.FirstOrDefault(f => f.nRequestTypeID == Obj.nRequestTypeID);
                if (UPT_RequestType != null)
                {
                    UPT_RequestType.sRequestTypeName = Obj.sRequestTypeName;
                    UPT_RequestType.nUpdate = 1;
                    UPT_RequestType.dUpdate = DateTime.Now;
                    UPT_RequestType.IsActive = Obj.sActive == "1" ? true : false;
                    UPT_RequestType.IsDelete = false;
                    DB.SaveChanges();
                }
                else
                {
                    TM_RequestType CRT_RequestType = new TM_RequestType();
                    CRT_RequestType.sRequestTypeName = Obj.sRequestTypeName;
                    var MaxOrder = DB.TM_RequestType.Max(m => m.nOrder);
                    if (MaxOrder.HasValue)
                    {
                        CRT_RequestType.nOrder = MaxOrder + 1;
                    }
                    CRT_RequestType.nCreate = 1;
                    CRT_RequestType.dCreate = DateTime.Now;
                    CRT_RequestType.nUpdate = 1;
                    CRT_RequestType.dUpdate = DateTime.Now;
                    CRT_RequestType.IsActive = Obj.sActive == "1" ? true : false;
                    CRT_RequestType.IsDelete = false;
                    DB.TM_RequestType.Add(CRT_RequestType);
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
                        var lstDelete = DB.TM_RequestType.FirstOrDefault(f => f.nRequestTypeID == item.nRequestTypeID);
                        if (lstDelete != null)
                        {
                            lstDelete.nDelete = 1;
                            lstDelete.dDelete = DateTime.Now;
                            lstDelete.IsDelete = true;
                            DB.SaveChanges();
                        }
                    }
                    Result.sStatus = Systemfunction.process_Success();
                }
                #endregion
            }
            catch (Exception ex)
            {
                Result.sMsg = ex.Message;
                Result.sStatus = Systemfunction.process_Failed();
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
            public int nRequestTypeID { get; set; }
            public string sRequestTypeName { get; set; }
            public int? nOrder { get; set; }
            public string sActive { get; set; }
            public bool IsDelete { get; set; }
        }
        public class lstSaveData
        {
            public int? nRequestTypeID { get; set; }
            public string sRequestTypeName { get; set; }
            public int? nOrder { get; set; }
            public string sActive { get; set; }
        }

      
        public class Droupdown
        {
            public string label { get; set; }
            public string value { get; set; }
        }
        #endregion
    }
}
