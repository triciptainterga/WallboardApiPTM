using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class LogInteractionOutOfSchedule
    {
        public int Id { get; set; }
        public int ChannelId { get; set; }
        public string Account { get; set; }
        public string FromId { get; set; }
        public string FromUsername { get; set; }
        public string FromName { get; set; }
        public string Message { get; set; }
        public string MediaOriginal { get; set; }
        public int? DateOrigin { get; set; }
        public int? DateMiddleware { get; set; }
        public int? DateReceived { get; set; }

        public virtual MChannel Channel { get; set; }
    }
}
