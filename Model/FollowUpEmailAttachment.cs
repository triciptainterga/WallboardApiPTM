using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class FollowUpEmailAttachment
    {
        public int EmailId { get; set; }
        public string ContentId { get; set; }
        public string ContentType { get; set; }
        public string ContentDisposition { get; set; }
        public string FileName { get; set; }
        public int Size { get; set; }
        public string Url { get; set; }
        public string Token { get; set; }
        public int? CreatedBy { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual FollowUpEmail Email { get; set; }
    }
}
