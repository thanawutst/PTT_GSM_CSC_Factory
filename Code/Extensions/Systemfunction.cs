using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SoftthaiIntranet.Models.SystemModels.AllClass;
using SoftthaiIntranet.Models.Softthai_Intranet_2021;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;

namespace SoftthaiIntranet.Extensions
{
    public class Systemfunction
    {
        public static string QryToJson(string _QRY, string _CONN)
        {
            DataTable _dt = new DataTable();
            string _Connect = _CONN;
            if (string.IsNullOrEmpty(_CONN))
            {
                _Connect = GetConnectionString();
            }

            if (!string.IsNullOrEmpty(_QRY))
            {
                using (SqlConnection _conn = new SqlConnection(_Connect))
                {

                    if (_conn.State == ConnectionState.Closed)
                    {
                        _conn.Open();
                    }

                    SqlCommand com = new SqlCommand(_QRY, _conn);
                    com.CommandTimeout = 6000;
                    new SqlDataAdapter(com).Fill(_dt);
                    _conn.Dispose();
                }
            }
            else
            {

            }
            string jsonString = string.Empty;
            if (_dt.Rows.Count > 0)
            {
                jsonString = JsonConvert.SerializeObject(_dt);
            }
            return jsonString;
        }
        public static string QryToJsonPIS(string _QRY, string _CONN)
        {
            DataTable _dt = new DataTable();
            string _Connect = _CONN;
            if (string.IsNullOrEmpty(_CONN))
            {
                _Connect = GetConnectionStringPIS();
            }

            if (!string.IsNullOrEmpty(_QRY))
            {
                using (SqlConnection _conn = new SqlConnection(_Connect))
                {

                    if (_conn.State == ConnectionState.Closed)
                    {
                        _conn.Open();
                    }

                    SqlCommand com = new SqlCommand(_QRY, _conn);
                    com.CommandTimeout = 6000;
                    new SqlDataAdapter(com).Fill(_dt);
                    _conn.Dispose();
                }
            }
            else
            {

            }
            string jsonString = string.Empty;
            if (_dt.Rows.Count > 0)
            {
                jsonString = JsonConvert.SerializeObject(_dt);
            }
            return jsonString;
        }
        public static string GetAppSettingJson(string GetParameter)
        {
            string Result = "";
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false);
            IConfigurationRoot configuration = builder.Build();
            IConfigurationSection section = configuration.GetSection(GetParameter);
            Result = section.Value;
            return Result;
        }
        public static int? GetIntNull(string sVal)
        {
            int? nTemp = null;
            int nCheck = 0;
            if (!string.IsNullOrEmpty(sVal))
            {
                sVal = ConvertExponentialToString(sVal);
                bool cCheck = int.TryParse(sVal, out nCheck);
                if (cCheck)
                {
                    nTemp = int.Parse(sVal);
                }
            }

            return nTemp;
        }
        public static string ConvertExponentialToString(string sVal)
        {
            string sRsult = "";
            try
            {
                decimal nTemp = 0;
                bool check = Decimal.TryParse((sVal + "").Replace(",", ""), System.Globalization.NumberStyles.Float, null, out nTemp);
                if (check)
                {
                    decimal d = Decimal.Parse((sVal + "").Replace(",", ""), System.Globalization.NumberStyles.Float);
                    sRsult = (d + "").Replace(",", "");
                }
                else
                {
                    sRsult = sVal;
                }
            }
            catch
            {
                sRsult = sVal;
            }

            return sRsult != null ? (sRsult.Length < 50 ? sRsult : sRsult.Remove(50)) : ""; //เพื่อไม่ให้ตอน Save Error หากค่าที่เกิดจากผลการคำนวนเกิน Type ใน DB (varchar(50))
        }
        public static string GetConnectionString()
        {
            return GetAppSettingJson("ConnectionStrings:Softthai_Intranet_2021_ConnectionStrings");
        }
        public static string GetConnectionStringPIS()
        {
            return GetAppSettingJson("ConnectionStrings:PIS");
        }
        public static string GetUrlSite()
        {
            return GetAppSettingJson("Url");
        }
        public static DataTable Get_Data(string _sql)
        {
            DataTable _dt = new DataTable();
            if (!string.IsNullOrEmpty(_sql))
            {
                using (SqlConnection _conn = new SqlConnection(GetConnectionString()))
                {

                    if (_conn.State == ConnectionState.Closed)
                    {

                    }

                    SqlCommand com = new SqlCommand(_sql, _conn);
                    com.CommandTimeout = 6000;
                    new SqlDataAdapter(com).Fill(_dt);

                    return _dt;
                }
            }
            else
            {
                return _dt;
            }
        }
        public string ExecuteSql(string pStrSql)
        {
            try
            {
                using (SqlConnection _conn = new SqlConnection(GetConnectionString()))
                {
                    SqlCommand Cmd = new SqlCommand(pStrSql, _conn);

                    object result = Cmd.ExecuteScalar();
                    string oresult = "1";

                    if (result != null)
                    {
                        oresult = result.ToString();
                    }

                    _conn.Close();

                    return oresult;
                }
            }
            catch
            {
                return "";
            }
        }
        public static string process_SessionExpired()
        {
            return "SSEXP";
        }
        public static string process_Success()
        {
            return "Success";
        }
        public static string process_Failed()
        {
            return "Failed";
        }
        public static string process_FileOversize()
        {
            return "OverSize";
        }
        public static string process_FileInvalidType()
        {
            return "InvalidType";
        }
        public static string process_Duplicate()
        {
            return "DUP";
        }
        public static int[] FixStatusBySSC()
        {
            int[] ArrData = { 8, 9, 10, 11, 12 };
            return ArrData;
        }
        public static string[] FixStatusByCopyRef()
        {
            string[] ArrData = { "8", "9", "11", "12", "13" };
            return ArrData;
        }
        public class CResutlWebMethod
        {
            public string Status { get; set; }
            public string Msg { get; set; }
            public string Content { get; set; }
        }
        #region Convert Format
        public static string StringToFix2Digit(string Value)
        {
            string result = "0";

            if (!string.IsNullOrEmpty(Value))
            {
                result = string.Format("{0:f2}", decimal.Parse(Value));
            }

            return result;
        }
        public static string SubstringText(string sVal, int Num)
        {
            return (sVal.Length > Num) ? sVal.Substring(0, Num) + "..." : sVal;
        }
        public static string SQL_String(string sVal)
        {
            return !string.IsNullOrEmpty(sVal) ? "'" + ReplaceInjection(sVal) + "'" : "null";
        }
        public static string SQL_String(DateTime? sVal)
        {
            string Result = "";
            if (sVal.HasValue)
            {
                string sDate = sVal.Value.ToString("yyyy-MM-dd");
                Result = "'" + ReplaceInjection(sDate) + "'";
            }
            else
            {
                Result = "null";
            }
            return Result;
        }

