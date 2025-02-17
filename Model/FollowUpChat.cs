using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class FollowUpChat
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public string ConvId { get; set; }
        public string StreamId { get; set; }
        public string Account { get; set; }
        public int CustId { get; set; }
        public string FromId { get; set; }
        public string FromUsername { get; set; }
        public string FromName { get; set; }
        public string MessageType { get; set; }
        public string Message { get; set; }
        public string Media { get; set; }
        public string MediaOriginal { get; set; }
        public byte? SendStatus { get; set; }
        public string SendDetail { get; set; }
        public int? DateOrigin { get; set; }
        public int? DateMiddleware { get; set; }
        public int? DateReceived { get; set; }
        public int? ResponseTime { get; set; }
        public int? CreatedBy { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual MCustomer Cust { get; set; }
    }
}
