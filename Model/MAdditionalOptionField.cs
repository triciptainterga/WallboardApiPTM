using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class MAdditionalOptionField
    {
        public int Id { get; set; }
        public int? AdditionalFieldId { get; set; }
        public string FromValue { get; set; }
        public string Value { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual MAdditionalFieldTicket AdditionalField { get; set; }
        public virtual User CreatedByNavigation { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
    }
}
