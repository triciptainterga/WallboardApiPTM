using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class InteractionVideocallLink
    {
        public string Sid { get; set; }
        public int DateCreate { get; set; }
        public int DateLastUpdate { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public string MessageId { get; set; }
        public string SendStatus { get; set; }
        public int TotalAttempt { get; set; }
        public byte IsPickup { get; set; }
        public byte IsExpired { get; set; }
        public byte IsFinish { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
    }
}
