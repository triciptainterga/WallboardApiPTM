using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class InteractionVoice
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public string Ruid { get; set; }
        public string Uid { get; set; }
        public int CustId { get; set; }
        public string FromId { get; set; }
        public string FromUsername { get; set; }
        public string FromName { get; set; }
        public string Extension { get; set; }
        public int? AgentId { get; set; }
        public string Queue { get; set; }
        public string Media { get; set; }
        public string MediaOriginal { get; set; }
        public int? DateRinging { get; set; }
        public int? DateAnswered { get; set; }
        public int? DateHangup { get; set; }
        public int? IdOn4 { get; set; }

        public virtual User Agent { get; set; }
        public virtual MCustomer Cust { get; set; }
    }
}
