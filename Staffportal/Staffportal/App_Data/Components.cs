using Staffportal.NAVWS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Staffportal
{
    public class Components
    {
        public static SqlConnection connection;
        public static string Company_Name = "AceMedia";

        public static Staffportall ObjNav
        {
            get
            {
                var ws = new Staffportall();
                try
                {
                    var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"]);
                    ws.PreAuthenticate = true;
                    ws.Credentials = credentials;
                }
                catch (Exception ex)
                {
                    ex.Data.Clear();
                }
                return ws;
            }
        }

        public static void SendEmailAlerts(string address, string subject, string message)
        {
            try
            {
                string email = "dynamicsselfservice@gmail.com";
                string password = "ydujienvejtdojgv";

                var loginInfo = new NetworkCredential(email, password);
                var msg = new MailMessage();
                var smtpClient = new SmtpClient("smtp.gmail.com", 25);

                msg.From = new MailAddress(email);
                msg.To.Add(new MailAddress(address));
                msg.Subject = subject;
                msg.Body = $"<body style='font-family: Cambria, Cochin, Georgia, Times, 'Times New Roman', serif;'>{message}</body>";
                msg.IsBodyHtml = true;

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = loginInfo;
                smtpClient.Send(msg);
            }
            catch (Exception Ex)
            {
                Ex.Data.Clear();
            }
        }

        public static string StatusClass(string status)
        {
            string statusCls = "default";
            switch (status)
            {
                case "Open":
                    statusCls = "warning";
                    break;
                case "Pending Approval":
                    statusCls = "primary";
                    break;
                case "Approved":
                    statusCls = "success";
                    break;
                case "Canceled":
                    statusCls = "danger";
                    break;
                case "Posted":
                    statusCls = "success";
                    break;
                default:
                    statusCls = "info";
                    break;
            }
            return statusCls;
        }


    }
}