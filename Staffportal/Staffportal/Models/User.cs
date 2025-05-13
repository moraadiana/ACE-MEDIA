using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Staffportal.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string StaffName { get; set; }
        public string JobTitle { get; set; }
        public string JobId { get; set; }
        public string StaffNo { get; set; }
        public string Gender { get; set; }
        public string IdNumber { get; set; }
        public string EmailAddress { get; set; }
        public decimal LeaveBalance { get; set; }
        public string PhoneNumber { get; set; }
        public string PostalAddress { get; set; }
        public string DOB { get; set; }
        public string MaritalStatus { get; set; }
        public string StaffStatus { get;set; }

        public string Religion { get; set; }
        public string Tribe { get; set; }
        public string County { get; set; }
        public string Title { get; set; }


    }
}