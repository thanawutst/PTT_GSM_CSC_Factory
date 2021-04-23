using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftthaiIntranet.Models.Softthai_Intranet_2021;
using Newtonsoft.Json;
using SoftthaiIntranet.Extensions;

//sun
namespace SoftthaiIntranet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class _AcceptWorkController : ControllerBase
    {
        private Softthai_Intranet_2021Context _db;
        public _AcceptWorkController()
        {
            _db = new Softthai_Intranet_2021Context();
        }
        [HttpGet("GetConditionWorkTime")]
        public ActionResult GetConditionWorkTime()
        {
            Softthai_Intranet_2021Context db = new Softthai_Intranet_2021Context();

            var lstCondition = db.TM_Condition_AcceptWorkTime.Where(w => w.IsActive == true && w.IsDelete == false).ToList();
            // string JData = JsonConvert.SerializeObject(lstCondition);
            return Ok(lstCondition);
        }
        [HttpGet("getStatus")]
        public ActionResult getStatus()
        {
            Softthai_Intranet_2021Context db = new Softthai_Intranet_2021Context();

            var lstStatus = db.TM_FlowProcess.Where(w => w.IsActive == true).OrderBy(o => o.nOrder).ToList();
            // string JData = JsonConvert.SerializeObject(lstStatus);
            return Ok(lstStatus);
        }
        [HttpGet("GetRequester")]
        public ActionResult GetRequester()
        {
            Softthai_Intranet_2021Context db = new Softthai_Intranet_2021Context();

            var lstRequester = db.T_Employee.Where(w => w.IsActive == true && w.IsDel == false).ToList();
            return Ok(lstRequester);
        }
        [HttpGet("GetProject")]
        public ActionResult GetProject([FromQuery] string Emp)
        {
            string JData = "";
            List<TM_Project> lstProject = new List<TM_Project>();
            Softthai_Intranet_2021Context db = new Softthai_Intranet_2021Context();
            if (!string.IsNullOrEmpty(Emp))
            {
                var oEmployee = db.T_Employee.FirstOrDefault(f => f.nEmployeeID == Int32.Parse(Emp));
                if (oEmployee != null)
                {
                    if (oEmployee.nRoleID != 7)
                    {
                        var lstProjectID = db.TM_ProjectMember.Where(w => w.nEmployeeID == oEmployee.nEmployeeID && w.IsDelete == false).Select(s => s.nProjectID);
                        lstProject = db.TM_Project.Where(w => lstProjectID.Contains(w.nProjectID)).ToList();

                        JData = lstProject.Count > 0 ? JsonConvert.SerializeObject(lstProject) : JsonConvert.SerializeObject("");
                    }
                    else
                    {
                        lstProject = db.TM_Project.Where(w => w.IsDelete == false && w.IsActive == true).ToList();
                        JData = lstProject.Count > 0 ? JsonConvert.SerializeObject(lstProject) : JsonConvert.SerializeObject("");

                    }
                }
            }
            return Ok(lstProject);
        }
        [HttpGet("GetEdit")]
        public ActionResult GetEdit([FromQuery] string ID)
        {
            string JData = "";
            T_Request_AcceptWorkTime oAcceptwork = new T_Request_AcceptWorkTime();
            Softthai_Intranet_2021Context db = new Softthai_Intranet_2021Context();
            if (!string.IsNullOrEmpty(ID))
            {
                oAcceptwork = db.T_Request_AcceptWorkTime.FirstOrDefault(f => f.nAcceptWorkTimeID == Int32.Parse(ID));
                if (oAcceptwork != null)
                {
                    JData = JsonConvert.SerializeObject(oAcceptwork);
                }
                else
                {
                    JData = JsonConvert.SerializeObject("");
                }
            }
            return Ok(oAcceptwork);
        }

        [HttpPost("SaveWorkTime")]
        async public Task<ActionResult> SaveWorkTime(CSaveWork Data)
        {
            try
            {
                int? nID = Data.nAcceptWorkTimeID == "" ? null : Int32.Parse(Data.nAcceptWorkTimeID);
                Softthai_Intranet_2021Context db = new Softthai_Intranet_2021Context();
                var oData = db.T_Request_AcceptWorkTime.FirstOrDefault(w => w.IsActive == true && !w.IsDelete && w.nAcceptWorkTimeID == nID);
                if (oData == null)
                {
                    var Create = new T_Request_AcceptWorkTime();
                    Create.nProjectID = Int32.Parse(Data.arrProject);
                    Create.dStartOverTime = ParseDateTime(Data.StartDate).Date;
                    Create.dEndOverTime = ParseDateTime(Data.EndDate).Date;
                    Create.sStartTime = ParseDateTime(Data.StartTime).ToString("HH:mm");
                    Create.sEndTime = ParseDateTime(Data.EndTime).ToString("HH:mm");
                    Create.nActualMinute = !string.IsNullOrEmpty(Data.ActualMinute) ? Int32.Parse(Data.ActualMinute) : 0;
                    Create.nAwtConditionID = !string.IsNullOrEmpty(Data.Timeattendance) ? Int32.Parse(Data.Timeattendance) : 0;
                    Create.nProjectID = !string.IsNullOrEmpty(Data.arrProject) ? Int32.Parse(Data.arrProject) : 0;
                    Create.nExpenseID = !string.IsNullOrEmpty(Data.benefit) ? Int32.Parse(Data.benefit) : 0;
                    Create.sDetailOfWork = Data.Descrition;
                    Create.dCertification = ParseDateTime(Data.CerDate);
                    Create.IsActive = true;
                    Create.IsDelete = false;
                    Create.nCreate = 0;
                    Create.dCreate = DateTime.Now;
                    Create.nUpdate = 0;
                    Create.dUpdate = DateTime.Now;
                    db.T_Request_AcceptWorkTime.Add(Create);
                    await db.SaveChangesAsync();
                }
                else
                {
                    oData.nProjectID = Int32.Parse(Data.arrProject);
                    oData.dStartOverTime = ParseDateTime(Data.StartDate).Date;
                    oData.dEndOverTime = ParseDateTime(Data.EndDate).Date;
                    oData.sStartTime = ParseDateTime(Data.StartTime).ToString("HH:mm");
                    oData.sEndTime = ParseDateTime(Data.EndTime).ToString("HH:mm");
                    oData.nActualMinute = !string.IsNullOrEmpty(Data.ActualMinute) ? Int32.Parse(Data.ActualMinute) : 0;
                    oData.nAwtConditionID = !string.IsNullOrEmpty(Data.Timeattendance) ? Int32.Parse(Data.Timeattendance) : 0;
                    oData.nProjectID = !string.IsNullOrEmpty(Data.arrProject) ? Int32.Parse(Data.arrProject) : 0;
                    oData.nExpenseID = !string.IsNullOrEmpty(Data.benefit) ? Int32.Parse(Data.benefit) : 0;
                    oData.sDetailOfWork = Data.Descrition;
                    oData.dCertification = ParseDateTime(Data.CerDate);
                    oData.nCreate = 32;
                    oData.dCreate = DateTime.Now;
                    oData.nUpdate = 0;
                    oData.dUpdate = DateTime.Now;
                    oData.IsDelete = false;
                    await db.SaveChangesAsync();
                }
                Systemfunction.FlowRequest(1,oData.nAcceptWorkTimeID,null,null,null);
                return Ok("Success");
            }
            catch (Exception e)
            {
                return Ok(e.ToString());
            }
        }
        [HttpPost("SearchData")]
        public ActionResult SearchData([FromBody] CSearch oSearch)
        {
            Softthai_Intranet_2021Context db = new Softthai_Intranet_2021Context();
            string Condition = "";
            if (!string.IsNullOrEmpty(oSearch.arrProject))
            {
                Condition += "and tr.nProjectID = " + Systemfunction.SQL_String(oSearch.arrProject) + "";
            }
            if (oSearch.OvertimeDate != null)
            {
                if (!string.IsNullOrEmpty(oSearch.OvertimeDate[0]))
                {
                    Condition += "and tr.dStartOverTime >= " + Systemfunction.SQL_String(ParseDateTime(oSearch.OvertimeDate[0]).ToString("yyyy-MM-dd")) + "";

                }

                if (!string.IsNullOrEmpty(oSearch.OvertimeDate[1]))
                {
                    Condition += "and  tr.dStartOverTime <= " + Systemfunction.SQL_String(ParseDateTime(oSearch.OvertimeDate[1]).ToString("yyyy-MM-dd")) + "";

                }
            }
            if (oSearch.CerDate != null)
            {
                if (!string.IsNullOrEmpty(oSearch.CerDate[0]))
                {
                    Condition += "and  tr.dCertification >= " + Systemfunction.SQL_String(ParseDateTime(oSearch.CerDate[0]).ToString("yyyy-MM-dd")) + "";

                }

                if (!string.IsNullOrEmpty(oSearch.CerDate[1]))
                {
                    Condition += "and  tr.dCertification <= " + Systemfunction.SQL_String(ParseDateTime(oSearch.CerDate[1]).ToString("yyyy-MM-dd")) + "";

                }
            }
            if (!string.IsNullOrEmpty(oSearch.Status))
            {
                Condition += "and tr.nFlowProcessID = " + Systemfunction.SQL_String(oSearch.Status) + "";
            }
            if (!string.IsNullOrEmpty(oSearch.Timeattendance))
            {
                Condition += "and tr.nAwtConditionID = " + Systemfunction.SQL_String(oSearch.Timeattendance) + "";
            }
            if (!string.IsNullOrEmpty(oSearch.Requester))
            {
                Condition += "and tr.nCreate = " + Systemfunction.SQL_String(oSearch.Requester) + "";
            }

            var sql = @"select tr.nAcceptWorkTimeID  nAcceptWorkTimeID,
tp.sProjectName sProjectName,
tey.sFirstname + ' ' + tey.sLastname Requester,
tr.dStartOverTime OvertimeDate,
tr.dCertification DateofCertification,
tr.dCreate RequestDate,
tc.sCertificateTitle Attend,
tc.sCertificateTitle,
tf.nFlowProcessID Status
from T_Request_AcceptWorkTime tr
left join TM_Project tp on tp.nProjectID = tr.nProjectID
left join TM_Condition_AcceptWorkTime tc on tc.nAwtConditionID = tr.nAwtConditionID
left join TM_Expense te on tr.nExpenseID = te.nExpenseID
left join TM_FlowProcess tf on tr.nFlowProcessID = tf.nFlowProcessID
left join T_Employee tey on tey.nEmployeeID = tr.nCreate where 1 =  1 and tr.IsDelete = 0 " + Condition + "";

            var Json = Systemfunction.QryToJson(sql, "");
            if (!string.IsNullOrEmpty(Json))
            {
                var obj = JsonConvert.DeserializeObject<List<CSearchResult>>(Json);
                return Ok(obj);
            }
            // var lstData = (from c in db.T_Request_AcceptWorkTime.Where(w => w.IsDelete == false)
            //                from b in db.TM_Project.Where(w => w.nProjectID == c.nProjectID)
            //                from d in db.TM_Condition_AcceptWorkTime.Where(w => w.nAwtConditionID == c.nAwtConditionID)
            //                from f in db.TM_Expense.Where(w => w.nExpenseID == c.nExpenseID)
            //                select new CReturnData
            //                {
            //                    nAcceptWorkTimeID = c.nAcceptWorkTimeID,
            //                    sProjectName = b.sProjectName,
            //                    Requester = c.nCreate + "",
            //                    //     OvertimeDate = c.dOverTimeDate,
            //                    DateofCertification = c.dCertification,
            //                    RequestDate = c.dCreate,
            //                    Attend = d.sCertificateTitle,
            //                    Status = ""

            //                }).ToList();

            return Ok("");
        }
        [HttpPost("DelAcceptWork")]
        public ActionResult DelAcceptWork(CDel Data)
        {
            try
            {
                Softthai_Intranet_2021Context db = new Softthai_Intranet_2021Context();
                var lstData = db.T_Request_AcceptWorkTime.Where(f => Data.ID.Contains(f.nAcceptWorkTimeID.ToString())).ToList();
                if (lstData.Count > 0)
                {
                    lstData.ForEach(f =>
                    {
                        f.IsDelete = true;
                    });
                }
                db.SaveChanges();
                return Ok("Success");
            }
            catch (Exception e)
            {

                return Ok(e.ToString());
            }
        }
        [HttpGet("GetDataApprove")]
        public ActionResult GetDataApprove([FromQuery] string ID)
        {
            var Json = "";
            var sql = @"select tr.nAcceptWorkTimeID  nAcceptWorkTimeID,
tp.sProjectName sProjectName,
tr.sDetailOfWork,
te.sExpenseName + ' ('+ str(te.nAmount,3) + ' baht' + ')' as sExpenseName,
tr.dStartOverTime dStartOverTime,
tr.dEndOverTime dEndOverTime,
tr.dCertification DateofCertification,
tr.dCreate RequestDate,
tr.sStartTime,
tr.sEndTime,
tc.sCertificateTitle Attend,
tc.sCertificateTitle,
tf.nFlowProcessID sStatus
from T_Request_AcceptWorkTime tr
left join TM_Project tp on tp.nProjectID = tr.nProjectID
left join TM_Condition_AcceptWorkTime tc on tc.nAwtConditionID = tr.nAwtConditionID
left join TM_Expense te on tr.nExpenseID = te.nExpenseID
left join TM_FlowProcess tf on tr.nFlowProcessID = tf.nFlowProcessID
left join T_Employee tey on tey.nEmployeeID = tr.nCreate where 1 =  1 and tr.nAcceptWorkTimeID = " + Systemfunction.SQL_String(ID) + "";
            Json = Systemfunction.QryToJson(sql, "");
            var obj = JsonConvert.DeserializeObject<List<CGetApporve>>(Json);
            return Ok(obj);
        }
        public static DateTime ParseDateTime(string Date)
        {
            var Result = DateTime.TryParse(Date, out DateTime date);

            return date;
        }
        public class CSaveWork
        {
            public string nAcceptWorkTimeID { get; set; }
            public string arrProject { get; set; }
            public string dRequestDate { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public string EndDate { get; set; }
            public string StartDate { get; set; }
            public string Descrition { get; set; }
            public string CerDate { get; set; }
            public string benefit { get; set; }
            public string Timeattendance { get; set; }
            public string ActualMinute { get; set; }

        }
        public class CSearch
        {
            public string arrProject { get; set; }
            public string[] OvertimeDate { get; set; }
            public string[] CerDate { get; set; }
            public string Timeattendance { get; set; }
            public string Status { get; set; }
            public string EmployeeID { get; set; }

            public string Requester { get; set; }
        }
        public class CSearchResult
        {
            public int? nAcceptWorkTimeID { get; set; }
            public string sProjectName { get; set; }
            public string Requester { get; set; }
            public DateTime? OvertimeDate { get; set; }

            public DateTime? DateofCertification { get; set; }
            public DateTime? RequestDate { get; set; }
            public string Attend { get; set; }

            public string sCertificateTitle { get; set; }
            public string Status { get; set; }
        }
        public class CReturnData
        {
            public int nAcceptWorkTimeID { get; set; }
            public string sProjectName { get; set; }
            public string Requester { get; set; }
            public DateTime? OvertimeDate { get; set; }
            public DateTime? DateofCertification { get; set; }
            public DateTime? RequestDate { get; set; }
            public string Attend { get; set; }
            public string Status { get; set; }
        }
        public class CDel
        {
            public string[] ID { get; set; }
        }
        public class CGetApporve
        {
            public int? nAcceptWorkTimeID { get; set; }
            public string sProjectName { get; set; }
            public string sDetailOfWork { get; set; }
            public string sExpenseName { get; set; }
            public DateTime dStartOverTime { get; set; }
            public DateTime dEndOverTime { get; set; }
            public DateTime DateofCertification { get; set; }
            public DateTime RequestDate { get; set; }
            public string sStartTime { get; set; }
            public string sEndTime { get; set; }
            public string Attend { get; set; }
            public string sCertificateTitle { get; set; }
            public int? sStatus { get; set; }
        }
    }
}
