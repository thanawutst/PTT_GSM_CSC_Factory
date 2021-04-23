using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftthaiIntranet.Models.SystemModels
{
    public class LoginProps
    {
        public string sUsername { get; set; }
        public string sPassword { get; set; }
    }
    public class UserAccount
    {
        public string sUserID { get; set; }
        public string sLogonName { get; set; }
        public string sName { get; set; }
        public string sSurname { get; set; }
        public string sFullname { get; set; }
    }

    public class TokenJWTSecret : UserAccount
    {
        public string sIssuer { get; set; }
        public string sAudience { get; set; }
        public DateTime? dTimeout { get; set; }
        public string sSecretKey { get; set; }
    }

    public partial class JwtClaimTypes
    {
        public static string USER_ID { get { return "USER_ID"; } }
        public static string LOGON_NAME { get { return "LOGON_NAME"; } }
        public static string NAME { get { return "NAME"; } }
        public static string SURNAME { get { return "SURNAME"; } }
        public static string FULLNAME { get { return "FULLNAME"; } }
    }



}
