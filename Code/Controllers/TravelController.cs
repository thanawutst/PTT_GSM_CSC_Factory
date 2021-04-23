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
    public class TravelController : ControllerBase
    {
        public IConfiguration _Configuration { get; }
        private readonly IHostEnvironment _env;
        private readonly Softthai_Intranet_2021Context _db;
        private readonly IAuthentication _Auth;

        private UserAccount user;

        public TravelController(IConfiguration Configuration, IHostEnvironment env, IAuthentication auth)
        {
            _Configuration = Configuration;
            _env = env;
            _Auth = auth;
            _db = new Softthai_Intranet_2021Context();
            user = _Auth.GetUserAccount();
        }

        [HttpGet]
        public ActionResult GetProjectList()
        {
            var lst = _db.TM_Project.Where(w => w.IsActive != false && w.IsDelete != true).Select(s =>
            new Dropdown
            {
                label = s.sProjectName,
                value = s.nProjectID + ""
            }).ToList();
            return Ok(lst);
        }

        [HttpGet]
        public ActionResult GetTravelList()
        {
            var lst = _db.TM_Destination.Where(w => w.IsActive != false && w.IsDelete != true).ToList().OrderBy(o => o.nDestinationID);
            return Ok(lst);
        }

        [HttpGet("{nReqTravelID?}")]
        public ActionResult GetTravelItemList(int nReqTravelID)
        {
            var lst = _db.T_Request_Travel_Item.Where(w => w.nReqTravelID == nReqTravelID).Select(s => new objTravelItem
            {
                cOtherFrom = false,
                cOtherTo = false,
                cRoundTrip = s.IsRoundTrip,
                dTravelDate = s.dTravelDate != null ? s.dTravelDate.Value.ToString("dd/MM/yyyy", new CultureInfo("en-US")) : "",
                id = s.nReqTravelID,
                nAmount = s.nAmount,
                nPriceKilometer = s.nPriceKilometer,
                nTollway = s.nTollway,
                sFrom = s.nLocationFromID.ToString(),
                sProject = s.nProjectID.ToString(),
                sTo = s.nLocationToID.ToString(),
                sVehicle = s.nTravelID.ToString(),
            }).ToList();

            foreach (var item in lst)
            {
                item.dRequestDate = _db.T_Request_Travel.Where(w => w.nReqTravelID == nReqTravelID).Select(s => s.dRequest).FirstOrDefault();
                item.sFrom = _db.TM_Destination.Where(w => w.nDestinationID == Convert.ToInt32(item.sFrom)).Select(s => s.sDestinationName).FirstOrDefault();
                item.sProject = _db.TM_Project.Where(w => w.nProjectID == Convert.ToInt32(item.sProject)).Select(s => s.sProjectName).FirstOrDefault();
                item.sTo = _db.TM_Destination.Where(w => w.nDestinationID == Convert.ToInt32(item.sTo)).Select(s => s.sDestinationName).FirstOrDefault();
                item.sVehicle = _db.TM_Travel.Where(w => w.nTravelID == Convert.ToInt32(item.sVehicle)).Select(s => s.sTravelName).FirstOrDefault();
            }

            return Ok(lst);
        }

        [HttpGet]
        public ActionResult GetVehicleList()
        {
            var lst = _db.TM_Travel.Where(w => w.IsActive != false && w.IsDelete != true).ToList().OrderBy(o => o.nOrder);
            return Ok(lst);
        }

        [HttpGet]
        public ActionResult GetReqTravelCount()
        {
            int count = _db.T_Request_Travel.Where(w => w.nCreate == Convert.ToInt16(user.sUserID) && w.dCreate.Value.Month == DateTime.Now.Month && w.IsDelete != true).Count();
            return Ok(count);
        }

        [HttpGet]
        public ActionResult GetEmployee()
        {
            List<Dropdown> lst = _db.T_Employee.Where(w => w.IsActive == true && w.IsDel != true).OrderBy(o => o.sFirstname).Select(s => new Dropdown
            {
                label = s.sFirstname + " " + s.sLastname,
                value = s.nEmployeeID + ""
            }).ToList();
            return Ok(lst);
        }

        [HttpGet]
        public ActionResult GetTravelReqList()
        {
            if (_Auth.IsHasExists())
            {
                List<TravelList> lst = _db.T_Request_Travel.Where(w => w.nCreate == Convert.ToInt16(user.sUserID) && w.IsDelete != true).OrderByDescending(o => o.dCreate).Select(s => new TravelList
                {
                    nReqTravelID = s.nReqTravelID,
                    nYear = s.dRequest.Value.Year,
                    sMonth = s.dRequest.Value.ToString("MMMM", new CultureInfo("en-US")),
                    nItem = 0,
                    sRequester = s.nCreate.ToString(),
                    dRequestDate = s.dRequest.Value.ToString("dd/MM/yyyy", new CultureInfo("en-US")),
                    sStatus = s.nFlowProcessID == 1 ? "Draft" : s.nFlowProcessID == 9 ? "Submit" : "",
                }).ToList();

                foreach (var item in lst)
                {
                    item.nItem = _db.T_Request_Travel_Item.Where(w => w.nReqTravelID == item.nReqTravelID).Count();
                    item.sRequester = _db.T_Employee.Where(w => w.nEmployeeID == Convert.ToInt16(item.sRequester)).Select(s => s.sUsername).FirstOrDefault();
                }
                return Ok(lst);
            }
            else
            {
                return Ok();
            }


        }

        [HttpPost]
        public async Task<IActionResult> SaveTravel(List<objTravel> model)
        {
            try
            {
                // _db.T_Request_Travel_Item.Where(w => w.nReqTravelID == model[0].nReqTravelID).ToList().ForEach(i =>
                //                           {
                //                               _db.T_Request_Travel_Item.Remove(i);
                //                           });
                // await _db.SaveChangesAsync();

                var hasPJ = _db.T_Request_Travel.Where(w => w.nReqTravelID == model[0].nReqTravelID).FirstOrDefault();

                //Add
                if (hasPJ == null)
                {
                    //Add PJ
                    T_Request_Travel addPJ = new T_Request_Travel();
                    addPJ.dRequest = DateTime.Now;
                    addPJ.nFlowProcessID = model[0].sSaveMode;
                    addPJ.nStep = model[0].sSaveMode;
                    addPJ.nCreate = Convert.ToInt16(user.sUserID);
                    addPJ.dCreate = DateTime.Now;
                    addPJ.nUpdate = Convert.ToInt16(user.sUserID);
                    addPJ.dUpdate = DateTime.Now;
                    addPJ.IsDelete = false;

                    await _db.T_Request_Travel.AddAsync(addPJ);
                    await _db.SaveChangesAsync();

                    // _db.T_Request_Travel.InsertOnSubmit(addPJ);
                    // _db.SubmitChanges();
                    // int NewID = addPJ.id;

                    //Add Item
                    int itemNo = 1;

                    foreach (var item in model)
                    {
                        T_Request_Travel_Item addItem = new T_Request_Travel_Item();

                        addItem.nReqTravelID = await _db.T_Request_Travel.OrderByDescending(o => o.nReqTravelID).Select(s => s.nReqTravelID).FirstOrDefaultAsync();
                        addItem.nItemNo = itemNo;
                        addItem.nPriceKilometer = item.nPriceKilometer;
                        addItem.nProjectID = item.nProjectID;
                        if (item.cOtherFrom) // Add Location From
                        {
                            var nOrder = await _db.TM_Destination.OrderByDescending(o => o.nOrder).Select(s => s.nOrder).FirstOrDefaultAsync();
                            TM_Destination addLo = new TM_Destination();
                            addLo.sDestinationName = item.nLocationFromID;
                            addLo.nOrder = nOrder != null ? nOrder + 1 : 1;
                            addLo.nCreate = Convert.ToInt16(user.sUserID); ;
                            addLo.dCreate = DateTime.Now;
                            addLo.nUpdate = Convert.ToInt16(user.sUserID); ;
                            addLo.dUpdate = DateTime.Now;
                            addLo.IsActive = true;
                            addLo.IsDelete = false;

                            await _db.TM_Destination.AddAsync(addLo);
                            await _db.SaveChangesAsync();

                            addItem.nLocationFromID = await _db.TM_Destination.OrderByDescending(o => o.nDestinationID).Select(s => s.nDestinationID).FirstOrDefaultAsync();
                        }
                        else
                        {
                            addItem.nLocationFromID = item.nLocationFromID.toIntNullToZero();
                        }

                        if (item.cOtherTo) // Add Location To
                        {
                            var nOrder = await _db.TM_Destination.OrderByDescending(o => o.nOrder).Select(s => s.nOrder).FirstOrDefaultAsync();
                            TM_Destination addLo = new TM_Destination();
                            addLo.sDestinationName = item.nLocationToID;
                            addLo.nOrder = nOrder != null ? nOrder + 1 : 1;
                            addLo.nCreate = Convert.ToInt16(user.sUserID); ;
                            addLo.dCreate = DateTime.Now;
                            addLo.nUpdate = Convert.ToInt16(user.sUserID); ;
                            addLo.dUpdate = DateTime.Now;
                            addLo.IsActive = true;
                            addLo.IsDelete = false;

                            await _db.TM_Destination.AddAsync(addLo);
                            await _db.SaveChangesAsync();

                            addItem.nLocationToID = await _db.TM_Destination.OrderByDescending(o => o.nDestinationID).Select(s => s.nDestinationID).FirstOrDefaultAsync();
                        }
                        else
                        {
                            addItem.nLocationToID = item.nLocationToID.toIntNullToZero();
                        }

                        addItem.IsRoundTrip = item.IsRoundTrip;
                        addItem.nTravelID = item.nTravelID;
                        addItem.nDistance = item.nDistance;
                        addItem.nTollway = item.nTollway;
                        addItem.nAmount = item.nAmount;
                        addItem.dTravelDate = item.dTravelDate;

                        itemNo += 1;

                        await _db.T_Request_Travel_Item.AddAsync(addItem);
                        await _db.SaveChangesAsync();
                    }

                }
                else //Edit
                {

                }
                return Ok(new { code = SystemMessageMethod.code_success });
            }
            catch (Exception ex)
            {
                string msgEx = ex.Message;
                return Ok(new { code = SystemMessageMethod.code_error });
            }
        }

        public class objTravel
        {
            public int nReqTravelID { get; set; }
            public decimal nPriceKilometer { get; set; }
            public int nProjectID { get; set; }
            public string nLocationFromID { get; set; }
            public string nLocationToID { get; set; }
            public bool IsRoundTrip { get; set; }
            public int nTravelID { get; set; }
            public decimal nDistance { get; set; }
            public decimal nTollway { get; set; }
            public decimal nAmount { get; set; }
            public DateTime dTravelDate { get; set; }
            public bool cOtherFrom { get; set; }
            public bool cOtherTo { get; set; }
            public bool IsPrivateCar { get; set; }
            public int sSaveMode { get; set; }
        }

        public class objTravelItem
        {
            public bool? cOtherFrom { get; set; }
            public bool? cOtherTo { get; set; }
            public bool? cRoundTrip { get; set; }
            public string dTravelDate { get; set; }
            public DateTime? dRequestDate { get; set; }
            public int id { get; set; }
            public decimal? nAmount { get; set; }
            public decimal? nPriceKilometer { get; set; }
            public decimal? nTollway { get; set; }
            public string sFrom { get; set; }
            public string sProject { get; set; }
            public string sTo { get; set; }
            public string sVehicle { get; set; }
        }

        public class Dropdown
        {
            public string label { get; set; }
            public string value { get; set; }
        }
        public class TravelList
        {
            public int nReqTravelID { get; set; }
            public int nYear { get; set; }
            public string sMonth { get; set; }
            public int nItem { get; set; }
            public string sRequester { get; set; }
            public string dRequestDate { get; set; }
            public string sStatus { get; set; }
        }
    }
}
