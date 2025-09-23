using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WEBAPI_Bravo.Model;

namespace WEBAPI_Bravo.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly OmnixContext _context;
        private readonly CrmContext _Crmcontext;
        private readonly IConfiguration _configuration;

        public TenantController(OmnixContext context, CrmContext Crmcontext, IConfiguration configuration)
        {
            _context = context;
            _Crmcontext = Crmcontext;
            _configuration = configuration;



        }

        [HttpGet]
        [Route("GetDataApiTenant")]
        public async Task<IActionResult> GetDataApiTenant([FromQuery] string TenantName)
        {
            var users = new List<dataTenant>();

            try
            {
                using (var connection = _Crmcontext.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataApiTenant @Name";
                        var ParameterName = new SqlParameter("@Name", TenantName);
                        command.Parameters.Add(ParameterName);
                        command.CommandType = CommandType.Text;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                var tenant = new dataTenant
                                {
                                    ID = result.GetInt32(0),
                                    TenantID = result.GetString(1),
                                    DomainAPI = result.GetString(2),
                                   // TenantApiKey = result.GetString(3),
                                    URLtoken = result.GetString(4),
                                    URLtoken_Body = JsonConvert.DeserializeObject<List<string>>(result.GetString(5)),
                                    URLtoken_ResponseSuc = JsonConvert.DeserializeObject<List<string>>(result.GetString(6)),
                                    URLtoken_Expired = result.GetString(7),
                                    URLtoken_ResponseErr = JsonConvert.DeserializeObject<List<string>>(result.GetString(8)),
                                    URLprofile = result.GetString(9),
                                    URLprofile_Body = result.GetString(10),
                                    URLprofile_ResponseSuc = JsonConvert.DeserializeObject<URLprofileResponseSuc>(result.GetString(11)),
                                    URLprofile_ResponseErr = JsonConvert.DeserializeObject<List<string>>(result.GetString(12)),
                                    ActiveTenant = result.GetString(13)
                                };
                                users.Add(tenant);
                            }
                        }

                       
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                return StatusCode(500, $"Database error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }

            return Ok(users);
        }



        [HttpGet]
        [Route("GetDataCustomerOmnixQuery")]


        [HttpPost]
        public async Task<IActionResult> MigrasiCustomer([FromQuery] int? StartId)
        {
            string mySqlConnStr = _configuration.GetConnectionString("OmnixConnection");   // MySQL
            string sqlServerConnStr = _configuration.GetConnectionString("CrmConnection"); // SQL Server

            const int batchSize = 1000;
            int success = 0;
            int failed = 0;
            int total = 0;

            // Buat folder log per hari
            string baseLogDir = @"C:\LogIntegrasi";
            string todayFolder = DateTime.UtcNow.ToString("yyyy-MM-dd");
            string fullLogPath = Path.Combine(baseLogDir, todayFolder);
            Directory.CreateDirectory(fullLogPath);

            string timestamp = DateTime.UtcNow.ToString("HHmmss");
            string logFilePath = Path.Combine(fullLogPath, $"Log_Insert_{timestamp}.txt");
            var logLines = new List<string>();
            logLines.Add($"=== LOG MIGRASI CUSTOMER {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC ===");

            // 1. Hitung total data MySQL
            using (var mySqlCountConn = new MySqlConnection(mySqlConnStr))
            {
                await mySqlCountConn.OpenAsync();
                using var countCmd = new MySqlCommand("SELECT COUNT(*) FROM m_customer", mySqlCountConn);
                total = Convert.ToInt32(await countCmd.ExecuteScalarAsync());
            }

            int totalBatches = (int)Math.Ceiling(total / (double)batchSize);

            using (var sqlServerConn = new SqlConnection(sqlServerConnStr))
            {
                await sqlServerConn.OpenAsync();

                // === Prepare SqlCommand sekali saja ===
                using (var cmd = new SqlCommand("sp_InsertCustomerIntegrasi", sqlServerConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Definisikan parameter sekali
                    cmd.Parameters.Add("@MemberId", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Hp", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@TelegramId", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@TelegramUsername", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@TelegramName", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@TwitterId", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@TwitterUsername", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@TwitterFollowers", SqlDbType.Int);
                    cmd.Parameters.Add("@TwitterFollowing", SqlDbType.Int);
                    cmd.Parameters.Add("@TwitterPicture", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@TwitterName", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@FacebookId", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@FacebookName", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@FacebookPicture", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@InstagramId", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@InstagramId2", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Application", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@InstagramName", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@InstagramPicture", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@LineId", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Gender", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@IdOn4", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Other", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@CreatedAt", SqlDbType.DateTime);

                    for (int batchNumber = 0; batchNumber < totalBatches; batchNumber++)
                    {
                        var customersBatch = new List<CustomerModel>();

                        // Ambil batch data dari MySQL
                        using (var mySqlConn = new MySqlConnection(mySqlConnStr))
                        {
                            await mySqlConn.OpenAsync();

                            string query = @"SELECT 
                        id As Id, 
                        member_id AS MemberId,
                        name AS Name,
                        address AS Address,
                        hp AS Hp,
                        email AS Email,
                        telegram_id AS TelegramId,
                        twitter_id AS TwitterId,
                        twitter_username AS TwitterUsername,
                        twitter_followers AS TwitterFollowers,
                        twitter_following AS TwitterFollowing,
                        twitter_picture AS TwitterPicture,
                        twitter_name AS TwitterName,
                        facebook_id AS FacebookId,
                        facebook_name AS FacebookName,
                        facebook_picture AS FacebookPicture,
                        instagram_id AS InstagramId,
                        instagram_id2 AS InstagramId2,
                        application AS Application,
                        instagram_name AS InstagramName,
                        instagram_picture AS InstagramPicture,
                        line_id AS LineId,
                        gender AS Gender,
                        id_on4 AS IdOn4,
                        other AS Other,
                        created_by AS CreatedBy,
                        created_at As CreatedAt 
                    FROM m_customer
                    WHERE id > @StartId
                    LIMIT @limit OFFSET @offset";

                            using var myCmd = new MySqlCommand(query, mySqlConn);
                            myCmd.Parameters.AddWithValue("@StartId", StartId);
                            myCmd.Parameters.AddWithValue("@limit", batchSize);
                            myCmd.Parameters.AddWithValue("@offset", batchNumber * batchSize);

                            using var reader = await myCmd.ExecuteReaderAsync();
                            while (await reader.ReadAsync())
                            {
                                customersBatch.Add(new CustomerModel
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    MemberId = reader["MemberId"] as string,
                                    Name = reader["Name"] as string,
                                    Address = reader["Address"] as string,
                                    Hp = reader["Hp"] as string,
                                    Email = reader["Email"] as string,
                                    TelegramId = reader["TelegramId"] as string,
                                    TwitterId = reader["TwitterId"] as string,
                                    TwitterUsername = reader["TwitterUsername"] as string,
                                    TwitterFollowers = reader["TwitterFollowers"] as int?,
                                    TwitterFollowing = reader["TwitterFollowing"] as int?,
                                    TwitterPicture = reader["TwitterPicture"] as string,
                                    TwitterName = reader["TwitterName"] as string,
                                    FacebookId = reader["FacebookId"] as string,
                                    FacebookName = reader["FacebookName"] as string,
                                    FacebookPicture = reader["FacebookPicture"] as string,
                                    InstagramId = reader["InstagramId"] as string,
                                    InstagramId2 = reader["InstagramId2"] as string,
                                    Application = reader["Application"] as string,
                                    InstagramName = reader["InstagramName"] as string,
                                    InstagramPicture = reader["InstagramPicture"] as string,
                                    LineId = reader["LineId"] as string,
                                    Gender = reader["Gender"] as string,
                                    IdOn4 = reader["IdOn4"]?.ToString(),
                                    Other = reader["Other"] as string,
                                    CreatedBy = reader["CreatedBy"] as string,
                                    CreatedDate = reader["CreatedAt"] as string
                                });
                            }
                        }

                        // Insert ke SQL Server, 1 row per loop
                        foreach (var item in customersBatch)
                        {
                            try
                            {
                                cmd.Parameters["@MemberId"].Value = (object)item.MemberId ?? DBNull.Value;
                                cmd.Parameters["@Name"].Value = (object)item.Name ?? DBNull.Value;
                                cmd.Parameters["@Address"].Value = (object)item.Address ?? DBNull.Value;
                                cmd.Parameters["@Hp"].Value = (object)item.Hp ?? DBNull.Value;
                                cmd.Parameters["@Email"].Value = (object)item.Email ?? DBNull.Value;
                                cmd.Parameters["@TelegramId"].Value = (object)item.TelegramId ?? DBNull.Value;
                                cmd.Parameters["@TelegramUsername"].Value = DBNull.Value;
                                cmd.Parameters["@TelegramName"].Value = DBNull.Value;
                                cmd.Parameters["@TwitterId"].Value = (object)item.TwitterId ?? DBNull.Value;
                                cmd.Parameters["@TwitterUsername"].Value = (object)item.TwitterUsername ?? DBNull.Value;
                                cmd.Parameters["@TwitterFollowers"].Value = (object)item.TwitterFollowers ?? DBNull.Value;
                                cmd.Parameters["@TwitterFollowing"].Value = (object)item.TwitterFollowing ?? DBNull.Value;
                                cmd.Parameters["@TwitterPicture"].Value = (object)item.TwitterPicture ?? DBNull.Value;
                                cmd.Parameters["@TwitterName"].Value = (object)item.TwitterName ?? DBNull.Value;
                                cmd.Parameters["@FacebookId"].Value = (object)item.FacebookId ?? DBNull.Value;
                                cmd.Parameters["@FacebookName"].Value = (object)item.FacebookName ?? DBNull.Value;
                                cmd.Parameters["@FacebookPicture"].Value = (object)item.FacebookPicture ?? DBNull.Value;
                                cmd.Parameters["@InstagramId"].Value = (object)item.InstagramId ?? DBNull.Value;
                                cmd.Parameters["@InstagramId2"].Value = (object)item.InstagramId2 ?? DBNull.Value;
                                cmd.Parameters["@Application"].Value = (object)item.Application ?? DBNull.Value;
                                cmd.Parameters["@InstagramName"].Value = (object)item.InstagramName ?? DBNull.Value;
                                cmd.Parameters["@InstagramPicture"].Value = (object)item.InstagramPicture ?? DBNull.Value;
                                cmd.Parameters["@LineId"].Value = (object)item.LineId ?? DBNull.Value;
                                cmd.Parameters["@Gender"].Value = (object)item.Gender ?? DBNull.Value;
                                cmd.Parameters["@IdOn4"].Value = (object)item.IdOn4 ?? DBNull.Value;
                                cmd.Parameters["@Other"].Value = (object)item.Other ?? DBNull.Value;
                                cmd.Parameters["@CreatedBy"].Value = (object)(item.CreatedBy ?? "migration");
                                cmd.Parameters["@CreatedAt"].Value = (object)item.CreatedDate ?? DBNull.Value;

                                await cmd.ExecuteNonQueryAsync();
                                success++;
                                logLines.Add($"INSERT DATA: {item.MemberId}, {item.Name}, {item.Address}, {item.Email} ➜ SUKSES");
                            }
                            catch (Exception ex)
                            {
                                failed++;
                                logLines.Add($"INSERT DATA: {item.MemberId}, {item.Name}, {item.Address}, {item.Email} ➜ GAGAL: {ex.Message}");
                            }
                        }
                    }
                }
            }

            logLines.Add($"=== TOTAL: SUKSES = {success}, GAGAL = {failed} ===");
            await System.IO.File.WriteAllLinesAsync(logFilePath, logLines);

            return Ok(new
            {
                message = "Proses arsip selesai.",
                total_data = total,
                inserted = success,
                failed = failed,
                log_path = logFilePath
            });
        }





        [HttpGet]
        [Route("GetDataCustomerOmnix")]
        public async Task<IActionResult> GetDataCustomerOmnix(int startId)
        {
            string connectionString = _configuration.GetConnectionString("CrmConnection");

             var customers = await _context.MCustomers.Where(x => x.Id > startId).ToListAsync(); // Ini masih dari MySQL pakai EF

      //      var customers = await _context.MCustomers
      //.Where(x => x.Id > 1320603)
      //.ToListAsync();




            if (customers == null || !customers.Any())
                return NotFound(new { message = "Data customer tidak ditemukan." });

            int success = 0;
            int failed = 0;
            int total = customers.Count;

            // ==== Tambahan: Siapkan log file ====
            string baseLogDir = @"C:\LogIntegrasi";
            string todayFolder = DateTime.UtcNow.ToString("yyyy-MM-dd");
            string fullLogPath = Path.Combine(baseLogDir, todayFolder);
            Directory.CreateDirectory(fullLogPath);

            string timestamp = DateTime.UtcNow.ToString("HHmmss");
            string logFilePath = Path.Combine(fullLogPath, $"Log_Omnix_{timestamp}.txt");

            var logLines = new List<string>();
            logLines.Add($"=== LOG MIGRASI CUSTOMER OMNIX {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC ===");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();

                foreach (var item in customers)
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_InsertCustomerIntegrasi", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Id", item.Id);
                            cmd.Parameters.AddWithValue("@MemberId", item.MemberId ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Name", item.Name ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Address", item.Address ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Hp", item.Hp ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Email", item.Email ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@TelegramId", item.TelegramId ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@TelegramUsername", DBNull.Value);
                            cmd.Parameters.AddWithValue("@TelegramName", DBNull.Value);
                            cmd.Parameters.AddWithValue("@TwitterId", item.TwitterId ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@TwitterUsername", item.TwitterUsername ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@TwitterFollowers", item.TwitterFollowers?.ToString() ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@TwitterFollowing", item.TwitterFollowing?.ToString() ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@TwitterPicture", item.TwitterPicture ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@TwitterName", item.TwitterName ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@FacebookId", item.FacebookId ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@FacebookName", item.FacebookName ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@FacebookPicture", item.FacebookPicture ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@InstagramId", item.InstagramId ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@InstagramId2", item.InstagramId2 ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Application", item.Application ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@InstagramName", item.InstagramName ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@InstagramPicture", item.InstagramPicture ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@LineId", item.LineId ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Gender", item.Gender ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@IdOn4", item.IdOn4?.ToString() ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Other", item.Other ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@CreatedBy", item.CreatedBy.ToString());
                            cmd.Parameters.AddWithValue("@CreatedAt", DateTime.UtcNow);

                            await cmd.ExecuteNonQueryAsync();
                            success++;

                            logLines.Add($"INSERT DATA: {item.MemberId}, {item.Name}, {item.Address}, {item.Email} ➜ SUKSES");
                        }
                    }
                    catch (Exception ex)
                    {
                        failed++;
                        logLines.Add($"INSERT DATA: {item.MemberId}, {item.Name}, {item.Address}, {item.Email} ➜ GAGAL: {ex.Message}");
                    }
                }
            }

            logLines.Add($"=== TOTAL: SUKSES = {success}, GAGAL = {failed} ===");

            await System.IO.File.WriteAllLinesAsync(logFilePath, logLines);

            return Ok(new
            {
                message = customers,
                total_data = total,
                inserted = success,
                failed = failed,
                log_path = logFilePath
            });
        }



       
      


        [HttpGet]
        [Route("GetDataCustomerOn4")]
        public async Task<IActionResult> GetDataCustomerOn412()
        {
            string mySqlConnStr = _configuration.GetConnectionString("On4Connection");   // MySQL
            string sqlServerConnStr = _configuration.GetConnectionString("CrmConnection"); // SQL Server

            const int batchSize = 1000;
            int success = 0;
            int failed = 0;
            int total = 0;

            // Buat folder log per hari
            string baseLogDir = @"C:\LogIntegrasi";
            string todayFolder = DateTime.UtcNow.ToString("yyyy-MM-dd");
            string fullLogPath = Path.Combine(baseLogDir, todayFolder);
            Directory.CreateDirectory(fullLogPath);

            string timestamp = DateTime.UtcNow.ToString("HHmmss");
            string logFilePath = Path.Combine(fullLogPath, $"Log_Insert_{timestamp}.txt");
            var logLines = new List<string>();
            logLines.Add($"=== LOG MIGRASI CUSTOMER {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC ===");

            // 1. Hitung total data MySQL
            using (var mySqlCountConn = new MySqlConnection(mySqlConnStr))
            {
                await mySqlCountConn.OpenAsync();
                using var countCmd = new MySqlCommand("SELECT COUNT(*) FROM m_customer", mySqlCountConn);
                total = Convert.ToInt32(await countCmd.ExecuteScalarAsync());
            }

            int totalBatches = (int)Math.Ceiling(total / (double)batchSize);

            using (var sqlServerConn = new SqlConnection(sqlServerConnStr))
            {
                await sqlServerConn.OpenAsync();

                for (int batchNumber = 0; batchNumber < totalBatches; batchNumber++)
                {
                    var customersBatch = new List<CustomerModel>();

                    // Ambil batch data dari MySQL
                    using (var mySqlConn = new MySqlConnection(mySqlConnStr))
                    {
                        await mySqlConn.OpenAsync();

                        string query = @"SELECT 
                        member_id AS MemberId,
                        name AS Name,
                        address AS Address,
                        hp AS Hp,
                        email AS Email,
                        telegram_id AS TelegramId,
                        twitter_id AS TwitterId,
                        twitter_username AS TwitterUsername,
                        twitter_followers AS TwitterFollowers,
                        twitter_following AS TwitterFollowing,
                        twitter_picture AS TwitterPicture,
                        twitter_name AS TwitterName,
                        facebook_id AS FacebookId,
                        facebook_name AS FacebookName,
                        facebook_picture AS FacebookPicture,
                        instagram_id AS InstagramId,
                        instagram_id2 AS InstagramId2,
                        application AS Application,
                        instagram_name AS InstagramName,
                        instagram_picture AS InstagramPicture,
                        line_id AS LineId,
                        gender AS Gender,
                        id_on4 AS IdOn4,
                        other AS Other,
                        created_by AS CreatedBy
                    FROM m_customer
                    ORDER BY member_id
                    LIMIT @limit OFFSET @offset";

                        using var cmd = new MySqlCommand(query, mySqlConn);
                        cmd.Parameters.AddWithValue("@limit", batchSize);
                        cmd.Parameters.AddWithValue("@offset", batchNumber * batchSize);

                        using var reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            customersBatch.Add(new CustomerModel
                            {
                                MemberId = reader["MemberId"] as string,
                                Name = reader["Name"] as string,
                                Address = reader["Address"] as string,
                                Hp = reader["Hp"] as string,
                                Email = reader["Email"] as string,
                                TelegramId = reader["TelegramId"] as string,
                                TwitterId = reader["TwitterId"] as string,
                                TwitterUsername = reader["TwitterUsername"] as string,
                                TwitterFollowers = reader["TwitterFollowers"] as int?,
                                TwitterFollowing = reader["TwitterFollowing"] as int?,
                                TwitterPicture = reader["TwitterPicture"] as string,
                                TwitterName = reader["TwitterName"] as string,
                                FacebookId = reader["FacebookId"] as string,
                                FacebookName = reader["FacebookName"] as string,
                                FacebookPicture = reader["FacebookPicture"] as string,
                                InstagramId = reader["InstagramId"] as string,
                                InstagramId2 = reader["InstagramId2"] as string,
                                Application = reader["Application"] as string,
                                InstagramName = reader["InstagramName"] as string,
                                InstagramPicture = reader["InstagramPicture"] as string,
                                LineId = reader["LineId"] as string,
                                Gender = reader["Gender"] as string,
                                IdOn4 = reader["IdOn4"]?.ToString(),
                                Other = reader["Other"] as string,
                                CreatedBy = reader["CreatedBy"] as string,
                            });
                        }
                    }

                    // Insert ke SQL Server
                    foreach (var item in customersBatch)
                    {
                        try
                        {
                            using (var cmd = new SqlCommand("sp_InsertCustomerIntegrasi", sqlServerConn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@MemberId", item.MemberId ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@Name", item.Name ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@Address", item.Address ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@Hp", item.Hp ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@Email", item.Email ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@TelegramId", item.TelegramId ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@TelegramUsername", DBNull.Value);
                                cmd.Parameters.AddWithValue("@TelegramName", DBNull.Value);
                                cmd.Parameters.AddWithValue("@TwitterId", item.TwitterId ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@TwitterUsername", item.TwitterUsername ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@TwitterFollowers", item.TwitterFollowers?.ToString() ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@TwitterFollowing", item.TwitterFollowing?.ToString() ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@TwitterPicture", item.TwitterPicture ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@TwitterName", item.TwitterName ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@FacebookId", item.FacebookId ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@FacebookName", item.FacebookName ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@FacebookPicture", item.FacebookPicture ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@InstagramId", item.InstagramId ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@InstagramId2", item.InstagramId2 ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@Application", item.Application ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@InstagramName", item.InstagramName ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@InstagramPicture", item.InstagramPicture ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@LineId", item.LineId ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@Gender", item.Gender ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@IdOn4", item.IdOn4?.ToString() ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@Other", item.Other ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@CreatedBy", item.CreatedBy?.ToString() ?? "migration");
                                cmd.Parameters.AddWithValue("@CreatedAt", DateTime.UtcNow);

                                await cmd.ExecuteNonQueryAsync();
                                success++;

                                logLines.Add($"INSERT DATA: {item.MemberId}, {item.Name}, {item.Address}, {item.Email} ➜ SUKSES");
                            }
                        }
                        catch (Exception ex)
                        {
                            failed++;
                            logLines.Add($"INSERT DATA: {item.MemberId}, {item.Name}, {item.Address}, {item.Email} ➜ GAGAL: {ex.Message}");
                        }
                    }
                }
            }

            logLines.Add($"=== TOTAL: SUKSES = {success}, GAGAL = {failed} ===");
            await System.IO.File.WriteAllLinesAsync(logFilePath, logLines); 
            return Ok(new
            {
                message = "Proses arsip selesai.",
                total_data = total,
                inserted = success,
                failed = failed,
                log_path = logFilePath
            });
        }

    }


}

