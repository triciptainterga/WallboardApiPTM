using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class MAdditionalFieldCustomer
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Key { get; set; }
        public int FieldNumber { get; set; }
        public string Type { get; set; }
        public string Option { get; set; }
        public byte IsMandatory { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? Order { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
    }
}
