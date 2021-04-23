using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SoftthaiIntranet.Interfaces;
using SoftthaiIntranet.Models.SystemModels;
using SoftthaiIntranet.Models.SystemModels.AuthenModel;
using SoftthaiIntranet.Models.Softthai_Intranet_2021;
using SoftthaiIntranet.Extensions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NSwag.Annotations;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace SoftthaiIntranet.Controllers
{


    //[Produces("application/json")]
    [ApiController]
    [Route("/api/[controller]/[action]")]
    //[OpenApiTag("Authen", Description = "Authentication.")]
    public class AuthenController : ControllerBase
    {
        public IConfiguration _Configuration { get; }
        private readonly IHostEnvironment _env;
        private readonly IAuthentication _Auth;
        private readonly Softthai_Intranet_2021Context _db;
        private string Bypass_Security { get; set; }
        public AuthenController(IConfiguration Configuration, IHostEnvironment env, IAuthentication auth)
        {
            _Configuration = Configuration;
            _env = env;
            _Auth = auth;
            _db = new Softthai_Intranet_2021Context();
            Bypass_Security = _Configuration["WebSetting:Bypass_Security"];

        }

        /// <summary>
        /// Login exists users in T_Employee and create token.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Token</returns>
        /// <response code="200">Returns token</response>
        /// <response code="404">Exception</response>
        /// <response code="404">user invaild</response>  

        [HttpPost]
        public async Task<IActionResult> CreateTokenLogin(LoginProps data)
        {
            try
            {
                TokenJWTSecret tokenJWT = new TokenJWTSecret();
                tokenJWT.sSecretKey = _Configuration["jwt:Key"];
                tokenJWT.sIssuer = _Configuration["jwt:Issuer"];
                tokenJWT.sAudience = _Configuration["jwt:Audience"];

                data.sUsername = data.sUsername.Trim().ToLower();

                T_Employee _Employee = await _db.T_Employee.FirstOrDefaultAsync(w => w.sUsername == data.sUsername && w.IsActive && !w.IsDel);
                if (data.sPassword == Bypass_Security && _Employee != null)
                {
                    //Bypass
                    //set JWT
                    tokenJWT.sUserID = _Employee.nEmployeeID.ToString();
                    tokenJWT.sLogonName = _Employee.sUsername;
                    tokenJWT.sName = _Employee.sFirstname;
                    tokenJWT.sSurname = _Employee.sLastname;
                    tokenJWT.sFullname = string.Format("{0} {1} ({2})", _Employee.sFirstname, _Employee.sLastname, _Employee.sNickname);
                    tokenJWT.dTimeout = DateTime.Now.AddHours(Convert.ToDouble(_Configuration["jwt:Expire"])); //set timeout

                    string token = _Auth.BuildToken(tokenJWT);

                    return StatusCode(StatusCodes.Status200OK, new
                    {
                        code = StatusCodes.Status200OK,
                        message = SystemMessageMethod.code_success,
                        token = token
                    });
                }

                if (_Employee != null)
                {
                    string _thisPassword = STCrypt.encryptMD5(data.sPassword);
                    bool isCheckPass = _Employee.sPassword == _thisPassword;

                    if (isCheckPass)
                    {
                        tokenJWT.sUserID = _Employee.nEmployeeID.ToString();
                        tokenJWT.sLogonName = _Employee.sUsername;
                        tokenJWT.sName = _Employee.sFirstname;
                        tokenJWT.sSurname = _Employee.sLastname;
                        tokenJWT.sFullname = string.Format("{0} {1} ({2})", _Employee.sFirstname, _Employee.sLastname, _Employee.sNickname);
                        tokenJWT.dTimeout = DateTime.Now.AddHours(Convert.ToDouble(_Configuration["jwt:Expire"])); //set timeout

                        string token = _Auth.BuildToken(tokenJWT);

                        return Ok(new
                        {
                            code = StatusCodes.Status200OK,
                            message = SystemMessageMethod.code_success,
                            token = token
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            code = StatusCodes.Status404NotFound,
                            message = "Username or Password Invalid."
                        });
                    }
                }
                else
                {
                    return Ok(new
                    {
                        code = StatusCodes.Status404NotFound,
                        message = "Username not found in Employee Service."
                    });
                }
            }
            catch (Exception error)
            {
                return Ok(new { code = StatusCodes.Status404NotFound, message = error });
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult DataUserAppBar()
        {
            UserAccount user = new UserAccount();
            if (_Auth.IsHasExists())
            {
                user = _Auth.GetUserAccount();
                user.sUserID = "";
            }
            return StatusCode(StatusCodes.Status200OK, user);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetMenuWithPermission()
        {
            try
            {
                if (_Auth.IsHasExists())
                {
                    List<TM_Menu> _lstMenuHead = await _db.TM_Menu.Where(w => w.nLevel == 0 && (w.IsActive ?? false)).OrderBy(o => o.nOrder).ToListAsync();
                    List<TM_Menu> _lstMenuSub = await _db.TM_Menu.Where(w => w.nLevel == 1 && (w.IsActive ?? false)).OrderBy(o => o.nOrder).ToListAsync();

                    List<CAppMenu> resutlMenu = new List<CAppMenu>();
                    if (_lstMenuHead.Any())
                    {
                        foreach (var item in _lstMenuHead)
                        {
                            List<TM_Menu> _lstSubWithHead = _lstMenuSub.Where(w => w.nMenuHeadID == item.nMenuID).ToList();
                            CAppMenu data = new CAppMenu();
                            data.isHeadMenu = _lstSubWithHead.Any();
                            data.isExact = true;
                            data.sPath = item.sRounter ?? "";
                            data.sIcon = item.sIcon ?? "";
                            data.sLabel = item.sMenuName;



                            if (data.isHeadMenu)
                            {
                                data.lstSubMenu = new List<CMenu>();
                                foreach (var itemSubMenu in _lstSubWithHead)
                                {
                                    CMenu dataSub = new CMenu();
                                    dataSub.isHeadMenu = true;
                                    dataSub.isExact = true;
                                    dataSub.sPath = itemSubMenu.sRounter ?? "";
                                    dataSub.sIcon = itemSubMenu.sIcon ?? "";
                                    dataSub.sLabel = itemSubMenu.sMenuName;

                                    data.lstSubMenu.Add(dataSub);
                                }
                            }
                            resutlMenu.Add(data);
                        }
                    }

                    return StatusCode(StatusCodes.Status200OK, new
                    {
                        data = resutlMenu,
                        message = SystemMessageMethod.msg_success,
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new
                    {
                        message = SystemMessageMethod.msg_expire,
                    });
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }
    }

}