public class CustomerModel
{
    public int Id { get; set; }
    public string MemberId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Hp { get; set; }
    public string Email { get; set; }
    public string TelegramId { get; set; }
    public string TwitterId { get; set; }
    public string TwitterUsername { get; set; }
    public int? TwitterFollowers { get; set; }
    public int? TwitterFollowing { get; set; }
    public string TwitterPicture { get; set; }
    public string TwitterName { get; set; }
    public string FacebookId { get; set; }
    public string FacebookName { get; set; }
    public string FacebookPicture { get; set; }
    public string InstagramId { get; set; }
    public string InstagramId2 { get; set; }
    public string Application { get; set; }
    public string InstagramName { get; set; }
    public string InstagramPicture { get; set; }
    public string LineId { get; set; }
    public string Gender { get; set; }
    public string IdOn4 { get; set; }
    public string Other { get; set; }
    public string CreatedBy { get; set; }
    public string CreatedDate { get; set; }
}

public class dataTenant
{
    public int ID { get; set; }
    public string TenantID { get; set; }
    public string DomainAPI { get; set; }
    public string URLtoken { get; set; }
    public List<string> URLtoken_Body { get; set; }
    public List<string> URLtoken_ResponseSuc { get; set; }
    public string URLtoken_Expired { get; set; }
    public List<string> URLtoken_ResponseErr { get; set; }
    public string URLprofile { get; set; }
    public string URLprofile_Body { get; set; }
    public URLprofileResponseSuc URLprofile_ResponseSuc { get; set; }
    public List<string> URLprofile_ResponseErr { get; set; }
    public string ActiveTenant { get; set; }
}

public class URLprofileResponseSuc
{
    public bool success { get; set; }
    public UserData data { get; set; }
}

public class UserData
{
    public string id { get; set; }
    public string mobileNumber { get; set; }
    public Address address { get; set; }
}

public class Address
{
    public string province { get; set; }
    public string district { get; set; }
    public string subDistrict { get; set; }
    public string village { get; set; }
    public string street { get; set; }
    public string zipCode { get; set; }
}

