using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class FollowUpVoice
    {
        public string SessionId { get; set; }
        public string CallType { get; set; }
        public int? DateCreate { get; set; }
        public int? DateStart { get; set; }
        public int? DateAnswer { get; set; }
        public int? DateEnd { get; set; }
        public int? AgentId { get; set; }
        public string AgentName { get; set; }
        public string Extension { get; set; }
        public string FromId { get; set; }
        public string ToId { get; set; }
        public string AStatus { get; set; }
        public string BStatus { get; set; }
        public int? Duration { get; set; }
        public int? Billsec { get; set; }
        public string Media { get; set; }
        public string MediaOriginal { get; set; }
    }
}
