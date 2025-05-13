using Staffportal.Models;
using Staffportal.NAVWS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Staffportal.Controllers
{
    public class HumanResourceController : Controller
    {
        Staffportall webportals = Components.ObjNav;
        string[] strLimiters = new string[] { "::" };
        string[] strLimiters2 = new string[] { "[]" };
        public ActionResult LeaveListing()
        {
            if (Session["username"] == null) return RedirectToAction("index", "account");
            HumanResource resource = new HumanResource();
            try
            {
                string username = Session["username"].ToString();
                var leaveRequests = HRHelper.GetLeaveRequests(username);
                resource.LeaveListing = leaveRequests;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(resource);
        }

        public ActionResult LeaveApplication()
        {
            if (Session["username"] == null) return RedirectToAction("index", "account");
            HumanResource resource = new HumanResource();
            try
            {
                string username = Session["username"].ToString();
                string grouping = "LEAVE";
                string gender = Components.ObjNav.GetStaffGender(username);
                Config departmentDetails = Helper.GetDepartmentDetails(username);
                var resCenters = Helper.GetResponsibilityCenters(grouping);
                var leaveTypes = HRHelper.GetLeaveTypes(gender);
                var relievers = Helper.GetRelievers(username);

                resource.Directorate = departmentDetails.Directorate;
                resource.Department = departmentDetails.Department;
                resource.ResponsibilityCenters = resCenters;
                resource.LeaveTypes = leaveTypes;
                resource.Relievers = relievers;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(resource);
        }

        [HttpPost]
        public ActionResult LeaveApplication(HumanResource human)
        {
            try
            {
                string username = Session["username"].ToString();
                string reliever = human.Reliever;
                string leaveType = human.LeaveType;
                decimal appliedDays = human.AppliedDays;
                DateTime startDate = human.StartDate;
                DateTime endDate = human.EndDate;
                DateTime returnDate = human.ReturnDate;
                string purpose = human.Purpose;
                string resCenter = human.ResponsibilityCenter;
                decimal leaveBalance = human.LeaveBalance;
                //string comments = human.Comments;
                string leaveNo = webportals.HRMLeaveApplication(username, "", leaveType, appliedDays, startDate, endDate, returnDate, purpose, resCenter, leaveBalance);
              String response=  webportals.OnsendLeaveRequisitionForApproval(leaveNo);
                if (response == "SUCCESS")
                {
                    TempData["Success"] = $"Leave requisition number {leaveNo} has been sent for approval successfully";
                }
                else
                {
                    TempData["Error"] = response;
                }
                //NotifyReliever(leaveNo, reliever, comments);
               TempData["Success"] = $"Leave requisition number {leaveNo} has been sent for approval successfully";
                return RedirectToAction("leavelisting", "humanresource");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("leaveapplication", "humanresource");
            }
        }

        private void NotifyReliever(string leaveNo, string reliever, string comments)
        {
            try
            {
                string employeeName = string.Empty;
                string leaveType = string.Empty;
                string appliedDays = string.Empty;
                string startDate = string.Empty;
                string endDate = string.Empty;
                string returnDate = string.Empty;
                string purpose = string.Empty;
                string email = string.Empty;
                string relieverName = string.Empty;

                string leaveDetails = webportals.GetLeaveDetails(leaveNo);
                if (!string.IsNullOrEmpty(leaveDetails))
                {
                    string[] leaveDetailsArr = leaveDetails.Split(strLimiters, StringSplitOptions.None);
                    employeeName = leaveDetailsArr[1];
                    leaveType = leaveDetailsArr[2];
                    appliedDays = leaveDetailsArr[3];
                    startDate = leaveDetailsArr[4];
                    endDate = leaveDetailsArr[5];
                    returnDate = leaveDetailsArr[6];
                    purpose = leaveDetailsArr[7];
                }

                string relieverDetails = webportals.GetRelieverDetails(reliever);
                if (!string.IsNullOrEmpty(relieverDetails))
                {
                    string[] reliverDetailsArr = relieverDetails.Split(strLimiters, StringSplitOptions.None);
                    email = string.IsNullOrEmpty(reliverDetailsArr[1]) ? reliverDetailsArr[2] : reliverDetailsArr[1];
                    relieverName = reliverDetailsArr[3];
                }

                string subject = "Leave Relieval Request";
                string message = $"Dear {relieverName}," +
                    $"<br/><br/>" +
                    $"You have been selected by {employeeName} to be his/her reliever for leave number {leaveNo}. Below are the leave details" +
                    $"<br/></br/>" +
                    $"Leave No: <strong>{leaveNo}</strong>" +
                    $"<br/>" +
                    $"Leave Type: <strong>{leaveType}</strong>" +
                    $"<br/>" +
                    $"Applied Days: <strong>{appliedDays}</strong>" +
                    $"<br/>" +
                    $"Start Date: <strong>{startDate}</strong>" +
                    $"<br/>" +
                    $"End Date: <strong>{endDate}</strong>" +
                    $"<br/>" +
                    $"Return Date: <strong>{returnDate}</strong>" +
                    $"<br/>" +
                    $"Purpose: <strong>{purpose}</strong>" +
                    $"<br/><br/>" +
                    $"Comments: <strong>{comments}</strong>" +
                    $"<br/><br/>" +
                    $"Kind Regard, Administrator" +
                    $"<br/><br/>" +
                    $"This email is system generated. <strong style='color: red;'>DO NOT REPLY</strong>";
                if (!string.IsNullOrEmpty(email)) Components.SendEmailAlerts("dmoraa@appkings.co.ke", subject, message);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
        }

        public ActionResult CancelLeaveRequisition(string leaveNo)
        {
            try
            {
                webportals.OnCancelLeaveApplication(leaveNo);
                TempData["Success"] = $"Leave requisition number {leaveNo} has been cancelled successfully";
                return RedirectToAction("leavelisting", "humanresource");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("leavelisting", "humanresource");
            }
        }

        public ActionResult LeaveTransactions()
        {
            if (Session["username"] == null) return RedirectToAction("index", "account");
            HumanResource human = new HumanResource();
            try
            {
                string username = Session["username"].ToString();
                var transactions = HRHelper.GetLeaveTransactions(username);
                human.LeaveTransactions = transactions;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(human);
        }
        public ActionResult Pnine()
        {
            if (Session["username"] == null)
                return RedirectToAction("index", "account");

            var model = new Pnine();
            try
            {
                model.payslipYears = HRHelper.GetPayslipYears();
                int year = Convert.ToInt32(model.Year);
                ViewBag.PdfUrl = TempData["PdfUrl"];
                ViewBag.Error = TempData["Error"];
                ViewBag.Success = TempData["Success"];

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View(model);
        }
        public ActionResult Payslip()
        {
            if (Session["username"] == null)
                return RedirectToAction("index", "account");

            var model = new Payslip();
            try
            {
                model.payslipYears = HRHelper.GetPayslipYears();
                int year =Convert.ToInt32( model.Year);
                //model.payslipMonths = HRHelper.GetPayslipMonths(year);
                model.payslipMonths = new List<Config>();

                ViewBag.PdfUrl = TempData["PdfUrl"];
                ViewBag.Error = TempData["Error"];
                ViewBag.Success = TempData["Success"];



            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View(model);
        }
        public ActionResult GenerateP9(string year)
        {
            string username = Session["username"]?.ToString();
            if (username == null)
                return RedirectToAction("index", "account");

            if (string.IsNullOrEmpty(year))
            {
                TempData["Error"] = "Please select a valid year ";
                return RedirectToAction("payslip");
            }
            try
            {
                string fileName = username.Replace(@"/", @"");
                string pdfFileName = $"Payslip-{fileName}.pdf";

                string path = Server.MapPath("~/Downloads/");
                if (string.IsNullOrEmpty(path))
                    throw new Exception("Resolved path is null or empty.");

                string pdfFilePath = Path.Combine(path, pdfFileName);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (System.IO.File.Exists(pdfFilePath))
                    System.IO.File.Delete(pdfFilePath);
                webportals.Generatep9Report(username,Convert.ToInt32(year), pdfFilePath);

                TempData["PdfUrl"] = Url.Content($"~/Downloads/{pdfFileName}");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred: {ex.Message}";
            }
            return RedirectToAction("Pnine");
        }

            [HttpGet]
        
        public JsonResult GetMonthsByYear(int year)
        {
            
            Payslip payslip = new Payslip();
           
                var months = HRHelper.GetPayslipMonths(year);
                payslip.payslipMonths = months;
               

            
            return Json(months, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GeneratePayslip(string year, string month)
        {
            string username = Session["username"]?.ToString();
            if (username == null)
                return RedirectToAction("index", "account");

            if (string.IsNullOrEmpty(year))
            {
                TempData["Error"] = "Please select a valid year ";
                return RedirectToAction("payslip");
            }
            if (string.IsNullOrEmpty(month))
            {
                TempData["Error"] = "Please select a valid month ";
                return RedirectToAction("payslip");
            }

            try
            {
                string fileName = username.Replace(@"/", @"");
                string pdfFileName = $"Payslip-{fileName}.pdf";

                string path = Server.MapPath("~/Downloads/");
                if (string.IsNullOrEmpty(path))
                    throw new Exception("Resolved path is null or empty.");

                string pdfFilePath = Path.Combine(path, pdfFileName);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (System.IO.File.Exists(pdfFilePath))
                    System.IO.File.Delete(pdfFilePath);
                //var model = new Payslip();
                //var month = model.Month;

                if (month == "12")
                {
                    month = "12";

                }
                else if (month == "11")
                {
                    month = "11";
                }
                else if (month == "10")
                {
                    month = "10";
                }
                else if (month == "")
                {
                    month = "01";
                }
                else
                {
                    month = "0" + month;
                }
                var myDate = month + "/01/" + year;
                var period = DateTime.ParseExact(myDate, "M/dd/yyyy", CultureInfo.InvariantCulture);


                 webportals.GeneratePaySlipReport(username, period, pdfFileName);
                
                 TempData["PdfUrl"] = Url.Content($"~/Downloads/{pdfFileName}");
                   

               

            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred: {ex.Message}";
            }

            return RedirectToAction("payslip");
        }

    }
}