using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("cwc")]
public class Cwc
{
    [Key]
    [Column("cwc_id")]
    public int CwcId { get; set; }

    [Column("cwc_type")]
    public string CwcType { get; set; }

    [Column("ticket_id")]
    public int? TicketId { get; set; }

    [Column("date_contact")]
    public DateTime? DateContact { get; set; }

    [Column("date_submit")]
    public DateTime? DateSubmit { get; set; }

    [Column("channel_id")]
    public int? ChannelId { get; set; }

    [Column("session_id")]
    public string SessionId { get; set; }

    [Column("cust_id")]
    public int? CustId { get; set; }

    [Column("cust_name")]
    public string CustName { get; set; }

    [Column("category_id")]
    public int? CategoryId { get; set; }

    [Column("sub_category_id")]
    public int? SubCategoryId { get; set; }

    [Column("remark")]
    public string Remark { get; set; }

    [Column("feedback")]
    public string Feedback { get; set; }

    [Column("sentiment")]
    public string Sentiment { get; set; }

    [Column("create_ticket")]
    public bool? CreateTicket { get; set; }

    [Column("agent_id")]
    public int? AgentId { get; set; }

    [Column("lup")]
    public string Lup { get; set; }

    [Column("socmed_acc")]
    public string SocmedAcc { get; set; }

    [Column("pertamina_account")]
    public string PertaminaAccount { get; set; }

    [Column("mor_id")]
    public int? MorId { get; set; }

    [Column("product_pertamina_id")]
    public int? ProductPertaminaId { get; set; }

    [Column("qty")]
    public int? Qty { get; set; }

    [Column("price")]
    public decimal? Price { get; set; }

    [Column("purpose")]
    public string Purpose { get; set; }

    [Column("flag_ticket")]
    public string FlagTicket { get; set; }

    [Column("is_migration")]
    public bool IsMigration { get; set; }
}
