using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftthaiIntranet.Models.Softthai_Intranet_2021;
using Newtonsoft.Json;
using SoftthaiIntranet.Models.SystemModels.AllClass;
using SoftthaiIntranet.Extensions;

namespace SoftthaiIntranet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskTypeController : ControllerBase
    {
        [HttpPost("SaveData")]
        public ActionResult SaveData([FromBody] ST_TaskType ObjData)
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            AllClass.CResutlWebMethod result = new AllClass.CResutlWebMethod();
            //var ua = new UserAcc.GetSession();
            try
            {
                var data = DB.TM_TaskType.FirstOrDefault(f => f.nTaskTypeID == ObjData.nTaskTypeID);
                var isNew = data == null;
                var dDate = DateTime.Now;
                var nOrder = (DB.TM_TaskType.Any() ? DB.TM_TaskType.Max(m => m.nOrder) : 0) + 1;

                if (isNew)
                {
                    data = new TM_TaskType();
                    data.nTaskTypeID = GenTaskTypeID(ObjData.nTaskTypeID);
                    data.nOrder = nOrder;
                    data.nCreateBy = 1;
                    data.dCreate = dDate;
                }
                data.nUpdateBy = 1;
                data.dUpdate = dDate;
                data.IsActive = ObjData.sActive;
                data.sTaskTypeName = ObjData.sTaskTypeName;

                if (isNew)
                {
                    DB.TM_TaskType.Add(data);
                }
                DB.SaveChanges();
                result.sStatus = Systemfunction.process_Success();
            }
            catch (Exception ex)
            {
                result.sMsg = ex.Message;
                result.sStatus = Systemfunction.process_Failed();
            }
            return Ok(result);

        }

         [HttpGet("ListData")]
        public ActionResult ListData()
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            var lstData = DB.TM_TaskType.Where(w => w.IsDel == false).OrderByDescending(o => o.dUpdate).Select(s =>
               new 
               {
                   nTaskTypeID = s.nTaskTypeID,
                   sTaskTypeName = s.sTaskTypeName,                
                   sTaskTypeStatus = s.IsActive ? "Active" : "Inactive",
               }).ToList();
            return Ok(lstData);

        }
        [HttpGet("GetData")]
        public ActionResult GetData([FromQuery] string sID)
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            int nID = int.Parse(sID);
            var lstData = DB.TM_TaskType.Where(w => w.nTaskTypeID == nID).Select(s =>
               new ST_TaskType()
               {
                   nTaskTypeID = s.nTaskTypeID,
                   sTaskTypeName = s.sTaskTypeName,                 
                   sActive = s.IsActive,
               }).ToList();
            return Ok(lstData);
        }

          [HttpPost("DeleteData")]
        public ActionResult DeleteData([FromBody] List<int> obj)
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            AllClass.CResutlWebMethod result = new AllClass.CResutlWebMethod();
            ///UserAcc ua = new UserAcc.GetSession();
            try
            {
                if (obj.Count() > 0)
                {

                    DB.TM_TaskType.Where(w => obj.Contains(w.nTaskTypeID)).ToList().ForEach(f =>
                    {
                        f.nUpdateBy = 1;
                        f.dUpdate = DateTime.Now;
                        f.IsDel = true;

                    });
                    DB.SaveChanges();
                    result.sStatus = Systemfunction.process_Success();
                }
            }
            catch (Exception ex)
            {
                result.sMsg = ex.Message;
                result.sStatus = Systemfunction.process_Failed();
            }
            return Ok(result);
        }

        public static int GenTaskTypeID(int? nID)
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            if (!DB.TM_TaskType.Any(w => w.nTaskTypeID == nID))
            {
                int nLast = DB.TM_TaskType.Any() ? DB.TM_TaskType.Max(m => m.nTaskTypeID) : 0;
                nID = nLast + 1;
            }
            else GenTaskTypeID(nID.Value + 1);

            return nID.Value;
        }
        public class ST_TaskType
        {
            public int? nTaskTypeID { get; set; }
            public string sTaskTypeName { get; set; }
            public bool sActive { get; set; }
        }

    }

}