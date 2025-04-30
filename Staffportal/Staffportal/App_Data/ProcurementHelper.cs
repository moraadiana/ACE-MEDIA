using Staffportal.Models;
using Staffportal.NAVWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Staffportal
{
    public class ProcurementHelper
    {
        private static Staffportall webportals = Components.ObjNav;
        private static string[] strLimiters = new string[] { "::" };
        private static string[] strLimiters2 = new string[] { "[]" };
        public static List<Procurement> GetStoreLines(string requisitionNo)
        {
            var list = new List<Procurement>();
            try
            {
                //string storeLines = webportals.GetMyStoreLines(requisitionNo);
                //if (!string.IsNullOrEmpty(storeLines))
                //{
                //    string[] storeLinesArr = storeLines.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                //    foreach (var item in storeLinesArr)
                //    {
                //        string[] responseArr = item.Split(strLimiters, StringSplitOptions.None);
                //        list.Add(new Procurement()
                //        {
                //            ItemType = responseArr[0],
                //            ItemNo = responseArr[1],
                //            Description = responseArr[2],
                //            Quantity = responseArr[3] == null ? 0 : Convert.ToDecimal(responseArr[3]),
                //            SystemId = responseArr[4]
                //        });
                //    }
                //}
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return list;
        }
    }
}