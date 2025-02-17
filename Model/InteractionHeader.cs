using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class InteractionHeader
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public string Account { get; set; }
        public string ConvId { get; set; }
        public int ChannelId { get; set; }
        public string FromId { get; set; }
        public string FromUsername { get; set; }
        public string FromName { get; set; }
        public int CustId { get; set; }
        public int? AgentId { get; set; }
        public int? DateOrigin { get; set; }
        public int DateStart { get; set; }
        public int? DateFirstPickup { get; set; }
        public int? DatePickup { get; set; }
        public int? DateFirstResponse { get; set; }
        public int? DateEnd { get; set; }
        public string Subject { get; set; }
        public byte IsAbandon { get; set; }
        public byte IsCreateCase { get; set; }
        public int Priority { get; set; }
        public int GroupId { get; set; }
        public string AccountName { get; set; }
        public string LastMessage { get; set; }
        public byte? IsRtc { get; set; }
        public string UniqueId { get; set; }
        public string AdditionalInfo { get; set; }
        public int? IdOn4 { get; set; }

        public virtual User Agent { get; set; }
        public virtual MChannel Channel { get; set; }
        public virtual MCustomer Cust { get; set; }
        public virtual MGroup Group { get; set; }
    }
}
