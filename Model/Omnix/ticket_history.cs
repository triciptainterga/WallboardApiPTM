using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ticket_history")]
public class TicketHistory
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("date_create")]
    public DateTime DateCreate { get; set; }

    [Column("ticket_id")]
    public int TicketId { get; set; }

    [Column("subject")]
    public string Subject { get; set; }

    [Column("category_id")]
    public int? CategoryId { get; set; }

    [Column("unit_id")]
    public int? UnitId { get; set; }

    [Column("status_id_before")]
    public int? StatusIdBefore { get; set; }

    [Column("status_id")]
    public int? StatusId { get; set; }

    [Column("priority_id")]
    public int? PriorityId { get; set; }

    [Column("remark")]
    public string Remark { get; set; }

    [Column("files")]
    public string Files { get; set; }

    [Column("duration")]
    public int? Duration { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("created_by")]
    public int? CreatedBy { get; set; }

    [Column("from_unit_id")]
    public int? FromUnitId { get; set; }

    [Column("additional_data")]
    public string AdditionalData { get; set; }

    [Column("feedback")]
    public string Feedback { get; set; }

    [Column("parent_id")]
    public int? ParentId { get; set; }
}
