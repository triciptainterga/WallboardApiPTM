using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("m_customer_suggestion_merge")]
public class MCustomerSuggestionMerge
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("cust_id")]
    public int CustId { get; set; }

    [Column("cust_id_suggestion")]
    public int CustIdSuggestion { get; set; }
}
