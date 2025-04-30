using Staffportal.Models;
using Staffportal.NAVWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Staffportal.Controllers
{
    public class ProcurementController : Controller
    {
        Staffportall webportals = Components.ObjNav;
        string[] strLimiters = new string[] { "::" };
        string[] strLimiters2 = new string[] { "[]" };
        public ActionResult StoreListing()
        {

            if (Session["username"] == null) return RedirectToAction("index", "account");
            Procurement procurement = new Procurement();
            try
            {
                var list = new List<Procurement>();
                string username = Session["username"].ToString();
                string storeRequests = webportals.GetMyStoresRequests(username);
                if (!string.IsNullOrEmpty(storeRequests) && storeRequests != "No requisitions found")
                {

                    int counter = 0;
                    string[] storeRequestsArr = storeRequests.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                    Array.Reverse(storeRequestsArr);
                    foreach (string storeRequest in storeRequestsArr)
                    {
                        counter++;
                        string[] responseArr = storeRequest.Split(strLimiters, StringSplitOptions.None);
                        Procurement requests = new Procurement()
                        {
                            Counter = counter,
                            DocumentNo = responseArr[0],
                            Date = responseArr[1] == null ? DateTime.Now : Convert.ToDateTime(responseArr[1]),
                            RequisitionType = responseArr[2],
                            Description = responseArr[3],
                            Status = responseArr[4],
                            StatusCls = Components.StatusClass(responseArr[4])
                        };
                        list.Add(requests);
                    }
                }
               
                // procurement.StoreListings = list;
                // Ensure the model is always initialized
                procurement.StoreListings = list ?? new List<Procurement>();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(procurement);
        }

        public ActionResult StoresHeader()
        {
            if (Session["username"] == null) return RedirectToAction("index", "account");
            Procurement procurement = new Procurement();
            try
            {
                string username = Session["username"].ToString();
                string staffName = Session["staffName"].ToString();
                string grouping = "SRN";
                var resCenters = Helper.GetResponsibilityCenters(grouping);
                var issuingStores = Helper.GetIssuingStores();
                Config departmentDetails = Helper.GetDepartmentDetails(username);
                procurement.StaffNo = username;
                procurement.StaffName = staffName;
                procurement.ResponsibilityCenters = resCenters;
                procurement.IssuingStores = issuingStores;
                procurement.Directorate = departmentDetails.Directorate;
                procurement.Department = departmentDetails.Department;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(procurement);
        }

        [HttpPost]
        public ActionResult StoresHeader(Procurement procurement)
        {
            if (Session["username"] == null) return RedirectToAction("index", "account");
            try
            {
                string username = Session["username"].ToString();
                DateTime requiredDate = procurement.RequiredDate;
                string issuingStore = procurement.IssuingStore;
                string resCenter = procurement.ResponsibilityCenter;
                string description = procurement.Description;

                // delete the variables below 
                int reqType = 1;
                string department = "test";
                string storeNo = webportals.CreateStoreRequisitionHeader(username, reqType, requiredDate, department, issuingStore, resCenter, description);
                TempData["Success"] = $"Store requisition number {storeNo} has been created successfully!"; ;
                return RedirectToAction("storelines", "procurement", new { storeNo, status = "Open" });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("storesheader", "procurement");
            }
        }

        public ActionResult StoreLines(string storeNo, string status)
        {
            if (Session["username"] == null) return RedirectToAction("index", "account");
            if (string.IsNullOrEmpty(storeNo)) return RedirectToAction("storelisting", "procurement");
            Procurement procurement = new Procurement();
            try
            {
                string username = Session["username"].ToString();
                string staffName = Session["staffName"].ToString();
                Config designation = Helper.GetDepartmentDetails(username);
                var storeLines = ProcurementHelper.GetStoreLines(storeNo);
                var issuingStores = Helper.GetIssuingStores();
                procurement.StaffNo = username;
                procurement.StaffName = staffName;
                procurement.Directorate = designation.Directorate;
                procurement.Department = designation.Department;
                procurement.DocumentNo = storeNo;
                procurement.Status = status;
                procurement.IssuingStores = issuingStores;
                procurement.StoreLines = storeLines;
                Session["storeNo"] = procurement.DocumentNo;
                Session["status"] = procurement.Status;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(procurement);
        }

        [HttpPost]
        public ActionResult StoreLines(Procurement procurement)
        {
            if (Session["username"] == null) return RedirectToAction("index", "account");
            try
            {
                string requisitionNo = procurement.DocumentNo;
                int type = procurement.Type;
                string itemNo = procurement.ItemNo;
                decimal quantity = procurement.Quantity;
                string issuingStore = procurement.IssuingStore;
                webportals.InsertStoreRequisitionLines(requisitionNo, type, itemNo, quantity, issuingStore);
                TempData["Success"] = "Line has been added successfully!";
                return RedirectToAction("storelines", "procurement", new { storeNo = procurement.DocumentNo, status = procurement.Status });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("storelines", "procurement", new { storeNo = procurement.DocumentNo, status = procurement.Status });
            }
        }

        public ActionResult RemoveStoreLine(int id)
        {
            string storeNo = Session["storeNo"].ToString();
            string status = Session["status"].ToString();
            try
            {
                if (status == "Open" || status == "Pending")
                {
                    string response = webportals.DeleteStoreLine(id);
                    if (response == "SUCCESS")
                    {
                        TempData["Success"] = "Line deleted successfully";
                        return RedirectToAction("storelines", "procurement", new { storeNo, status });
                    }
                    //if (webportals.DeleteStoreLine(id))
                    //{
                    //    TempData["Success"] = "Line deleted successfully";
                    //    return RedirectToAction("storelines", "procurement", new { storeNo, status });
                    //}
                    else
                    {
                        TempData["Error"] = "An error occured while deleting line. Please try again later!";
                        return RedirectToAction("storelines", "procurement", new { storeNo, status });
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View();
        }

        public ActionResult SendStoreApproval(string storeNo)
        {
            string status = Session["status"].ToString();
            try
            {
                var storeLines = ProcurementHelper.GetStoreLines(storeNo);
                if (storeLines.Count < 1)
                {
                    TempData["Error"] = "Please lines before sending for approval";
                    return RedirectToAction("storelines", "procurement", new { storeNo, status });
                }
                webportals.OnSendStoreRequisitionForApproval(storeNo);
                TempData["Success"] = $"Store requisition number {storeNo} has been sent for approval successfully";
                return RedirectToAction("storelisting", "procurement");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("storelines", "procurement", new { storeNo, status });
            }
        }

        public ActionResult CancelStoreApproval(string storeNo)
        {
            string status = Session["status"].ToString();
            try
            {
                webportals.OnCancelStoreRequisitionApproval(storeNo);
                TempData["Success"] = $"Store requisition number {storeNo} has been cancelled successfully";
                return RedirectToAction("storelisting", "procurement");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("storelines", "procurement", new { storeNo, status });
            }
        }
    }
}