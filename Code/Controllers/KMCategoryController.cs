using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftthaiIntranet.Models.Softthai_Intranet_2021;
using Newtonsoft.Json;
using SoftthaiIntranet.Models.SystemModels.AllClass;
using SoftthaiIntranet.Extensions;
using SoftthaiIntranet.Models;


namespace SoftthaiIntranet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KMCategoryController : ControllerBase
    {
        [HttpPost("SaveData")]
        public ActionResult SaveData([FromBody] ST_KM ObjData)
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            AllClass.CResutlWebMethod result = new AllClass.CResutlWebMethod();
            //UserAcc ua = new UserAcc.GetSession();
            try
            {
                var data = DB.TM_CategoryKM.FirstOrDefault(f => f.nCategoryKMID == ObjData.nKMID);
                var isNew = data == null;
                var dDate = DateTime.Now;
                var nOrder = (DB.TM_CategoryKM.Any() ? DB.TM_CategoryKM.Max(m => m.nOrder) : 0) + 1;

                if (isNew)
                {
                    data = new TM_CategoryKM();
                    data.nCategoryKMID = GenKMID(ObjData.nKMID);
                    data.nOrder = nOrder;
                    data.nCreateBy = 1;
                    data.dCreate = dDate;
                }
                data.nUpdateBy = 1;
                data.dUpdate = dDate;
                data.IsActive = ObjData.sActive;
                data.sCategoryName = ObjData.sCategoryName;
                data.sDescription = ObjData.sDetail;
                if (isNew)
                {
                    DB.TM_CategoryKM.Add(data);
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
            var lstData = DB.TM_CategoryKM.Where(w => w.IsDel == false).OrderByDescending(o => o.dUpdate).Select(s =>
               new 
               {
                   nCategoryID = s.nCategoryKMID,
                   sCategoryName = s.sCategoryName,
                   sCategoryDescription = s.sDescription,
                   sCategoryStatus = s.IsActive ? "Active" : "Inactive",
               }).ToList();
            return Ok(lstData);

        }
         [HttpGet("GetData")]
        public ActionResult GetData([FromQuery] string sID)
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            int nID = int.Parse(sID);
            var lstData = DB.TM_CategoryKM.Where(w => w.nCategoryKMID == nID).Select(s =>
               new ST_KM()
               {
                   nKMID = s.nCategoryKMID,
                   sCategoryName = s.sCategoryName,
                   sDetail = s.sDescription,
                   sActive = s.IsActive,
               }).ToList();
            return Ok(lstData);
        }
       
        [HttpPost("DeleteData")]
        public ActionResult DeleteData([FromBody] List<int> obj)
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            Systemfunction.CResutlWebMethod result = new Systemfunction.CResutlWebMethod();
            ///UserAcc ua = new UserAcc.GetSession();
            try
            {
                if (obj.Count() > 0)
                {

                    DB.TM_CategoryKM.Where(w => obj.Contains(w.nCategoryKMID)).ToList().ForEach(f =>
                    {
                        f.nUpdateBy = 1;
                        f.dUpdate = DateTime.Now;
                        f.IsDel = true;

                    });
                    DB.SaveChanges();
                    result.Status = Systemfunction.process_Success();
                }
            }
            catch (Exception ex)
            {
                result.Msg = ex.Message;
                result.Status = Systemfunction.process_Failed();
            }
            return Ok(result);
        }
        public static int GenKMID(int? nID)
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            if (!DB.TM_CategoryKM.Any(w => w.nCategoryKMID == nID))
            {
                int nLast = DB.TM_CategoryKM.Any() ? DB.TM_CategoryKM.Max(m => m.nCategoryKMID) : 0;
                nID = nLast + 1;
            }
            else GenKMID(nID.Value + 1);

            return nID.Value;
        }

        public class ST_KM
        {
            public int? nKMID { get; set; }
            public string sCategoryName { get; set; }
            public string sDetail { get; set; }
            public bool sActive { get; set; }          

        }

    }

}

