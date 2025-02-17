using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class MCustomerContact
    {
        public int Id { get; set; }
        public int CustId { get; set; }
        public int? CustIdOld { get; set; }
        public string ContactType { get; set; }
        public string ContactValue { get; set; }

        public virtual MCustomer Cust { get; set; }
        public virtual MCustomer CustIdOldNavigation { get; set; }
    }
}
