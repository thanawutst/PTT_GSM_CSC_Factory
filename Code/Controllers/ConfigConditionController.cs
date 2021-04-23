using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftthaiIntranet.Models.Softthai_Intranet_2021;
using Newtonsoft.Json;
using SoftthaiIntranet.Models.SystemModels.AllClass;

//sun
namespace SoftthaiIntranet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigConditionController : ControllerBase
    {
        private Softthai_Intranet_2021Context _db;
        public ConfigConditionController()
        {
            _db = new Softthai_Intranet_2021Context();
        }
        [HttpGet("GetEmployee")]
        public ActionResult GetEmployee()
        {
            var lstDataEmployee = (from c in _db.T_Employee.Where(w => w.IsDel == false)
                                   from b in _db.TM_Role.Where(w => w.nRoleID == c.nRoleID)
                                   from f in _db.T_HR_Config.Where(w => w.nEmployeeID == c.nEmployeeID)
                                   select new CEmployee
                                   {
                                       nEmployeeID = c.nEmployeeID,
                                       sEmployeeCode = c.sEmployeeCode,
                                       sFirstname = c.sFirstname,
                                       sLastname = c.sLastname,
                                       sNickname = c.sNickname,
                                       nRoleID = c.nRoleID,
                                       sRole = b.sRoleName,
                                       IsCheck = f.IsAcceptWorkTime,
                                   }).ToList();

            return Ok(lstDataEmployee);
        }
        [HttpPost("SaveCondition")]
        public ActionResult SaveCondition([FromBody] CSaveConditon Data)
        {
            try
            {

                var objData = _db.T_HR_Config.FirstOrDefault(f => f.nEmployeeID == Data.EmpID);
                if (objData == null)
                {
                    var Create = new T_HR_Config();
                    Create.nEmployeeID = Data.EmpID;
                    Create.IsAcceptWorkTime = Data.IsActive;
                    Create.dCreate = DateTime.Now;
                    Create.dUpdate = DateTime.Now;
                    _db.T_HR_Config.Add(Create);
                }
                else
                {
                    objData.IsAcceptWorkTime = Data.IsActive;

                }
                _db.SaveChanges();

                return Ok("Success");
            }
            catch (Exception e)
            {
                return Ok(e.ToString());
            }
        }
    }
    public class CEmployee : T_Employee
    {
        public string sRole { get; set; }
        public bool? IsCheck { get; set; }

    }
    public class CSaveConditon
    {
        public int EmpID { get; set; }
        public bool IsActive { get; set; }

    }
}
