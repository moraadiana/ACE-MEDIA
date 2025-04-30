using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Staffportal.Models
{
    public class Procurement
    {
        public int Counter { get; set; }
        public int Type { get; set; }
        public string StaffNo { get; set; }
        public string StaffName { get; set; }
        public string Directorate { get; set; }
        public string Department { get; set; }
        public string Project { get; set; }
        public string Activity { get; set; }
        public string DocumentNo { get; set; }
        public DateTime Date { get; set; }
        public string RequisitionType { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string StatusCls { get; set; }
        public string IssuingStore { get; set; }
        public string ResponsibilityCenter { get; set; }
        public DateTime RequiredDate { get; set; }
        public string ItemType { get; set; }
        public string ItemNo { get; set; }
        public decimal Quantity { get; set; }
        public string SystemId { get; set; }
        public List<Config> ResponsibilityCenters { get; set; }
        public List<Config> IssuingStores { get; set; }
        public List<Procurement> StoreListings { get; set; }
        public List<Procurement> StoreLines { get; set; }
    }
}