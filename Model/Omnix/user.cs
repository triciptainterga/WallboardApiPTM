using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("user")]
public class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("password")]
    public string Password { get; set; }

    [Column("level")]
    public int? Level { get; set; }

    [Column("unit_id")]
    public int? UnitId { get; set; }

    [Column("supervisor_id")]
    public int? SupervisorId { get; set; }

    [Column("fullname")]
    public string Fullname { get; set; }

    [Column("nickname")]
    public string Nickname { get; set; }

    [Column("phone")]
    public string Phone { get; set; }

    [Column("user_pbx")]
    public string UserPbx { get; set; }

    [Column("pwd_pbx")]
    public string PwdPbx { get; set; }

    [Column("pabx_host")]
    public string PabxHost { get; set; }

    [Column("user_pbx_sales")]
    public string UserPbxSales { get; set; }

    [Column("pwd_pbx_sales")]
    public string PwdPbxSales { get; set; }

    [Column("pabx_host_sales")]
    public string PabxHostSales { get; set; }

    [Column("profile_pic")]
    public string ProfilePic { get; set; }

    [Column("expired_password_at")]
    public DateTime? ExpiredPasswordAt { get; set; }

    [Column("fail_login")]
    public int? FailLogin { get; set; }

    [Column("is_internal")]
    public bool IsInternal { get; set; }

    [Column("is_service")]
    public bool IsService { get; set; }

    [Column("is_sales")]
    public bool IsSales { get; set; }

    [Column("is_marketer")]
    public bool IsMarketer { get; set; }

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

    [Column("generated_key")]
    public string GeneratedKey { get; set; }

    [Column("username")]
    public string Username { get; set; }

    [Column("is_2fa")]
    public bool Is2Fa { get; set; }

    [Column("generated_auth")]
    public string GeneratedAuth { get; set; }

    [Column("provinsi")]
    public string Provinsi { get; set; }

    [Column("kota")]
    public string Kota { get; set; }

    [Column("jabatan")]
    public string Jabatan { get; set; }

    [Column("job_group")]
    public string JobGroup { get; set; }

    [Column("area")]
    public string Area { get; set; }
}
