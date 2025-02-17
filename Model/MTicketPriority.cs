using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class MTicketPriority
    {
        public MTicketPriority()
        {
            MCategories = new HashSet<MCategory>();
            TicketHistories = new HashSet<TicketHistory>();
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte IsActive { get; set; }
        public byte IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int Sla { get; set; }
        public string SlaType { get; set; }
        public string TimeType { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
        public virtual ICollection<MCategory> MCategories { get; set; }
        public virtual ICollection<TicketHistory> TicketHistories { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
