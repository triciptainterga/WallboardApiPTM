using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("m_sub_category")]
public class MSubCategory
{
    [Key]
    [Column("sub_category_id")]
    public int SubCategoryId { get; set; }

    [Column("category_id")]
    public int CategoryId { get; set; }

    [Column("sub_category")]
    public string SubCategory { get; set; }

    [Column("sub_category_desc")]
    public string SubCategoryDesc { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("sla")]
    public string Sla { get; set; }

    [Column("upd")]
    public string Upd { get; set; }

    [Column("lup")]
    public string Lup { get; set; }

    [Column("is_migration")]
    public bool IsMigration { get; set; }
}
