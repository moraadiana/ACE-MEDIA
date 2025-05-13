using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Staffportal.Models
{
    public class Payslip
    {
        public string Year { get; set; }
        public string Month { get; set; }
        public string PdfUrl { get; set; }
        public List<string> payslipYears { get; set; }
        public List<Config> payslipMonths { get; set; }
    }
}