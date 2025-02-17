using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class InteractionFacebookCommentTraffic
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string ParentId { get; set; }
        public string ParentCommentId { get; set; }
        public string StreamId { get; set; }
        public string CommentType { get; set; }
        public string FromId { get; set; }
        public string FromUsername { get; set; }
        public string FromName { get; set; }
        public string MessageType { get; set; }
        public string Message { get; set; }
        public string Media { get; set; }
        public string MediaOriginal { get; set; }
        public int? DateOrigin { get; set; }
        public int? DateMiddleware { get; set; }
        public int? DateReceived { get; set; }
        public int? IdOn4 { get; set; }
    }
}
