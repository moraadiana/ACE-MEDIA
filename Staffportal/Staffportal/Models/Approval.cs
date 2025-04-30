using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Staffportal.Models
{
    public class Approval
    {
        public string DocumentNo { get; set; }
        public int Counter { get; set; }
        public int EntryNo { get; set; }
        public int SequenceNo { get; set; }
        public DateTime DateSentForApproval { get; set; }
        public string SenderId { get; set; }
        public string ApproverId { get; set; }
        public string Status { get; set; }
        public string StatusCls { get; set; }
        public string Comments { get; set; }
        public List<Approval> Approvals { get; set; }
    }
}