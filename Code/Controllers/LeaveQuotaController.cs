using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftthaiIntranet.Models.Softthai_Intranet_2021;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftthaiIntranet.Models.SystemModels.AllClass;
using SoftthaiIntranet.Extensions;

namespace SoftthaiIntranet.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LeaveQuotaController : ControllerBase
    {

        public Softthai_Intranet_2021Context _db = new Softthai_Intranet_2021Context();

        [HttpGet]
        public ActionResult GetEmployee()
        {
            List<AllClass.Dropdown> lst = _db.T_Employee.Where(w => w.IsActive == true && w.IsDel != true).OrderBy(o => o.sFirstname).Select(s => new AllClass.Dropdown
            {
                label = s.sFirstname + " " + s.sLastname,
                value = s.nEmployeeID + ""
            }).ToList();
            return Ok(lst);
        }

        [HttpGet]
        public ActionResult GetYearLeave()
        {
            List<AllClass.Dropdown> lst = _db.T_Emp_LeaveQuota.GroupBy(g => g.nYear).Select(s => new AllClass.Dropdown
            {
                label = s.Key.ToString(),
                value = s.Key.ToString()
            }).ToList();

            if (!lst.Any())
            {
                AllClass.Dropdown item = new AllClass.Dropdown();
                item.label = DateTime.Now.Year + "";
                item.value = DateTime.Now.Year + "";

                lst.Add(item);
            }
            return Ok(lst);

        }

        [HttpGet]
        public ActionResult GetTMLeave()
        {
            var lst = _db.TM_Leave.Where(w => w.IsActive != false && w.IsDelete != true).Select(s => new TMLeave
            {
                id = s.nLeaveID,
                label = s.sLeaveName.Trim() + "",
                disable = false,
                color = "#FFF",
            }).ToList();

            return Ok(lst);
        }

        [HttpGet("{sEmpID?}")]
        public ActionResult GetEmpGender(string sEmpID)
        {
            var item = _db.T_Employee.Where(w => w.nEmployeeID == Convert.ToInt32(sEmpID)).Select(s => new
            {
                sGender = s.sGender
            }).FirstOrDefault();
            return Ok(item);
        }

        [HttpGet("{sEmpID?}/{sYear?}")]
        public async Task<IActionResult> GetEmpQuotaLeave(string sEmpID, string sYear)
        {
            var lst = await _db.T_Emp_LeaveQuota.Where(w => w.nEmployeeID == Convert.ToInt32(sEmpID) && w.nYear == Convert.ToInt32(sYear)).OrderBy(o => o.nLeaveID).ToListAsync();
            return Ok(lst);
        }

        [HttpGet]
        public async Task<IActionResult> GetQuotaDefault()
        {
            var lst = await _db.TM_Leave.Where(w => w.IsActive != false && w.IsDelete != true).OrderBy(o => o.nLeaveID).ToListAsync();
            return Ok(lst);
        }


        [HttpPost]
        public async Task<IActionResult> SaveQuotaLeave(List<TMLeave> model)
        {
            foreach (var item in model)
            {
                var thisRec = _db.T_Emp_LeaveQuota.Where(w => w.nEmployeeID == item.EmpID && w.nLeaveID == item.id && w.nYear == item.Year).FirstOrDefault();
                if (thisRec == null)
                {
                    T_Emp_LeaveQuota newItem = new T_Emp_LeaveQuota();
                    newItem.nEmployeeID = item.EmpID;
                    newItem.nYear = item.Year;
                    newItem.nLeaveID = item.id;
                    newItem.nQuota = item.value;
                    await _db.T_Emp_LeaveQuota.AddAsync(newItem);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    thisRec.nQuota = item.value;
                    await _db.SaveChangesAsync();
                }
            }
            return Ok(new { code = SystemMessageMethod.code_success });
        }

        // [HttpPost]
        // public async Task<IActionResult> SaveQuotaLeave(QuotaLeave model)
        // {


        //     var lstQuota = _db.T_Emp_LeaveQuota.Where(w => w.nEmployeeID == Convert.ToInt32(model.sEmployee) && w.nYear == Convert.ToInt32(model.sYear)).ToList();

        //     if (!lstQuota.Any()) //Add
        //     {
        //         T_Emp_LeaveQuota item = new T_Emp_LeaveQuota();
        //         item.nEmployeeID = Convert.ToInt32(model.sEmployee);
        //         item.nYear = Convert.ToInt32(model.sYear);

        //         if (!string.IsNullOrEmpty(model.nSickLeave))
        //         {
        //             item.nLeaveID = 2;
        //             item.nQuota = Convert.ToInt32(model.nSickLeave);
        //             await _db.AddAsync(item);
        //             await _db.SaveChangesAsync();
        //         }

        //         if (!string.IsNullOrEmpty(model.nPersonalLeave))
        //         {
        //             item.nLeaveID = 1;
        //             item.nQuota = Convert.ToInt32(model.nPersonalLeave);
        //             await _db.AddAsync(item);
        //             await _db.SaveChangesAsync();
        //         }

        //         if (!string.IsNullOrEmpty(model.nVacationLeave))
        //         {
        //             item.nLeaveID = 3;
        //             item.nQuota = Convert.ToInt32(model.nVacationLeave);
        //             await _db.AddAsync(item);
        //             await _db.SaveChangesAsync();
        //         }
        //         if (!string.IsNullOrEmpty(model.nMaternityLeave))
        //         {
        //             item.nLeaveID = 4;
        //             item.nQuota = Convert.ToInt32(model.nMaternityLeave);
        //             await _db.AddAsync(item);
        //             await _db.SaveChangesAsync();
        //         }
        //         if (!string.IsNullOrEmpty(model.nOrdinationLeave))
        //         {
        //             item.nLeaveID = 5;
        //             item.nQuota = Convert.ToInt32(model.nOrdinationLeave);
        //             await _db.AddAsync(item);
        //             await _db.SaveChangesAsync();
        //         }
        //         if (!string.IsNullOrEmpty(model.nBirthDayLeave))
        //         {
        //             item.nLeaveID = 6;
        //             item.nQuota = Convert.ToInt32(model.nBirthDayLeave);
        //             await _db.AddAsync(item);
        //             await _db.SaveChangesAsync();
        //         }
        //     }
        //     else //Edit
        //     {
        //         foreach (var item in lstQuota)
        //         {
        //             if (item.nLeaveID == 2 && !string.IsNullOrEmpty(model.nSickLeave))
        //             {
        //                 item.nQuota = Convert.ToInt32(model.nSickLeave);
        //                 await _db.SaveChangesAsync();
        //             }

        //             else if (item.nLeaveID == 1 && !string.IsNullOrEmpty(model.nPersonalLeave))
        //             {
        //                 item.nQuota = Convert.ToInt32(model.nPersonalLeave);
        //                 await _db.SaveChangesAsync();
        //             }

        //             else if (item.nLeaveID == 3 && !string.IsNullOrEmpty(model.nVacationLeave))
        //             {
        //                 item.nQuota = Convert.ToInt32(model.nVacationLeave);
        //                 await _db.SaveChangesAsync();
        //             }
        //             else if (item.nLeaveID == 4 && !string.IsNullOrEmpty(model.nMaternityLeave))
        //             {
        //                 item.nQuota = Convert.ToInt32(model.nMaternityLeave);
        //                 await _db.SaveChangesAsync();
        //             }
        //             else if (item.nLeaveID == 5 && !string.IsNullOrEmpty(model.nOrdinationLeave))
        //             {
        //                 item.nQuota = Convert.ToInt32(model.nOrdinationLeave);
        //                 await _db.SaveChangesAsync();
        //             }
        //             else if (item.nLeaveID == 6 && !string.IsNullOrEmpty(model.nBirthDayLeave))
        //             {
        //                 item.nQuota = Convert.ToInt32(model.nBirthDayLeave);
        //                 await _db.SaveChangesAsync();
        //             }
        //         }
        //     }

        //     return Ok(new { code = SystemMessageMethod.code_success });
        // }
        public class TMLeave
        {
            public int EmpID { get; set; }
            public int Year { get; set; }
            public int id { get; set; }
            public string label { get; set; }
            public int value { get; set; }
            public bool disable { get; set; }
            public string color { get; set; }
        }
        public class QuotaLeave
        {
            public string sEmployee { get; set; }
            public string sYear { get; set; }
            public string nSickLeave { get; set; }
            public string nPersonalLeave { get; set; }
            public string nVacationLeave { get; set; }
            public string nMaternityLeave { get; set; }
            public string nOrdinationLeave { get; set; }
            public string nBirthDayLeave { get; set; }
        }
    }
}
