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
    public class OTTypeController : ControllerBase
    {
         [HttpPost("SaveData")]
        public ActionResult SaveData([FromBody] OverTimeOTType ObjData)
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            AllClass.CResutlWebMethod result = new AllClass.CResutlWebMethod();
            //var ua = new UserAcc.GetSession();
            try
            {
                var data = DB.TM_OverTime.FirstOrDefault(f => f.nOverTimeID == ObjData.nOverTimeID);
                var isNew = data == null;
                var dDate = DateTime.Now;
                var nOrder = (DB.TM_OverTime.Any() ? DB.TM_OverTime.Max(m => m.nOrder) : 0) + 1;
                decimal? dRate = decimal.Parse(ObjData.nRate);
                if (isNew)
                {
                    data = new TM_OverTime();
                    //data.nOverTimeID = GenOverTimeID(ObjData.nOverTimeID);
                    data.nOrder = nOrder;
                    data.nCreate = 1;
                    data.dCreate = dDate;
                }
                data.nUpdate = 1;
                data.dUpdate = dDate;
                data.IsActive = ObjData.sActive;
                data.sOverTimeName = ObjData.sOverTimeName;
                data.nRate = dRate;

                if (isNew)
                {
                    DB.TM_OverTime.Add(data);
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

         [HttpGet("GetData")]
        public ActionResult GetData([FromQuery] string sID)
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            int nID = int.Parse(sID);
            var lstData = DB.TM_OverTime.Where(w => w.nOverTimeID == nID).Select(s =>
               new OverTimeOTType()
               {
                   nOverTimeID = s.nOverTimeID,
                   sOverTimeName = s.sOverTimeName,
                   nRate = s.nRate+"",            
                   sActive = s.IsActive,
               }).ToList();
            return Ok(lstData);
        }

         [HttpGet("ListData")]
        public ActionResult ListData()
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            var lstData = DB.TM_OverTime.Where(w => w.IsDelete == false).OrderByDescending(o => o.dUpdate).Select(s =>
               new 
               {
                   nOverTimeID = s.nOverTimeID,
                   sOverTimeName = s.sOverTimeName,    
                   nRate = s.nRate+"",      
                   sOverTimeStatus = s.IsActive ? "Active" : "Inactive",
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

                    DB.TM_OverTime.Where(w => obj.Contains(w.nOverTimeID)).ToList().ForEach(f =>
                    {                             
                        f.nDelete = 1;
                        f.dDelete = DateTime.Now;
                        f.IsDelete = true;

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
        public static int GenOverTimeID(int? nID)
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            if (!DB.TM_OverTime.Any(w => w.nOverTimeID == nID))
            {
                int nLast = DB.TM_OverTime.Any() ? DB.TM_OverTime.Max(m => m.nOverTimeID) : 0;
                nID = nLast + 1;
            }
            else GenOverTimeID(nID.Value + 1);

            return nID.Value;
        } 
    }
    public class OverTimeOTType{
        public int? nOverTimeID{get;set;}
        public string sOverTimeName{get;set;}
        public string nRate{get;set;}
        public bool sActive { get; set; }

    }
}