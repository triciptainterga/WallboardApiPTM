using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class FollowUpEmailMessage
    {
        public int EmailId { get; set; }
        public string MessageText { get; set; }
        public string MessageHtml { get; set; }
        public int? CreatedBy { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual FollowUpEmail Email { get; set; }
    }
}