        public static string SQL_Array_To_StringIn(string sVal)
        {
            string Result = "";
            if (!string.IsNullOrEmpty(sVal))
            {
                string[] Arr = sVal.Split(',');
                foreach (var _Item in Arr)
                {
                    Result += ",'" + ReplaceInjection(_Item) + "'";
                }
                Result = Result.Length > 0 ? Result.Remove(0, 1) : "''";

            }
            else
            {
                Result = "''";
            }
            return Result;
        }
        public static string ReplaceInjection(string str)
        {
            string[] blacklist = new string[] { "'", "\\", "\"", "*/", ";", "--", "<script", "/*", "</script>" };
            string strRep = str;
            if (strRep == null || strRep.Trim().Equals(String.Empty))
                return strRep;
            foreach (string blk in blacklist) { strRep = strRep.Replace(blk, ""); }

            return strRep;
        }
        public static string ConvertDatTimeToString(string sDate, string Mode)
        {
            string result = "";
            DateTime dtTemp;
            switch (Mode)
            {
                case "EN":
                    result = DateTime.TryParse(ReplaceInjection(sDate), out dtTemp) ? dtTemp.ToString("yyyy-MM-dd", new CultureInfo("en-US")) : "";
                    break;
                case "TH":
                    result = DateTime.TryParse(ReplaceInjection(sDate), out dtTemp) ? dtTemp.ToString("dd/MM/yyyy", new CultureInfo("th-th")) : "";
                    break;
                case "TH2":
                    result = DateTime.TryParse(ReplaceInjection(sDate), out dtTemp) ? dtTemp.ToString("D", new CultureInfo("th-th")) : "";
                    break;
                case "TH3":
                    result = DateTime.TryParse(ReplaceInjection(sDate), out dtTemp) ? dtTemp.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-th")) : "";
                    break;
                case "EN1":
                    result = DateTime.TryParse(ReplaceInjection(sDate), out dtTemp) ? dtTemp.ToString("dd-MMM-yyyy", new CultureInfo("en-US")) : "";
                    break;
                case "EN2":
                    result = DateTime.TryParse(ReplaceInjection(sDate), out dtTemp) ? dtTemp.ToString("dd-MMM-yy", new CultureInfo("en-US")) : "";
                    break;
            }
            return result;
        }
        public static string ConvertMonthName(string sMonth)
        {
            string result = "";

            DateTime date = Convert.ToDateTime(sMonth + " 01, 1900");
            result = date.ToString("MMMM", CultureInfo.CreateSpecificCulture("th-TH"));
            return result;
        }

        public static string ConvertMonthName_US(string sMonth)
        {
            string result = "";

            DateTime date = Convert.ToDateTime(sMonth + " 01, 1900");
            result = date.ToString("MMM", CultureInfo.CreateSpecificCulture("en-US"));
            return result;
        }
        #endregion
        #region Parse Variable

        public static string StringTrim(string sVal)
        {
            string Result = "";
            if (!string.IsNullOrEmpty(sVal))
            {
                Result = (sVal + "").Trim();
            }
            else
            {
                Result = null;
            }

            return Result;
        }

        public static string StringTrimWhiteSpace(string sVal)
        {
            string Result = "";
            if (!string.IsNullOrEmpty(sVal))
            {
                Result = (sVal + "").Trim().Replace(" ", "");
            }
            else
            {
                Result = "";
            }

            return Result;
        }

        public static DateTime? ParseDateTimeToNull(string sVal)
        {
            DateTime nTemp;
            CultureInfo EN = new CultureInfo("en-US");
            CultureInfo TH = new CultureInfo("th-TH");
            DateTime? Result = (DateTime?)null;
            if (!string.IsNullOrEmpty(sVal))
            {
                string cutVal = sVal.Length > 10 ? sVal.Substring(0, 10) : sVal;
                if (DateTime.TryParseExact(cutVal, "dd/MM/yyyy", EN, DateTimeStyles.None, out nTemp))
                {
                    Result = DateTime.ParseExact(cutVal, "dd/MM/yyyy", EN, DateTimeStyles.None);
                }
                else if (DateTime.TryParseExact(cutVal, "yyyy-MM-dd", EN, DateTimeStyles.None, out nTemp))
                {
                    Result = DateTime.ParseExact(cutVal, "yyyy-MM-dd", EN, DateTimeStyles.None);
                }
                else if (DateTime.TryParseExact(cutVal, "d/MM/yyyy", EN, DateTimeStyles.None, out nTemp))
                {
                    Result = DateTime.ParseExact(cutVal, "d/MM/yyyy", EN, DateTimeStyles.None);
                }
                else if (DateTime.TryParseExact(cutVal, "d/M/yyyy", EN, DateTimeStyles.None, out nTemp))
                {
                    Result = DateTime.ParseExact(cutVal, "d/M/yyyy", EN, DateTimeStyles.None);
                }
                else
                {
                    if (!string.IsNullOrEmpty(sVal))
                    {
                        try
                        {
                            Result = Convert.ToDateTime(sVal);

                        }
                        catch (Exception)
                        {
                            Result = null;
                        }
                    }
                }
            }
            return Result;
        }

        public static DateTime ParseDateTimeToDateTimeNow(string sVal)
        {
            DateTime nTemp;
            CultureInfo EN = new CultureInfo("en-US");
            CultureInfo TH = new CultureInfo("th-TH");
            DateTime Result = DateTime.Now;
            if (!string.IsNullOrEmpty(sVal))
            {
                string cutVal = sVal.Length > 10 ? sVal.Substring(0, 10) : sVal;
                if (DateTime.TryParseExact(cutVal, "dd/MM/yyyy", EN, DateTimeStyles.None, out nTemp))
                {
                    Result = DateTime.ParseExact(cutVal, "dd/MM/yyyy", EN, DateTimeStyles.None);
                }
                else if (DateTime.TryParseExact(cutVal, "yyyy-MM-dd", EN, DateTimeStyles.None, out nTemp))
                {
                    Result = DateTime.ParseExact(cutVal, "yyyy-MM-dd", EN, DateTimeStyles.None);
                }
                else if (DateTime.TryParseExact(cutVal, "d/MM/yyyy", EN, DateTimeStyles.None, out nTemp))
                {
                    Result = DateTime.ParseExact(cutVal, "d/MM/yyyy", EN, DateTimeStyles.None);
                }
                else if (DateTime.TryParseExact(cutVal, "d/M/yyyy", EN, DateTimeStyles.None, out nTemp))
                {
                    Result = DateTime.ParseExact(cutVal, "d/M/yyyy", EN, DateTimeStyles.None);
                }
                else
                {
                    if (!string.IsNullOrEmpty(sVal))
                    {
                        try
                        {
                            Result = Convert.ToDateTime(sVal);

                        }
                        catch (Exception)
                        {
                            Result = DateTime.Now;
                        }
                    }
                }
            }
            return Result;
        }

        public static DateTime? ParseExactDateTimeToNull(string sVal)
        {
            DateTime? Result = !string.IsNullOrEmpty(sVal) ? DateTime.ParseExact(sVal, "dd/MM/yyyy", null) : (DateTime?)null;
            return Result;
        }

        public static string ParseDateTimeToString(DateTime? dDate, string sMode, string sFormat)
        {
            string result = "";
            sMode = !string.IsNullOrEmpty(sMode) ? sMode.ToLower() : "en";
            string sCultureInfo = "";
            switch (sMode)
            {
                case "th":
                    sCultureInfo = "th-TH";
                    break;
                case "en":
                    sCultureInfo = "en-US";
                    break;
            }

            if (dDate.HasValue)
            {
                result = dDate.Value.ToString(sFormat, new CultureInfo(sCultureInfo));
            }

            return result;
        }

        public static int? ParseIntToNull(string sVal)
        {
            int nTemp;
            int? Result = int.TryParse(sVal, out nTemp) ? nTemp : (int?)null;
            return Result;
        }

