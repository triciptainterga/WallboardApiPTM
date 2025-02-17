using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class InteractionEmail
    {
        public InteractionEmail()
        {
            InteractionEmailAttachments = new HashSet<InteractionEmailAttachment>();
        }

        public int Id { get; set; }
        public string SessionId { get; set; }
        public string Account { get; set; }
        public string MessageId { get; set; }
        public string MessageIdFormat { get; set; }
        public string MessageIdReferences { get; set; }
        public string InReplyTo { get; set; }
        public int? CustId { get; set; }
        public string FromId { get; set; }
        public string FromUsername { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string CcEmail { get; set; }
        public string BccEmail { get; set; }
        public string FolderEmail { get; set; }
        public string Subject { get; set; }
        public string ActionType { get; set; }
        public byte? SendStatus { get; set; }
        public string SendDetail { get; set; }
        public int? DateOrigin { get; set; }
        public int? DateMiddleware { get; set; }
        public int? DateReceived { get; set; }
        public int? ResponseTime { get; set; }
        public int? CreatedBy { get; set; }
        public int? IdOn4 { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual MCustomer Cust { get; set; }
        public virtual InteractionEmailMessage InteractionEmailMessage { get; set; }
        public virtual ICollection<InteractionEmailAttachment> InteractionEmailAttachments { get; set; }
    }
}
