using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PTT_GSM_CSC_Factory.Models.FactoryDB;
using PTT_GSM_CSC_Factory.Extensions;

namespace PTT_GSM_CSC_Factory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackOfficeController : ControllerBase
    {
        public IConfiguration _Configuration { get; }
        private readonly IHostEnvironment _env;
        private readonly PTT_GSM_CSC_Factory_Context _db;
        // private readonly IAuthentication _Auth;

        // private UserAccount user;

        public BackOfficeController(IConfiguration Configuration, IHostEnvironment env)
        {
            _Configuration = Configuration;
            _env = env;
            // _Auth = auth;
            _db = new PTT_GSM_CSC_Factory_Context();
            // user = _Auth.GetUserAccount();
        }

        [HttpGet("[action]/{id?}")]
        public ActionResult GetDistributionDetail(string id)
        {
            SAP_CUST_GENERAL_VIEW obj = _db.SAP_CUST_GENERAL_VIEW.Where(w => w.CUST_NUMBER == id).FirstOrDefault();
            return Ok(obj);
        }

        [HttpGet("[action]/{id?}")]
        public ActionResult GetDistributorDetail(int id)
        {
            TM_Distributor obj = _db.TM_Distributor.Where(w => w.nDistributorID == id).FirstOrDefault();
            return Ok(obj);
        }

        [HttpGet("[action]/{id?}")]
        public ActionResult GetDistributorStaff(int id)
        {
            List<TM_Staff> obj = _db.TM_Staff.Where(w => w.nDistributorID == id).ToList();
            return Ok(obj);
        }

        [HttpGet("[action]")]
        public ActionResult GetDistributorList()
        {
            List<lstDistributor> lstDist = _db.TM_Distributor.Where(w => w.cDel != "Y").Select(s => new lstDistributor
            {
                sDistributorName = s.sDistributorName,
                sSoldTo = s.sSoldTo,
                cStatus = s.cStatus,
                nStaff = 0,
                nDistributorID = s.nDistributorID,
            }).ToList();

            foreach (var item in lstDist)
            {
                item.nStaff = _db.TM_Staff.Where(w => w.nDistributorID == item.nDistributorID).Count();
            }

            return Ok(lstDist);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SaveDistributor(objSaveDistributor model)
        {
            try
            {
                TM_Distributor hasRec = _db.TM_Distributor.Where(w => w.nDistributorID == model.nDistributorID).FirstOrDefault();
                int DistID = 0;
                //Add
                if (hasRec == null)
                {
                    DistID = await _db.TM_Distributor.CountAsync() + 1;
                    //Add
                    TM_Distributor addPJ = new TM_Distributor();
                    addPJ.nDistributorID = DistID;
                    addPJ.sEmail = model.sEmail;
                    addPJ.sDistributorCode = model.sDistributorCode;
                    addPJ.sDistributorName = model.sDistributorName;
                    addPJ.sCode = model.sCode;
                    addPJ.sSoldTo = model.sSoldTo;
                    addPJ.sAddressSoldTo = model.sAddressSoldTo;
                    addPJ.sDistrict = model.sDistrict;
                    addPJ.sMeterName = model.sMeterName;
                    addPJ.sABDistributorName = model.sABDistributorName;
                    addPJ.sWebsite = model.sWebsite;
                    addPJ.sLogoName = model.sLogoName;
                    addPJ.sSysLogoName = model.sSysLogoName;
                    addPJ.sLogoPath = model.sLogoPath;
                    addPJ.sOrgChartName = model.sOrgChartName;
                    addPJ.sSysOrgChartName = model.sSysOrgChartName;
                    addPJ.sOrgChartPath = model.sOrgChartPath;
                    addPJ.sDetail = model.sDetail;
                    addPJ.cStatus = model.cStatus;
                    addPJ.dAdd = DateTime.Now;
                    addPJ.cDel = "N";

                    await _db.TM_Distributor.AddAsync(addPJ);
                    await _db.SaveChangesAsync();
                }
                else //Edit
                {
                    DistID = hasRec.nDistributorID;
                    hasRec.sEmail = model.sEmail;
                    hasRec.sDistributorCode = model.sDistributorCode;
                    hasRec.sDistributorName = model.sDistributorName;
                    hasRec.sCode = model.sCode;
                    hasRec.sSoldTo = model.sSoldTo;
                    hasRec.sAddressSoldTo = model.sAddressSoldTo;
                    hasRec.sDistrict = model.sDistrict;
                    hasRec.sMeterName = model.sMeterName;
                    hasRec.sABDistributorName = model.sABDistributorName;
                    hasRec.sWebsite = model.sWebsite;
                    hasRec.sLogoName = model.sLogoName;
                    hasRec.sSysLogoName = model.sSysLogoName;
                    hasRec.sLogoPath = model.sLogoPath;
                    hasRec.sOrgChartName = model.sOrgChartName;
                    hasRec.sSysOrgChartName = model.sSysOrgChartName;
                    hasRec.sOrgChartPath = model.sOrgChartPath;
                    hasRec.sDetail = model.sDetail;
                    hasRec.cStatus = model.cStatus;
                    hasRec.dUpdate = DateTime.Now;

                    await _db.SaveChangesAsync();

                    //Delete old Staff
                    _db.TM_Staff.Where(w => w.nDistributorID == model.nDistributorID).ToList().ForEach(i =>
                    {
                        _db.TM_Staff.Remove(i);
                    });
                    await _db.SaveChangesAsync();
                }

                //Add Staff
                foreach (var item in model.arrStaff)
                {
                    TM_Staff st = new TM_Staff();
                    st.nDistributorID = DistID;
                    st.sAvatarName = item.sAvatarName;
                    st.sAvatarPath = item.sAvatarPath;
                    st.sAvatarSysFileName = item.sAvatarSysFileName;
                    st.sName = item.sName;
                    st.sUserName = item.sUserName;
                    st.sPassword = item.sPassword;
                    st.sEmail = item.sEmail;
                    st.cStatus = item.cStatus;

                    await _db.TM_Staff.AddAsync(st);
                    await _db.SaveChangesAsync();
                }
                return Ok(new { code = SystemMessageMethod.code_success });
            }
            catch (Exception ex)
            {
                string msgEx = ex.Message;
                return Ok(new { code = SystemMessageMethod.code_error });
            }
        }

        public class objSaveDistributor : TM_Distributor
        {
            public List<TM_Staff> arrStaff { get; set; }
        }
        public class lstDistributor : TM_Distributor
        {
            public int nStaff { get; set; }
        }
    }
}