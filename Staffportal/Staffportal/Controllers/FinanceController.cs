using Staffportal.Models;
using Staffportal.NAVWS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Staffportal.Controllers
{
    public class FinanceController : Controller
    {
        Staffportall webportals = Components.ObjNav;
        string[] strLimiters = new string[] { "::" };
        string[] strLimiters2 = new string[] { "[]" };
        public ActionResult MemoListing()
        {
            if (Session["username"] == null) return RedirectToAction("index", "account");
            Finance finance = new Finance();
            try
            {
                //var list = new List<Finance>();
                //string username = Session["username"].ToString();
                //string memoLines = webportals.GetMemoRequisitions(username);
                //if (!string.IsNullOrEmpty(memoLines))
                //{
                //    int counter = 0;
                //    string[] memoLinesArr = memoLines.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                //    Array.Reverse(memoLinesArr);
                //    foreach (string memoLine in memoLinesArr)
                //    {
                //        counter++;
                //        string[] responseArr = memoLine.Split(strLimiters, StringSplitOptions.None);
                //        list.Add(new Finance()
                //        {
                //            Counter = counter,
                //            DocumentNo = responseArr[0],
                //            Date = Convert.ToDateTime(responseArr[1]),
                //            IsPrn = responseArr[2],
                //            To = responseArr[3],
                //            Status = responseArr[4],
                //            StatusCls = Components.StatusClass(responseArr[4])
                //        });
                //    }
                //}
                //finance.MemoListing = list;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(finance);
        }

        public ActionResult MemoHeader()
        {
            if (Session["username"] == null) return RedirectToAction("index", "account");
            Finance finance = new Finance();
            try
            {
                string username = Session["username"].ToString();
                string staffName = Session["staffName"].ToString();
                string grouping = "MEMO";
                var jobs = Helper.GetJobs();
                var resCenters = Helper.GetResponsibilityCenters(grouping);
                Config departmentDetails = Helper.GetDepartmentDetails(username);
                finance.StaffNo = username;
                finance.StaffName = staffName;
                finance.Directorate = departmentDetails.Directorate;
                finance.Department = departmentDetails.Department;
                finance.Jobs = jobs;
                finance.ResponsibilityCenters = resCenters;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(finance);
        }

        [HttpPost]
        public ActionResult MemoHeader(Finance finance)
        {
            try
            {
                string username = Session["username"].ToString();
                string to = finance.To;
                string through = finance.Through == null ? string.Empty : finance.Through;
                string resCenter = finance.ResponsibilityCenter;
                string subject = finance.Subject;
                DateTime startDate = finance.ActivityStartDate;
                DateTime endDate = finance.ActivityEndDate;
                bool prn = finance.IsPrn == "on" ? true : false;
                string description = finance.Description;
                string purpose = finance.Purpose == null ? string.Empty : finance.Purpose;

                //string response = webportals.CreateMemoHeader(username, to, through, resCenter, subject, startDate, endDate, prn, description, purpose);
                //if (!string.IsNullOrEmpty(response))
                //{
                //    string[] responseArr = response.Split(strLimiters, StringSplitOptions.None);
                //    string returnMsg = responseArr[0];
                //    if (returnMsg == "SUCCESS")
                //    {
                //        string memoNo = responseArr[1];
                //        TempData["Success"] = $"Memo number {memoNo} has been created successfully!";
                //        return RedirectToAction("memoattachments", "finance", new { memoNo, status = "Open" });
                //    }
                //    else
                //    {
                //        TempData["Error"] = "An error occured while creating the memo header. Please try again later!";
                //        return RedirectToAction("memoheader", "finance");
                //    }
                //}
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("memoheader", "finance");
            }
            return View();
        }

        public ActionResult MemoPrnLines(string memoNo, string status)
        {
            if (Session["username"] == null) return RedirectToAction("index", "account");
            if (string.IsNullOrEmpty(memoNo)) return RedirectToAction("memolisting", "finance");
            Finance finance = new Finance();
            try
            {
                string username = Session["username"].ToString();
                string staffName = Session["staffName"].ToString();
                var prnLines = FinanceHelper.GetMemoPrnLines(memoNo);
                var directorates = Helper.GetDirectorates();
                var departments = Helper.GetDepartments();
                var projects = Helper.GetProjects();
                var issuingStores = Helper.GetIssuingStores();
                finance.StaffNo = username;
                finance.StaffName = staffName;
                finance.DocumentNo = memoNo;
                finance.Status = status;
                finance.MemoPrnLines = prnLines;
                finance.Directorates = directorates;
                finance.Departments = departments;
                finance.Projects = projects;
                finance.IssuingStores = issuingStores;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(finance);
        }

        [HttpPost]
        public ActionResult MemoPrnLines(Finance finance)
        {
            try
            {
                string documentNo = finance.DocumentNo;
                string status = finance.Status;
                string directorate = finance.Directorate ?? string.Empty;
                string department = finance.Department ?? string.Empty;
                string project = finance.Project ?? string.Empty;
                string activity = finance.Activity ?? string.Empty;
                decimal quantity = finance.Quantity;
                decimal amount = finance.Amount;
                int type = finance.Type;
                string issuingStore = finance.IssuingStore;
                string itemNo = finance.Description;
                string purpose = finance.Purpose ?? string.Empty;

                //webportals.InsertMemoPrnLine(documentNo, type, itemNo, quantity, amount, directorate, department, project, activity, issuingStore, purpose);
                //TempData["Success"] = "Line added successfully";
                return RedirectToAction("memoprnlines", "finance", new { memoNo = documentNo, status });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("memoprnlines", "finance", new { memoNo = finance.DocumentNo, status = finance.Status });
            }
        }

        public ActionResult NavigateMemoLines(string memoNo, string status)
        {
            try
            {
                var prnLines = FinanceHelper.GetMemoPrnLines(memoNo);
                if (prnLines.Count > 0)
                {
                    return RedirectToAction("memolines", "finance", new { memoNo, status });
                }
                else
                {
                    TempData["Error"] = "Please add prn lines before you continue!";
                    return RedirectToAction("memoprnlines", "finance", new { memoNo, status });
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("memoprnlines", "finance", new { memoNo, status });
            }
        }

        public ActionResult MemoLines(string memoNo, string status)
        {
            if (Session["username"] == null) return RedirectToAction("index", "account");
            if (string.IsNullOrEmpty(memoNo)) return RedirectToAction("memolisting", "finance");
            Finance finance = new Finance();
            try
            {
                string username = Session["username"].ToString();
                finance.DocumentNo = memoNo;
                finance.Status = status;
                var directorates = Helper.GetDirectorates();
                var departments = Helper.GetDepartments();
                var projects = Helper.GetProjects();
                //var employees = Helper.GetEmployees(username);
                var imprestTypes = Helper.GetImprestTypes();
                var dsaLines = FinanceHelper.GetMemoDsaLines(memoNo);
                var otherCostLines = FinanceHelper.GetMemoOtherCostLines(memoNo);
                finance.Directorates = directorates;
                finance.Departments = departments;
                finance.Projects = projects;
               // finance.Employees = employees;
                finance.ImprestTypes = imprestTypes;
                finance.DsaLines = dsaLines;
                finance.OtherCostsLines = otherCostLines;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(finance);
        }

        [HttpPost]
        public ActionResult DsaLines(Finance finance)
        {
            string memoNo = finance.DocumentNo;
            string status = finance.Status;
            try
            {
                string staffNo = finance.EmployeeNo;
                string expenseCode = "DSA";
                string region = finance.Region;
                string imprestType = finance.ImprestType;
                string direcorate = finance.Directorate == null ? string.Empty : finance.Directorate;
                string department = finance.Department == null ? string.Empty : finance.Department;
                string project = finance.Project == null ? string.Empty : finance.Project;
                string activity = finance.Activity == null ? string.Empty : finance.Activity;
                decimal days = finance.Days;
                //webportals.InsertDsaMemoLines(memoNo, staffNo, expenseCode, region, imprestType, direcorate, department, days, project, activity);
                //TempData["Success"] = "DSA lines has been added successfully";
                return RedirectToAction("memolines", "finance", new { memoNo, status });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("memolines", "finance", new { memoNo, status });
            }
        }

        [HttpPost]
        public ActionResult OtherCostLines(Finance finance)
        {
            string memoNo = finance.DocumentNo;
            string status = finance.Status;
            try
            {
                string staffNo = finance.EmployeeNo;
                string expenseCode = "OTHER COSTS";
                string imprestType = finance.ImprestType;
                string direcorate = finance.Directorate == null ? string.Empty : finance.Directorate;
                string department = finance.Department == null ? string.Empty : finance.Department;
                string project = finance.Project == null ? string.Empty : finance.Project;
                string activity = finance.Activity == null ? string.Empty : finance.Activity;
                decimal amount = finance.Amount;
                string description = finance.Description;
                //webportals.InsertOtherCostsMemoLines(memoNo, staffNo, expenseCode, imprestType, direcorate, department, amount, project, activity, description);
                //TempData["Success"] = "Other costs lines has been added successfully";
                return RedirectToAction("memolines", "finance", new { memoNo, status });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("memolines", "finance", new { memoNo, status });
            }
        }

        public ActionResult RemoveDsaLine(string memoNo, string status, string id)
        {
            try
            {
                if (status == "Pending")
                {
                    //webportals.DeleteDsaMemoLine(id);
                    TempData["Success"] = "DSA line has been deleted successfully!";
                }
                else
                {
                    TempData["Error"] = "You can only edit open documents";
                }
                return RedirectToAction("memolines", "finance", new { memoNo, status });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("memolines", "finance", new { memoNo, status });
            }
        }

        public ActionResult RemoveOtherCostLine(string memoNo, string status, string id)
        {
            try
            {
                if (status == "Pending")
                {
                   // webportals.DeleteDsaMemoLine(id);
                    TempData["Success"] = "Other cost line has been deleted successfully!";
                }
                else
                {
                    TempData["Error"] = "You can only edit open documents";
                }
                return RedirectToAction("memolines", "finance", new { memoNo, status });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("memolines", "finance", new { memoNo, status });
            }
        }

        public ActionResult RemoveMemoLine(string id, string memoNo, string status, string query)
        {
            try
            {
                if (status == "Pending")
                {
                    //webportals.RemoveMemoPrnLine(id);
                    TempData["Success"] = "Line deleted successfully";
                }
                else
                {
                    TempData["Error"] = "You can only edit open documents";
                }
                if (query == "prn") return RedirectToAction("memoprnlines", "finance", new { memoNo, status });
                else return RedirectToAction("memolines", "finance", new { memoNo, status });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                if (query == "prn") return RedirectToAction("memoprnlines", "finance", new { memoNo, status });
                else return RedirectToAction("memolines", "finance", new { memoNo, status });
            }
        }

        public ActionResult SendMemoApproval(string memoNo, string status, string query)
        {
            try
            {
                //webportals.OnSendMemoForApproval(memoNo);
                TempData["Success"] = $"Memo number {memoNo} has been sent for approval successfully!";
                return RedirectToAction("memolisting", "finance");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                if (query == "prn") return RedirectToAction("memoprnlines", "finance", new { memoNo, status });
                else return RedirectToAction("memolines", "finance", new { memoNo, status });
            }
        }

        public ActionResult CancelMemoApproval(string memoNo, string status, string query)
        {
            try
            {
              //  webportals.OnCancelMemoApproval(memoNo);
                TempData["Success"] = $"Memo number {memoNo} has been cancelled successfully!";
                return RedirectToAction("memolisting", "finance");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                if (query == "prn") return RedirectToAction("memoprnlines", "finance", new { memoNo, status });
                else return RedirectToAction("memolines", "finance", new { memoNo, status });
            }
        }

        public ActionResult MemoAttachments(string memoNo, string status)
        {
            if (Session["username"] == null) return RedirectToAction("index", "account");
            Finance finance = new Finance();
            try
            {
                var attachments = Helper.GetMyAttachments(memoNo);
                finance.DocumentNo = memoNo;
                finance.Status = status;
                //finance.PRN = webportals.IsMemoPrn(memoNo);
                finance.FinanceAttachments = attachments;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(finance);
        }

        [HttpPost]
        public ActionResult MemoAttachments(Finance finance)
        {
            string memoNo = finance.DocumentNo;
            string status = finance.Status;
            try
            {
                string fileName = finance.AttachmentFile.FileName;
                string fileExtension = Path.GetExtension(fileName).Split('.')[1].ToLower();
                if (fileExtension == "pdf" || fileExtension == "png" || fileExtension == "jpg" || fileExtension == "svg" || fileExtension == "jpeg")
                {
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string pathToUpload = Path.Combine(path, memoNo.Replace("/", "") + fileName);
                    if (System.IO.File.Exists(pathToUpload))
                    {
                        System.IO.File.Delete(pathToUpload);
                    }
                    finance.AttachmentFile.SaveAs(pathToUpload);
                    Stream fs = finance.AttachmentFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    byte[] bytes = br.ReadBytes((int)fs.Length);
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                 //   webportals.UploadDocument(memoNo, fileName.ToUpper(), base64String, 52178771);
                    TempData["Success"] = "Document has been uploaded successfully";
                    return RedirectToAction("memoattachments", "finance", new { memoNo, status });
                }
                else
                {
                    TempData["Error"] = "Please upload files with .pdf, .png, .jpg, .jpeg and .svg extensions only.";
                    return RedirectToAction("memoattachments", "finance", new { memoNo, status });
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("memoattachments", "finance", new { memoNo, status });
            }
        }

        public ActionResult RemoveMemoAttachment(string memoNo, string status, string id)
        {
            try
            {
                if (status == "Pending")
                {
                    //webportals.RemoveAttachment(id);
                    TempData["Success"] = "Document has been deleted successfully";
                }
                else
                {
                    TempData["Error"] = "You can only edit open documents";
                }
                return RedirectToAction("memoattachments", "finance", new { memoNo, status });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("memoattachments", "finance", new { memoNo, status });
            }
        }

        public ActionResult ImprestListing()
        {
            if (Session["username"] == null) return RedirectToAction("index", "account");
            Finance finance = new Finance();
            try
            {
                string username = Session["username"].ToString();
                var imprestListing = FinanceHelper.GetImprests(username);
                finance.ImprestListing = imprestListing;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(finance);
        }
        public ActionResult ImprestHeader()
        {

            if (Session["username"] == null) return RedirectToAction("index", "account");
            Finance finance = new Finance();
            try
            {
                string username = Session["username"].ToString();
                string staffName = Session["staffName"].ToString();
                string grouping = "IMPREST";
                var clientCodes= Helper.GetClientCodes();
                var jobOrderCodes = Helper.GetJobOrderCodes();
                var resCenters = Helper.GetResponsibilityCenters(grouping);
                Config departmentDetails = Helper.GetDepartmentDetails(username);
                var customers = Helper.GetCustomers();
                finance.Customers = customers;
                finance.StaffNo = username;
                finance.StaffName = staffName;
                finance.Directorate = departmentDetails.Directorate;
                finance.Department = departmentDetails.Department;
                finance.ResponsibilityCenters = resCenters;
                finance.ClientCodes = clientCodes;
                finance.JobOrderCodes = jobOrderCodes;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(finance);
        }

        [HttpPost]
        public ActionResult ImprestHeader(Finance finance)
        {
            try
            {
                string username = Session["username"].ToString();
                string resCenter = finance.ResponsibilityCenter;
                string purpose = finance.Purpose;
                string Department = finance.Department;
                string customer = finance.Customer;
                string clientCode = finance.ClientCode;
                string jobOrderCode = finance.JobOrderCode;
                //remove dept when users are assiggned
                string dept = "FINANCE";
                string imprestNo = "";
                string reponse = webportals.CreateImprestRequisitionHeader(username, resCenter, purpose, clientCode,jobOrderCode);
                if (!string.IsNullOrEmpty(reponse))
                {
                    string[] responseArr = reponse.Split(strLimiters, StringSplitOptions.None);
                    string returnMsg = responseArr[0];
                    if (returnMsg == "SUCCESS")
                    {
                       imprestNo= responseArr[1];
                        //lblDirectorate.Text = responseArr[2];

                    }
                }

                TempData["Success"] = $"Imprest requisition number {imprestNo} has been created successfully";
                return RedirectToAction("imprestlines", "finance", new { imprestNo, status = "Pending" });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("imprestheader", "finance");
            }
        }
        public ActionResult ImprestLines(string imprestNo, string status)
        {
            if (string.IsNullOrEmpty(imprestNo)) return RedirectToAction("imprestlisting", "finance");
            Finance finance = new Finance();
            try
            {
                string grouping = "IMPREST";
                var resCenters = Helper.GetResponsibilityCenters(grouping);
                var imprestLines = FinanceHelper.GetImprestsLines(imprestNo);
                var advanceTypes = Helper.GetImprestTypes();
                
                //string resCenter = webportals.GetImprestResponsibilityCenter(imprestNo);
                var attachments = Helper.GetMyAttachments(imprestNo);
                finance.DocumentNo = imprestNo;
                finance.Status = status;
                
                finance.ResponsibilityCenters = resCenters;
                
                finance.ImprestLines = imprestLines;
                finance.FinanceAttachments = attachments;
                finance.ImprestTypes = advanceTypes;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(finance);
        }
        [HttpPost]
        public ActionResult AddImprestLines(Finance finance)
        {
            
            try
            {
                string imprestNo = finance.DocumentNo;
                string status = finance.Status;
                decimal amount = finance.Amount;
                string imprestType = finance.ImprestType;
                //string resCenter = finance.ResponsibilityCenter;
                string response = webportals.InsertImprestRequisitonLines(imprestNo, imprestType, amount);
                if (response == "SUCCESS")
                {
                    TempData["Success"] = "Line added successfully!";
                    return RedirectToAction("imprestlines", "finance", new { imprestNo, status = "Pending" });
                }
                else
                {
                    TempData["Error"] = response;
                }
                     return RedirectToAction("imprestlines", "finance", new { imprestNo, status = "Pending" });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("imprestlines", "finance");
            }

           
        }
        [HttpPost]
        public ActionResult ImprestLines(Finance finance)
        {
            string imprestNo = finance.DocumentNo;
            string status = finance.Status;
            string resCenter = finance.ResponsibilityCenter;
            
            try
            {
                
                decimal totalAmount = webportals.GetTotalImprestAmount(imprestNo);

                webportals.UpdateImprestHeader(imprestNo, resCenter, totalAmount);
                TempData["Success"] = $"Imprest number {imprestNo} has been sent for approval successfully!";
                return RedirectToAction("imprestlisting", "finance");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("imprestlines", "finance", new { imprestNo, status });
            }
        }

        public ActionResult CancelImprestApproval(string imprestNo, string status)
        {
            try
            {
                webportals.OnCancelImprestRequisition(imprestNo);
                TempData["Success"] = $"Approval request for imprest number {imprestNo} has been cancelled successfully!";
                return RedirectToAction("imprestlisting", "finance");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("imprestlines", "finance", new { imprestNo, status });
            }
        }

        [HttpPost]
        public ActionResult UploadImprestDocuments(Finance finance)
        {
            string imprestNo = finance.DocumentNo;
            string status = finance.Status;
            try
            {
                string fileName = finance.AttachmentFile.FileName;
                string fileExtension = Path.GetExtension(fileName).Split('.')[1].ToLower();
                if (fileExtension == "pdf" || fileExtension == "png" || fileExtension == "jpg" || fileExtension == "svg" || fileExtension == "jpeg")
                {
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string pathToUpload = Path.Combine(path, imprestNo.Replace("/", "") + fileName);
                    if (System.IO.File.Exists(pathToUpload))
                    {
                        System.IO.File.Delete(pathToUpload);
                    }
                    finance.AttachmentFile.SaveAs(pathToUpload);
                    Stream fs = finance.AttachmentFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    byte[] bytes = br.ReadBytes((int)fs.Length);
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    webportals.UploadDocument(imprestNo, fileName.ToUpper(), base64String, 52178708);
                    TempData["Success"] = "Document has been uploaded successfully";
                    return RedirectToAction("imprestlines", "finance", new { imprestNo, status });
                }
                else
                {
                    TempData["Error"] = "Please upload files with .pdf, .png, .jpg, .jpeg and .svg extensions only.";
                    return RedirectToAction("imprestlines", "finance", new { imprestNo, status });
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("imprestlines", "finance", new { imprestNo, status });
            }
        }

        public ActionResult RemoveImprestAttachment(string imprestNo, string status, string id)
        {
            try
            {
                if (status == "Pending")
                {
                    webportals.RemoveAttachment(id);
                    TempData["Success"] = "Document has been deleted successfully";
                }
                else
                {
                    TempData["Error"] = "You can only edit open documents";
                }
                return RedirectToAction("imprestlines", "finance", new { imprestNo, status });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("imprestlines", "finance", new { imprestNo, status });
            }
        }

        public ActionResult PostedImprests()
        {
            if (Session["username"] == null) return RedirectToAction("index", "account");
            Finance finance = new Finance();
            try
            {
                string username = Session["username"].ToString();
                var postedImprests = FinanceHelper.GetPostedImprests(username);
                finance.PostedImprests = postedImprests;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(finance);
        }

        public ActionResult ImprestSurrenderListing()
        {
            if (Session["username"] == null) return RedirectToAction("index", "account");
            Finance finance = new Finance();
            try
            {
                string username = Session["username"].ToString();
                var surrenderListing = FinanceHelper.GetImprestSurrenders(username);
                finance.ImprestSurrenderListing = surrenderListing;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(finance);
        }

        public ActionResult ImprestSurrenderLines(string imprestNo, string surrenderNo, string status)
        {
            if (Session["username"] == null) return RedirectToAction("index", "account");
            Finance finance = new Finance();
            try
            {
                string documentNo = string.Empty;
                if (string.IsNullOrEmpty(surrenderNo)) documentNo = webportals.GetNextImprestSurrenderNo();
                else documentNo = surrenderNo;
                string username = Session["username"].ToString();
                string grouping = "IMPSURR";
                var resCenters = Helper.GetResponsibilityCenters(grouping);
                var imprestLines = FinanceHelper.GetImprestsLines(imprestNo);
                var attachments = Helper.GetMyAttachments(documentNo);
                var receipts = Helper.GetMyImprestReceipts(username);
              string resCenter = webportals.GetImprestSurrenderResponsibilityCenter(documentNo);
                finance.ResponsibilityCenters = resCenters;
                finance.SurrenderNo = documentNo;
                finance.Status = status;
                finance.DocumentNo = imprestNo;
               // finance.ResponsibilityCenter = resCenter;
                finance.ImprestLines = imprestLines;
                finance.FinanceAttachments = attachments;
                finance.ImprestReceipts = receipts;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(finance);
        }

        [HttpPost]
        public ActionResult ImprestSurrenderLines(Finance finance)
        {
            string imprestNo = finance.DocumentNo;
            string surrenderNo = finance.SurrenderNo;
            string status = finance.Status;
            try
            {
                var attachments = Helper.GetMyAttachments(surrenderNo);
                if (attachments.Count < 1)
                {
                    TempData["Error"] = "Please upload supporting documents";
                    return RedirectToAction("imprestsurrenderlines", "finance", new { imprestNo, surrenderNo, status });
                }
                string resCenter = finance.ResponsibilityCenter;
                string selectedCategories = finance.SelectedCategories;
                webportals.CreateImprestSurrenderHeader(surrenderNo, imprestNo, resCenter);
                string[] selectedCategoriesArr = selectedCategories.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                foreach (string category in selectedCategoriesArr)
                {
                    string[] responseArr = category.Split(strLimiters, StringSplitOptions.None);
                    string advanceType = responseArr[0];
                    string accountNo = responseArr[1];
                    string amount = responseArr[2];
                    decimal actualSpent = Convert.ToDecimal(responseArr[3]);
                    decimal cashReturned = string.IsNullOrEmpty(responseArr[4]) ? 0 : Convert.ToDecimal(responseArr[4]);
                    string receipt = responseArr[5];
                    webportals.InsertImprestSurrenderLines(surrenderNo,  actualSpent, Convert.ToDecimal(amount), imprestNo, accountNo);
                }
                webportals.OnSendImprestSurrenderForApproval(surrenderNo);
                TempData["Success"] = $"Imprest surrender number {surrenderNo} has been sent for approval successfully!";
                return RedirectToAction("imprestsurrenderlisting", "finance");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("imprestsurrenderlines", "finance", new { imprestNo, surrenderNo, status });
            }
        }

        [HttpPost]
        public ActionResult UploadSurrenderDocuments(Finance finance)
        {
            string surrenderNo = finance.SurrenderNo;
            string status = finance.Status;
            string imprestNo = finance.DocumentNo;
            try
            {
                string fileName = finance.AttachmentFile.FileName;
                string fileExtension = Path.GetExtension(fileName).Split('.')[1].ToLower();
                if (fileExtension == "pdf")
                {
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string pathToUpload = Path.Combine(path, surrenderNo.Replace("/", "") + fileName);
                    if (System.IO.File.Exists(pathToUpload))
                    {
                        System.IO.File.Delete(pathToUpload);
                    }
                    finance.AttachmentFile.SaveAs(pathToUpload);
                    Stream fs = finance.AttachmentFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    byte[] bytes = br.ReadBytes((int)fs.Length);
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                   webportals.UploadDocument(surrenderNo, fileName.ToUpper(), base64String, 52178705);
                    TempData["Success"] = "Document has been uploaded successfully";
                    return RedirectToAction("imprestsurrenderlines", "finance", new { surrenderNo, imprestNo, status });
                }
                else
                {
                    TempData["Error"] = "Please upload files with .pdf extensions only.";
                    return RedirectToAction("imprestsurrenderlines", "finance", new { surrenderNo, imprestNo, status });
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("imprestsurrenderlines", "finance", new { surrenderNo, imprestNo, status });
            }
        }

        public ActionResult RemoveImprestSurrenderAttachment(string surrenderNo, string imprestNo, string status, string id)
        {
            try
            {
                if (status == "Pending")
                {
                    webportals.RemoveAttachment(id);
                    TempData["Success"] = "Document has been deleted successfully";
                }
                else
                {
                    TempData["Error"] = "You can only edit open documents";
                }
                return RedirectToAction("imprestsurrenderlines", "finance", new { surrenderNo, imprestNo, status });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("imprestsurrenderlines", "finance", new { surrenderNo, imprestNo, status });
            }
        }

        public ActionResult ClaimListing()
        {
            if (Session["username"] == null) return RedirectToAction("index", "login");
            Finance finance = new Finance();
            try
            {
                string username = Session["username"].ToString();
                var claimListing = FinanceHelper.GetMyClaims(username);
                finance.ClaimListing = claimListing;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(finance);
        }
      
        public ActionResult ClaimHeader()
        {
            if (Session["username"] == null) return RedirectToAction("index", "account");
            Finance finance = new Finance();
            try
            {
                string username = Session["username"].ToString();
                string staffName = Session["staffName"].ToString();
                var clientCodes = Helper.GetClientCodes();
                var jobOrderCodes = Helper.GetJobOrderCodes();
                string grouping = "CLAIM";
                var resCenters = Helper.GetResponsibilityCenters(grouping);
                Config departmentDetails = Helper.GetDepartmentDetails(username);

                finance.StaffNo = username;
                finance.StaffName = staffName;
                finance.Directorate = departmentDetails.Directorate;
                finance.Department = departmentDetails.Department;
                finance.ResponsibilityCenters = resCenters;
                finance.ClientCodes = clientCodes;
                finance.JobOrderCodes = jobOrderCodes;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(finance);
        }

        [HttpPost]
        public ActionResult ClaimHeader(Finance finance)
        {
            try
            {
                string username = Session["username"].ToString();
                string resCenter = finance.ResponsibilityCenter;
                string purpose = finance.Purpose;
                string clientCode = finance.ClientCode;
                string jobOrderCode = finance.JobOrderCode;
                
                string claimNo = " ";
                string response = webportals.CreateClaimRequisitionHeader(username, resCenter, purpose,jobOrderCode,clientCode);
                if (!string.IsNullOrEmpty(response))
                {
                    string[] responseArr = response.Split(strLimiters, StringSplitOptions.None);
                    string returnMsg = responseArr[0];
                    if (returnMsg == "SUCCESS")
                    {
                          claimNo = responseArr[1];
                        //lblDirectorate.Text = responseArr[2];

                    }
                }
                TempData["Success"] = $"Claim requisition number {claimNo} has been created successfully";
                return RedirectToAction("claimlines", "finance", new { claimNo, status = "Pending" });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("claimheader", "finance");
            }
        }

        public ActionResult ClaimLines(string claimNo, string status)
        {
            if (string.IsNullOrEmpty(claimNo)) return RedirectToAction("claimheader", "finance");
            Finance finance = new Finance();
            try
            {
                var attachments = Helper.GetMyAttachments(claimNo);
                var claimLines = FinanceHelper.GetMyClaimLines(claimNo);
                var claimTypes = Helper.GetClaimTypes();
                var directorates = Helper.GetDirectorates();
                var departments = Helper.GetDepartments();
                var projects = Helper.GetProjects();
                var clientCodes = Helper.GetClientCodes();
                var jobOrderCodes = Helper.GetJobOrderCodes();
                var AccountNos = Helper.GetGlAccounts();
                finance.GlAccounts = AccountNos;
                finance.FinanceAttachments = attachments;
                finance.Status = status;
                finance.DocumentNo = claimNo;
                finance.ClaimLines = claimLines;
                finance.ClaimTypes = claimTypes;
                finance.Directorates = directorates;
                finance.Departments = departments;
                finance.Projects = projects;
                finance.ClientCodes = clientCodes;
                finance.JobOrderCodes = jobOrderCodes;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(finance);
        }

        [HttpPost]
        public ActionResult ClaimLines(Finance finance)
        {
            string claimNo = finance.DocumentNo;
            string status = finance.Status;
            try
            {
                string claimType = finance.AdvanceType;
                string directorate = finance.Directorate ?? string.Empty;
                string department = finance.Department ?? string.Empty;
                string project = finance.Project ?? string.Empty;
                string activity = finance.Activity ?? string.Empty;
                decimal amount = finance.Amount;
                string purpose = finance.Purpose;
                string clientCode = finance.ClientCode;
                string jobOrderCode = finance.JobOrderCode;
                string account = finance.GlAccount;
                string receipt = finance.ReceiptNo;
                string date = finance.ExpenditureDate;
                
                //procedure InsertClaimRequisitionLines(claimNo: Text; claimType: Text; amount: Decimal; AccNo: Text ; clientCode: Text; jobCode: Text; receiptNo: Text; expenditureDate: Date; purpose: Text) Message: Text
                webportals.InsertClaimRequisitionLines(claimNo, claimType, amount,account,clientCode,jobOrderCode,receipt,Convert.ToDateTime(date),purpose);
                TempData["Success"] = "Claim line has been added successfully";
                return RedirectToAction("claimlines", "finance", new { claimNo, status });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("claimlines", "finance", new { claimNo, status });
            }
        }

        public ActionResult RemoveClaimLine(string claimNo, string status, int id)
        {
            try
            {
                if (status == "Pending")
                {
                    webportals.RemoveClaimRequisitionLines(id);
                    TempData["Success"] = "Claim line has been removed successfully";
                }
                else
                {
                    TempData["Error"] = "You can only edit open documents";
                }
                return RedirectToAction("claimlines", "finance", new { claimNo, status });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("claimlines", "finance", new { claimNo, status });
            }
        }

        [HttpPost]
        public ActionResult UploadClaimDocument(Finance finance)
        {
            string claimNo = finance.DocumentNo;
            string status = finance.Status;
            try
            {
                string fileName = finance.AttachmentFile.FileName;
                string fileExtension = Path.GetExtension(fileName).Split('.')[1].ToLower();
                if (fileExtension == "pdf" || fileExtension == "png" || fileExtension == "jpg" || fileExtension == "svg" || fileExtension == "jpeg")
                {
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string pathToUpload = Path.Combine(path, claimNo.Replace("/", "") + fileName);
                    if (System.IO.File.Exists(pathToUpload))
                    {
                        System.IO.File.Delete(pathToUpload);
                    }
                    finance.AttachmentFile.SaveAs(pathToUpload);
                    Stream fs = finance.AttachmentFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    byte[] bytes = br.ReadBytes((int)fs.Length);
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    webportals.UploadDocument(claimNo, fileName.ToUpper(), base64String, 52178720);
                    TempData["Success"] = "Document has been uploaded successfully";
                    return RedirectToAction("claimlines", "finance", new { claimNo, status });
                }
                else
                {
                    TempData["Error"] = "Please upload files with .pdf, .png, .jpg, .jpeg and .svg extensions only.";
                    return RedirectToAction("claimlines", "finance", new { claimNo, status });
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("claimlines", "finance", new { claimNo, status });
            }
        }

        public ActionResult RemoveClaimAttachment(string claimNo, string status, string id)
        {
            try
            {
                if (status == "Pending")
                {
                    webportals.RemoveAttachment(id);
                    TempData["Success"] = "Document has been removed successfully";
                }
                else
                {
                    TempData["Error"] = "You can only edit open documents";
                }
                return RedirectToAction("claimlines", "finance", new { claimNo, status });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("claimlines", "finance", new { claimNo, status });
            }
        }

        public ActionResult SendClaimForApproval(string claimNo, string status)
        {
            try
            {
                var attachments = Helper.GetMyAttachments(claimNo);
                if (attachments.Count < 1)
                {
                    TempData["Error"] = "Please upload supporting documents!";
                    return RedirectToAction("claimlines", "finance", new { claimNo, status });
                }
                webportals.OnSendClaimRequisitionForApproval(claimNo);
                TempData["Success"] = $"Claim number {claimNo} has been sent for approval successfully";
                return RedirectToAction("claimlisting", "finance");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("claimlines", "finance", new { claimNo, status });
            }
        }

        public ActionResult CancelClaimApproval(string claimNo, string status)
        {
            try
            {
                webportals.OnCancelClaimRequisition(claimNo);
                TempData["Success"] = $"Approval request for claim number {claimNo} has been cancelled successfully";
                return RedirectToAction("claimlisting", "finance");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("claimlines", "finance", new { claimNo, status });
            }
        }

        public ActionResult PettyCashListing()
        {
            if (Session["username"] == null) return RedirectToAction("index", "account");
            Finance finance = new Finance();
            try
            {
                string username = Session["username"].ToString();
             string pettyCashAccountNo = webportals.GetStaffPettyCashNo(username);
              var pettyCashListing = FinanceHelper.GetMyPettyCashListing(pettyCashAccountNo);
                finance.PettyCashAccountNo = pettyCashAccountNo;
                finance.PettyCashListing = pettyCashListing;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(finance);
        }

        public ActionResult PettyCashHeader(string pettyCashAccountNo)
        {
            if (Session["username"] == null) return RedirectToAction("index", "account");
            Finance finance = new Finance();
            try
            {
                if (string.IsNullOrEmpty(pettyCashAccountNo))
                {
                    TempData["Error"] = "You do not have a petty cash account mapped to your employee number. Please contact the system administrator";
                    return RedirectToAction("pettycashlisting", "finance");
                }
                string username = Session["username"].ToString();
                string staffName = Session["staffName"].ToString();
                string grouping = "P-CASH";
                var resCenters = Helper.GetResponsibilityCenters(grouping);
                Config departmentDetails = Helper.GetDepartmentDetails(username);

                finance.StaffNo = pettyCashAccountNo;
                finance.StaffName = staffName;
                finance.Directorate = departmentDetails.Directorate;
                finance.Department = departmentDetails.Department;
                finance.ResponsibilityCenters = resCenters;
                finance.PettyCashAccountNo = pettyCashAccountNo;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(finance);
        }

        [HttpPost]
        public ActionResult PettyCashHeader(Finance finance)
        {
            string pettyCashAccountNo = finance.PettyCashAccountNo;
            try
            {
                if (webportals.HasPendingPettyCash(pettyCashAccountNo))
                {
                    TempData["Error"] = "You have a pending petty cash. Please surrender it and then apply for a new one!";
                    return RedirectToAction("pettycashlisting", "finance");
                }
                string resCenter = finance.ResponsibilityCenter;
                string purpose = finance.Purpose;
                // delete
                string dept = "test";
               // string dir = "test";
                string pettyCashNo = webportals.CreatePettyCashRequisitionHeader(pettyCashAccountNo, dept, resCenter, purpose);
                TempData["Success"] = $"Petty cash number {pettyCashNo} has been created successfully!";
                return RedirectToAction("pettycashlines", "finance", new { pettyCashNo, status = "Pending" });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("pettycashheader", "finance", new { pettyCashAccountNo });
            }
        }

        public ActionResult PettyCashLines(string pettyCashNo, string status)
        {
            if (Session["username"] == null) return RedirectToAction("index", "account");
            Finance finance = new Finance();
            try
            {
                var attachments = Helper.GetMyAttachments(pettyCashNo);
                var pettyCashLines = FinanceHelper.GetMyPettyCashLines(pettyCashNo);
                var pettyCashTypes = Helper.GetPettyCashTypes();
                var glAccounts = Helper.GetGlAccounts();
                var directorates = Helper.GetDirectorates();
                var departments = Helper.GetDepartments();
                var projects = Helper.GetProjects();
                finance.FinanceAttachments = attachments;
                finance.Status = status;
                finance.DocumentNo = pettyCashNo;
                finance.PettyCashLines = pettyCashLines;
                finance.PettyCashTypes = pettyCashTypes;
                finance.Directorates = directorates;
                finance.Departments = departments;
                finance.Projects = projects;
                finance.GlAccounts = glAccounts;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(finance);
        }

        [HttpPost]
        public ActionResult PettyCashLines(Finance finance)
        {
            string pettyCashNo = finance.DocumentNo;
            string status = finance.Status;
            try
            {
                string advanceType = finance.AdvanceType;
                string directorate = finance.Directorate ?? string.Empty;
                string department = finance.Department ?? string.Empty;
                string project = finance.Project ?? string.Empty;
                string activity = finance.Activity ?? string.Empty;
                decimal amount = finance.Amount;
                string accountNo = finance.GlAccount;
               //webportals.InsertPettyCashRequisitionLine(pettyCashNo, advanceType, accountNo, directorate, department, project, activity, amount);
                webportals.InsertPettyCashRequisitionLine(pettyCashNo, advanceType, amount);
                TempData["Success"] = "Petty cash line has been added successfully!";
                return RedirectToAction("pettycashlines", "finance", new { pettyCashNo, status });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("pettycashlines", "finance", new { pettyCashNo, status });
            }
        }

        public ActionResult RemovePettyCashLine(string pettyCashNo, string status, int id)
        {
            try
            {
                if (status == "Pending")
                {
                    webportals.RemovePettyCashRequisitionLine(id);
                    TempData["Success"] = "Petty cash line has been removed successfully";
                }
                else
                {
                    TempData["Error"] = "You can only edit open documents";
                }
                return RedirectToAction("pettycashlines", "finance", new { pettyCashNo, status });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("pettycashlines", "finance", new { pettyCashNo, status });
            }
        }

        [HttpPost]
        public ActionResult UploadPettyCashDocument(Finance finance)
        {
            string pettyCashNo = finance.DocumentNo;
            string status = finance.Status;
            try
            {
                string fileName = finance.AttachmentFile.FileName;
                string fileExtension = Path.GetExtension(fileName).Split('.')[1].ToLower();
                if (fileExtension == "pdf" || fileExtension == "png" || fileExtension == "jpg" || fileExtension == "svg" || fileExtension == "jpeg")
                {
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string pathToUpload = Path.Combine(path, pettyCashNo.Replace("/", "") + fileName);
                    if (System.IO.File.Exists(pathToUpload))
                    {
                        System.IO.File.Delete(pathToUpload);
                    }
                    finance.AttachmentFile.SaveAs(pathToUpload);
                    Stream fs = finance.AttachmentFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    byte[] bytes = br.ReadBytes((int)fs.Length);
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                   webportals.UploadDocument(pettyCashNo, fileName.ToUpper(), base64String, 52178624);
                    TempData["Success"] = "Document has been uploaded successfully";
                    return RedirectToAction("pettycashlines", "finance", new { pettyCashNo, status });
                }
                else
                {
                    TempData["Error"] = "Please upload files with .pdf, .png, .jpg, .jpeg and .svg extensions only.";
                    return RedirectToAction("pettycashlines", "finance", new { pettyCashNo, status });
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("pettycashlines", "finance", new { pettyCashNo, status });
            }
        }

        public ActionResult RemovePettyCashAttachment(string pettyCashNo, string status, string id)
        {
            try
            {
                if (status == "Pending")
                {
                  webportals.RemoveAttachment(id);
                    TempData["Success"] = "Document has been removed successfully";
                }
                else
                {
                    TempData["Error"] = "You can only edit open documents";
                }
                return RedirectToAction("pettycashlines", "finance", new { pettyCashNo, status });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("pettycashlines", "finance", new { pettyCashNo, status });
            }
        }

        public ActionResult SendPettyCashForApproval(string pettyCashNo, string status)
        {
            try
            {
                var attachments = Helper.GetMyAttachments(pettyCashNo);
                if (attachments.Count < 1)
                {
                    TempData["Error"] = "Please upload supporting documents!";
                    return RedirectToAction("pettycashlines", "finance", new { pettyCashNo, status });
                }
                webportals.OnSendPettyCashRequisitionForApproval(pettyCashNo);
                TempData["Success"] = $"Petty cash number {pettyCashNo} has been sent for approval successfully";
                return RedirectToAction("pettycashlisting", "finance");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("pettycashlines", "finance", new { pettyCashNo, status });
            }
        }

        public ActionResult CancelPettyCashApproval(string pettyCashNo, string status)
        {
            try
            {
                webportals.OnCancelPettyCashRequisition(pettyCashNo);
                TempData["Success"] = $"Approval request for petty cash number {pettyCashNo} has been cancelled successfully";
                return RedirectToAction("pettycashlisting", "finance");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("pettycashlines", "finance", new { pettyCashNo, status });
            }
        }

        public ActionResult PettyCashSurrenderListing()
        {
            if (Session["username"] == null) return RedirectToAction("index", "account");
            Finance finance = new Finance();
            try
            {
                string username = Session["username"].ToString();
                string pettyCashAccountNo = webportals.GetStaffPettyCashNo(username);
                var pettyCashSurrenderListing = FinanceHelper.GetMyPettyCashSurrenderListing(pettyCashAccountNo);
                finance.PettyCashAccountNo = pettyCashAccountNo;
                finance.PettyCashSurrenderListing = pettyCashSurrenderListing;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(finance);
        }

        public ActionResult PettyCashSurrenderLines(string surrenderNo, string status, string documentNo)
        {
            if (string.IsNullOrEmpty(surrenderNo)) return RedirectToAction("pettycashsurrenderlisting", "finance");
            Finance finance = new Finance();
            try
            {
                string username = Session["username"].ToString();
                string pettyCashAccountNo = webportals.GetStaffPettyCashNo(username);
                var receipts = Helper.GetPettyCashReceipts(pettyCashAccountNo);
                var pettyCashSurrenderLines = FinanceHelper.GetMyPettyCashSurrenderLines(surrenderNo);
                var attachments = Helper.GetMyAttachments(surrenderNo);
                finance.SurrenderNo = surrenderNo;
                finance.DocumentNo = documentNo;
                finance.Status = status;
                finance.PettyCashAccountNo = pettyCashAccountNo;
                finance.ImprestReceipts = receipts;
                finance.PettyCashSurrenderLines = pettyCashSurrenderLines;
                finance.FinanceAttachments = attachments;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(finance);
        }

        [HttpPost]
        public ActionResult PettyCashSurrenderLines(Finance finance)
        {
            string surrenderNo = finance.SurrenderNo;
            string status = finance.Status;
            string documentNo = finance.DocumentNo;
            try
            {
                var attachments = Helper.GetMyAttachments(surrenderNo);
                if (attachments.Count < 1)
                {
                    TempData["Error"] = "Please upload supporting documents";
                    return RedirectToAction("pettycashsurrenderlines", "finance", new { surrenderNo, status, documentNo });
                }
                string categories = finance.SelectedCategories;
                string resCenter = finance.ResponsibilityCenter;
                string[] categoriesArr = categories.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                foreach (string category in categoriesArr)
                {
                    string[] responseArr = category.Split(strLimiters, StringSplitOptions.None);
                    string accountNo = responseArr[0];
                    string accountName = responseArr[1];
                    string amount = responseArr[2];
                    string receiptNo = responseArr[3];
                    decimal actualSpent = Convert.ToDecimal(responseArr[4]);
                    decimal cashReturned = string.IsNullOrEmpty(responseArr[5]) ? 0 : Convert.ToDecimal(responseArr[5]);
                    webportals.UpdatePettyCashHeader(documentNo);
                  
                    webportals.UpdatePettyCashSurrenderHeader(surrenderNo, resCenter);
                    webportals.UpdatePettyCashSurrenderLines(surrenderNo, accountNo, actualSpent, cashReturned, receiptNo);
                }
                webportals.OnSendPettyCashSurrenderForApproval(surrenderNo);
                TempData["Success"] = $"Petty cash surrender number {surrenderNo} has been sent for approval successfully";
                return RedirectToAction("pettycashsurrenderlisting", "finance");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("pettycashsurrenderlines", "finance", new { surrenderNo, status, documentNo });
            }
        }

        [HttpPost]
        public ActionResult UploadPettyCashSurrenderDocument(Finance finance)
        {
            string surrenderNo = finance.SurrenderNo;
            string status = finance.Status;
            string documentNo = finance.DocumentNo;
            try
            {
                string fileName = finance.AttachmentFile.FileName;
                string fileExtension = Path.GetExtension(fileName).Split('.')[1].ToLower();
                if (fileExtension == "pdf" || fileExtension == "png" || fileExtension == "jpg" || fileExtension == "svg" || fileExtension == "jpeg")
                {
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string pathToUpload = Path.Combine(path, surrenderNo.Replace("/", "") + fileName);
                    if (System.IO.File.Exists(pathToUpload))
                    {
                        System.IO.File.Delete(pathToUpload);
                    }
                    finance.AttachmentFile.SaveAs(pathToUpload);
                    Stream fs = finance.AttachmentFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    byte[] bytes = br.ReadBytes((int)fs.Length);
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    //webportals.UploadDocument(surrenderNo, fileName.ToUpper(), base64String, 52178626);
                    TempData["Success"] = "Document has been uploaded successfully";
                    return RedirectToAction("pettycashsurrenderlines", "finance", new { surrenderNo, status, documentNo });
                }
                else
                {
                    TempData["Error"] = "Please upload files with .pdf, .png, .jpg, .jpeg and .svg extensions only.";
                    return RedirectToAction("pettycashsurrenderlines", "finance", new { surrenderNo, status, documentNo });
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("pettycashsurrenderlines", "finance", new { surrenderNo, status, documentNo });
            }
        }

        public ActionResult RemovePettyCashSurrenderAttachment(string surrenderNo, string status, string id, string documentNo)
        {
            try
            {
                if (status == "Pending")
                {
                   webportals.RemoveAttachment(id);
                    TempData["Success"] = "Document has been removed successfully";
                }
                else
                {
                    TempData["Error"] = "You can only edit open documents";
                }
                return RedirectToAction("pettycashsurrenderlines", "finance", new { surrenderNo, status, documentNo });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("pettycashsurrenderlines", "finance", new { surrenderNo, status, documentNo });
            }
        }

        public ActionResult CancelPettyCashSurrenderApproval(string surrenderNo, string status, string documentNo)
        {
            try
            {
               webportals.OnCancelPettyCashSurrender(surrenderNo);
                TempData["Success"] = $"Approval request for petty cash surrender number {surrenderNo} has been cancelled successfully";
                return RedirectToAction("pettycashsurrenderlisting", "finance");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("pettycashsurrenderlines", "finance", new { surrenderNo, status, documentNo });
            }
        }
    }
}