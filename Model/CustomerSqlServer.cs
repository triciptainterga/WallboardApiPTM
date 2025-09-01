using System;
using System.ComponentModel.DataAnnotations.Schema;

[Table("m_customer_integrasi")]
public class CustomerSqlServer
{
    public int Id { get; set; }
    public string member_id { get; set; }
    public string name { get; set; }
    public string address { get; set; }
    public string hp { get; set; }
    public string email { get; set; }
    public string telegram_id { get; set; }
    public string telegram_username { get; set; }
    public string telegram_name { get; set; }
    public string twitter_id { get; set; }
    public string twitter_username { get; set; }
    public string twitter_followers { get; set; }
    public string twitter_following { get; set; }
    public string twitter_picture { get; set; }
    public string twitter_name { get; set; }
    public string facebook_id { get; set; }
    public string facebook_name { get; set; }
    public string facebook_picture { get; set; }
    public string instagram_id { get; set; }
    public string instagram_id2 { get; set; }
    public string application { get; set; }
    public string instagram_name { get; set; }
    public string instagram_picture { get; set; }
    public bool is_deleted { get; set; } = false;
    public DateTime created_at { get; set; } = DateTime.UtcNow;
    public DateTime? updated_at { get; set; }
    public string created_by { get; set; } = "migration";
    public string updated_by { get; set; }
    public string line_id { get; set; }
    public string gender { get; set; }
    public string id_on4 { get; set; }
    public string other { get; set; }
}
