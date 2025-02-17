using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class MTicketStatus
    {
        public MTicketStatus()
        {
            TicketHistoryStatusIdBeforeNavigations = new HashSet<TicketHistory>();
            TicketHistoryStatuses = new HashSet<TicketHistory>();
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte IsActive { get; set; }
        public byte IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public string Color { get; set; }
        public byte IsClose { get; set; }
        public byte IsPending { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
        public virtual ICollection<TicketHistory> TicketHistoryStatusIdBeforeNavigations { get; set; }
        public virtual ICollection<TicketHistory> TicketHistoryStatuses { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
