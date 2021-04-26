using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Globalization;
using Microsoft.Extensions.Hosting;

namespace PTT_GSM_CSC_Factory.Extensions
{
    public class SystemConfigSendMail
    {
        public IConfiguration _Configuration { get; }
        private readonly IHostEnvironment _env;
        public string sSMTP { get; set; }
        public string sDemoSend { get; set; }
        public string sDemoMailTo { get; set; }
        public string sDemeMailCC { get; set; }
        public bool IsTestMode { get; set; }
        public SystemConfigSendMail(IConfiguration Configuration, IHostEnvironment env)
        {
            this._Configuration = Configuration;
            this._env = env;
            this.sSMTP = _Configuration["SettingMail:SMTPSever"];
            this.sDemoMailTo = _Configuration["SettingMail:DemoMailTo"];
            this.sDemeMailCC = _Configuration["SettingMail:DemeMailCC"];
            this.sDemoSend = _Configuration["SettingMail:DemoSend"];
            this.IsTestMode = _Configuration.GetValue<bool>("SettingMail:TestMode");
        }
        private MailMessage Create(string sFrom, string sFromName, string sSubject, string sMessage, List<string> lstFile) => Create(new MailAddress(sFrom, sFromName, System.Text.Encoding.UTF8), sSubject, sMessage, lstFile);

        private MailMessage Create(MailAddress addr, string sSubject, string sMessage, List<string> lstFile)
        {
            if (lstFile == null) lstFile = new List<string>();

            MailMessage m = new MailMessage();
            m.From = addr;
            m.Subject = sSubject;
            m.IsBodyHtml = true;
            m.BodyEncoding = System.Text.Encoding.UTF8;
            m.Body = sMessage;
            m.Priority = MailPriority.Normal;
            if (lstFile.Any()) lstFile.Select(s => new Attachment(s)).ToList().ForEach(m.Attachments.Add);

            return m;
        }
        public async Task<bool> Send(string sFrom, string sTo, List<string> lstCC, string sSubject, string sMessage) => await Send(sFrom, "", new List<string> { sTo }, lstCC, sSubject, sMessage);
        public async Task<bool> Send(string sFrom, List<string> lstTo, List<string> lstCC, string sSubject, string sMessage) => await Send(sFrom, "", lstTo, lstCC, sSubject, sMessage);
        public async Task<bool> Send(string sFrom, string sFromName, string sTo, List<string> lstCC, string sSubject, string sMessage) => await Send(sFrom, sFromName, new List<string> { sTo }, lstCC, sSubject, sMessage);
        public async Task<bool> Send(string sFrom, string sFromName, List<string> lstTo, List<string> lstCC, string sSubject, string sMessage) => await SendWithAttachment(sFrom, sFromName, lstTo, lstCC, sSubject, sMessage, null);
        public async Task<bool> SendWithAttachment(string sFrom, List<string> lstTo, List<string> lstCC, string sSubject, string sMessage, List<string> lstFile) => await SendWithAttachment(sFrom, "", lstTo, lstCC, sSubject, sMessage, lstFile);
        public async Task<bool> SendWithAttachment(string sFrom, string sFromName, List<string> lstTo, List<string> lstCC, string sSubject, string sMessage, List<string> lstFile)
        {
            bool isSuccess = false;
            bool isTestMode = IsTestMode; //Check TESTING MODE

            string SMTP = sSMTP;
            MailMessage m = Create(isTestMode ? sDemoSend : sFrom, sFromName, sSubject, sMessage, lstFile);
            using (var mClient = new SmtpClient(SMTP))
            {
                try
                {
                    #region IF code in TESTING MODE then SEND to TEST RECEIVER
                    if (isTestMode) //IF code in TESTING MODE then SEND to TEST RECEIVER
                    {
                        m.Subject = "[E-MAIL TEST MODE] " + m.Subject;

                        string HTML_NewLine = "<br>";
                        m.Body = m.Body +
                            "THIS MAIL WAS SENT FOR TESTING" +
                            "<hr>" +
                            "<b>FROM : </b> " + sFrom + HTML_NewLine +
                            "<b>TO : </b> " + string.Join(", ", lstTo) + HTML_NewLine +
                            "<b>CC : </b> " + string.Join(", ", lstCC) +
                            "<hr>"
                           ;

                        lstTo = sDemoMailTo.Split(',').ToList();
                    }
                    #endregion

                    lstTo.ForEach(m.To.Add);
                    if (!isTestMode) lstCC.ForEach(m.CC.Add);

                    await mClient.SendMailAsync(m);
                    isSuccess = true;

                }
                catch (Exception)
                {
                    isSuccess = false;

                }
                finally
                {

                    mClient.Dispose();
                }

            }

            return isSuccess;
        }

        [Serializable]
        public class VCALENDAR
        {
            public bool isCanceled { get; set; }
            public DateTime dBegin { get; set; }
            public DateTime dEnd { get; set; }
            public string sLocation { get; set; } //สถานที่นัดหมาย
            public MailAddressCollection colAttendee { get; set; }
            public string sUID { get; set; }

