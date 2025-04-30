using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Staffportal.Models
{
    public class Config
    {
        public string Code { get; set; }
       public string monthNumber { get; set; }
        public string monthName { get; set; }
   
        public string reliverNo { get; set; }
        public string reliverName { get; set; }
        public string Year { get; set; }
        public string Description { get; set; }
        public string Directorate { get; set; }
        public string Department { get; set; }
        public string DocumentNo { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public DateTime CreatedAt { get; set; }
        public string SystemId { get; set; }
        public string SubDirectorate { get; set; }
        public string UnitCode { get; set; }
    }
}