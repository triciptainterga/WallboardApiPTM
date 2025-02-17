using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class TicketIem
    {
        public int Id { get; set; }
        public string ExceptCode { get; set; }
        public string ExceptName { get; set; }
        public string ItemId { get; set; }
        public string SignalCode { get; set; }
        public DateTime? SignalDate { get; set; }
        public DateTime? TransaksiDate { get; set; }
        public string ProductName { get; set; }
        public int? Regional { get; set; }
        public string TipeAgen { get; set; }
        public string Kota { get; set; }
        public string Provinsi { get; set; }
        public int? DeliveryVolume { get; set; }
        public int? Jumlah { get; set; }
        public string VehicleNumber { get; set; }
        public int? SiteId { get; set; }
        public string AttendantName { get; set; }
        public string Mor { get; set; }
        public string Sbm { get; set; }
        public DateTime? InputDate { get; set; }
        public string TicketId { get; set; }
        public string TicketStatus { get; set; }
        public DateTime? TicketDate { get; set; }
        public int? LastUpdate { get; set; }
        public string Remark { get; set; }
        public string Feedback { get; set; }
        public string TicketEscalation { get; set; }
        public string Attachment { get; set; }
        public int NotificationStatus { get; set; }
        public int? MorId { get; set; }
        public int? UpdatedAt { get; set; }
        public string ActionCategory { get; set; }
        public string MainResponse { get; set; }
        public int? Sla { get; set; }
        public string Product { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? IdIems { get; set; }
    }
}
