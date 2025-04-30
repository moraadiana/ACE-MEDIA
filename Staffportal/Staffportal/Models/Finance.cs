using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Staffportal.Models
{
    public class Finance
    {
        public int Counter { get; set; }
        public string DocumentNo { get; set; }
        public string SurrenderNo { get; set; }
        public DateTime SurrenderDate { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string StatusCls { get; set; }
        public bool PRN { get; set; }
        public string IsPrn { get; set; }
        public string PrnNo { get; set; }
        public string To { get; set; }
        public string Through { get; set; }
        public string StaffNo { get; set; }
        public string StaffName { get; set; }
        public string Directorate { get; set; }
        public string Department { get; set; }
        public string Project { get; set; }
        public string Activity { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }

        public string Purpose { get; set; }
        public string ResponsibilityCenter { get; set; }
        public DateTime ActivityStartDate { get; set; }
        public DateTime ActivityEndDate { get; set; }
        public int Type { get; set; }
        public string ItemNo { get; set; }
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
        public string SystemId { get; set; }
        public string IssuingStore { get; set; }
        public string EmployeeNo { get; set; }
        public string SalaryGrade { get; set; }
        public string Region { get; set; }
        public string ImprestType { get; set; }
        public decimal Days { get; set; }
        public decimal Rate { get; set; }
        public string GlAccount { get; set; }
        public string AdvanceType { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string SelectedCategories { get; set; }
        public string ReceiptNo { get; set; }
        public string ActualSpent { get; set; }
        public string CashReturned { get; set; }
        public string PettyCashAccountNo { get; set; }

        public string ClientCode { get; set; }
        public string JobOrderCode { get; set; }

        public string Customer { get; set; }


        public string ExpenditureDate { get; set; }

        //public string ReceiptNo { get; set; }
        public HttpPostedFileBase AttachmentFile { get; set; }
        public List<Finance> MemoListing { get; set; }
        public List<Finance> MemoPrnLines { get; set; }
        public List<Config> Jobs { get; set; }
        public List<Config> ResponsibilityCenters { get; set; }

        public List<Config> Customers { get; set; }
        public List<Config> Directorates { get; set; }

        public List<Config> ClientCodes { get; set; }

        public List<Config> JobOrderCodes { get; set; }
        public List<Config> Departments { get; set; }
        public List<Config> Projects { get; set; }
        public List<Config> IssuingStores { get; set; }
        public List<Config> Employees { get; set; }
        public List<Config> MemoRegions { get; set; }
        public List<Config> ImprestTypes { get; set; }
        public List<Config> ClaimTypes { get; set; }
        public List<Config> PettyCashTypes { get; set; }
        public List<Config> GlAccounts { get; set; }
        public List<Finance> DsaLines { get; set; }
        public List<Finance> OtherCostsLines { get; set; }
        public List<Config> FinanceAttachments { get; set; }
        public List<Config> ImprestReceipts { get; set; }
        public List<Finance> ImprestListing { get; set; }
        public List<Finance> ImprestLines { get; set; }
        public List<Finance> PostedImprests { get; set; }
        public List<Finance> ImprestSurrenderListing { get; set; }
        public List<Finance> ImprestSurrenderLines { get; set; }
        public List<Finance> ClaimListing { get; set; }
        public List<Finance> ClaimLines { get; set; }
        public List<Finance> PettyCashListing { get; set; }
        public List<Finance> PettyCashLines { get; set; }
        public List<Finance> PettyCashSurrenderListing { get; set; }
        public List<Finance> PettyCashSurrenderLines { get; set; }

    }
}