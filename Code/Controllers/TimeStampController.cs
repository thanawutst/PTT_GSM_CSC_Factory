using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftthaiIntranet.Models.Softthai_Intranet_2021;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftthaiIntranet.Extensions;
using System.Globalization;
using SoftthaiIntranet.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SoftthaiIntranet.Models.SystemModels;

namespace SoftthaiIntranet.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TimeStampController : ControllerBase
    {
        public IConfiguration _Configuration { get; }
        private readonly IHostEnvironment _env;
        private readonly Softthai_Intranet_2021Context _db;
        private readonly IAuthentication _Auth;

        private UserAccount user;

        public TimeStampController(IConfiguration Configuration, IHostEnvironment env, IAuthentication auth)
        {
            _Configuration = Configuration;
            _env = env;
            _Auth = auth;
            _db = new Softthai_Intranet_2021Context();
            user = _Auth.GetUserAccount();
        }

        [HttpGet]
        public ActionResult GetTimeStampList()
        {
            if (_Auth.IsHasExists())
            {
                List<TimeStamp> lstEmp = _db.T_Employee.Where(w => w.IsActive != false && w.IsDel != true).Select(s => new TimeStamp
                {
                    sEmployeeCode = s.sEmployeeCode,
                    sName = s.sFirstname + " " + s.sLastname + " (" + s.sNickname + ")"
                }).ToList();

                DateTime dNow = DateTime.Now.Date;
                DateTime dCompen = new DateTime(dNow.Year, dNow.Month, dNow.Day, 19, 0, 0);
                foreach (var item in lstEmp)
                {
                    TM_Leave oLeave = null;

                    //get value
                    T_WorkingTime iWorkingTime = _db.T_WorkingTime.Where(w => w.dDateWork == dNow && w.sEmployeeCode == item.sEmployeeCode).FirstOrDefault();

                    if (iWorkingTime != null)
                    {
                        oLeave = _db.TM_Leave.Where(w => w.nLeaveID == iWorkingTime.nLeaveID).FirstOrDefault();
                    }

                    if (oLeave != null)
                    {
                        var a = oLeave.sLeaveName;
                    }

                    //set value
                    item.sCheckIn = (iWorkingTime != null && iWorkingTime.dSignIn.HasValue) ? iWorkingTime.dSignIn.Value.ToString("HH:mm") + " น." : oLeave != null ? "-" : "N/A";

                    item.sCheckOut = (iWorkingTime != null && iWorkingTime.dSignOut.HasValue) ? iWorkingTime.dSignOut.Value.ToString("HH:mm") + " น." : oLeave != null ? "-" : "N/A";

                    item.nLateTime = (iWorkingTime != null && iWorkingTime.nMinuteDelay.HasValue && iWorkingTime.nLeaveID == null) ? iWorkingTime.nMinuteDelay : null;

                    // Sign Out > 19.00 = Not Late
                    if (iWorkingTime != null && iWorkingTime.dSignOut.HasValue)
                    {
                        if (iWorkingTime.dSignOut.Value > dCompen)
                        {
                            item.nLateTime = 0;
                        }
                    }

                    item.sLeaveType = oLeave != null ? oLeave.sLeaveName.Trim() : "-";

                    item.sRemark = iWorkingTime != null ? iWorkingTime.sRemark : "";

                    //Sum Late
                    List<T_WorkingTime> lstAllMonth = _db.T_WorkingTime.Where(w => w.dDateWork.Value.Month == dNow.Month && w.sEmployeeCode == item.sEmployeeCode).ToList();

                    if (lstAllMonth.Any())
                    {
                        var lstDayLate = lstAllMonth.Where(w => w.nMinuteDelay != null);

                        int? sumMinLate = 0;
                        int sumDayLate = 0;
                        foreach (var iDayLate in lstDayLate)
                        {
                            if (iDayLate.nLeaveID == null) // ต้องไม่ลา
                            {
                                if (iDayLate.dSignOut != null)
                                {
                                    //singout != null && ออกหลัง 19.00 
                                    DateTime _thisCompen = new DateTime(iDayLate.dSignIn.Value.Year, iDayLate.dSignIn.Value.Month, iDayLate.dSignIn.Value.Day, 19, 0, 0);
                                    if (iDayLate.dSignOut < _thisCompen)
                                    {
                                        sumMinLate += iDayLate.nMinuteDelay;
                                        sumDayLate += 1;
                                    }
                                }
                                else
                                {
                                    sumMinLate += iDayLate.nMinuteDelay;
                                    sumDayLate += 1;
                                }
                            }
                        }


                        if (sumMinLate > 0)
                        {
                            TimeSpan spWorkMin = TimeSpan.FromMinutes(Convert.ToDouble(sumMinLate));
                            string sWorkHour = string.Format("{0} hr {1} min", (int)spWorkMin.TotalHours, spWorkMin.Minutes);

                            item.sSumLateTime = sWorkHour + " (" + sumDayLate + " Day) "; ;
                        }
                        else
                        {
                            item.sSumLateTime = "";
                        }
                    }
                    else
                    {
                        item.sSumLateTime = "";
                    }

                }

                return Ok(lstEmp);
            }
            else
            {
                return Ok();
            }
        }
        public class TimeStamp
        {
            public string sEmployeeCode { get; set; }
            public string sName { get; set; }
            public string sCheckIn { get; set; }
            public string sCheckOut { get; set; }
            public int? nLateTime { get; set; }
            public string sLeaveType { get; set; }
            public string sRemark { get; set; }
            public string sSumLateTime { get; set; }
        }
        public class Dropdown
        {
            public string label { get; set; }
            public string value { get; set; }
        }
    }
}
