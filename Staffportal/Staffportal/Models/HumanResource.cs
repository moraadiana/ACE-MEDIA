using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Staffportal.Models
{
    public class HumanResource
    {
        public string LeaveNo { get; set; }
        public string LeaveType { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string EndingDate { get; set; }
        public string ReturningDate { get; set; }
        public decimal AppliedDays { get; set; }
        public string Status { get; set; }
        public string StatusCls { get; set; }
        public string Reliever { get; set; }
        public string ResponsibilityCenter { get; set; }
        public decimal LeaveBalance { get; set; }
        public string Directorate { get; set; }
        public string Department { get; set; }
        public string Purpose { get; set; }
        public string Comments { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string NoOfDays { get; set; }
        public string TransactionDescription { get; set; }
        public int PeriodYear { get; set; }

        public string PeriodMonth { get; set; }

        public string StaffName { get; set; }

        public string StaffNo { get; set; }
        public List<Config> PeriodYears {  get; set; }

        public List<Config> PeriodMonths { get; set; }
        public List<HumanResource> LeaveListing { get; set; }
        public List<HumanResource> LeaveTransactions { get; set; }
        public List<Config> LeaveTypes { get; set; }
        public List<Config> Relievers { get; set; }
        public List<Config> ResponsibilityCenters { get; set; }
    }
}