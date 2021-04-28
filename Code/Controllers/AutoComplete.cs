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

namespace PTT_GSM_CSC_Factory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoComplete : ControllerBase
    {
        public IConfiguration _Configuration { get; }
        private readonly IHostEnvironment _env;
        private readonly PTT_GSM_CSC_Factory_Context _db;
        // private readonly IAuthentication _Auth;

        // private UserAccount user;

        public AutoComplete(IConfiguration Configuration, IHostEnvironment env)
        {
            _Configuration = Configuration;
            _env = env;
            // _Auth = auth;
            _db = new PTT_GSM_CSC_Factory_Context();
            // user = _Auth.GetUserAccount();
        }

        [HttpGet("[action]/{label?}")]
        public ActionResult GetDistributionName(string label)
        {
            var lstData = _db.SAP_CUST_GENERAL_VIEW.Where(w => w.CUST_NAME.Contains(label)).OrderBy(o => o.CUST_NAME).Select(s => new
            {
                value = s.CUST_NUMBER,
                label = s.CUST_NAME
            }).Take(10).ToList();
            return Ok(lstData);
        }
    }
}