using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftthaiIntranet.Models.Softthai_Intranet_2021;
using SoftthaiIntranet.Models.SystemModels.AllClass;
using SoftthaiIntranet.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftthaiIntranet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskController : ControllerBase
    {
        [HttpGet("GetRole")]
        public ActionResult GetRole()
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            var lst = DB.TM_Role.Where(w => w.IsActive == true && w.IsDelete != true).Select(s =>
            new Droupdown
            {
                label = s.sRoleName,
                value = s.nRoleID + ""
            }).ToList();
            return Ok(lst);
        }
        [HttpGet("GetProjectType")]
        public ActionResult GetProjectType()
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            var lst = DB.TM_ProjectType.Where(w => w.IsActive == true).Select(s =>
            new Droupdown
            {
                label = s.sProjectTypeName,
                value = s.nProjectTypeID + ""
            }).ToList();
            return Ok(lst);
        }
        [HttpGet("GetProjectProgresses")]
        public ActionResult GetProjectProgresses()
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            var lst = DB.TM_ProjectProgress.Where(w => w.cStatus == "Y").OrderBy(o => o.nOrder).Select(s =>
            new Droupdown
            {
                label = s.sProjectProgressName,
                value = s.nProjectProgressID + ""
            }).ToList();
            return Ok(lst);
        }

        [HttpGet("GetEmployee")]
        public ActionResult GetEmployee()
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            var lst = DB.T_Employee.Where(w => w.IsActive == true && w.IsDel != true).OrderBy(o => o.sFirstname).Select(s => new Droupdown
            {
                label = s.sFirstname + " " + s.sLastname,
                value = s.nEmployeeID + ""
            }).ToList();
            return Ok(lst);
        }

        [HttpGet("GetProject")]
        public ActionResult GetlstProject()
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            var lst = new List<lstProject>();
            try
            {
                lst = DB.TM_Project.Where(w => w.IsDelete != true).Select(s => new lstProject
                {
                    nOrder = s.nProjectID,
                    nProjectID = s.nProjectID,
                    sProjectName = s.sProjectName,
                    nYear = s.nYear,
                    sStatus = s.IsActive ? "Active" : "Inactive",
                    sTypeName = DB.TM_ProjectType.FirstOrDefault(f => f.nProjectTypeID == s.nProjectType) == null ? null : DB.TM_ProjectType.FirstOrDefault(f => f.nProjectTypeID == s.nProjectType).sProjectTypeName,
                    nTeam = DB.TM_ProjectMember.Where(w => w.nProjectID == s.nProjectID).ToList().Count,
                    IsDelete = false

                }).ToList();
            }
            catch (Exception)
            {
                lst = new List<lstProject>();
            }

            return Ok(lst);
        }

        [HttpGet("GetProjectbyID")]
        public ActionResult GetProjectbyID([FromQuery] objSearch data)
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            var objSaves = new objSave();
            try
            {
                var lst = DB.TM_Project.FirstOrDefault(f => f.nProjectID == Systemfunction.ParseIntToNull(data.sID));
                if (lst != null)
                {
                    var lstDate = new List<DateTime>();
                    var datail = new objProject();
                    datail.sProjectID = lst.nProjectID + "";
                    lstDate.Add(lst.dStartDate.Value);
                    lstDate.Add(lst.dEndDate.Value);
                    datail._dDate = lstDate;
                    datail.sProgress = lst.nProjectProgress + "";
                    datail.sProject_Name = lst.sProjectName;
                    datail.sProject_Abbr = lst.sProjectAbbr;
                    datail.sProject_Old = lst.nProjectProgress == null ? null : lst.nProjectHeadID + "";
                    datail.sType = lst.nProjectType + "";
                    datail.dYear = new DateTime(lst.nYear.Value, lst.dStartDate.Value.Month, lst.dStartDate.Value.Day);
                    datail.IsStatus = lst.IsActive;

                    var lstMember = DB.TM_ProjectMember.Where(w => w.nProjectID == Systemfunction.ParseIntToNull(data.sID)).ToList();
                    var lstMem = new List<objMember>();
                    foreach (var item in lstMember)
                    {
                        var _member = new objMember();
                        _member.sEmpID = item.nEmployeeID + "";
                        _member.sRole = item.nRoleID + "";
                        _member.sProMemID = item.nMemberID + "";
                        _member.IsDelete = item.IsDelete;
                        var _emp = DB.T_Employee.FirstOrDefault(f => f.nEmployeeID == item.nEmployeeID);
                        _member.sName = _emp == null ? null : _emp.sFirstname + " " + _emp.sLastname;
                        var _role = DB.TM_Role.FirstOrDefault(f => f.nRoleID == item.nRoleID);
                        _member.sRoleName = _role == null ? "" : _role.sRoleName;
                        lstMem.Add(_member);
                    }

                    objSaves.lstMember = lstMem;
                    objSaves.objDatail = datail;
                }

            }
            catch (Exception)
            {
                objSaves = new objSave();
            }

            return Ok(objSaves);
        }

        [HttpPost("SaveData")]
        public ActionResult SaveData([FromBody] objSave obj)
        {
            AllClass.CResutlWebMethod Result = new AllClass.CResutlWebMethod();
            try
            {
                //IsActive ซ้ำไม่ได้
                //IsDelet ซ้ำได้
                Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
                var lst = DB.TM_Project.Where(w => w.sProjectName.Trim().ToUpper() == obj.objDatail.sProject_Name.Trim().ToUpper()
                && w.nProjectID != Systemfunction.ParseIntToNull(obj.objDatail.sProjectID)
                && w.IsDelete != true).ToList();
                bool IsDup = lst.Count > 0 ? false : true;
                if (IsDup)
                {
                    var lst_Pro = DB.TM_Project.FirstOrDefault(f => f.nProjectID == Systemfunction.ParseIntToNull(obj.objDatail.sProjectID));
                    if (lst_Pro == null)
                    {
                        lst_Pro = new TM_Project();
                        lst_Pro.nProjectHeadID = Systemfunction.ParseIntToNull(obj.objDatail.sProject_Old);
                        lst_Pro.sProjectName = obj.objDatail.sProject_Name.Trim();
                        lst_Pro.sProjectAbbr = obj.objDatail.sProject_Abbr.Trim();
                        var _Year = Systemfunction.ParseDateTimeToNull(obj.objDatail.sYear);
                        lst_Pro.nYear = _Year.HasValue ? _Year.Value.Year : null;
                        if (obj.objDatail.dDate.Count > 0)
                        {
                            lst_Pro.dStartDate = Systemfunction.ParseDateTimeToNull(obj.objDatail.dDate[0]);
                            lst_Pro.dEndDate = Systemfunction.ParseDateTimeToNull(obj.objDatail.dDate[1]);
                        }
                        lst_Pro.nProjectType = Systemfunction.ParseIntToNull(obj.objDatail.sType);
                        lst_Pro.nProjectProgress = Systemfunction.ParseIntToNull(obj.objDatail.sProgress);
                        lst_Pro.IsActive = obj.objDatail.IsStatus;
                        lst_Pro.nCreate = 1;
                        lst_Pro.dCreate = DateTime.Now;
                        lst_Pro.nUpdate = 1;
                        lst_Pro.dUpdate = DateTime.Now;
                        lst_Pro.IsDelete = false;
                        DB.TM_Project.Add(lst_Pro);
                    }
                    else
                    {
                        lst_Pro.nProjectHeadID = Systemfunction.ParseIntToNull(obj.objDatail.sProject_Old);
                        lst_Pro.sProjectName = obj.objDatail.sProject_Name.Trim();
                        lst_Pro.sProjectAbbr = obj.objDatail.sProject_Abbr.Trim();
                        var _Year = Systemfunction.ParseDateTimeToNull(obj.objDatail.sYear);
                        lst_Pro.nYear = _Year.HasValue ? _Year.Value.Year : null;
                        if (obj.objDatail.dDate.Count > 0)
                        {
                            lst_Pro.dStartDate = Systemfunction.ParseDateTimeToNull(obj.objDatail.dDate[0]);
                            lst_Pro.dEndDate = Systemfunction.ParseDateTimeToNull(obj.objDatail.dDate[1]);
                        }
                        lst_Pro.nProjectType = Systemfunction.ParseIntToNull(obj.objDatail.sType);
                        lst_Pro.nProjectProgress = Systemfunction.ParseIntToNull(obj.objDatail.sProgress);
                        lst_Pro.IsActive = obj.objDatail.IsStatus;
                        lst_Pro.nUpdate = 1;
                        lst_Pro.dUpdate = DateTime.Now;
                        lst_Pro.IsDelete = false;
                    }
                    DB.SaveChanges();

                    var Emp = DB.TM_Project.FirstOrDefault(f => f.sProjectName == obj.objDatail.sProject_Name.Trim() && f.IsDelete != true);
                    if (Emp != null)
                    {
                        SaveMember(obj.lstMember, Emp.nProjectID);
                    }

                    Result.sStatus = Systemfunction.process_Success();
                }
                else
                {
                    Result.sStatus = Systemfunction.process_Duplicate();
                }

            }
            catch (Exception ex)
            {
                Result.sStatus = Systemfunction.process_Failed();
                Result.sMsg = ex.Message;
            }

            return Ok(Result);
        }

        public void SaveMember(List<objMember> lstData, int nProID)
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            foreach (var item in lstData)
            {
                var lst = DB.TM_ProjectMember.FirstOrDefault(f => f.nProjectID == nProID && f.nMemberID == Systemfunction.ParseIntToNull(item.sProMemID));
                if (lst == null)
                {
                    lst = new TM_ProjectMember();
                    lst.nProjectID = nProID;
                    lst.nEmployeeID = Systemfunction.ParseIntToNull(item.sEmpID);
                    lst.IsDelete = item.IsDelete;
                    lst.nRoleID = Systemfunction.ParseIntToNull(item.sRole);
                    lst.nCreate = 1;
                    lst.dCreate = DateTime.Now;
                    lst.dUpdate = DateTime.Now;
                    lst.nUpdate = 1;
                    DB.TM_ProjectMember.Add(lst);
                }
                else
                {
                    lst.nProjectID = nProID;
                    lst.nEmployeeID = Systemfunction.ParseIntToNull(item.sEmpID);
                    lst.nRoleID = Systemfunction.ParseIntToNull(item.sRole);
                    lst.IsDelete = item.IsDelete;
                    lst.dUpdate = DateTime.Now;
                    lst.nUpdate = 1;
                }
            }
            DB.SaveChanges();

        }

        [HttpPost("DeleteProject")]
        public ActionResult DeleteProject([FromBody] List<lstProject> data)
        {
            AllClass.CResutlWebMethod Result = new AllClass.CResutlWebMethod();
            try
            {
                Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
                foreach (var item in data)
                {
                    var lst = DB.TM_Project.FirstOrDefault(f => f.nProjectID == item.nProjectID);
                    if (lst != null)
                    {
                        lst.IsDelete = true;
                        lst.dDelete = DateTime.Now;
                    }
                }
                DB.SaveChanges();
                Result.sStatus = Systemfunction.process_Success();
            }
            catch (Exception ex)
            {
                Result.sStatus = Systemfunction.process_Failed();
                Result.sMsg = ex.Message;
            }
            return Ok(Result);
        }

        public class Droupdown
        {
            public string label { get; set; }
            public string value { get; set; }
        }

        public class objSave
        {
            public objProject objDatail { get; set; }
            public List<objMember> lstMember { get; set; }
        }

        public class objProject
        {
            public string sProjectID { get; set; }
            public List<string> dDate { get; set; }
            public List<DateTime> _dDate { get; set; }
            public string sProgress { get; set; }
            public string sProject_Name { get; set; }
            public string sProject_Abbr { get; set; }
            public string sProject_Old { get; set; }
            public string sType { get; set; }
            public string sYear { get; set; }
            public DateTime? dYear { get; set; }
            public bool IsStatus { get; set; }
        }

        public class objMember
        {
            public string sRole { get; set; }
            public string sName { get; set; }
            public string sRoleName { get; set; }
            public string sEmpID { get; set; }
            public string sProMemID { get; set; }
            public bool IsDelete { get; set; }
        }

        public class lstProject
        {
            public int nOrder { get; set; }
            public int nProjectID { get; set; }
            public string sProjectName { get; set; }
            public int? nYear { get; set; }
            public string sStatus { get; set; }
            public int nTeam { get; set; }
            public string sType { get; set; }
            public string sTypeName { get; set; }
            public bool? IsDelete { get; set; }
        }

        public class objSearch
        {
            public string sID { get; set; }
        }

    }
}
