using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class MCategoryMain
    {
        public MCategoryMain()
        {
            MCategories = new HashSet<MCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte IsActive { get; set; }
        public byte IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? IdOn4 { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
        public virtual ICollection<MCategory> MCategories { get; set; }
    }
}
