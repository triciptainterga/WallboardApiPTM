using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class Journey
    {
        public int Id { get; set; }
        public string Application { get; set; }
        public string SessionId { get; set; }
        public int ChannelId { get; set; }
        public int CustId { get; set; }
        public int? AgentId { get; set; }
        public int? DateOrigin { get; set; }
        public int? DateStart { get; set; }
        public int? DateEnd { get; set; }
        public int? TotalCase { get; set; }
        public int? TotalTicket { get; set; }
        public string SalesProduct { get; set; }
        public string SalesCampaign { get; set; }
        public string StatusCall { get; set; }
        public string ReasonCall { get; set; }
        public string SubreasonCall { get; set; }
        public string MarketerProduct { get; set; }
        public string MarketerCampaign { get; set; }
        public int? IdOn4 { get; set; }

        public virtual User Agent { get; set; }
        public virtual MChannel Channel { get; set; }
        public virtual MCustomer Cust { get; set; }
    }
}
