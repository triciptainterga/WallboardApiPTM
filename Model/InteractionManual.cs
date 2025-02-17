using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class InteractionManual
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public int SourceId { get; set; }
        public int CustId { get; set; }
        public string FromId { get; set; }
        public string FromUsername { get; set; }
        public string FromName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public string Media { get; set; }
        public int? DateCreate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual MCustomer Cust { get; set; }
        public virtual MSource Source { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
    }
}