            public AlternateView Build(MailMessage m)
            {
                //   Func<DateTime, string> DateToStr = new Func<DateTime, string>(d => EasyFunction.DateTime_to_StringEN(d, "yyyyMMddTHHmmssZ", ""));
                Func<DateTime, string> DateToStr = (d) =>
                {
                    return d.ToString("yyyyMMddTHHmmssZ", new CultureInfo("en-US"));
                };

                string sMethod = isCanceled ? "CANCEL" : "REQUEST";
                string sStatus = isCanceled ? "CANCELLED" : "CONFIRMED";
                string sListAttendee = colAttendee != null ? colAttendee.ToString() : m.To.ToString();

                System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType("text/calendar");
                ct.Parameters.Add("method", sMethod);

                #region VCALENDAR
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.AppendLine("BEGIN:VCALENDAR");
                sb.AppendLine("PRODID:-//CARS//Outlook MIMEDIR//EN");
                sb.AppendLine("VERSION:2.0");
                sb.AppendLine("METHOD:" + sMethod);

                #region VEVENT
                sb.AppendLine("BEGIN:VEVENT");

                sb.AppendLine("UID:" + sUID);
                sb.AppendLine("DTSTART:" + DateToStr(dBegin.AddHours(-7)));
                sb.AppendLine("DTEND:" + DateToStr(dEnd.AddHours(-7)));
                sb.AppendLine("LOCATION:" + sLocation);
                sb.AppendLine("SUMMARY:" + m.Subject);
                sb.AppendLine("DESCRIPTION:" + "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 3.2//EN'><HTML><BODY>" + m.Body + "</BODY></HTML>"); //m.Subject
                sb.AppendLine("X-ALT-DESC;FMTTYPE=text/html:" + "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 3.2//EN'><HTML><BODY>" + m.Body + "</BODY></HTML>");
                sb.AppendLine("ACTION;RSVP=TRUE;CN=\"" + m.From.Address + "\":MAILTO:" + m.From.Address);
                sb.AppendLine("ORGANIZER;CN=\"" + m.From.DisplayName + "\":mailto:" + m.From.Address);
                sb.AppendLine("ATTENDEE;ROLE=REQ-PARTICIPANT;PARTSTAT=NEEDS-ACTION;RSVP=TRUE;CN=\"" + sListAttendee + "\":MAILTO:" + sListAttendee);
                sb.AppendLine("STATUS:" + sStatus);

                sb.AppendLine("BEGIN:VALARM");
                sb.AppendLine("TRIGGER:-PT15M");
                sb.AppendLine("ACTION:DISPLAY");
                sb.AppendLine("DESCRIPTION:Reminder");
                sb.AppendLine("END:VALARM");

                sb.AppendLine("END:VEVENT");
                #endregion

                sb.AppendLine("END:VCALENDAR");
                sb.AppendLine("SEQUENCE:0");
                #endregion

                return AlternateView.CreateAlternateViewFromString(sb.ToString(), ct);
            }
        }

        public class ActionMailResult
        {
            public string sGUID { get; set; }
            public bool isSuccess { get; set; }
            public string sMessage { get; set; }
        }

        public async Task<ActionMailResult> SendWithAppointment(string sFrom, string sTo, List<string> lstCC, string sSubject, string sMessage, VCALENDAR cld) => await SendWithAppointment(sFrom, "", new List<string>() { sTo }, lstCC, sSubject, sMessage, cld);
        public async Task<ActionMailResult> SendWithAppointment(string sFrom, string sFromName, List<string> lstTo, List<string> lstCC, string sSubject, string sMessage, VCALENDAR cld)
        {
            ActionMailResult r = new ActionMailResult();
            bool isTestMode = IsTestMode; //Check TESTING MODE


            string SMTP = sSMTP;

            using (var mClient = new SmtpClient(SMTP))
            {
                try
                {
                    MailMessage m = Create(isTestMode ? sDemoSend : sFrom, sFromName, sSubject, sMessage, new List<string>() { });
                    var alvBody = AlternateView.CreateAlternateViewFromString(m.Body, System.Text.Encoding.UTF8, "text/html");
                    m.AlternateViews.Add(alvBody);

                    if (string.IsNullOrEmpty(cld.sUID)) cld.sUID = Guid.NewGuid().ToString();
                    var alvCld = cld.Build(m);
                    m.AlternateViews.Add(alvCld);

                    m.Body = "";

                    #region IF code in TESTING MODE then SEND to TEST RECEIVER
                    if (isTestMode) //IF code in TESTING MODE then SEND to TEST RECEIVER
                    {
                        m.Subject = "[TEST] " + m.Subject;

                        string HTML_NewLine = "<br>";
                        m.Body =
                            "THIS MAIL WAS SENT FOR TESTING" +
                            "<hr>" +
                            "<b>FROM : </b> " + sFrom + HTML_NewLine +
                            "<b>TO : </b> " + string.Join(", ", lstTo) + HTML_NewLine +
                            "<b>CC : </b> " + string.Join(", ", lstCC) +
                            "<hr>" +
                            m.Body;

                        lstTo = sDemeMailCC.Split(',').ToList();
                    }
                    #endregion

                    lstTo.ForEach(m.To.Add);
                    if (!isTestMode) lstCC.ForEach(m.CC.Add);

                    await mClient.SendMailAsync(m);

                    r.isSuccess = true;
                    r.sGUID = cld.sUID;
                }
                catch (Exception ex)
                {
                    r.isSuccess = false;
                    r.sMessage = ex.Message;
                }
                finally
                {
                    mClient.Dispose();
                }
            }

            return r;
        }
    }


}