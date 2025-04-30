using Staffportal.Models;
using Staffportal.NAVWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Staffportal.Controllers
{
    public class AccountController : Controller
    {
        Staffportall webportals = Components.ObjNav;
        string[] strLimiters = new string[] { "::" };
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            try
            {
                string username = user.UserName;
                string password = user.Password;
                string validstaffNo = webportals.CheckValidStaffNo(username);
                 if (validstaffNo != "Valid")
                {
                    TempData["Error"] = "Invalid staff No.";
                    return RedirectToAction("index", "account");
                }
                //if (!webportals.ValidStaffNo(username))
                //{
                //    TempData["Error"] = "Invalid staff No.";
                //    return RedirectToAction("index", "account");
                //}

                if (ChangedPassword(username))
                {
                    return RedirectToAction("loginforchangedpassword", "account", new { username, password });
                }
                else
                {
                    return RedirectToAction("loginforunchangedpassword", "account", new { username });
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("index");
            }
        }

        public ActionResult LoginForChangedPassword(string username, string password)
        {
            try
            {
                string response = webportals.StaffLogin(username, password);
                if (!string.IsNullOrEmpty(response))
                {
                    string[] responseArr = response.Split(strLimiters, StringSplitOptions.None);
                    string returnMsg = responseArr[0];
                    if (returnMsg == "SUCCESS")
                    {
                        string staffNo = responseArr[1];
                        string staffName = responseArr[2];
                        Session["username"] = staffNo;
                        Session["staffName"] = staffName;
                        return RedirectToAction("index", "dashboard");
                    }
                    else
                    {
                        TempData["Error"] = returnMsg;
                        return RedirectToAction("index", "account");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("index", "account");
            }
            return RedirectToAction("index", "account");
        }

        public ActionResult LoginForUnchangedPassword(string username)
        {
            try
            {
                string response = webportals.LoginForUnchangedPassword(username);
                if (!string.IsNullOrEmpty(response))
                {
                    string[] responseArr = response.Split(strLimiters, StringSplitOptions.None);
                    string returnMsg = responseArr[0];
                    if (returnMsg == "SUCCESS")
                    {
                        string staffNo = responseArr[1];
                        string companyEmail = responseArr[2];
                        //string staffEmail = responseArr[3];
                        Session["staffNo"] = staffNo;
                        Session["companyEmail"] = companyEmail == null ? companyEmail : companyEmail;
                        return RedirectToAction("resetpassword", "account");
                    }
                    else
                    {
                        TempData["Error"] = returnMsg;
                        return RedirectToAction("index", "account");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("index", "account");
            }
            return RedirectToAction("index", "account");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(User user)
        {
            try
            {
                string username = user.UserName;
                string validstaffNo = webportals.CheckValidStaffNo(username);
                if (validstaffNo != "Valid")
                {
                    TempData["Error"] = "Invalid staff No.";
                    return RedirectToAction("forgotpassword", "account");
                }
                //if (!webportals.ValidStaffNo(username))
                //{
                //    TempData["Error"] = "Invalid staff No.";
                //    return RedirectToAction("forgotpassword", "account");
                //}
                Session["staffNo"] = username;
                return RedirectToAction("resetpassword", "account");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("forgotpassword", "account");
            }
        }

        public ActionResult ResetPassword()
        {
            if (Session["staffNo"] == null) return RedirectToAction("index", "account");
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(User user)
        {
            try
            {
                string username = Session["staffNo"].ToString();
                string newPassword = user.NewPassword;
                string response = webportals.UpdateStaffPassword(username, newPassword);
                if (response == "SUCCESS")
                {
                    TempData["Success"] = "Password has been updated successfully";
                    return RedirectToAction("index", "account");

                }
                //if (webportals.UpdateStaffPassword(username, newPassword))
                //{
                //    TempData["Success"] = "Password has been updated successfully";
                //    return RedirectToAction("index", "account");
                //}
                else
                {
                    TempData["Error"] = "An error occured while updating the password. Please try again later!";
                    return RedirectToAction("resetpassword", "account");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("resetpassword", "account");
            }
        }

        private bool ChangedPassword(string username)
        {
            bool changed = false;

            try
            {
                //changed = webportals.CheckStaffPasswordChanged(username);

                string response = webportals.CheckStaffPasswordChanged(username);
                if (!string.IsNullOrEmpty(response))
                {
                    if (response == "Yes")
                    {
                        changed = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return changed;
        }
    }
}