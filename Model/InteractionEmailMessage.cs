using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class InteractionEmailMessage
    {
        public int EmailId { get; set; }
        public string MessageText { get; set; }
        public string MessageHtml { get; set; }
        public int? CreatedBy { get; set; }
        public int? IdOn4 { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual InteractionEmail Email { get; set; }
    }
}
