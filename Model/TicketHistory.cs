using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class TicketHistory
    {
        public int Id { get; set; }
        public int DateCreate { get; set; }
        public int TicketId { get; set; }
        public string Subject { get; set; }
        public int CategoryId { get; set; }
        public int UnitId { get; set; }
        public int? StatusIdBefore { get; set; }
        public int StatusId { get; set; }
        public int? PriorityId { get; set; }
        public string Remark { get; set; }
        public string Files { get; set; }
        public int? Duration { get; set; }
        public int? CreatedBy { get; set; }
        public int? FromUnitId { get; set; }
        public string AdditionalData { get; set; }
        public string Feedback { get; set; }
        public int? ParentId { get; set; }

        public virtual MCategory Category { get; set; }
        public virtual User CreatedByNavigation { get; set; }
        public virtual MUnit FromUnit { get; set; }
        public virtual Ticket Parent { get; set; }
        public virtual MTicketPriority Priority { get; set; }
        public virtual MTicketStatus Status { get; set; }
        public virtual MTicketStatus StatusIdBeforeNavigation { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual MUnit Unit { get; set; }
    }
}
