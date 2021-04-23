using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using SoftthaiIntranet.Interfaces;
using SoftthaiIntranet.Models.SystemModels;
using SoftthaiIntranet.Models.Softthai_Intranet_2021;
using SoftthaiIntranet.Extensions;
using Microsoft.AspNetCore.Http;


namespace SoftthaiIntranet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        public IConfiguration _Configuration { get; }
        private readonly IHostEnvironment _env;
        private readonly IAuthentication _Auth;

        public EmployeeController(IConfiguration Configuration, IHostEnvironment env, IAuthentication auth)
        {
            _Configuration = Configuration;
            _env = env;
            _Auth = auth;
        }


        [HttpGet("GetEmp")]
        public async Task<ActionResult> GetEmp()
        {
            UserAccount user = new UserAccount();
            if (_Auth.IsHasExists())
            {
                try
                {
                    Softthai_Intranet_2021Context db = new Softthai_Intranet_2021Context();
                    var lstRole = await db.TM_Role.Where(w => w.IsActive && !w.IsDelete).ToListAsync();
                    var lstPosition = await db.TM_Position.Where(w => w.IsActive && !w.IsDelete).ToListAsync();
                    var lstData = await (from a in db.T_Employee
                                         where a.IsActive && !a.IsDel
                                         select new TData_Emp
                                         {
                                             sID = a.nEmployeeID + "",
                                             sFullName = a.sFirstname + " " + a.sLastname + " (" + a.sNickname + ")",
                                             sUserName = a.sUsername,
                                             sPosition = "",
                                             sRoleName = "",
                                             nPositionID = a.nPositionID,
                                             nRoleID = a.nRoleID,
                                         }).ToListAsync();
                    foreach (var item in lstData)
                    {
                        var qRole = lstRole.FirstOrDefault(w => w.nRoleID == item.nRoleID);
                        if (qRole != null)
                        {
                            item.sRoleName = qRole.sRoleName;
                        }
                        var qPosition = lstPosition.FirstOrDefault(w => w.nPositionID == item.nPositionID);
                        if (qPosition != null
                        )
                        {
                            item.sPosition = qPosition.sPositionName;
                        }
                    }

                    return Ok(new { data = lstData, code = SystemMessageMethod.code_success, msg = SystemMessageMethod.msg_success });
                }
                catch (System.Exception error)
                {
                    return Ok(new
                    {
                        data = "",
                        code = SystemMessageMethod.code_error,
                        msg = error.Message + "",
                    });
                }
            }
            else
            {
                return Ok(new
                {
                    data = "",
                    code = SystemMessageMethod.code_expeire,
                    msg = SystemMessageMethod.msg_expire,
                });
            }
        }

        [HttpPost("DeleteDataEmp")]
        public ActionResult DeleteDataEmp([FromBody] List<TData_Emp> lstData)
        {
            UserAccount user = new UserAccount();
            if (_Auth.IsHasExists())
            {
                user = _Auth.GetUserAccount();
                int nUserID = user.sUserID.toIntNullToZero();
                try
                {
                    #region Check จำนวนข้อมูลที่ทำการ Select
                    if (lstData.Count > 0)
                    {
                        foreach (var item in lstData)
                        {
                            Softthai_Intranet_2021Context db = new Softthai_Intranet_2021Context();
                            int nRequestID = item.sID.toIntNullToZero();
                            var qData = db.T_Employee.FirstOrDefault(f => f.nEmployeeID == nRequestID);
                            if (qData != null)
                            {
                                qData.nUpdateBy = nUserID;
                                qData.dUpdate = DateTime.Now;
                                qData.IsDel = true;
                                db.SaveChanges();
                            }
                        }
                    }
                    #endregion

                    return Ok(new { data = "", code = SystemMessageMethod.code_success, msg = SystemMessageMethod.msg_success });
                }
                catch (System.Exception error)
                {
                    return Ok(new
                    {
                        data = "",
                        code = SystemMessageMethod.code_error,
                        msg = error.Message + "",
                    });
                }
            }
            else
            {
                return Ok(new
                {
                    data = "",
                    code = SystemMessageMethod.code_expeire,
                    msg = SystemMessageMethod.msg_expire,
                });
            }
        }

        public class TData_Emp
        {
            public string sID { get; set; }
            public string sFullName { get; set; }
            public string sUserName { get; set; }
            public string sPosition { get; set; }
            public string sRoleName { get; set; }
            public int? nRoleID { get; set; }
            public int? nPositionID { get; set; }
        }

    }
}