using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class MAdditionalFieldTicket
    {
        public MAdditionalFieldTicket()
        {
            InverseLinkedField = new HashSet<MAdditionalFieldTicket>();
            MAdditionalOptionFields = new HashSet<MAdditionalOptionField>();
        }

        public int Id { get; set; }
        public string Label { get; set; }
        public string Key { get; set; }
        public int FieldNumber { get; set; }
        public string Type { get; set; }
        public string Option { get; set; }
        public byte IsMandatory { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public byte IsLinked { get; set; }
        public int? LinkedFieldId { get; set; }
        public int? Order { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual MAdditionalFieldTicket LinkedField { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
        public virtual ICollection<MAdditionalFieldTicket> InverseLinkedField { get; set; }
        public virtual ICollection<MAdditionalOptionField> MAdditionalOptionFields { get; set; }
    }
}
