using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class MCategory
    {
        public MCategory()
        {
            TicketHistories = new HashSet<TicketHistory>();
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public int MainCategoryId { get; set; }
        public string Level2 { get; set; }
        public string Level3 { get; set; }
        public string Level4 { get; set; }
        public string Level5 { get; set; }
        public int? PriorityId { get; set; }
        public int? UnitId { get; set; }
        public byte IsActive { get; set; }
        public byte IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? IdOn4 { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual MCategoryMain MainCategory { get; set; }
        public virtual MTicketPriority Priority { get; set; }
        public virtual MUnit Unit { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
        public virtual ICollection<TicketHistory> TicketHistories { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
