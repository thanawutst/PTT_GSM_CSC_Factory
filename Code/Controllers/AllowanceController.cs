using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SoftthaiIntranet.Models.Softthai_Intranet_2021;
using SoftthaiIntranet.Extensions;
using SoftthaiIntranet.Models.SystemModels.AllClass;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using SoftthaiIntranet.Interfaces;
using SoftthaiIntranet.Models.SystemModels;
using Microsoft.AspNetCore.Http;

namespace SoftthaiIntranet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Route("api/[controller]/[action]")]
    public class AllowanceController : ControllerBase
    {
        public IConfiguration _Configuration { get; }
        private readonly IHostEnvironment _env;
        private readonly IAuthentication _Auth;

        public AllowanceController(IConfiguration Configuration, IHostEnvironment env, IAuthentication auth)
        {
            _Configuration = Configuration;
            _env = env;
            _Auth = auth;
        }

        #region GETDATA_DATE_BIND_TABLE
        [HttpPost("GetDate")]
        public ResultData GetDate(TSearch_Date item)
        {
            ResultData result = new ResultData();
            if (!string.IsNullOrEmpty(item.sStartDate) && !string.IsNullOrEmpty(item.sEndDate))
            {
                // M/d/yyyy
                string[] str1 = item.sStartDate.Split("/"); string[] str2 = item.sEndDate.Split("/");
                string sNewDate1 = str1[1].PadLeft(2, '0') + "/" + str1[0].PadLeft(2, '0') + "/" + str1[2];
                string sNewDate2 = str2[1].PadLeft(2, '0') + "/" + str2[0].PadLeft(2, '0') + "/" + str2[2];
                DateTime? dStart = sNewDate1.ConvertStringToDateTimeNull("");
                DateTime? dEnd = sNewDate2.ConvertStringToDateTimeNull("");

                List<TData_Time> lstData = new List<TData_Time>();
                int nID = 1;
                result.lstTime = lstData;

                if (dStart.HasValue && dEnd.HasValue)
                {
                    if (dStart.Value.Date == dEnd.Value.Date)
                    {
                        var qData = GetCheckDayNotWeeken(dStart, nID, true);
                        if (qData != null)
                        {
                            lstData.Add(qData);
                            nID++;

                            result.lstTime = lstData;
                            result.sStatus = "SUCCESS";
                        }
                        else
                        {
                            result.sStatus = "ERROR";
                        }
                    }
                    else if (dStart.Value.Date < dEnd.Value.Date)
                    {
                        var qData = GetCheckDayNotWeeken(dStart, nID, false);
                        if (qData != null)
                        {
                            lstData.Add(qData);
                            nID++;
                        }
                        ///
                        DateTime? dNext = dStart.Value.Date.AddDays(1);
                        if (dNext.Value.Date <= dEnd.Value.Date)
                        {
                            GetDataDateSub(dNext, dEnd, nID, lstData);
                        }

                        result.lstTime = lstData;
                        result.sStatus = "SUCCESS";
                    }
                    else
                    {
                        /// ERROR
                        result.sStatus = "ERROR";
                    }
                }
                else
                {
                    result.sStatus = "ERROR";
                }
            }

            return result;
        }

        public void GetDataDateSub(DateTime? dStartDate, DateTime? dEndDate, int nID, List<TData_Time> lstData)
        {

            if (dStartDate.Value.Date < dEndDate.Value.Date)
            {
                var qData = GetCheckDayNotWeeken(dStartDate, nID, false);
                if (qData != null)
                {
                    lstData.Add(qData);
                    nID++;
                }

                DateTime? dNext = dStartDate.Value.Date.AddDays(1);
                if (dNext.Value.Date <= dEndDate.Value.Date)
                {
                    GetDataDateSub(dNext, dEndDate, nID, lstData);
                }
            }
            else if (dStartDate.Value.Date == dEndDate.Value.Date)
            {
                var qData2 = GetCheckDayNotWeeken(dStartDate, nID, true);
                if (qData2 != null)
                {
                    lstData.Add(qData2);
                    nID++;
                }
            }
        }

        public TData_Time GetCheckDayNotWeeken(DateTime? dDate, int nID, bool IsLast)
        {
            TData_Time data = new TData_Time();
            string sTypeTime = IsLast ? "R" : "O";
            string sAllowance = IsLast ? "0" : "100";
            string sStarttime = IsLast ? "" : "9:00";
            string sEndtime = IsLast ? "" : "18:00";
            if ((dDate.Value.DayOfWeek != DayOfWeek.Saturday) && (dDate.Value.DayOfWeek != DayOfWeek.Sunday))
            {
                data.typetime = sTypeTime;
                data.allowance = sAllowance;
                data.endtime = sEndtime;
                data.starttime = sStarttime;
                data.id = nID + "";
                string sDate = dDate.DateStringCulture("en-Us");
                data.date = "(" + dDate.Value.DayOfWeek + ") " + sDate;
                data.sDate = sDate;
            }
            else
            {
                data = null;
            }

            return data;
        }

        [HttpGet("GetProject")]
        public ActionResult GetProject()
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            var lst = DB.TM_Project.Where(w => w.IsActive && !w.IsDelete).Select(s =>
            new AllClass.Dropdown
            {
                label = s.sProjectName,
                value = s.nProjectID + ""
            }).ToList();
            return Ok(lst);
        }

        [HttpGet("GetEmployee")]
        public ActionResult GetEmployee()
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            var lst = DB.T_Employee.Where(w => w.IsActive && !w.IsDel).Select(s =>
            new AllClass.Dropdown
            {
                label = s.sFirstname + " " + s.sLastname,
                value = s.nEmployeeID + ""
            }).ToList();
            return Ok(lst);
        }

        [HttpGet("GetSatus")]
        public ActionResult GetSatus()
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            var lst = DB.TM_FlowProcess.Where(w => w.IsActive).Select(s =>
            new AllClass.Dropdown
            {
                label = s.sFlowProcessName,
                value = s.nFlowProcessID + ""
            }).ToList();
            return Ok(lst);
        }

        [HttpGet("GetLocation")]
        public ActionResult GetLocation()
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            var lst = DB.TM_Location.Where(w => w.IsActive && !w.IsDelete).Select(s =>
            new AllClass.Dropdown
            {
                label = s.sLocationName,
                value = s.nLocationID + ""
            }).ToList();
            return Ok(lst);
        }

        [HttpGet("GETDataAllowanceRequest/{sRequestID?}")]
        public async Task<ActionResult> GETDataAllowanceRequest(string sRequestID)
        {
            try
            {
                UserAccount user = new UserAccount();
                if (_Auth.IsHasExists())
                {
                    user = _Auth.GetUserAccount();
                }
                TData_Allow result = new TData_Allow();
                Softthai_Intranet_2021Context db = new Softthai_Intranet_2021Context();
                List<TData_Time> lstTime = new List<TData_Time>();
                int nRequestID = sRequestID.toIntNullToZero();
                var qData = await db.T_Request_Allowance.FirstOrDefaultAsync(w => w.nAllowanceID == nRequestID && !w.IsDelete);
                if (qData != null)
                {
                    var lstProject = await db.TM_Project.Where(w => !w.IsDelete && w.IsActive).ToListAsync();
                    var lstLocation = await db.TM_Location.Where(w => !w.IsDelete && w.IsActive).ToListAsync();

                    var qProject = lstProject.FirstOrDefault(w => w.nProjectID == qData.nProjectID);
                    if (qProject != null)
                    {
                        result.sProjectName = qProject.sProjectName;
                    }

                    var qLocation = lstLocation.FirstOrDefault(w => w.nLocationID == qData.nLocationID);
                    if (qLocation != null)
                    {
                        result.sLocationName = qLocation.sLocationName;
                    }

                    result.isReadOnly = qData.nFlowProcessID > 1 ? true : false;
                    result.sProjectID = qData.nProjectID + "";
                    result.sMsgDetail = qData.sMeetingDetail;
                    result.sLocationID = qData.nLocationID + "";
                    if (!string.IsNullOrEmpty(qData.dCreate.DateString()))
                    {
                        string[] str1 = qData.dCreate.DateString().Split("/");
                        result.sTransactionDate = str1[2] + "-" + str1[1] + "-" + str1[0];
                        result.sRequestDate = qData.dCreate.DateString();
                    }
                    else
                    {
                        result.sTransactionDate = ""; result.sRequestDate = "";
                    }
                    if (!string.IsNullOrEmpty(qData.dMeetingStart.DateString()))
                    {
                        string[] str1 = qData.dMeetingStart.DateString().Split("/");
                        result.sStartDate = str1[2] + "-" + str1[1] + "-" + str1[0];
                        result.sMeetingDate = qData.dMeetingStart.DateString();
                    }
                    else
                    {
                        result.sStartDate = ""; result.sMeetingDate = "";
                    }
                    if (!string.IsNullOrEmpty(qData.dMeetingEnd.DateString()))
                    {
                        string[] str1 = qData.dMeetingEnd.DateString().Split("/");
                        result.sEndDate = str1[2] + "-" + str1[1] + "-" + str1[0];
                        result.sMeetingDate = result.sMeetingDate + " - " + qData.dMeetingEnd.DateString();
                    }
                    else
                    {
                        result.sEndDate = ""; result.sMeetingDate = "";
                    }

                    var lstDataTime = await (from a in db.T_Request_Allowance_Item
                                             where a.nAllowanceID == nRequestID && !a.IsDelete
                                             select new TData_Time
                                             {
                                                 id = a.nItemAllowanceID + "",
                                                 date = a.dMeetingt != null ? "(" + a.dMeetingt.Value.DayOfWeek + ") " + a.dMeetingt.DateStringCulture("en-Us") : "",
                                                 typetime = (a.IsOvernight ?? false) ? "O" : (a.IsRoundtrip ?? false) ? "R" : "",
                                                 starttime = a.sStartTime,
                                                 endtime = a.sEndTime,
                                                 allowance = a.nAmount + "",
                                                 sDate = a.dMeetingt.DateStringCulture("en-Us"),
                                             }).ToListAsync();

                    if (lstDataTime.Any())
                    {
                        lstTime.AddRange(lstDataTime);
                    }
                    result.lstDataTime = lstTime;

                    if (qData.nFlowProcessID == 10 || qData.nFlowProcessID == 11)
                    {
                        var qFlow = await db.T_Request_Allowance_Flow.FirstOrDefaultAsync(w => w.nAllowanceID == nRequestID);
                        if (qFlow != null)
                        {
                            result.sComment = !string.IsNullOrEmpty(qFlow.sComment) ? qFlow.sComment : "";
                        }
                    }
                }

                return Ok(new { data = result, code = SystemMessageMethod.code_success, msg = SystemMessageMethod.msg_success });
            }
            catch (System.Exception error)
            {
                return Ok(new
                {
                    data = "",
                    code = SystemMessageMethod.code_error,
                    msg = error
                });
            }
        }

        //   [HttpGet("[action]/{cType?}/{sID?}")]
        [HttpGet("GetAllowanceRequest/{cType?}/{sRequestID?}/{sProjectID?}/{sLocationID?}/{sReqStartDate?}/{sReqEndDate?}/{sMeetStartDate?}/{sMeetEndDate?}/{sStatusID?}")]
        public async Task<ActionResult> GETAllowanceRequest(string cType, string sRequestID, string sProjectID, string sReqStartDate, string sReqEndDate, string sMeetStartDate, string sMeetEndDate, string sStatusID)
        {
            try
            {
                // cType : M = my Approve เฉพาะ isHR && admin ที่รออนุมัติ, R = my Reqest ของตัวเอง ทุกสถานะ, A = Admin เฉพาะ isHR && admin ทุกสถานะ
                bool isRequest = !string.IsNullOrEmpty(cType) ? (cType == "R" ? true : false) : false;
                bool isApprove = !string.IsNullOrEmpty(cType) ? (cType == "M" ? true : false) : false;
                bool isAdmin = !string.IsNullOrEmpty(cType) ? (cType == "A" ? true : false) : false;
                bool isSearch = (!string.IsNullOrEmpty(sRequestID) || !string.IsNullOrEmpty(sProjectID)
                || !string.IsNullOrEmpty(sReqStartDate) || !string.IsNullOrEmpty(sReqEndDate)
                || !string.IsNullOrEmpty(sMeetStartDate) || !string.IsNullOrEmpty(sMeetEndDate)) ? true : false;
                int? nReqestID = string.IsNullOrEmpty(sRequestID) ? 12 : sRequestID.toIntNull();
                int? nProjectID = sProjectID.toIntNull();
                DateTime? dReqStart = sReqStartDate.ConvertStringToDateTimeNull("");
                DateTime? dReqEnd = sReqEndDate.ConvertStringToDateTimeNull("");
                DateTime? dMeetStartDate = sMeetStartDate.ConvertStringToDateTimeNull("");
                DateTime? dMeetEndDate = sMeetEndDate.ConvertStringToDateTimeNull("");

                Softthai_Intranet_2021Context db = new Softthai_Intranet_2021Context();
                List<TData_Time> lstTime = new List<TData_Time>();
                var lstProject = await db.TM_Project.Where(w => !w.IsDelete && w.IsActive).ToListAsync();
                var lstLocation = await db.TM_Location.Where(w => !w.IsDelete && w.IsActive).ToListAsync();
                var lstFlowProcess = await db.TM_FlowProcess.Where(w => w.IsActive).ToListAsync();


                var lstData = await (from req in db.T_Request_Allowance
                                     from emp in db.T_Employee
                                     where req.nCreate == emp.nEmployeeID && !req.IsDelete
                                     && (isRequest ? (req.nCreate == nReqestID) : isApprove ? (req.nFlowProcessID == 9) : true)
                                     && (isSearch ? (
                                         string.IsNullOrEmpty(sRequestID) ? true : (isRequest ? true : req.nCreate == nReqestID)
                                         && string.IsNullOrEmpty(sProjectID) ? true : (req.nProjectID == nProjectID)
                                     ) : true)
                                     select new TData_Allow
                                     {
                                         sRequestID = req.nAllowanceID + "",
                                         sRequestName = emp != null ? emp.sFirstname + " " + emp.sLastname : "",
                                         sProjectID = req.nProjectID + "",
                                         sProjectName = "",
                                         sLocationID = req.nLocationID + "",
                                         sLocationName = "",
                                         sTransactionDate = req.dCreate.DateString(),
                                         sMsgDetail = req.sMeetingDetail,
                                         sStatus = "",
                                         sTotal = "",
                                         nFlowProcessID = req.nFlowProcessID ?? 0,
                                     }).ToListAsync();

                foreach (var item in lstData)
                {
                    int nP = item.sProjectID.toIntNullToZero();
                    int nL = item.sLocationID.toIntNullToZero();
                    var qFlow = lstFlowProcess.FirstOrDefault(w => w.nFlowProcessID == item.nFlowProcessID);
                    if (qFlow != null) item.sStatus = qFlow.sFlowProcessName;
                    var qProject = lstProject.FirstOrDefault(w => w.nProjectID == nP);
                    if (qProject != null) item.sProjectName = qProject.sProjectName;
                    var qlocation = lstLocation.FirstOrDefault(w => w.nLocationID == nL);
                    if (qlocation != null) item.sLocationName = qlocation.sLocationName;
                }

                return Ok(new { data = lstData, code = SystemMessageMethod.code_success, msg = SystemMessageMethod.msg_success });
            }
            catch (System.Exception error)
            {
                return Ok(new
                {
                    data = "",
                    code = SystemMessageMethod.code_error,
                    msg = error
                });
            }
        }

        [HttpPost("DeleteData")]
        public AllClass.CResutlWebMethod DeleteData([FromBody] List<TData_Allow> lstData)
        {
            AllClass.CResutlWebMethod Result = new AllClass.CResutlWebMethod();
            try
            {
                #region Check จำนวนข้อมูลที่ทำการ Select
                if (lstData.Count > 0)
                {
                    foreach (var item in lstData)
                    {
                        Softthai_Intranet_2021Context db = new Softthai_Intranet_2021Context();
                        int nRequestID = item.sRequestID.toIntNullToZero();
                        var qData = db.T_Request_Allowance.FirstOrDefault(f => f.nAllowanceID == nRequestID);
                        if (qData != null)
                        {
                            qData.nDelete = 1;
                            qData.dDelete = DateTime.Now;
                            qData.IsDelete = true;
                            db.SaveChanges();
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

        #endregion

        #region SAVE     
        [HttpPost("PostSaveAllowancce")]
        public AllClass.CResutlWebMethod PostSaveAllowancce(TData_Allow item)
        {
            Softthai_Intranet_2021Context db = new Softthai_Intranet_2021Context();
            AllClass.CResutlWebMethod result = new AllClass.CResutlWebMethod();
            try
            {
                DateTime now = DateTime.Now;
                int nRequestID = 0; bool isNew = false;
                int nProjectID = 0; int nLocationID = 0;
                int nUserID = 12;
                DateTime? dStart = null; DateTime? dEnd = null;
                if (!string.IsNullOrEmpty(item.sLocationID))
                {
                    nLocationID = item.sLocationID.toIntNullToZero();
                }
                if (!string.IsNullOrEmpty(item.sProjectID))
                {
                    nProjectID = item.sProjectID.toIntNullToZero();
                }
                if (!string.IsNullOrEmpty(item.sStartDate))
                {
                    string[] str1 = item.sStartDate.Split("/");
                    string sNewDate1 = str1[1].PadLeft(2, '0') + "/" + str1[0].PadLeft(2, '0') + "/" + str1[2];
                    dStart = sNewDate1.ConvertStringToDateTimeNull("");
                }
                if (!string.IsNullOrEmpty(item.sEndDate))
                {
                    string[] str2 = item.sEndDate.Split("/");
                    string sNewDate2 = str2[1].PadLeft(2, '0') + "/" + str2[0].PadLeft(2, '0') + "/" + str2[2];
                    dEnd = sNewDate2.ConvertStringToDateTimeNull("");
                }

                T_Request_Allowance data = new T_Request_Allowance();
                if (string.IsNullOrEmpty(item.sRequestID))
                {
                    nRequestID = db.T_Request_Allowance.Any() ? db.T_Request_Allowance.Max(m => m.nAllowanceID) + 1 : 1;
                    isNew = true;
                }
                else
                {
                    nRequestID = item.sRequestID.toIntNullToZero();
                    data = db.T_Request_Allowance.FirstOrDefault(w => w.nAllowanceID == nRequestID);
                }

                data.nLocationID = nLocationID;
                data.nProjectID = nProjectID;
                data.sMeetingDetail = item.sMsgDetail;
                data.dUpdate = now;
                data.nUpdate = nUserID;
                data.nFlowProcessID = item.nStep == 0 ? 1 : 9; //Waiting HR
                data.nStep = item.nStep;
                data.IsDelete = false;
                data.dMeetingStart = dStart;
                data.dMeetingEnd = dEnd;

                if (isNew)
                {
                    // data.nAllowanceID = nRequestID;
                    data.dCreate = now;
                    data.nCreate = nUserID;
                    db.T_Request_Allowance.Add(data);
                }
                db.SaveChanges();

                if (isNew)
                {
                    /// หาไอดี 
                    nRequestID = db.T_Request_Allowance.Any() ? db.T_Request_Allowance.Max(m => m.nAllowanceID) : 0;
                }

                if (item.nStep != 0)
                {
                    T_Request_Allowance_Flow flow = new T_Request_Allowance_Flow();
                    flow.nAllowanceID = nRequestID;
                    // flow.nFlowID = 1;
                    flow.nRoleID = 7; // Admin
                    flow.nFlowProcessID = 9; //Waiting HR
                    flow.cApprove = "W";
                    flow.nCreate = nUserID;
                    flow.dCreate = now;
                    flow.nUpdate = nUserID;
                    flow.dUpdate = now;
                    flow.IsDelete = false;
                    db.T_Request_Allowance_Flow.Add(flow);
                    db.SaveChanges();
                }

                /// ลบอันเก่าไว้ก่อน
                db.T_Request_Allowance_Item.Where(w => w.nAllowanceID == nRequestID && !w.IsDelete).ToList().ForEach(f => f.IsDelete = true);
                db.SaveChanges();

                if (item.lstDataTime.Any())
                {
                    foreach (var i in item.lstDataTime)
                    {
                        T_Request_Allowance_Item dataItem = new T_Request_Allowance_Item();
                        int nItemID = !string.IsNullOrEmpty(i.id) ? i.id.toIntNullToZero() : 0;
                        bool isNewITem = false;
                        if (!isNew)
                        {
                            dataItem = db.T_Request_Allowance_Item.FirstOrDefault(w => w.nItemAllowanceID == nItemID && w.nAllowanceID == nRequestID);
                        }

                        if (dataItem == null || isNew)
                        {
                            dataItem = new T_Request_Allowance_Item();
                            nItemID = db.T_Request_Allowance_Item.Where(w => w.nAllowanceID == nRequestID).Any() ? db.T_Request_Allowance_Item.Where(w => w.nAllowanceID == nRequestID).Max(m => m.nAllowanceID) + 1 : 1;
                            isNewITem = true;
                        }
                        string[] strMeet = i.sDate.Split("/");
                        string sMeetDate = strMeet[0].PadLeft(2, '0') + "/" + strMeet[1].PadLeft(2, '0') + "/" + strMeet[2];
                        DateTime? dMeetting = sMeetDate.ConvertStringToDateTimeNull("");
                        dataItem.dMeetingt = dMeetting;
                        dataItem.IsOvernight = i.typetime == "O" ? true : false;
                        dataItem.IsRoundtrip = i.typetime == "R" ? true : false;
                        dataItem.IsDelete = false;
                        dataItem.dDelete = null;
                        dataItem.nDelete = null;
                        dataItem.sStartTime = i.starttime;
                        dataItem.sEndTime = i.endtime;
                        dataItem.nAmount = i.allowance.toDecimalNull();
                        dataItem.nExpenseID = i.typetime == "O" ? 4 : 3;
                        dataItem.dUpdate = now;
                        dataItem.nUpdate = nUserID;

                        if (isNewITem)
                        {
                            dataItem.nAllowanceID = nRequestID;
                            // dataItem.nItemAllowanceID = nItemID;
                            dataItem.nCreate = nUserID;
                            dataItem.dCreate = now;
                            db.T_Request_Allowance_Item.Add(dataItem);
                        }

                        db.SaveChanges();
                    }
                }
                Systemfunction.FlowRequest(nRequestID, 32, 0, "", "");
                result.sStatus = "Success";
            }
            catch (Exception e)
            {
                result.sMsg = e.Message;
                result.sStatus = "Failed";
            }


            return result;
        }

        [HttpPost("PostSaveAdmin")]
        public AllClass.CResutlWebMethod PostSaveAdmin(TData_Admin item)
        {
            AllClass.CResutlWebMethod result = new AllClass.CResutlWebMethod();
            Softthai_Intranet_2021Context db = new Softthai_Intranet_2021Context();
            try
            {
                DateTime now = DateTime.Now;
                int nRequestID = item.sRequestID.toIntNullToZero();
                var qData = db.T_Request_Allowance.FirstOrDefault(w => w.nAllowanceID == nRequestID && !w.IsDelete);
                if (qData != null)
                {
                    qData.nStep = item.nStep;
                    qData.dUpdate = now;
                    qData.nFlowProcessID = item.cApprove == "A" ? 11 : 10; //11 complete 10 cancel
                                                                           // Role 7 = admin
                    var dataflow = db.T_Request_Allowance_Flow.FirstOrDefault(w => w.nAllowanceID == nRequestID && w.nRoleID == 7);
                    if (dataflow != null)
                    {
                        dataflow.nApproveID = 1;
                        dataflow.dApprove = now;
                        dataflow.cApprove = item.cApprove;
                        dataflow.sComment = item.sComment;
                        dataflow.dUpdate = now;
                        dataflow.nUpdate = 1;
                    }

                    db.SaveChanges();

                    result.sStatus = "Success";
                }
                else
                {
                    result.sMsg = "Data not found.";
                    result.sStatus = "Failed";
                }
            }
            catch (Exception e)
            {
                result.sMsg = e.Message;
                result.sStatus = "Failed";
            }

            return result;
        }
        #endregion

        #region CLASS


        public class ResultData
        {
            public string sStatus { get; set; }
            public List<TData_Time> lstTime { get; set; }
        }

        public class TSearch_Date
        {
            public string sStartDate { get; set; }
            public string sEndDate { get; set; }
            public DateTime? dStart { get; set; }
        }

        public class TSearch_Data
        {
            public string cType { get; set; }
            public string sRequestID { get; set; }
            // public string sProjectID { get; set; }
            // public string sLocationID { get; set; }
            // public string sReqStartDate { get; set; }
            // public string sReqEndDate { get; set; }
            // public string sMeetStartDate { get; set; }
            // public string sMeetEndDate { get; set; }
        }

        public class TData_Time
        {
            public string id { get; set; }
            public string date { get; set; }
            public string typetime { get; set; }
            public string starttime { get; set; }
            public string endtime { get; set; }
            public string allowance { get; set; }
            public string sDate { get; set; }
            public DateTime? dMeet { get; set; }
        }

        public class TData_Allow
        {
            public string sRequestID { get; set; }
            public string sRequestName { get; set; }
            public string sProjectID { get; set; }
            public string sProjectName { get; set; }
            public string sLocationID { get; set; }
            public string sLocationName { get; set; }
            public string sTransactionDate { get; set; }
            public string sMsgDetail { get; set; }
            public string sStartDate { get; set; }
            public string sEndDate { get; set; }
            public int nStep { get; set; }
            public string sStatus { get; set; }
            public int nFlowProcessID { get; set; }
            public string sTotal { get; set; }
            public DateTime? dStart { get; set; }
            public List<TData_Time> lstDataTime { get; set; }
            public bool isReadOnly { get; set; }
            public string sMeetingDate { get; set; }
            public string sRequestDate { get; set; }
            public string sComment { get; set; }
        }

        public class TData_Admin
        {
            public string sRequestID { get; set; }
            public string cApprove { get; set; }
            public string sComment { get; set; }
            public int nStep { get; set; }
        }
        #endregion
    }
}