using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class MCustomerSuggestionMerge
    {
        public int Id { get; set; }
        public int CustId { get; set; }
        public int CustIdSuggestion { get; set; }

        public virtual MCustomer Cust { get; set; }
        public virtual MCustomer CustIdSuggestionNavigation { get; set; }
    }
}
