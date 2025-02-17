using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class FollowUp
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public string Account { get; set; }
        public int ChannelId { get; set; }
        public int CustId { get; set; }
        public int? TicketId { get; set; }
        public int DateCreate { get; set; }
        public string Notes { get; set; }
        public int AgentId { get; set; }

        public virtual User Agent { get; set; }
        public virtual MChannel Channel { get; set; }
        public virtual MCustomer Cust { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
