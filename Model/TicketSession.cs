using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class TicketSession
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public int TicketId { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
