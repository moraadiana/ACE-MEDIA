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
                string leaveType = "ANNUAL";
                if (!string.IsNullOrEmpty(response))
                {
                    string[] responseArr = response.Split(strLimiters, StringSplitOptions.None);
                   // user.JobId = responseArr[1];
                    user.Gender = responseArr[2];
                    user.IdNumber = responseArr[3];
                    user.EmailAddress = responseArr[5];
                    user.PhoneNumber = responseArr[6];
                    user.PostalAddress = responseArr[9];
                    user.JobTitle = responseArr[10];
                    //string leaveBalance = responseArr[11];
                    //user.LeaveBalance = Convert.ToDecimal(leaveBalance);

                     string leaveBalance = webportals.AvailableLeaveDays(username, leaveType);;
                     user.LeaveBalance = Convert.ToDecimal(leaveBalance);
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
        public ActionResult UpdateProfile()
        {
            if(Session["username"] == null) return RedirectToAction("index", "account");
            User user = new User();
            try
               
            {
                string username = Session["username"].ToString();
                string staffName = Session["staffName"].ToString();
                string response = webportals.GetEmployeeDetails(username);
                if (!string.IsNullOrEmpty(response))
                {
                    string[] responseArr = response.Split(strLimiters, StringSplitOptions.None);
                    // user.JobId = responseArr[1];
                    user.Gender = responseArr[4];
                    user.DOB = responseArr[5];
                    user.MaritalStatus = responseArr[6];
                    user.Religion= responseArr[7];
                    user.Tribe = responseArr[8];
                    user.EmailAddress = responseArr[9];
                    user.PhoneNumber = responseArr[10];
                    user.County = responseArr[11];
                    user.IdNumber = responseArr[13];
                    
                    user.PostalAddress = responseArr[14];
                    user.Title = responseArr[15];
                   
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

        [HttpPost]
        public ActionResult UpdateProfileData(User model)
        {
            if (Session["username"] == null) return RedirectToAction("index", "account");
            try
            {
                string empNo = Session["username"].ToString();
                string gender = model.Gender;
                string Dob = model.DOB;
                string maritalStatus = model.MaritalStatus;
                string religion = model.Religion;
                string tribe = model.Tribe;
                string email = model.EmailAddress;
                string phoneNo = model.PhoneNumber;
                string county = model.County;
                string IdNo = model.IdNumber;
                string postAddress = model.PostalAddress;
                string title = model.Title;
               

                string response = webportals.UpdateEmployeeDetails(empNo,Convert.ToInt32(gender),Convert.ToDateTime(Dob), Convert.ToInt32(maritalStatus), religion, tribe, email, phoneNo, county, IdNo, postAddress,Convert.ToInt32(title));
                if (response == "Success")
                {
                    TempData["Success"]= "Profile updated successfully.";

                }
                else
                {
                    TempData["Error"] = "Failed to update";

                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("index", "dashboard");

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