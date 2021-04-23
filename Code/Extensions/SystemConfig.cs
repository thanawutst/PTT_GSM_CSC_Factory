using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using Microsoft.Extensions.Hosting;
using System.Web;

namespace SoftthaiIntranet.Extensions
{
    public partial class SystemConfig
    {
        public static int nLimitItemAutocomplete { get { return 30; } }
        public static string Actived { get { return "Y"; } }
        public static string Inactived { get { return "N"; } }
        public static string Deleted { get { return "Y"; } }
        public static string NotDelete { get { return "N"; } }
        public static string ValueYes { get { return "Y"; } }
        public static string ValueNo { get { return "N"; } }

    }

    public partial class SystemMessageMethod
    {
        // Return Json message
        // msg
        public static string msg_success { get { return "SUCCESS"; } }
        public static string msg_error { get { return "ERROR"; } }
        public static string msg_expire { get { return "EXPRIRE"; } }
        public static string msg_duplicate { get { return "DUPLICATE"; } }

        // code
        public static int code_success { get { return 200; } }
        public static int code_expeire { get { return 401; } }
        public static int code_invalid { get { return 403; } }
        public static int code_not_found { get { return 404; } }
        public static int code_error { get { return 500; } }
    }
}