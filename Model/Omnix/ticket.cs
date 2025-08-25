using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WEBAPI_Bravo.Model;

public class Ticket
{
    public Ticket()
    {
        FollowUps = new HashSet<FollowUp>();
        InverseParent = new HashSet<Ticket>();
        TicketHistoryParents = new HashSet<TicketHistory>();
        TicketHistoryTickets = new HashSet<TicketHistory>();
        TicketSessions = new HashSet<TicketSession>();
    }

    public int Id { get; set; }
    public DateTime? DateCreate { get; set; }
    public int InformantId { get; set; }
    public int CustId { get; set; }
    public int CategoryId { get; set; }
    public string Subject { get; set; }
    public string Remark { get; set; }
    public int Sentiment { get; set; }
    public int UnitId { get; set; }
    public int StatusId { get; set; }
    public int? PriorityId { get; set; }
    public int? DateEscalate { get; set; }
    public int? DateClose { get; set; }
    public byte IsEscalated { get; set; }
    public int DateLastUpdate { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public int? PickupBy { get; set; }
    public string IdOn4 { get; set; }
    public string Feedback { get; set; }
    public int? ParentId { get; set; }
    public int CountMerged { get; set; }
    public int? MorId { get; set; }
    public int DurationPending { get; set; }
    public int? DateStartPending { get; set; }
    public string Type { get; set; }

    // Navigational Properties
    public virtual MCategory Category { get; set; }
    public virtual User CreatedByNavigation { get; set; }
    public virtual MCustomer Cust { get; set; }
    public virtual MCustomer Informant { get; set; }
    public virtual Ticket Parent { get; set; }
    public virtual User PickupByNavigation { get; set; }
    public virtual MTicketPriority Priority { get; set; }
    public virtual MTicketStatus Status { get; set; }
    public virtual MUnit Unit { get; set; }
    public virtual User UpdatedByNavigation { get; set; }
    public virtual AdditionalFieldTicket AdditionalFieldTicket { get; set; }

    public virtual ICollection<FollowUp> FollowUps { get; set; }
    public virtual ICollection<Ticket> InverseParent { get; set; }
    public virtual ICollection<TicketHistory> TicketHistoryParents { get; set; }
    public virtual ICollection<TicketHistory> TicketHistoryTickets { get; set; }
    public virtual ICollection<TicketSession> TicketSessions { get; set; }
}

