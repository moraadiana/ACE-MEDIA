using Staffportal.NAVWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Staffportal.Controllers
{
    public class DownloadsController : Controller
    {
        Staffportall webportals = Components.ObjNav;
        public ActionResult MemoReport(string documentNo)
        {
            try
            {
                string filename = documentNo.Replace("/", "");
                string pdfFile = $"MemoReport-{filename}.pdf";
                webportals.GenerateMemoReport(documentNo, pdfFile);
                string filepath = Url.Content($"~/Downloads/{pdfFile}");
                ViewBag.filepath = filepath;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View();
        }
    }
}