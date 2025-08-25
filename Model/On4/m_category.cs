using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEBAPI_Bravo.Models.On4
{

    [Table("m_category")]
    public class MCategory
    {
        [Key]
        [Column("category_id")]
        public int CategoryId { get; set; }

        [Column("category")]
        public string Category { get; set; }

        [Column("category_desc")]
        public string CategoryDesc { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("upd")]
        public string Upd { get; set; }

        [Column("lup")]
        public string Lup { get; set; }

        [Column("sla")]
        public string Sla { get; set; }

        [Column("is_migration")]
        public bool IsMigration { get; set; }
    }
}