        public static int ParseIntToZero(string sVal)
        {
            int nTemp;
            int Result = int.TryParse(sVal, out nTemp) ? nTemp : 0;
            return Result;
        }

        public static int ParseIntToZeroByFormatDouble(string sVal)
        {
            Double temp;
            Boolean isOk = Double.TryParse(sVal, out temp);
            int Result = isOk ? (int)temp : 0;
            //int nTemp;
            //int Result = int.TryParse(sVal, out nTemp) ? nTemp : 0;
            return Result;
        }

        public static decimal? ParseDecimalToNull(string sVal)
        {
            decimal nTemp;
            decimal? Result = decimal.TryParse(sVal, out nTemp) ? nTemp : (decimal?)null;
            return Result;
        }

        public static decimal? ParseDecimalToZero(string sVal)
        {
            decimal nTemp;
            decimal? Result = decimal.TryParse(sVal, out nTemp) ? nTemp : 0;
            return Result;
        }

        public static double? ParseDoubleToNull(string sVal)
        {
            double nTemp;
            double? Result = double.TryParse(sVal, out nTemp) ? nTemp : (double?)null;
            return Result;
        }

        public static double? ParseDoubleToNull(decimal? sVal)
        {
            double nTemp;

            string sValue = sVal.HasValue ? sVal.Value + "" : "";

            double? Result = double.TryParse(sValue, out nTemp) ? nTemp : (double?)null;
            return Result;
        }

        public static float? ParseFloatToZero(string sVal)
        {
            float nTemp;
            float? Result = float.TryParse(sVal, out nTemp) ? nTemp : 0;
            return Result;
        }
        public static string CheckDataToSAP_STR(string sVal, int MaxLength)
        {
            string Result = "";
            if (!string.IsNullOrEmpty(sVal))
            {
                Result = sVal.Length > MaxLength ? sVal.Substring(0, MaxLength) : sVal;
            }
            return Result;
        }

