using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class MSource
    {
        public MSource()
        {
            InteractionManuals = new HashSet<InteractionManual>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte IsActive { get; set; }
        public byte IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
        public virtual ICollection<InteractionManual> InteractionManuals { get; set; }
    }
}
