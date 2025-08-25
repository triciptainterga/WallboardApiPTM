using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("m_customer_contact_detail")]
public class MCustomerContactDetail
{
    [Key]
    [Column("contact_id")]
    public int ContactId { get; set; }

    [Column("username")]
    public string Username { get; set; }

    [Column("display_name")]
    public string DisplayName { get; set; }

    [Column("display_picture")]
    public string DisplayPicture { get; set; }

    [Column("followers")]
    public int? Followers { get; set; }

    [Column("following")]
    public int? Following { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}
