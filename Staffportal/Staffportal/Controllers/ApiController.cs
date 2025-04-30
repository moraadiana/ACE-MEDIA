using Staffportal.Models;
using Staffportal.NAVWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Staffportal.Controllers
{
    public class ApiController : Controller
    {
        Staffportall webportals = Components.ObjNav;
        public JsonResult GetItems()
        {
            var items = Helper.GetItems();
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFixedAssets()
        {
            var items = Helper.GetFixedAssets();
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGlAccounts()
        {
            var items = Helper.GetGlAccounts();
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetActivities(string project)
        {
            var items = Helper.GetActivities(project);
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetSalaryGrade(string employee)
        //{
        //   string items = Components.ObjNav.GetJobGrade(employee);
        //    return Json(items, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetMemoRegions(string grade)
        {
            var items = Helper.GetMemoRegions(grade);
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetImprestSurrenderDetails(string surrenderNo, string accountNo)
        {
            var items = FinanceHelper.GetImprestSurrenderDetails(surrenderNo, accountNo);
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMyPettyCashSurrenderDetails(string surrenderNo, string accountNo)
        {
            var items = FinanceHelper.GetMyPettyCashSurrenderDetails(surrenderNo, accountNo);
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetReceiptAmount(string receiptNo)
        {
            var items = Components.ObjNav.GetReceiptAmount(receiptNo);
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetItemQuantity(string itemNo)
        {
            decimal quantity = webportals.GetQuantityInStore(itemNo);
            var list = new List<string>() { quantity.ToString() };
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidateStartDate(string startDate)
        {
            string valid = HRHelper.ValidateLeaveStartDate(Convert.ToDateTime(startDate));
            return Json(valid, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LeaveBalance(string leaveType)
        {
            string username = Session["username"].ToString();
            string balance = HRHelper.LoadLeaveBalance(username, leaveType);
            return Json(balance, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CalculateEndDate(string startDate, string appliedDays, string leaveType)
        {
            string username = Session["username"].ToString();
            HumanResource dates = HRHelper.CalculateDates(Convert.ToDateTime(startDate), Convert.ToInt32(appliedDays), leaveType);
            return Json(dates, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetBudgetComparisonSummary(string budgetName)
        //{
        //    string username = Session["username"].ToString();
        //    var items = FinanceHelper.GetBudgetComparisonSummary(username, budgetName);
        //    return Json(items, JsonRequestBehavior.AllowGet);
        //}
    }
}