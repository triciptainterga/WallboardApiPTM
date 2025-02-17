using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class FaqCategory
    {
        public FaqCategory()
        {
            Faqs = new HashSet<Faq>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte IsPublished { get; set; }
        public string Media { get; set; }
        public byte IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
        public virtual ICollection<Faq> Faqs { get; set; }
    }
}
