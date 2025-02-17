using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class InteractionVideoCall
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public string Account { get; set; }
        public string Room { get; set; }
        public string Token { get; set; }
        public int CustId { get; set; }
        public string FromId { get; set; }
        public string FromUsername { get; set; }
        public string FromName { get; set; }
        public int? AgentId { get; set; }
        public string Message { get; set; }
        public string AdditionalInfo { get; set; }
        public string Media { get; set; }
        public string MediaOriginal { get; set; }
        public int? DateRinging { get; set; }
        public int? DateAnswered { get; set; }
        public int? DateHangup { get; set; }
        public int? Duration { get; set; }
        public string RoomDetail { get; set; }
        public string StreamId { get; set; }
        public string Chat { get; set; }

        public virtual User Agent { get; set; }
        public virtual MCustomer Cust { get; set; }
    }
}