        public static TimeSpan? ParseTimeSpanToNull(string sVal)
        {
            TimeSpan nTemp;
            TimeSpan? Result = (TimeSpan?)null;
            if (!string.IsNullOrEmpty(sVal))
            {
                Result = TimeSpan.TryParse(sVal, out nTemp) ? TimeSpan.Parse(sVal) : (TimeSpan?)null;
            }
            return Result;
        }
        #endregion


        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public string RandomPassword(int size = 0)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }


        public static string UrlEncode(string QRYSTR)
        {
            string Result = "";
            try
            {
                Result = !string.IsNullOrEmpty(QRYSTR) ? HttpUtility.UrlEncode(STCrypt.Encrypt(QRYSTR)) : "";
            }
            catch (Exception ex)
            {
                Result = ex.Message;
            }
            return Result;
        }

        public static string UrlDecode(string QRYSTR)
        {
            string sID = "";

            try
            {
                sID = !string.IsNullOrEmpty(QRYSTR) ? HttpUtility.UrlDecode(STCrypt.Decrypt(QRYSTR)) : "";
            }
            catch
            {
                try
                {
                    sID = !string.IsNullOrEmpty(QRYSTR) ? STCrypt.Decrypt(HttpUtility.UrlDecode(QRYSTR)) : "";
                }
                catch
                {
                    try
                    {
                        sID = !string.IsNullOrEmpty(QRYSTR) ? STCrypt.Decrypt(QRYSTR) : "";
                    }
                    catch (Exception ex)
                    {
                        string MSG = ex.Message;
                        sID = "";
                    }
                }
            }

            return sID;
        }

        public static void _FlowRequest(int nRequestID, int? nEmp, int nMenuTypeID)
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            var lst_emp = DB.T_Employee.FirstOrDefault(f => f.nEmployeeID == nEmp && f.IsDel != true);
            var lst_ptoject = new List<TM_ProjectMember>();
            var lst_project_Lead = new List<T_Employee>();
            var lst_project_PM = new List<TM_ProjectMember>();
            var lst_project_MD = new List<T_Employee>();
            var lst_project_HR = DB.T_Employee.Where(w => w.IsHR == true).ToList();
            int? nStap = 1;
            if (lst_emp != null)
            {
                switch (nMenuTypeID)
                {
                    case 0:
                        var lst = DB.T_Request_Allowance_Flow.Where(w => w.nAllowanceID == nRequestID && w.IsDelete != true).OrderByDescending(o => o.nFlowID).FirstOrDefault();
                        var lst_allow = DB.T_Request_Allowance.FirstOrDefault(f => f.nAllowanceID == nRequestID && f.IsDelete != true);
                        if (lst == null && lst_allow != null)
                        {
                            lst_ptoject = DB.TM_ProjectMember.Where(f => f.nProjectID == lst_allow.nProjectID && f.IsDelete != true).ToList();
                            lst_project_Lead = DB.T_Employee.Where(w => lst_ptoject.Select(s => s.nEmployeeID).Contains(w.nEmployeeID) && w.nPositionID == 1).ToList();
                            lst_project_HR = DB.T_Employee.Where(w => w.IsHR == true).ToList();

                            if (lst_project_Lead.Count > 0)
                            {
                                var data = new T_Request_Allowance_Flow();
                                data.nAllowanceID = nRequestID;
                                data.nFlowProcessID = null;
                                data.sTypeFlow = lst_project_Lead.Count > 1 ? "R" : "P";
                                data.nRoleID = null;
                                data.nApproveID = null;
                                data.cApprove = null;
                                data.dApprove = null;
                                data.sComment = null;
                                data.nStep = null;
                                data.nCreate = null;
                                data.dCreate = null;
                                data.nUpdate = null;
                                data.dUpdate = null;
                                data.nDelete = null;
                                data.dDelete = null;
                                data.IsDelete = false;
                                DB.T_Request_Allowance_Flow.Add(data);
                            }


                            if (lst_project_PM.Count > 0)
                            {
                                var data = new T_Request_Allowance_Flow();
                                data.nAllowanceID = nRequestID;
                                data.nFlowProcessID = null;
                                data.sTypeFlow = lst_project_PM.Count > 1 ? "R" : "P"; ;
                                data.nRoleID = lst_project_PM.FirstOrDefault().nRoleID;
                                data.nApproveID = null;
                                data.cApprove = null;
                                data.dApprove = null;
                                data.sComment = null;
                                data.nStep = null;
                                data.nCreate = null;
                                data.dCreate = null;
                                data.nUpdate = null;
                                data.dUpdate = null;
                                data.nDelete = null;
                                data.dDelete = null;
                                data.IsDelete = false;
                                DB.T_Request_Allowance_Flow.Add(data);
                            }
                            if (lst_project_HR.Count > 0)
                            {
                                var data = new T_Request_Allowance_Flow();
                                data.nAllowanceID = nRequestID;
                                data.nFlowProcessID = null;
                                data.sTypeFlow = lst_project_HR.Count > 1 ? "R" : "P"; ;
                                data.nRoleID = lst_project_HR.FirstOrDefault().nRoleID;
                                data.nApproveID = null;
                                data.cApprove = null;
                                data.dApprove = null;
                                data.sComment = null;
                                data.nStep = null;
                                data.nCreate = null;
                                data.dCreate = null;
                                data.nUpdate = null;
                                data.dUpdate = null;
                                data.nDelete = null;
                                data.dDelete = null;
                                data.IsDelete = false;
                                DB.T_Request_Allowance_Flow.Add(data);
                            }
                        }
                        break;
                    case 1:
                        var lst_Acc = DB.T_Request_AcceptWorkTime_Flow.Where(w => w.nAcceptWorkTimeID == nRequestID && w.IsDelete != true).ToList();
                        var lst_allow_Acc = DB.T_Request_AcceptWorkTime.FirstOrDefault(f => f.nAcceptWorkTimeID == nRequestID && f.IsDelete != true);
                        lst_ptoject = DB.TM_ProjectMember.Where(f => f.nProjectID == lst_allow_Acc.nProjectID && f.IsDelete != true).ToList();
                        var TLade = (from TM in DB.TM_ProjectMember.Where(w => w.IsDelete != true && w.nProjectID == lst_allow_Acc.nProjectID).ToList()
                                     from TE in DB.T_Employee.Where(w => w.nEmployeeID == TM.nEmployeeID && w.nPositionID == 1)
                                     select new
                                     {
                                         TM
                                     }).ToList();
                        lst_project_PM = lst_ptoject.Where(f => f.nRoleID == 2).ToList();
                        //lst_project_MD = DB.TM_Condition_AcceptWorkTime.Where(w=> w.nrole);
                        if (lst_allow_Acc != null)
                        {
                            if (lst_Acc.Count == 0)
                            {
                                var data = new T_Request_AcceptWorkTime_Flow();
                                switch (lst_emp.nRoleID)
                                {
                                    case 2:
                                    case 3:
                                        data = new T_Request_AcceptWorkTime_Flow();
                                        data.nAcceptWorkTimeID = nRequestID;
                                        data.nFlowProcessID = 7;
                                        data.sTypeFlow = lst_project_PM.Count > 1 ? "R" : "P"; ;
                                        data.nRoleID = lst_ptoject.FirstOrDefault().nRoleID;
                                        data.nApproveID = null;
                                        data.cApprove = "W";
                                        data.dApprove = null;
                                        data.sComment = null;
                                        data.nStep = 1;
                                        data.nCreate = null;
                                        data.dCreate = null;
                                        data.nUpdate = null;
                                        data.dUpdate = null;
                                        data.nDelete = null;
                                        data.dDelete = null;
                                        data.IsDelete = false;
                                        DB.T_Request_AcceptWorkTime_Flow.Add(data);
                                        break;
                                    case 4:
                                    case 5:
                                    case 6:
                                    case 7:
                                    case 8:
                                    case 9:
                                    case 15:
                                    case 16:
                                    case 17:
                                        if (lst_emp.nPositionID != 1)
                                        {
                                            var nRole_Lead = TLade.Count == 1 ? TLade.FirstOrDefault().TM.nRoleID : TLade.FirstOrDefault(f => f.TM.nRoleID == lst_emp.nRoleID).TM.nRoleID;
                                            if (lst_project_Lead.Count > 0)
                                            {
                                                data = new T_Request_AcceptWorkTime_Flow();
                                                data.nAcceptWorkTimeID = nRequestID;
                                                data.nFlowProcessID = 3;
                                                data.sTypeFlow = TLade.Count > 1 ? "R" : "P";
                                                data.nRoleID = nRole_Lead;
                                                data.nApproveID = null;
                                                data.cApprove = "W";
                                                data.dApprove = null;
                                                data.sComment = null;
                                                data.nStep = 1;
                                                data.nCreate = null;
                                                data.dCreate = null;
                                                data.nUpdate = null;
                                                data.dUpdate = null;
                                                data.nDelete = null;
                                                data.dDelete = null;
                                                data.IsDelete = false;
                                                DB.T_Request_AcceptWorkTime_Flow.Add(data);
                                            }
                                            else
                                            {
                                                var _lead = DB.T_Employee.FirstOrDefault(f => f.nEmployeeID == nEmp);
                                                var lead = DB.T_Employee.FirstOrDefault(f => f.nEmployeeID == _lead.nApproverLevel1ID);
                                                data = new T_Request_AcceptWorkTime_Flow();
                                                data.nAcceptWorkTimeID = nRequestID;
                                                data.nFlowProcessID = 3;
                                                data.sTypeFlow = "P";
                                                data.nRoleID = lead.nRoleID == null ? 5 : lead.nRoleID;
                                                data.nApproveID = null;
                                                data.cApprove = "W";
                                                data.dApprove = null;
                                                data.sComment = null;
                                                data.nStep = 1;
                                                data.nCreate = null;
                                                data.dCreate = null;
                                                data.nUpdate = null;
                                                data.dUpdate = null;
                                                data.nDelete = null;
                                                data.dDelete = null;
                                                data.IsDelete = false;
                                                DB.T_Request_AcceptWorkTime_Flow.Add(data);
                                            }
                                        }
                                        if (lst_project_PM.Count > 0)
                                        {
                                            data = new T_Request_AcceptWorkTime_Flow();
                                            data.nAcceptWorkTimeID = nRequestID;
                                            data.nFlowProcessID = lst_emp.nPositionID == 1 ? 3 : null;
                                            data.sTypeFlow = lst_project_PM.Count > 1 ? "R" : "P"; ;
                                            data.nRoleID = lst_project_PM.FirstOrDefault().nRoleID;
                                            data.nApproveID = null;
                                            data.cApprove = lst_emp.nPositionID == 1 ? "W" : null; ;
                                            data.dApprove = null;
                                            data.sComment = null;
                                            data.nStep = lst_emp.nPositionID == 1 ? 1 : null;
                                            data.nCreate = null;
                                            data.dCreate = null;
                                            data.nUpdate = null;
                                            data.dUpdate = null;
                                            data.nDelete = null;
                                            data.dDelete = null;
                                            data.IsDelete = false;
                                            DB.T_Request_AcceptWorkTime_Flow.Add(data);
                                        }
                                        break;
                                    default:
                                        break;
                                }

                                if (lst_project_HR.Count > 0)
                                {
                                    data = new T_Request_AcceptWorkTime_Flow();
                                    data.nAcceptWorkTimeID = nRequestID;
                                    data.nFlowProcessID = null;
                                    data.sTypeFlow = lst_project_HR.Count > 1 ? "R" : "P"; ;
                                    data.nRoleID = lst_project_HR.FirstOrDefault().nRoleID;
                                    data.nApproveID = null;
                                    data.cApprove = null;
                                    data.dApprove = null;
                                    data.sComment = null;
                                    data.nStep = null;
                                    data.nCreate = null;
                                    data.dCreate = null;
                                    data.nUpdate = null;
                                    data.dUpdate = null;
                                    data.nDelete = null;
                                    data.dDelete = null;
                                    data.IsDelete = false;
                                    DB.T_Request_AcceptWorkTime_Flow.Add(data);
                                }


                            }
                            else
                            {
                                nStap = lst_allow_Acc.nStep;
                                var Acc_detail = lst_Acc.FirstOrDefault(f => f.nStep == lst_allow_Acc.nStep);
                                if (Acc_detail != null)
                                {
                                    Acc_detail.nStep = nStap;
                                    Acc_detail.nUpdate = 1;
                                    Acc_detail.dUpdate = DateTime.Now;
                                    Acc_detail.nApproveID = 1;
                                    Acc_detail.dApprove = DateTime.Now;
                                    Acc_detail.sComment = null;
                                }

                            }

                        }

                        break;
                }

                DB.SaveChanges();
            }

        }


        public static void FlowRequest(int nMenuTypeID, int nRequestID, int? nEmp_App, string cApprove, string sComment)
        {
            Softthai_Intranet_2021Context DB = new Softthai_Intranet_2021Context();
            int? nStap = null;
            int? nFlow = null;
            switch (nMenuTypeID)
            {
                case 1:
                    var Data_Acc = DB.T_Request_AcceptWorkTime.FirstOrDefault(f => f.nAcceptWorkTimeID == nRequestID && f.IsDelete != true);
                    if (Data_Acc != null)
                    {
                        var lst_Mem = DB.TM_ProjectMember.Where(w => w.nProjectID == Data_Acc.nProjectID && w.IsDelete != true).ToList();
                        var lst_flow = DB.T_Request_AcceptWorkTime_Flow.Where(w => w.nAcceptWorkTimeID == nRequestID && w.IsDelete != true).ToList();

                        var _Laed = (from TM in DB.TM_ProjectMember.Where(w => w.nProjectID == Data_Acc.nProjectID && w.IsDelete != true).ToList()
                                     from TE in DB.T_Employee.Where(w => w.nEmployeeID == TM.nEmployeeID && w.nPositionID == 1)
                                     select TM).ToList();

                        var _PM = lst_Mem.Where(w => w.nRoleID == 2).ToList();

                        var _Hr = DB.T_Employee.Where(w => w.IsHR == true && w.IsDel != true).ToList();

                        var _Request = lst_Mem.FirstOrDefault(f => f.nEmployeeID == Data_Acc.nCreate);

                        nStap = Data_Acc.nStep == null ? 0 : Data_Acc.nStep;
                        var _Emp = DB.T_Employee.FirstOrDefault(f => f.nEmployeeID == Data_Acc.nCreate);
                        var item = new T_Request_AcceptWorkTime_Flow();
                        if (lst_flow.Count == 0)
                        {
                            item = new T_Request_AcceptWorkTime_Flow();
                            item.nAcceptWorkTimeID = nRequestID;
                            item.nFlowProcessID = 1;
                            item.sTypeFlow = "P";
                            item.nRoleID = _Request.nRoleID;
                            item.nApproveID = null;
                            item.cApprove = "A";
                            item.dApprove = null;
                            item.sComment = null;
                            item.nStep = nStap;
                            item.nCreate = Data_Acc.nCreate;
                            item.dCreate = DateTime.Now;
                            item.nUpdate = null;
                            item.dUpdate = null;
                            item.nDelete = null;
                            item.dDelete = null;
                            item.IsDelete = false;
                            DB.T_Request_AcceptWorkTime_Flow.Add(item);

                            switch (_Request.nRoleID)
                            {
                                case 2:
                                case 3:
                                    item = new T_Request_AcceptWorkTime_Flow();
                                    item.nAcceptWorkTimeID = nRequestID;
                                    item.nFlowProcessID = 7;
                                    item.sTypeFlow = _PM.Count > 1 ? "R" : "P";
                                    item.nRoleID = _PM.FirstOrDefault().nRoleID;
                                    item.nApproveID = null;
                                    item.cApprove = null;
                                    item.dApprove = null;
                                    item.sComment = null;
                                    item.nStep = nStap;
                                    item.nCreate = Data_Acc.nCreate;
                                    item.dCreate = DateTime.Now;
                                    item.nUpdate = null;
                                    item.dUpdate = null;
                                    item.nDelete = null;
                                    item.dDelete = null;
                                    item.IsDelete = false;
                                    DB.T_Request_AcceptWorkTime_Flow.Add(item);
                                    nFlow = item.nFlowProcessID;
                                    nStap++;
                                    break;
                                default:
                                    if (_Emp.nPositionID != 1)
                                    {
                                        item = new T_Request_AcceptWorkTime_Flow();
                                        item.nAcceptWorkTimeID = nRequestID;
                                        item.nFlowProcessID = 3;
                                        item.sTypeFlow = _Laed.Count > 1 ? "R" : "P"; ;
                                        item.nRoleID = _Laed.FirstOrDefault().nRoleID;
                                        item.nApproveID = null;
                                        item.cApprove = null;
                                        item.dApprove = null;
                                        item.sComment = null;
                                        item.nStep = nStap;
                                        item.nCreate = Data_Acc.nCreate;
                                        item.dCreate = DateTime.Now;
                                        item.nUpdate = null;
                                        item.dUpdate = null;
                                        item.nDelete = null;
                                        item.dDelete = null;
                                        item.IsDelete = false;
                                        DB.T_Request_AcceptWorkTime_Flow.Add(item);
                                        nFlow = item.nFlowProcessID;
                                        nStap++;
                                    }

                                    item = new T_Request_AcceptWorkTime_Flow();
                                    item.nAcceptWorkTimeID = nRequestID;
                                    item.nFlowProcessID = _Emp.nPositionID == 1 ? 5 : null;
                                    item.sTypeFlow = _PM.Count > 1 ? "R" : "P"; ;
                                    item.nRoleID = _PM.FirstOrDefault().nRoleID;
                                    item.nApproveID = null;
                                    item.cApprove = null;
                                    item.dApprove = null;
                                    item.sComment = null;
                                    item.nStep = _Emp.nPositionID == 1 ? nStap : null;
                                    item.nCreate = Data_Acc.nCreate;
                                    item.dCreate = DateTime.Now;
                                    item.nUpdate = null;
                                    item.dUpdate = null;
                                    item.nDelete = null;
                                    item.dDelete = null;
                                    item.IsDelete = false;
                                    DB.T_Request_AcceptWorkTime_Flow.Add(item);
                                    nFlow = _Emp.nPositionID == 1 ? item.nFlowProcessID : nFlow;
                                    nStap++;
                                    break;
                            }

                            item = new T_Request_AcceptWorkTime_Flow();
                            item.nAcceptWorkTimeID = nRequestID;
                            item.nFlowProcessID = null;
                            item.sTypeFlow = _Hr.Count > 1 ? "R" : "P"; ;
                            item.nRoleID = _Hr.FirstOrDefault().nRoleID;
                            item.nApproveID = null;
                            item.cApprove = null;
                            item.dApprove = null;
                            item.sComment = null;
                            item.nStep = null;
                            item.nCreate = Data_Acc.nCreate;
                            item.dCreate = DateTime.Now;
                            item.nUpdate = null;
                            item.dUpdate = null;
                            item.nDelete = null;
                            item.dDelete = null;
                            item.IsDelete = false;
                            DB.T_Request_AcceptWorkTime_Flow.Add(item);

                        }
                        if (lst_flow.Count > 0)
                        {
                            var De_flow = lst_flow.FirstOrDefault(f => f.nStep == Data_Acc.nStep);
                            if (De_flow != null)
                            {
                                if (_Emp.nPositionID == 1 && _Request.nRoleID != 1 && _Request.nRoleID != 2 && _Request.nRoleID != 3)
                                {
                                    switch (Data_Acc.nStep)
                                    {
                                        case 1:
                                            if (cApprove == "A")
                                            {
                                                De_flow.nFlowProcessID = 9; //approve by PM => waiting Hr
                                                De_flow.nStep = Data_Acc.nStep + 1;
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            else if (cApprove == "R")
                                            {
                                                De_flow.nFlowProcessID = 6; //Reject by PM
                                                nStap = 1;
                                                De_flow.cApprove = cApprove;
                                            }
                                            break;
                                        case 2:
                                            if (cApprove == "A")
                                            {
                                                De_flow.nFlowProcessID = 11;//Hr complete
                                                De_flow.nStep = Data_Acc.nStep + 1;
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            else if (cApprove == "R")
                                            {
                                                De_flow.nFlowProcessID = 10;//Reject by Hr
                                                nStap = 1;
                                                De_flow.cApprove = cApprove;
                                            }
                                            break;
                                        case 3:
                                            if (cApprove == "A")
                                            {
                                                De_flow.nFlowProcessID = 11;//Hr complete
                                                De_flow.nStep = Data_Acc.nStep + 1;
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            else if (cApprove == "R")
                                            {
                                                De_flow.nFlowProcessID = 10;//Reject by Hr
                                                nStap = 1;
                                                De_flow.cApprove = cApprove;
                                            }
                                            break;
                                    }
                                    De_flow.nApproveID = nEmp_App;
                                    De_flow.dApprove = DateTime.Now;
                                    De_flow.sComment = sComment;
                                    De_flow.nUpdate = nEmp_App;
                                    De_flow.dUpdate = DateTime.Now;

                                    nFlow = De_flow.nFlowProcessID;
                                }
                                else
                                if (_Request.nRoleID == 2 && _Request.nRoleID == 3)
                                {
                                    switch (Data_Acc.nStep)
                                    {
                                        case 1:
                                            if (cApprove == "A")
                                            {
                                                De_flow.nFlowProcessID = 9; //approve by MD => waiting Hr
                                                De_flow.nStep = (Data_Acc.nStep == null ? 0 : Data_Acc.nStep) + 1;
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            else if (cApprove == "R")
                                            {
                                                De_flow.nFlowProcessID = 8; //Reject by MD
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;

                                            }
                                            break;
                                        case 2:
                                            if (cApprove == "A")
                                            {
                                                De_flow.nFlowProcessID = 11;//Hr complete
                                                De_flow.nStep = (Data_Acc.nStep == null ? 0 : Data_Acc.nStep) + 1;
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            else if (cApprove == "R")
                                            {
                                                De_flow.nFlowProcessID = 10;//Reject by Hr
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            break;
                                        case 3:
                                            if (cApprove == "A")
                                            {
                                                De_flow.nFlowProcessID = 11;//Hr complete
                                                De_flow.nStep = (Data_Acc.nStep == null ? 0 : Data_Acc.nStep) + 1;
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            else if (cApprove == "R")
                                            {
                                                De_flow.nFlowProcessID = 10;//Reject by Hr
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            break;
                                    }
                                    De_flow.nApproveID = nEmp_App;
                                    De_flow.dApprove = DateTime.Now;
                                    De_flow.sComment = sComment;
                                    De_flow.nUpdate = nEmp_App;
                                    De_flow.dUpdate = DateTime.Now;
                                    nFlow = De_flow.nFlowProcessID;
                                }
                                else
                                if (_Emp.nPositionID != 1 && _Request.nRoleID != 1 && _Request.nRoleID != 2 && _Request.nRoleID != 3)
                                {
                                    switch (Data_Acc.nStep)
                                    {
                                        case 1:
                                            if (cApprove == "A")
                                            {
                                                De_flow.nFlowProcessID = 5; //approve by TL => waiting PM
                                                De_flow.nStep = (Data_Acc.nStep == null ? 0 : Data_Acc.nStep) + 1;
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            else if (cApprove == "R")
                                            {
                                                De_flow.nFlowProcessID = 4; //Reject by TL
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            break;
                                        case 2:
                                            if (cApprove == "A")
                                            {
                                                De_flow.nFlowProcessID = 9; //approve by PM => waiting Hr
                                                De_flow.nStep = (Data_Acc.nStep == null ? 0 : Data_Acc.nStep) + 1;
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            else if (cApprove == "R")
                                            {
                                                De_flow.nFlowProcessID = 6; //Reject by PM
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            break;
                                        case 3:
                                            if (cApprove == "A")
                                            {
                                                De_flow.nFlowProcessID = 11;//Hr complete
                                                De_flow.nStep = (Data_Acc.nStep == null ? 0 : Data_Acc.nStep) + 1;
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            else if (cApprove == "R")
                                            {
                                                De_flow.nFlowProcessID = 10;//Reject by Hr
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            break;
                                        case 4:
                                            if (cApprove == "A")
                                            {
                                                De_flow.nFlowProcessID = 11;//Hr complete
                                                De_flow.nStep = (Data_Acc.nStep == null ? 0 : Data_Acc.nStep) + 1;
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            else if (cApprove == "R")
                                            {
                                                De_flow.nFlowProcessID = 10;//Reject by Hr
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            break;
                                    }
                                    De_flow.nApproveID = nEmp_App;
                                    De_flow.dApprove = DateTime.Now;
                                    De_flow.sComment = sComment;
                                    De_flow.nUpdate = nEmp_App;
                                    De_flow.dUpdate = DateTime.Now;
                                    nStap = De_flow.nStep;
                                    nFlow = De_flow.nFlowProcessID;
                                }
                            }
                        }

                        Data_Acc.nStep = nStap;
                        Data_Acc.nFlowProcessID = nFlow;
                    }
                    break;
                case 2:
                    var Data_Allow = DB.T_Request_Allowance.FirstOrDefault(f => f.nAllowanceID == nRequestID && f.IsDelete != true);
                    if (Data_Allow != null)
                    {
                        var lst_Mem = DB.TM_ProjectMember.Where(w => w.nProjectID == Data_Allow.nProjectID && w.IsDelete != true).ToList();
                        var lst_flow = DB.T_Request_Allowance_Flow.Where(w => w.nAllowanceID == nRequestID && w.IsDelete != true).ToList();

                        var _Laed = (from TM in DB.TM_ProjectMember.Where(w => w.nProjectID == Data_Allow.nProjectID && w.IsDelete != true).ToList()
                                     from TE in DB.T_Employee.Where(w => w.nEmployeeID == TM.nEmployeeID && w.nPositionID == 1)
                                     select TM).ToList();

                        var _PM = lst_Mem.Where(w => w.nRoleID == 2).ToList();

                        var _Hr = DB.T_Employee.Where(w => w.IsHR == true && w.IsDel != true).ToList();

                        nStap = Data_Allow.nStep == null ? 0 : Data_Allow.nStep;
                        var _Request = lst_Mem.FirstOrDefault(f => f.nEmployeeID == Data_Allow.nCreate);
                        var _Emp = DB.T_Employee.FirstOrDefault(f => f.nEmployeeID == Data_Allow.nCreate);
                        var item_Allow = new T_Request_Allowance_Flow();
                        if (lst_flow.Count == 0)
                        {
                            nStap++;
                            item_Allow = new T_Request_Allowance_Flow();
                            item_Allow.nAllowanceID = nRequestID;
                            item_Allow.nFlowProcessID = 1;
                            item_Allow.sTypeFlow = "P";
                            item_Allow.nRoleID = _Request.nRoleID;
                            item_Allow.nApproveID = null;
                            item_Allow.cApprove = "A";
                            item_Allow.dApprove = null;
                            item_Allow.sComment = null;
                            item_Allow.nStep = nStap;
                            item_Allow.nCreate = Data_Allow.nCreate;
                            item_Allow.dCreate = DateTime.Now;
                            item_Allow.nUpdate = null;
                            item_Allow.dUpdate = null;
                            item_Allow.nDelete = null;
                            item_Allow.dDelete = null;
                            item_Allow.IsDelete = false;
                            DB.T_Request_Allowance_Flow.Add(item_Allow);

                            switch (_Request.nRoleID)
                            {
                                case 2:
                                case 3:
                                    item_Allow = new T_Request_Allowance_Flow();
                                    item_Allow.nAllowanceID = nRequestID;
                                    item_Allow.nFlowProcessID = 7;
                                    item_Allow.sTypeFlow = _PM.Count > 1 ? "R" : "P"; ;
                                    item_Allow.nRoleID = _PM.FirstOrDefault().nRoleID;
                                    item_Allow.nApproveID = null;
                                    item_Allow.cApprove = "W";
                                    item_Allow.dApprove = null;
                                    item_Allow.sComment = null;
                                    item_Allow.nStep = nStap;
                                    item_Allow.nCreate = Data_Allow.nCreate;
                                    item_Allow.dCreate = DateTime.Now;
                                    item_Allow.nUpdate = null;
                                    item_Allow.dUpdate = null;
                                    item_Allow.nDelete = null;
                                    item_Allow.dDelete = null;
                                    item_Allow.IsDelete = false;
                                    DB.T_Request_Allowance_Flow.Add(item_Allow);
                                    nStap++;
                                    break;
                                default:
                                    if (_Emp.nPositionID != 1)
                                    {
                                        item_Allow = new T_Request_Allowance_Flow();
                                        item_Allow.nAllowanceID = nRequestID;
                                        item_Allow.nFlowProcessID = 3;
                                        item_Allow.sTypeFlow = _Laed.Count > 1 ? "R" : "P"; ;
                                        item_Allow.nRoleID = _Laed.FirstOrDefault().nRoleID;
                                        item_Allow.nApproveID = null;
                                        item_Allow.cApprove = null;
                                        item_Allow.dApprove = null;
                                        item_Allow.sComment = null;
                                        item_Allow.nStep = nStap;
                                        item_Allow.nCreate = Data_Allow.nCreate;
                                        item_Allow.dCreate = DateTime.Now;
                                        item_Allow.nUpdate = null;
                                        item_Allow.dUpdate = null;
                                        item_Allow.nDelete = null;
                                        item_Allow.dDelete = null;
                                        item_Allow.IsDelete = false;
                                        DB.T_Request_Allowance_Flow.Add(item_Allow);
                                        nStap++;
                                    }

                                    item_Allow = new T_Request_Allowance_Flow();
                                    item_Allow.nAllowanceID = nRequestID;
                                    item_Allow.nFlowProcessID = _Emp.nPositionID == 1 ? 5 : null;
                                    item_Allow.sTypeFlow = _PM.Count > 1 ? "R" : "P"; ;
                                    item_Allow.nRoleID = _PM.FirstOrDefault().nRoleID;
                                    item_Allow.nApproveID = null;
                                    item_Allow.cApprove = null;
                                    item_Allow.dApprove = null;
                                    item_Allow.sComment = null;
                                    item_Allow.nStep = _Emp.nPositionID == 1 ? nStap : null;
                                    item_Allow.nCreate = Data_Allow.nCreate;
                                    item_Allow.dCreate = DateTime.Now;
                                    item_Allow.nUpdate = null;
                                    item_Allow.dUpdate = null;
                                    item_Allow.nDelete = null;
                                    item_Allow.dDelete = null;
                                    item_Allow.IsDelete = false;
                                    DB.T_Request_Allowance_Flow.Add(item_Allow);
                                    break;
                            }

                            item_Allow = new T_Request_Allowance_Flow();
                            item_Allow.nAllowanceID = nRequestID;
                            item_Allow.nFlowProcessID = null;
                            item_Allow.sTypeFlow = _Hr.Count > 1 ? "R" : "P"; ;
                            item_Allow.nRoleID = _Hr.FirstOrDefault().nRoleID;
                            item_Allow.nApproveID = null;
                            item_Allow.cApprove = null;
                            item_Allow.dApprove = null;
                            item_Allow.sComment = null;
                            item_Allow.nStep = null;
                            item_Allow.nCreate = Data_Allow.nCreate;
                            item_Allow.dCreate = DateTime.Now;
                            item_Allow.nUpdate = null;
                            item_Allow.dUpdate = null;
                            item_Allow.nDelete = null;
                            item_Allow.dDelete = null;
                            item_Allow.IsDelete = false;
                            DB.T_Request_Allowance_Flow.Add(item_Allow);

                        }
                        if (lst_flow.Count > 0)
                        {
                            var De_flow = lst_flow.FirstOrDefault(f => f.nStep == Data_Allow.nStep);
                            if (De_flow != null)
                            {
                                if (_Emp.nPositionID == 1 && _Request.nRoleID != 1 && _Request.nRoleID != 2 && _Request.nRoleID != 3)
                                {
                                    switch (Data_Allow.nStep)
                                    {
                                        case 1:
                                            if (cApprove == "A")
                                            {
                                                De_flow.nFlowProcessID = 9; //approve by PM => waiting Hr
                                                De_flow.nStep = Data_Allow.nStep + 1;
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            else if (cApprove == "R")
                                            {
                                                De_flow.nFlowProcessID = 6; //Reject by PM
                                                nStap = 1;
                                                De_flow.cApprove = cApprove;
                                            }
                                            break;
                                        case 2:
                                            if (cApprove == "A")
                                            {
                                                De_flow.nFlowProcessID = 11;//Hr complete
                                                De_flow.nStep = Data_Allow.nStep + 1;
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            else if (cApprove == "R")
                                            {
                                                De_flow.nFlowProcessID = 10;//Reject by Hr
                                                nStap = 1;
                                                De_flow.cApprove = cApprove;
                                            }
                                            break;
                                        case 3:
                                            if (cApprove == "A")
                                            {
                                                De_flow.nFlowProcessID = 11;//Hr complete
                                                De_flow.nStep = Data_Allow.nStep + 1;
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            else if (cApprove == "R")
                                            {
                                                De_flow.nFlowProcessID = 10;//Reject by Hr
                                                nStap = 1;
                                                De_flow.cApprove = cApprove;
                                            }
                                            break;
                                    }
                                    De_flow.nApproveID = nEmp_App;
                                    De_flow.dApprove = DateTime.Now;
                                    De_flow.sComment = sComment;
                                    De_flow.nUpdate = nEmp_App;
                                    De_flow.dUpdate = DateTime.Now;

                                    nFlow = De_flow.nFlowProcessID;
                                }
                                else
                                if (_Request.nRoleID == 2 && _Request.nRoleID == 3)
                                {
                                    switch (Data_Allow.nStep)
                                    {
                                        case 1:
                                            if (cApprove == "A")
                                            {
                                                De_flow.nFlowProcessID = 9; //approve by MD => waiting Hr
                                                De_flow.nStep = (Data_Allow.nStep == null ? 0 : Data_Allow.nStep) + 1;
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            else if (cApprove == "R")
                                            {
                                                De_flow.nFlowProcessID = 8; //Reject by MD
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;

                                            }
                                            break;
                                        case 2:
                                            if (cApprove == "A")
                                            {
                                                De_flow.nFlowProcessID = 11;//Hr complete
                                                De_flow.nStep = (Data_Allow.nStep == null ? 0 : Data_Allow.nStep) + 1;
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            else if (cApprove == "R")
                                            {
                                                De_flow.nFlowProcessID = 10;//Reject by Hr
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            break;
                                        case 3:
                                            if (cApprove == "A")
                                            {
                                                De_flow.nFlowProcessID = 11;//Hr complete
                                                De_flow.nStep = (Data_Allow.nStep == null ? 0 : Data_Allow.nStep) + 1;
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            else if (cApprove == "R")
                                            {
                                                De_flow.nFlowProcessID = 10;//Reject by Hr
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            break;
                                    }
                                    De_flow.nApproveID = nEmp_App;
                                    De_flow.dApprove = DateTime.Now;
                                    De_flow.sComment = sComment;
                                    De_flow.nUpdate = nEmp_App;
                                    De_flow.dUpdate = DateTime.Now;
                                    nFlow = De_flow.nFlowProcessID;
                                }
                                else
                                if (_Emp.nPositionID != 1 && _Request.nRoleID != 1 && _Request.nRoleID != 2 && _Request.nRoleID != 3)
                                {
                                    switch (Data_Allow.nStep)
                                    {
                                        case 1:
                                            if (cApprove == "A")
                                            {
                                                De_flow.nFlowProcessID = 5; //approve by TL => waiting PM
                                                De_flow.nStep = (Data_Allow.nStep == null ? 0 : Data_Allow.nStep) + 1;
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            else if (cApprove == "R")
                                            {
                                                De_flow.nFlowProcessID = 4; //Reject by TL
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            break;
                                        case 2:
                                            if (cApprove == "A")
                                            {
                                                De_flow.nFlowProcessID = 9; //approve by PM => waiting Hr
                                                De_flow.nStep = (Data_Allow.nStep == null ? 0 : Data_Allow.nStep) + 1;
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            else if (cApprove == "R")
                                            {
                                                De_flow.nFlowProcessID = 6; //Reject by PM
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            break;
                                        case 3:
                                            if (cApprove == "A")
                                            {
                                                De_flow.nFlowProcessID = 11;//Hr complete
                                                De_flow.nStep = (Data_Allow.nStep == null ? 0 : Data_Allow.nStep) + 1;
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            else if (cApprove == "R")
                                            {
                                                De_flow.nFlowProcessID = 10;//Reject by Hr
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            break;
                                        case 4:
                                            if (cApprove == "A")
                                            {
                                                De_flow.nFlowProcessID = 11;//Hr complete
                                                De_flow.nStep = (Data_Allow.nStep == null ? 0 : Data_Allow.nStep) + 1;
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            else if (cApprove == "R")
                                            {
                                                De_flow.nFlowProcessID = 10;//Reject by Hr
                                                De_flow.cApprove = cApprove;
                                                nStap = De_flow.nStep;
                                            }
                                            break;
                                    }
                                    De_flow.nApproveID = nEmp_App;
                                    De_flow.dApprove = DateTime.Now;
                                    De_flow.sComment = sComment;
                                    De_flow.nUpdate = nEmp_App;
                                    De_flow.dUpdate = DateTime.Now;
                                    nStap = De_flow.nStep;
                                    nFlow = De_flow.nFlowProcessID;
                                }
                            }
                        }

                        Data_Allow.nStep = nStap;
                        Data_Allow.nFlowProcessID = nFlow;
                    }
                    break;
                case 3:
                    var Data_Travel = DB.T_Request_Travel.FirstOrDefault(f => f.nReqTravelID == nRequestID && f.IsDelete != true);
                    var Data_Travel_F = DB.T_Request_Travel_Flow.Where(w => w.nReqTravelID == Data_Travel.nReqTravelID).ToList();
                    if (Data_Travel != null)
                    {
                        var _Hr = DB.T_Employee.Where(w => w.IsHR == true && w.IsDel != true).ToList();

                        var _Emp = DB.T_Employee.FirstOrDefault(f => f.nEmployeeID == Data_Travel.nCreate);
                        var item_Travel = new T_Request_Travel_Flow();
                        if (Data_Travel_F.Count == 0)
                        {
                            item_Travel = new T_Request_Travel_Flow();
                            item_Travel.nReqTravelID = nRequestID;
                            item_Travel.nFlowProcessID = 1;
                            item_Travel.sTypeFlow = "P";
                            item_Travel.nRoleID = _Emp.nRoleID;
                            item_Travel.nApproveID = null;
                            item_Travel.cApprove = "A";
                            item_Travel.dApprove = null;
                            item_Travel.sComment = null;
                            item_Travel.nStep = 1;
                            item_Travel.nCreate = Data_Travel.nCreate;
                            item_Travel.dCreate = DateTime.Now;
                            item_Travel.nUpdate = null;
                            item_Travel.dUpdate = null;
                            item_Travel.nDelete = null;
                            item_Travel.dDelete = null;
                            item_Travel.IsDelete = false;
                            DB.T_Request_Travel_Flow.Add(item_Travel);

                            item_Travel = new T_Request_Travel_Flow();
                            item_Travel.nReqTravelID = nRequestID;
                            item_Travel.nFlowProcessID = null;
                            item_Travel.sTypeFlow = _Hr.Count > 1 ? "R" : "P"; ;
                            item_Travel.nRoleID = _Hr.FirstOrDefault().nRoleID;
                            item_Travel.nApproveID = null;
                            item_Travel.cApprove = "W";
                            item_Travel.dApprove = null;
                            item_Travel.sComment = null;
                            item_Travel.nStep = 2;
                            item_Travel.nCreate = Data_Travel.nCreate;
                            item_Travel.dCreate = DateTime.Now;
                            item_Travel.nUpdate = null;
                            item_Travel.dUpdate = null;
                            item_Travel.nDelete = null;
                            item_Travel.dDelete = null;
                            item_Travel.IsDelete = false;
                            DB.T_Request_Travel_Flow.Add(item_Travel);
                            nFlow = item_Travel.nFlowProcessID;
                        }
                        if (Data_Travel_F.Count > 0)
                        {
                            var De_flow = Data_Travel_F.FirstOrDefault(f => f.nStep == Data_Travel.nStep);
                            if (De_flow != null)
                            {
                                switch (Data_Travel.nStep)
                                {
                                    case 1:
                                        if (cApprove == "A")
                                        {
                                            De_flow.nFlowProcessID = 1; //approve by requester => waiting Hr
                                            De_flow.nStep = Data_Travel.nStep + 1;
                                            De_flow.cApprove = cApprove;
                                            nStap = De_flow.nStep;
                                        }
                                        else if (cApprove == "R")
                                        {
                                            De_flow.nFlowProcessID = 2; //Reject by requester
                                            nStap = 1;
                                            De_flow.cApprove = cApprove;
                                        }
                                        break;
                                    case 2:
                                        if (cApprove == "A")
                                        {
                                            De_flow.nFlowProcessID = 11;//Hr complete
                                            De_flow.nStep = Data_Travel.nStep + 1;
                                            De_flow.cApprove = cApprove;
                                            nStap = De_flow.nStep;
                                        }
                                        else if (cApprove == "R")
                                        {
                                            De_flow.nFlowProcessID = 10;//Reject by Hr
                                            nStap = 1;
                                            De_flow.cApprove = cApprove;
                                        }
                                        break;
                                }
                                De_flow.nApproveID = nEmp_App;
                                De_flow.dApprove = DateTime.Now;
                                De_flow.sComment = sComment;
                                De_flow.nUpdate = nEmp_App;
                                De_flow.dUpdate = DateTime.Now;

                                nFlow = De_flow.nFlowProcessID;
                            }

                        }

                        Data_Travel.nStep = nStap;
                        Data_Travel.nFlowProcessID = nFlow;
                    }
                    break;
            }
            DB.SaveChanges();
        }
    }
}
