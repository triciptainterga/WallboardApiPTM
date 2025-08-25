using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("m_customer_contact")]
public class MCustomerContact
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("cust_id")]
    public int CustId { get; set; }

    [Column("cust_id_old")]
    public int? CustIdOld { get; set; }

    [Column("contact_type")]
    public string ContactType { get; set; }

    [Column("contact_value")]
    public string ContactValue { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}
