using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("interaction_header_history")]
public class InteractionHeaderHistory
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("session_id")]
    public string SessionId { get; set; }

    [Column("message_type")]
    public string MessageType { get; set; }

    [Column("channel_id")]
    public int? ChannelId { get; set; }

    [Column("from")]
    public string From { get; set; }

    [Column("from_name")]
    public string FromName { get; set; }

    [Column("agent_id")]
    public int? AgentId { get; set; }

    [Column("fr_date")]
    public DateTime? FrDate { get; set; }

    [Column("pickup_date")]
    public DateTime? PickupDate { get; set; }

    [Column("start_date")]
    public DateTime? StartDate { get; set; }

    [Column("end_date")]
    public DateTime? EndDate { get; set; }

    [Column("response_time")]
    public int? ResponseTime { get; set; }

    [Column("cust_id")]
    public int? CustId { get; set; }

    [Column("socket")]
    public string Socket { get; set; }

    [Column("socket_date")]
    public DateTime? SocketDate { get; set; }

    [Column("group_id")]
    public int? GroupId { get; set; }

    [Column("lup")]
    public string Lup { get; set; }

    [Column("case_in")]
    public string CaseIn { get; set; }

    [Column("case_out")]
    public string CaseOut { get; set; }

    [Column("is_migration")]
    public bool IsMigration { get; set; }
}
