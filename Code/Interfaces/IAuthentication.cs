using System;
using System.Web;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using PTT_GSM_CSC_Factory.Models.SystemModels;
using Microsoft.AspNetCore.Http;

namespace PTT_GSM_CSC_Factory.Interfaces
{
    public interface IAuthentication
    {
        string BuildToken(TokenJWTSecret jWTSecret);
        bool IsHasExists();
        UserAccount GetUserAccount();
    }

    public class Authentication : IAuthentication
    {
        private readonly IHttpContextAccessor httpContext;

        public Authentication(IHttpContextAccessor httpContextAccessor)
        {
            httpContext = httpContextAccessor;
        }
        string IAuthentication.BuildToken(TokenJWTSecret jWTSecret)
        {
            Claim[] claims = new Claim[] {
                new Claim(JwtClaimTypes.USER_ID,jWTSecret.sUserID),
                new Claim(JwtClaimTypes.LOGON_NAME,jWTSecret.sLogonName),
                new Claim(JwtClaimTypes.NAME,  jWTSecret.sName),
                new Claim(JwtClaimTypes.SURNAME, jWTSecret.sSurname),
                new Claim(JwtClaimTypes.FULLNAME, jWTSecret.sFullname),
            };

            DateTime? expires = jWTSecret.dTimeout; //set timeout
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWTSecret.sSecretKey));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: jWTSecret.sIssuer,
                audience: jWTSecret.sAudience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        UserAccount IAuthentication.GetUserAccount()
        {
            try
            {
                var claimsPrincipal = httpContext.HttpContext.User;
                UserAccount userAccount = new UserAccount();
                userAccount.sUserID = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.USER_ID).Value;
                userAccount.sLogonName = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.LOGON_NAME).Value;
                userAccount.sName = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.NAME).Value;
                userAccount.sSurname = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.SURNAME).Value;
                userAccount.sFullname = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.FULLNAME).Value;

                return userAccount;
            }
            catch
            {
                return new UserAccount();
            }
        }

        bool IAuthentication.IsHasExists()
        {
            try
            {
                return httpContext.HttpContext.User.HasClaim(c => c.Type == JwtClaimTypes.USER_ID);

            }
            catch
            {

                return false;
            }
        }
    }
}
