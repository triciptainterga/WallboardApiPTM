using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("m_customer")]
public class MCustomer
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("member_id")]
    public int? MemberId { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("address")]
    public string Address { get; set; }

    [Column("hp")]
    public string Hp { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("telegram_id")]
    public string TelegramId { get; set; }

    [Column("telegram_username")]
    public string TelegramUsername { get; set; }

    [Column("telegram_name")]
    public string TelegramName { get; set; }

    [Column("twitter_id")]
    public string TwitterId { get; set; }

    [Column("twitter_username")]
    public string TwitterUsername { get; set; }

    [Column("twitter_followers")]
    public int? TwitterFollowers { get; set; }

    [Column("twitter_following")]
    public int? TwitterFollowing { get; set; }

    [Column("twitter_picture")]
    public string TwitterPicture { get; set; }

    [Column("twitter_name")]
    public string TwitterName { get; set; }

    [Column("facebook_id")]
    public string FacebookId { get; set; }

    [Column("facebook_name")]
    public string FacebookName { get; set; }

    [Column("facebook_picture")]
    public string FacebookPicture { get; set; }

    [Column("instagram_id")]
    public string InstagramId { get; set; }

    [Column("instagram_id2")]
    public string InstagramId2 { get; set; }

    [Column("application")]
    public string Application { get; set; }

    [Column("instagram_name")]
    public string InstagramName { get; set; }

    [Column("instagram_picture")]
    public string InstagramPicture { get; set; }

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

    [Column("line_id")]
    public string LineId { get; set; }

    [Column("gender")]
    public string Gender { get; set; }

    [Column("id_on4")]
    public string IdOn4 { get; set; }

    [Column("other")]
    public string Other { get; set; }
}
