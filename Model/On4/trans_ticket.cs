using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace WEBAPI_Bravo.Models.On4
{
    [Table("trans_ticket")]
    public class TransTicket
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("ticket_id")]
        public int? TicketId { get; set; }

        [Column("create_date")]
        public DateTime? CreateDate { get; set; }

        [Column("cust_id")]
        public int? CustId { get; set; }

        [Column("cust_name")]
        public string CustName { get; set; }

        [Column("cust_hp")]
        public string CustHp { get; set; }

        [Column("cust_info1")]
        public string CustInfo1 { get; set; }

        [Column("cust_info2")]
        public string CustInfo2 { get; set; }

        [Column("cust_info3")]
        public string CustInfo3 { get; set; }

        [Column("subject")]
        public string Subject { get; set; }

        [Column("remark")]
        public string Remark { get; set; }

        [Column("feedback")]
        public string Feedback { get; set; }

        [Column("file_path")]
        public string FilePath { get; set; }

        [Column("category_id")]
        public int? CategoryId { get; set; }

        [Column("sub_category_id")]
        public int? SubCategoryId { get; set; }

        [Column("product_id")]
        public int? ProductId { get; set; }

        [Column("ticket_priority")]
        public int? TicketPriority { get; set; }

        [Column("ticket_action")]
        public string TicketAction { get; set; }

        [Column("ticket_status")]
        public int? TicketStatus { get; set; }

        [Column("date_close")]
        public DateTime? DateClose { get; set; }

        [Column("unit_id")]
        public int? UnitId { get; set; }

        [Column("agent_id")]
        public int? AgentId { get; set; }

        [Column("ticket_tdscc")]
        public string TicketTdscc { get; set; }

        [Column("pic_tdscc")]
        public string PicTdscc { get; set; }

        [Column("no_ticket_telkom")]
        public string NoTicketTelkom { get; set; }

        [Column("pic_telkom")]
        public string PicTelkom { get; set; }

        [Column("pertamina_account")]
        public string PertaminaAccount { get; set; }

        [Column("upd")]
        public string Upd { get; set; }

        [Column("lup")]
        public string Lup { get; set; }

        [Column("socmed_acc")]
        public string SocmedAcc { get; set; }

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

        [Column("pic_mor")]
        public string PicMor { get; set; }

        [Column("flag_system")]
        public string FlagSystem { get; set; }

        [Column("login_mbl")]
        public string LoginMbl { get; set; }

        [Column("pangkat_mbl")]
        public string PangkatMbl { get; set; }

        [Column("flag_ticket")]
        public string FlagTicket { get; set; }

        [Column("is_migration")]
        public bool IsMigration { get; set; }
    }
}
