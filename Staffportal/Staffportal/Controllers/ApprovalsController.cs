using Staffportal.Models;
using Staffportal.NAVWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Staffportal.Controllers
{
    public class ApprovalsController : Controller
    {
        Staffportall webportals = Components.ObjNav;
        string[] strLimiters = new string[] { "::" };
        string[] strLimiters2 = new string[] { "[]" };
        public ActionResult ApprovalTracking(string documentNo)
        {
            if (Session["username"] == null) return RedirectToAction("index", "account");
            Approval approval = new Approval();
            try
            {
                var list = new List<Approval>();
                string entries = webportals.GetApprovalEntries(documentNo);
                if (!string.IsNullOrEmpty(entries))
                {
                    int counter = 0;
                    string[] entriesArr = entries.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string entry in entriesArr)
                    {
                        counter++;
                        string[] responseArr = entry.Split(strLimiters, StringSplitOptions.None);
                        Approval approvalEntries = new Approval()
                        {
                            Counter = counter,
                            DocumentNo = documentNo,
                            EntryNo = Convert.ToInt32(responseArr[1]),
                            SequenceNo = Convert.ToInt32(responseArr[2]),
                            DateSentForApproval = Convert.ToDateTime(responseArr[3]),
                            SenderId = responseArr[4],
                            ApproverId = responseArr[5],
                            Comments = responseArr[6],
                            Status = responseArr[7],
                            StatusCls = Components.StatusClass(responseArr[7]),
                        };
                        list.Add(approvalEntries);
                    }
                }
                approval.Approvals = list;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(approval);
        }
    }
}