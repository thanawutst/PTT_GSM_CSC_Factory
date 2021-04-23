using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftthaiIntranet.Models.Softthai_Intranet_2021;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using SoftthaiIntranet.Interfaces;
using  SoftthaiIntranet.Models.SystemModels.AllClass;
using SoftthaiIntranet.Models.SystemModels;
using SoftthaiIntranet.Extensions;
namespace SoftthaiIntranet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : Controller
    {

        private readonly IAuthentication _Auth;
        public HolidayController(IAuthentication auth)
        {
            _Auth = auth;
        }
        public Softthai_Intranet_2021Context db = new Softthai_Intranet_2021Context();

        [HttpPost("Savedata")]
        public AllClass.CResutlWebMethod Savedata(lstHolidaySavedata data)
        {
            AllClass.CResutlWebMethod reuslt = new AllClass.CResutlWebMethod();
            try
            {
                db = new Softthai_Intranet_2021Context();
                List<TM_Holiday> LstData = new List<TM_Holiday>();

                var Qsavedata = db.TM_Holiday.Where(w => w.dHolidayDate == data.dDateID.Date).FirstOrDefault();
                if (Qsavedata != null)
                {
                    Qsavedata.IsSubstitutionDay = data.IsSubstitutionDayID; //1 = Holiday compensation, 0 = Public holiday
                    Qsavedata.sHolidayName = data.sHolidayNameID;
                    Qsavedata.nUpdate = null;
                    Qsavedata.dHolidayDate = data.dDateID.Date;
                    Qsavedata.dUpdate = DateTime.Now;
                    Qsavedata.IsActive = data.nStatusID == "1" ? true : false;  //1 = Active , 0 = Not Active
                }
                else
                {
                    TM_Holiday s = new TM_Holiday();
                    s.sHolidayName = data.sHolidayNameID;
                    s.dHolidayDate = data.dDateID.Date;
                    s.IsSubstitutionDay = data.IsSubstitutionDayID; //1 = Holiday compensation, 0 = Public holiday
                    s.nCreate = null;
                    s.IsActive = data.nStatusID == "1" ? true : false; //1 = Active , 0 = Not Active
                    s.dCreate = DateTime.Now;

                    db.TM_Holiday.Add(s);
                }
                db.SaveChanges();

                reuslt.sStatus = Systemfunction.process_Success();
                return reuslt;
            }
            catch (Exception er)
            {
                reuslt.sMsg = er.Message;
                reuslt.sStatus = Systemfunction.process_Failed();

                return reuslt;
            }
        }

        [HttpPost("Getdata")]
        public lstTM_Holiday Getdata(lstHolidaygetdata data)
        {
            lstTM_Holiday reuslt = new lstTM_Holiday();
            try
            {
                db = new Softthai_Intranet_2021Context();
                List<lstTableDetail> lstdata = new List<lstTableDetail>();
                var Qdata = db.TM_Holiday.Where(w => w.IsDelete == false && data.nYear.HasValue ? data.nYear == w.dHolidayDate.Value.Year : true).ToList();

                foreach (var i in Qdata)
                {
                    lstTableDetail s = new lstTableDetail();
                    s.sHolidayName = i.sHolidayName;
                    s.nHolidayID = i.nHolidayID;
                    s.IsSubstitutionDay = i.IsSubstitutionDay;
                    s.IsActive = i.IsActive;
                    s.dHolidayDate = i.dHolidayDate;
                    lstdata.Add(s);
                }
                reuslt.lstData = lstdata;
                reuslt.sStatus = Systemfunction.process_Success();
                return reuslt;
            }
            catch (Exception er)
            {
                reuslt.sMsg = er.Message;
                reuslt.sStatus = Systemfunction.process_Failed();
                return reuslt;
            }

        }
        [HttpPost("Getdata_datail")]
        public lstTM_Holiday Getdata_datail(lstHolidaygetdata_datail data)
        {
            lstTM_Holiday reuslt = new lstTM_Holiday();
            try
            {
                db = new Softthai_Intranet_2021Context();
                List<lstTableDetail> lstdata = new List<lstTableDetail>();
                var Qdata = db.TM_Holiday.FirstOrDefault(w => w.nHolidayID == data.nHolidayID);


                lstTableDetail s = new lstTableDetail();
                s.sHolidayName = Qdata.sHolidayName;
                s.nHolidayID = Qdata.nHolidayID;
                s.IsSubstitutionDay = Qdata.IsSubstitutionDay;
                s.IsActive = Qdata.IsActive;
                s.dHolidayDate = Qdata.dHolidayDate;
                lstdata.Add(s);

                reuslt.lstData = lstdata;
                reuslt.sStatus = Systemfunction.process_Success();
                return reuslt;
            }
            catch (Exception er)
            {
                reuslt.sMsg = er.Message;
                reuslt.sStatus = Systemfunction.process_Failed();
                return reuslt;
            }

        }
        [HttpPost("DelGetdata_list")]
        public lstTM_Holiday DelGetdata_list(DellstHolidaygetdata_list data)
        {
            lstTM_Holiday reuslt = new lstTM_Holiday();
            try
            {
                db = new Softthai_Intranet_2021Context();
                List<lstTableDetail> lstdata = new List<lstTableDetail>();
                UserAccount user = new UserAccount();
                if (_Auth.IsHasExists())
                {
                    var Qdata = db.TM_Holiday.Where(w => data.nHolidayID.Contains(w.nHolidayID)).ToList();

                    user = _Auth.GetUserAccount();

                    foreach (var i in Qdata)
                    {
                        i.IsDelete = true;
                        i.dDelete = DateTime.Now;
                        i.nDelete = user.sUserID.toIntNull();

                        db.SaveChanges();
                    }
                    reuslt.sStatus = Systemfunction.process_Success();
                }
                return reuslt;
            }
            catch (Exception er)
            {
                reuslt.sMsg = er.Message;
                reuslt.sStatus = Systemfunction.process_Failed();
                return reuslt;
            }

        }
        public class lstHolidaySavedata
        {
            public DateTime dDateID { get; set; }
            public string sHolidayNameID { get; set; }
            public string nStatusID { get; set; }
            public bool IsSubstitutionDayID { get; set; }
        }
        public class lstHolidaygetdata
        {
            public int? nYear { get; set; }
        }
        public class lstHolidaygetdata_datail
        {
            public int nHolidayID { get; set; }
        }
        public class DellstHolidaygetdata_list
        {
            public List<int> nHolidayID { get; set; }
        }
        public class lstTM_Holiday : AllClass.CResutlWebMethod
        {
            public List<lstTableDetail> lstData { get; set; }
        }
        public class lstTableDetail
        {
            public int nHolidayID { get; set; }
            public string sHolidayName { get; set; }
            public DateTime? dHolidayDate { get; set; }
            public bool? IsSubstitutionDay { get; set; }
            public int? nCreate { get; set; }
            public string dCreate { get; set; }
            public int? nUpdate { get; set; }
            public string dUpdate { get; set; }
            public bool IsActive { get; set; }
            public int? nDelete { get; set; }
            public string dDelete { get; set; }
            public bool IsDelete { get; set; }
        }
    }
}
