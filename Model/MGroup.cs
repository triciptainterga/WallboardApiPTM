using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class MGroup
    {
        public MGroup()
        {
            InteractionHeaderHistories = new HashSet<InteractionHeaderHistory>();
            InteractionHeaderTakeouts = new HashSet<InteractionHeaderTakeout>();
            InteractionHeaders = new HashSet<InteractionHeader>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public byte IsActive { get; set; }
        public byte IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
        public virtual ICollection<InteractionHeaderHistory> InteractionHeaderHistories { get; set; }
        public virtual ICollection<InteractionHeaderTakeout> InteractionHeaderTakeouts { get; set; }
        public virtual ICollection<InteractionHeader> InteractionHeaders { get; set; }
    }
}
