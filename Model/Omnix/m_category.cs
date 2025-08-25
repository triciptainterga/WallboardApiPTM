using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("m_category")]
public class MCategory
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("mainCategory_id")]
    public int? MainCategoryId { get; set; }

    [Column("level_2")]
    public string Level2 { get; set; }

    [Column("level_3")]
    public string Level3 { get; set; }

    [Column("level_4")]
    public string Level4 { get; set; }

    [Column("level_5")]
    public string Level5 { get; set; }

    [Column("priority_id")]
    public int? PriorityId { get; set; }

    [Column("unit_id")]
    public int? UnitId { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("created_by")]
    public int? CreatedBy { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Column("id_on4")]
    public string IdOn4 { get; set; }
}
