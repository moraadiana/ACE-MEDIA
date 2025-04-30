using Staffportal.Models;
using Staffportal.NAVWS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Staffportal.Controllers
{
    public class DashboardController : Controller
    {
        Staffportall webportals = Components.ObjNav;
        string[] strLimiters = new string[] { "::" };
        string[] strLimiters2 = new string[] { "[]" };
        public ActionResult Index()
        {
            if (Session["username"] == null) return RedirectToAction("index", "account");
            User user = new User();
            try
            {
                string downloads = Server.MapPath("~/Downloads/");
                if (!Directory.Exists(downloads)) Directory.CreateDirectory(downloads);
                string username = Session["username"].ToString();
                string staffName = Session["staffName"].ToString();
                string response = webportals.GetStaffDetails(username);
                if (!string.IsNullOrEmpty(response))
                {
                    string[] responseArr = response.Split(strLimiters, StringSplitOptions.None);
                    user.JobId = responseArr[1];
                    user.JobTitle = responseArr[2];
                    user.Gender = responseArr[3];
                    user.IdNumber = responseArr[4];
                    user.EmailAddress = responseArr[5];
                    user.PhoneNumber = responseArr[6];
                    user.PostalAddress = responseArr[7];
                    user.LeaveBalance = 2;
                }
                user.StaffNo = username;
                user.StaffName = staffName;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("index", "account");
        }
    }
}