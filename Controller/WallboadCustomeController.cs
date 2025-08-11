using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using WEBAPI_Bravo.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Org.BouncyCastle.Asn1.X509;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using WEBAPI_Bravo.Services;

namespace WEBAPI_Bravo.Controller
{


    [Route("api/[controller]")]
    [ApiController]
    public class WallboadCustomeController : ControllerBase
    {


        private readonly sqlContext _context;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString2;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly iDetailServices _detailServices;


        public WallboadCustomeController(sqlContext context, IConfiguration configuration, IHttpClientFactory httpClientFactory, iDetailServices DetailServices)
        {
            _context = context;
            _configuration = configuration;
            _connectionString2 = _configuration.GetConnectionString("DefaultConnection2");
            _httpClientFactory = httpClientFactory;
            _detailServices = DetailServices;

        }


      

   
        [HttpGet("detail-data")]
        public IActionResult GetDetailData123(string Tenant, string Channel)
        {

            string localDirectory = @"E:\DataAvaya";

            string FilePath = Path.Combine(localDirectory, "DataIdle_All.txt");
            if (!System.IO.File.Exists(FilePath))
            {
                return NotFound("File not found.");
            }

            var data = _detailServices.ReadDataFromTxt(FilePath);
            return Ok(data);
        }

   

        [HttpGet("GetDataSosmedCurrentMonth")]
        public async Task<List<datas>> GetDataSosmedCurrentMonth(string Tenant)
        {
            var users = new List<datas>();
            string skill = string.Empty;

            // var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var crmConnectionString = _configuration.GetConnectionString("CrmConnection");

            using (var conn = new SqlConnection(crmConnectionString))
            using (var cmd = new SqlCommand("GetSkillByChannelAndUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Channel", "Social Media");
                cmd.Parameters.AddWithValue("@Users", Tenant);
                conn.Open();
                var result = await cmd.ExecuteScalarAsync();
                skill = result?.ToString();
            }

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataSosmedCurrentMonth @Tenant,@Skill";
                        var ParameterStart = new SqlParameter("@Tenant", Tenant);
                        var ParameterEnd = new SqlParameter("@Skill", skill);

                        command.Parameters.Add(ParameterStart);
                        command.Parameters.Add(ParameterEnd);
                        command.CommandType = CommandType.Text;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new datas
                                {
                                    Name = result.GetString(0),
                                    Jumlah = result.GetDecimal(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"An error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return users;
        }

        [HttpGet("GetDataVideoCurrentMonth")]
        public async Task<List<datas>> GetDataVideoCurrentMonth(string Tenant)
        {
            var users = new List<datas>();
            string skill = string.Empty;

            // var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var crmConnectionString = _configuration.GetConnectionString("CrmConnection");

            using (var conn = new SqlConnection(crmConnectionString))
            using (var cmd = new SqlCommand("GetSkillByChannelAndUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Channel", "Video Call");
                cmd.Parameters.AddWithValue("@Users", Tenant);
                conn.Open();
                var result = await cmd.ExecuteScalarAsync();
                skill = result?.ToString();
            }

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataVideoCurrentMonth @Tenant,@Skill";
                        var ParameterStart = new SqlParameter("@Tenant", Tenant);
                        var ParameterEnd = new SqlParameter("@Skill", skill);

                        command.Parameters.Add(ParameterStart);
                        command.Parameters.Add(ParameterEnd);
                        command.CommandType = CommandType.Text;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new datas
                                {
                                    Name = result.GetString(0),
                                    Jumlah = result.GetDecimal(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"An error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return users;
        }


        [HttpGet("GetDataSosmedMonth")]
        public async Task<List<datas>> GetDataSosmedMonth(string startDate, string EndDate)
        {
            var users = new List<datas>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataSosmedMonth @p_startdate,@p_enddate";
                        var ParameterStart = new SqlParameter("@p_startdate", startDate);
                        var ParameterEnd = new SqlParameter("@p_enddate", EndDate);

                        command.Parameters.Add(ParameterStart);
                        command.Parameters.Add(ParameterEnd);
                        command.CommandType = CommandType.Text;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new datas
                                {
                                    Name = result.GetString(0),
                                    Jumlah = result.GetDecimal(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"An error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return users;
        }

        [HttpGet("GetDataVideoMonth")]
        public async Task<List<datas>> GetDataVideoMonth(string startDate, string EndDate)
        {
            var users = new List<datas>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataSosmedMonth @p_startdate,@p_enddate";
                        var ParameterStart = new SqlParameter("@p_startdate", startDate);
                        var ParameterEnd = new SqlParameter("@p_enddate", EndDate);

                        command.Parameters.Add(ParameterStart);
                        command.Parameters.Add(ParameterEnd);
                        command.CommandType = CommandType.Text;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new datas
                                {
                                    Name = result.GetString(0),
                                    Jumlah = result.GetDecimal(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"An error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return users;
        }
        [HttpGet("get-customer-by-mobile")]
        public async Task<IActionResult> GetCustomerByMobile()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var url = "https://boostapi.kanmogroup.com/api/CRMCap/V1/GetCallCenterCustomerDataByMobile?custMobile=628129144531&formatData=json";
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "<TOKEN_KAMU>");
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.SendAsync(request);

                if (response == null)
                    return StatusCode(500, "Response is null");

                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, $"API call failed: {response.StatusCode}, Content: {content}");

                return Ok(content);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Exception: {ex.ToString()}"); // Penting untuk debugging
            }
        }


        [HttpGet("GetDataEmailMonth")]
        public async Task<List<datas>> GetDataEmailMonth(string Tenant)
        {
            var users = new List<datas>();
            string skill = string.Empty;

            // var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var crmConnectionString = _configuration.GetConnectionString("CrmConnection");

            using (var conn = new SqlConnection(crmConnectionString))
            using (var cmd = new SqlCommand("GetSkillByChannelAndUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Channel", "Email");
                cmd.Parameters.AddWithValue("@Users", Tenant);
                conn.Open();
                var result = await cmd.ExecuteScalarAsync();
                skill = result?.ToString();
            }

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataEmailMonth @Tenant,@Skill";
                        var ParameterStart = new SqlParameter("@Tenant", Tenant);
                        var ParameterEnd = new SqlParameter("@Skill", skill);

                        command.Parameters.Add(ParameterStart);
                        command.Parameters.Add(ParameterEnd);
                        command.CommandType = CommandType.Text;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new datas
                                {
                                    Name = result.GetString(0),
                                    Jumlah = result.GetDecimal(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"An error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return users;
        }
        [HttpGet("GetDataEmail1")]
        public async Task<List<datas>> GetDataEmail1(string Tenant)
        {
            var users = new List<datas>();

            string skill = string.Empty;

            // var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var crmConnectionString = _configuration.GetConnectionString("CrmConnection");

            using (var conn = new SqlConnection(crmConnectionString))
            using (var cmd = new SqlCommand("GetSkillByChannelAndUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Channel", "Email");
                cmd.Parameters.AddWithValue("@Users", Tenant);
                conn.Open();
                var result = await cmd.ExecuteScalarAsync();
                skill = result?.ToString();
            }


            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC  GetDataEmail1 @Tenant,@Skill";
                        var ParameterStart = new SqlParameter("@Tenant", Tenant);
                        var ParameterEnd = new SqlParameter("@Skill", skill);

                        command.Parameters.Add(ParameterStart);
                        command.Parameters.Add(ParameterEnd);
                        command.CommandType = CommandType.Text;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new datas
                                {
                                    Name = result.GetString(0),
                                    Jumlah = result.GetDecimal(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"An error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return users;
        }

        //public async Task<IActionResult> GetDataEmail1(string Tenant)
        //{
        //    try
        //    {
        //        string skill = string.Empty;

        //        // var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
        //        var crmConnectionString = _configuration.GetConnectionString("CrmConnection");

        //        using (var conn = new SqlConnection(crmConnectionString))
        //        using (var cmd = new SqlCommand("GetSkillByChannelAndUser", conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@Channel", "Email");
        //            cmd.Parameters.AddWithValue("@Users", Tenant);
        //            conn.Open();
        //            var result = await cmd.ExecuteScalarAsync();
        //            skill = result?.ToString();
        //        }

        //        Console.WriteLine($"Skill: {skill}");

        //        string localDirectory = @"E:\DataAvaya";
        //        string NameFile = "VOICE_TODAY_ALL.txt";
        //        string FilePath = Path.Combine(localDirectory, NameFile);

        //        if (!System.IO.File.Exists(FilePath))
        //        {
        //            return NotFound("File not found.");
        //        }

        //        var data = await _detailServices.ReadDataTodayFromFile(FilePath, skill);

        //        return Ok(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log error jika perlu
        //        Console.WriteLine($"Error: {ex.Message}");

        //        return StatusCode(500, new
        //        {
        //            status = "error",
        //            message = "Terjadi kesalahan saat memproses permintaan.",
        //            detail = ex.Message
        //        });
        //    }

        //}

        [HttpGet("GetDataEmailWaiting")]
        public async Task<List<datas>> GetDataEmailWaiting()
        {
            var users = new List<datas>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataEmailWaiting";
                        //var ParameterStart = new SqlParameter("@p_startdate", startDate);
                        //var ParameterEnd = new SqlParameter("@p_enddate", EndDate);

                        //command.Parameters.Add(ParameterStart);
                        //command.Parameters.Add(ParameterEnd);
                        command.CommandType = CommandType.Text;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new datas
                                {
                                    Name = result.GetString(0)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"An error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return users;
        }

        [HttpGet("GetDataEmailIntervalHari")]
        public async Task<List<dataInteractionDay>> GetDataEmailIntervalHari(string Tenant)
        {
            var users = new List<dataInteractionDay>();
            string skill = string.Empty;

            // var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var crmConnectionString = _configuration.GetConnectionString("CrmConnection");

            using (var conn = new SqlConnection(crmConnectionString))
            using (var cmd = new SqlCommand("GetSkillByChannelAndUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Channel", "Email");
                cmd.Parameters.AddWithValue("@Users", Tenant);
                conn.Open();
                var result = await cmd.ExecuteScalarAsync();
                skill = result?.ToString();
            }
            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataEmailIntervalHari @Tenant,@Skill";
                        var ParameterStart = new SqlParameter("@Tenant", Tenant);
                        var ParameterEnd = new SqlParameter("@Skill", skill);

                        command.Parameters.Add(ParameterStart);
                        command.Parameters.Add(ParameterEnd);
                        command.CommandType = CommandType.Text;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new dataInteractionDay
                                {
                                    Name = result.GetString(0),
                                    DateInteraction = result.GetString(1),
                                    Jumlah = result.GetDecimal(2),
                                    Aht = result.GetDecimal(3)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)

            {
                throw new Exception($"An error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return users;
        }

        [HttpGet("GetDataLivechatMonth")]
        public async Task<List<datas>> GetDataLivechatMonth(string Tenant)
        {
            var users = new List<datas>();
            string skill = string.Empty;

            // var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var crmConnectionString = _configuration.GetConnectionString("CrmConnection");

            using (var conn = new SqlConnection(crmConnectionString))
            using (var cmd = new SqlCommand("GetSkillByChannelAndUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Channel", "Live Chat");
                cmd.Parameters.AddWithValue("@Users", Tenant);
                conn.Open();
                var result = await cmd.ExecuteScalarAsync();
                skill = result?.ToString();
            }

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataLivechatMonth @Tenant,@Skill";
                        var ParameterStart = new SqlParameter("@Tenant", Tenant);
                        var ParameterEnd = new SqlParameter("@Skill", skill);

                        command.Parameters.Add(ParameterStart);
                        command.Parameters.Add(ParameterEnd);
                        command.CommandType = CommandType.Text;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new datas
                                {
                                    Name = result.GetString(0),
                                    // DateInteraction = result.GetString(1),
                                    Jumlah = result.GetDecimal(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"An error occurred: {sqlEx.Message}");
                // Console.WriteLine($"SQL error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}");
                //    Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return users;
        }

        [HttpGet("GetDataEmailInterval")]
        public async Task<List<dataInteraction>> GetDataEmailInterval(string Tenant)
        {
            var users = new List<dataInteraction>();
            string skill = string.Empty;

            // var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var crmConnectionString = _configuration.GetConnectionString("CrmConnection");

            using (var conn = new SqlConnection(crmConnectionString))
            using (var cmd = new SqlCommand("GetSkillByChannelAndUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Channel", "Email");
                cmd.Parameters.AddWithValue("@Users", Tenant);
                conn.Open();
                var result = await cmd.ExecuteScalarAsync();
                skill = result?.ToString();
            }

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataEmailInterval @Tenant,@Skill";
                        var ParameterStart = new SqlParameter("@Tenant", Tenant);
                        var ParameterEnd = new SqlParameter("@Skill", skill);

                        command.Parameters.Add(ParameterStart);
                        command.Parameters.Add(ParameterEnd);
                        command.CommandType = CommandType.Text;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new dataInteraction
                                {
                                    Name = result.GetString(0),
                                    DateInteraction = result.GetDateTime(1).ToString(),
                                    Jumlah = result.GetDecimal(2).ToString(),
                                    Aht = result.GetDecimal(3).ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"An error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return users;
        }


        [HttpGet("GetDataSosmedInterval")]
        public async Task<List<dataInteraction>> GetDataSosmedInterval(string Tenant)
        {
            var users = new List<dataInteraction>();
            string skill = string.Empty;

            // var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var crmConnectionString = _configuration.GetConnectionString("CrmConnection");

            using (var conn = new SqlConnection(crmConnectionString))
            using (var cmd = new SqlCommand("GetSkillByChannelAndUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Channel", "Social Media");
                cmd.Parameters.AddWithValue("@Users", Tenant);
                conn.Open();
                var result = await cmd.ExecuteScalarAsync();
                skill = result?.ToString();
            }


            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataSosmedInterval @Tenant,@Skill";
                        var ParameterStart = new SqlParameter("@Tenant", Tenant);
                        var ParameterEnd = new SqlParameter("@Skill", skill);

                        command.Parameters.Add(ParameterStart);
                        command.Parameters.Add(ParameterEnd);
                        command.CommandType = CommandType.Text;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new dataInteraction
                                {
                                    Name = result.GetString(0),
                                    DateInteraction = result.GetDateTime(1).ToString(),
                                    Jumlah = result.GetDecimal(2).ToString(),
                                    Aht = result.GetDecimal(3).ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"An error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return users;
        }


        [HttpGet("GetDataVideoInterval")]
        public async Task<List<dataInteraction>> GetDataVideoInterval(string Tenant)
        {
            var users = new List<dataInteraction>();
            string skill = string.Empty;

            // var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var crmConnectionString = _configuration.GetConnectionString("CrmConnection");

            using (var conn = new SqlConnection(crmConnectionString))
            using (var cmd = new SqlCommand("GetSkillByChannelAndUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Channel", "Video Call");
                cmd.Parameters.AddWithValue("@Users", Tenant);
                conn.Open();
                var result = await cmd.ExecuteScalarAsync();
                skill = result?.ToString();
            }


            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataVideoInterval @Tenant,@Skill";
                        var ParameterStart = new SqlParameter("@Tenant", Tenant);
                        var ParameterEnd = new SqlParameter("@Skill", skill);

                        command.Parameters.Add(ParameterStart);
                        command.Parameters.Add(ParameterEnd);
                        command.CommandType = CommandType.Text;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new dataInteraction
                                {
                                    Name = result.GetString(0),
                                    DateInteraction = result.GetDateTime(1).ToString(),
                                    Jumlah = result.GetDecimal(2).ToString(),
                                    Aht = result.GetDecimal(3).ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"An error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return users;
        }

        [HttpGet("GetDataLiveChatInterval")]
        public async Task<List<dataInteraction>> GetDataLiveChatInterval(string Tenant)
        {
            var users = new List<dataInteraction>();
            string skill = string.Empty;

            // var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var crmConnectionString = _configuration.GetConnectionString("CrmConnection");

            using (var conn = new SqlConnection(crmConnectionString))
            using (var cmd = new SqlCommand("GetSkillByChannelAndUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Channel", "Live Chat");
                cmd.Parameters.AddWithValue("@Users", Tenant);
                conn.Open();
                var result = await cmd.ExecuteScalarAsync();
                skill = result?.ToString();
            }


            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataLiveChatInterval @Tenant,@Skill";
                        var ParameterStart = new SqlParameter("@Tenant", Tenant);
                        var ParameterEnd = new SqlParameter("@Skill", skill);

                        command.Parameters.Add(ParameterStart);
                        command.Parameters.Add(ParameterEnd);
                        command.CommandType = CommandType.Text;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new dataInteraction
                                {
                                    Name = result.GetString(0),
                                    DateInteraction = result.GetDateTime(1).ToString(),
                                    Jumlah = result.GetDecimal(2).ToString(),
                                    Aht = result.GetDecimal(3).ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"An error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return users;
        }

        [HttpGet("GetDataSosmedIntervalHari")]
        public async Task<List<dataInteraction>> GetDataSosmedIntervalHari(string Tenant)
        {
            var users = new List<dataInteraction>();

            string skill = string.Empty;

            // var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var crmConnectionString = _configuration.GetConnectionString("CrmConnection");

            using (var conn = new SqlConnection(crmConnectionString))
            using (var cmd = new SqlCommand("GetSkillByChannelAndUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Channel", "Social Media");
                cmd.Parameters.AddWithValue("@Users", Tenant);
                conn.Open();
                var result = await cmd.ExecuteScalarAsync();
                skill = result?.ToString();
            }

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataSosmedIntervalHari @Tenant,@Skill";
                        var ParameterStart = new SqlParameter("@Tenant", Tenant);
                        var ParameterEnd = new SqlParameter("@Skill", skill);

                        command.Parameters.Add(ParameterStart);
                        command.Parameters.Add(ParameterEnd);
                        command.CommandType = CommandType.Text;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new dataInteraction
                                {
                                    Name = result.GetString(0),
                                    DateInteraction = result.GetString(1),
                                    Jumlah = result.GetDecimal(2).ToString(),
                                    Aht = result.GetDecimal(3).ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"An error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return users;
        }
        [HttpGet("GetDataVideoIntervalHari")]
        public async Task<List<dataInteraction>> GetDataVideoIntervalHari(string Tenant)
        {
            var users = new List<dataInteraction>();

            string skill = string.Empty;

            // var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var crmConnectionString = _configuration.GetConnectionString("CrmConnection");

            using (var conn = new SqlConnection(crmConnectionString))
            using (var cmd = new SqlCommand("GetSkillByChannelAndUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Channel", "Video Call");
                cmd.Parameters.AddWithValue("@Users", Tenant);
                conn.Open();
                var result = await cmd.ExecuteScalarAsync();
                skill = result?.ToString();
            }

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataVideoIntervalHari @Tenant,@Skill";
                        var ParameterStart = new SqlParameter("@Tenant", Tenant);
                        var ParameterEnd = new SqlParameter("@Skill", skill);

                        command.Parameters.Add(ParameterStart);
                        command.Parameters.Add(ParameterEnd);
                        command.CommandType = CommandType.Text;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new dataInteraction
                                {
                                    Name = result.GetString(0),
                                    DateInteraction = result.GetString(1),
                                    Jumlah = result.GetDecimal(2).ToString(),
                                    Aht = result.GetDecimal(2).ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"An error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return users;
        }

        [HttpGet("GetDataLiveChatIntervalHari")]
        public async Task<List<dataInteraction>> GetDataLiveChatIntervalHari(string Tenant)
        {
            var users = new List<dataInteraction>();

            string skill = string.Empty;

            // var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var crmConnectionString = _configuration.GetConnectionString("CrmConnection");

            using (var conn = new SqlConnection(crmConnectionString))
            using (var cmd = new SqlCommand("GetSkillByChannelAndUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Channel", "Live Chat");
                cmd.Parameters.AddWithValue("@Users", Tenant);
                conn.Open();
                var result = await cmd.ExecuteScalarAsync();
                skill = result?.ToString();
            }

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataLiveChatIntervalHari @Tenant,@Skill";
                        var ParameterStart = new SqlParameter("@Tenant", Tenant);
                        var ParameterEnd = new SqlParameter("@Skill", skill);

                        command.Parameters.Add(ParameterStart);
                        command.Parameters.Add(ParameterEnd);
                        command.CommandType = CommandType.Text;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new dataInteraction
                                {
                                    Name = result.GetString(0),
                                    DateInteraction = result.GetString(1),
                                    Jumlah = result.GetDecimal(2).ToString(),
                                    Aht = result.GetDecimal(3).ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"An error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return users;
        }

        //[HttpGet("GetDataSosmed1")]
        //public async Task<IActionResult> GetDataSosmed1(string Tenant)
        //{
        //    try
        //    {
        //        string skill = string.Empty;

        //        // var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
        //        var crmConnectionString = _configuration.GetConnectionString("CrmConnection");

        //        using (var conn = new SqlConnection(crmConnectionString))
        //        using (var cmd = new SqlCommand("GetSkillByChannelAndUser", conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@Channel", "Social Media");
        //            cmd.Parameters.AddWithValue("@Users", Tenant);
        //            conn.Open();
        //            var result = await cmd.ExecuteScalarAsync();
        //            skill = result?.ToString();
        //        }

        //        Console.WriteLine($"Skill: {skill}");

        //        string localDirectory = @"E:\DataAvaya";
        //        string NameFile = "VOICE_TODAY_ALL.txt";
        //        string FilePath = Path.Combine(localDirectory, NameFile);

        //        if (!System.IO.File.Exists(FilePath))
        //        {
        //            return NotFound("File not found.");
        //        }

        //        var data = await _detailServices.ReadDataTodayFromFile(FilePath, skill);

        //        return Ok(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log error jika perlu
        //        Console.WriteLine($"Error: {ex.Message}");

        //        return StatusCode(500, new
        //        {
        //            status = "error",
        //            message = "Terjadi kesalahan saat memproses permintaan.",
        //            detail = ex.Message
        //        });
        //    }
        //}
        [HttpGet("GetDataSosmed1")]
        public async Task<List<datas>> GetDataSosmed1(string Tenant)
        {
            var users = new List<datas>();

            string skill = string.Empty;

            // var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var crmConnectionString = _configuration.GetConnectionString("CrmConnection");

            using (var conn = new SqlConnection(crmConnectionString))
            using (var cmd = new SqlCommand("GetSkillByChannelAndUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Channel", "Social Media");
                cmd.Parameters.AddWithValue("@Users", Tenant);
                conn.Open();
                var result = await cmd.ExecuteScalarAsync();
                skill = result?.ToString();
            }


            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC  GetDataSosmed1 @Tenant,@Skill";
                        var ParameterStart = new SqlParameter("@Tenant", Tenant);
                        var ParameterEnd = new SqlParameter("@Skill", skill);

                        command.Parameters.Add(ParameterStart);
                        command.Parameters.Add(ParameterEnd);
                        command.CommandType = CommandType.Text;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new datas
                                {
                                    Name = result.GetString(0),
                                    Jumlah = result.GetDecimal(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"An error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return users;
        }
        [HttpGet("GetDataVideo1")]
        public async Task<List<datas>> GetDataVideo1(string Tenant)
        {
            var users = new List<datas>();

            string skill = string.Empty;

            // var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var crmConnectionString = _configuration.GetConnectionString("CrmConnection");

            using (var conn = new SqlConnection(crmConnectionString))
            using (var cmd = new SqlCommand("GetSkillByChannelAndUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Channel", "Video Call");
                cmd.Parameters.AddWithValue("@Users", Tenant);
                conn.Open();
                var result = await cmd.ExecuteScalarAsync();
                skill = result?.ToString();
            }


            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC  GetDataVideo1 @Tenant,@Skill";
                        var ParameterStart = new SqlParameter("@Tenant", Tenant);
                        var ParameterEnd = new SqlParameter("@Skill", skill);

                        command.Parameters.Add(ParameterStart);
                        command.Parameters.Add(ParameterEnd);
                        command.CommandType = CommandType.Text;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new datas
                                {
                                    Name = result.GetString(0),
                                    Jumlah = result.GetDecimal(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"An error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return users;
        }
        [HttpGet("GetDataLivechat1")]

        public async Task<List<datas>> GetDataLivechat1(string Tenant)
        {
            var users = new List<datas>();

            string skill = string.Empty;

            // var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var crmConnectionString = _configuration.GetConnectionString("CrmConnection");

            using (var conn = new SqlConnection(crmConnectionString))
            using (var cmd = new SqlCommand("GetSkillByChannelAndUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Channel", "Live Chat");
                cmd.Parameters.AddWithValue("@Users", Tenant);
                conn.Open();
                var result = await cmd.ExecuteScalarAsync();
                skill = result?.ToString();
            }


            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC  GetDataLivechat1 @Tenant,@Skill";
                        var ParameterStart = new SqlParameter("@Tenant", Tenant);
                        var ParameterEnd = new SqlParameter("@Skill", skill);

                        command.Parameters.Add(ParameterStart);
                        command.Parameters.Add(ParameterEnd);
                        command.CommandType = CommandType.Text;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new datas
                                {
                                    Name = result.GetString(0),
                                    Jumlah = result.GetDecimal(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"An error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return users;
        }
        //public async Task<IActionResult> GetDataLivechat1(string Tenant)
        //{
        //    try
        //    {
        //        string skill = string.Empty;

        //        // var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
        //        var crmConnectionString = _configuration.GetConnectionString("CrmConnection");

        //        using (var conn = new SqlConnection(crmConnectionString))
        //        using (var cmd = new SqlCommand("GetSkillByChannelAndUser", conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@Channel", "Live Chat");
        //            cmd.Parameters.AddWithValue("@Users", Tenant);
        //            conn.Open();
        //            var result = await cmd.ExecuteScalarAsync();
        //            skill = result?.ToString();
        //        }

        //        Console.WriteLine($"Skill: {skill}");

        //        string localDirectory = @"E:\DataAvaya";
        //        string NameFile = "VOICE_TODAY_ALL.txt";
        //        string FilePath = Path.Combine(localDirectory, NameFile);

        //        if (!System.IO.File.Exists(FilePath))
        //        {
        //            return NotFound("File not found.");
        //        }

        //        var data = await _detailServices.ReadDataTodayFromFile(FilePath, skill);

        //        return Ok(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log error jika perlu
        //        Console.WriteLine($"Error: {ex.Message}");

        //        return StatusCode(500, new
        //        {
        //            status = "error",
        //            message = "Terjadi kesalahan saat memproses permintaan.",
        //            detail = ex.Message
        //        });
        //    }

        //}



        [HttpGet("GetDataWaiting")]
        public async Task<List<datas>> GetDataWaiting()
        {
            var users = new List<datas>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataWaiting";
                        //var ParameterStart = new SqlParameter("@p_startdate", startDate);
                        //var ParameterEnd = new SqlParameter("@p_enddate", EndDate);

                        //command.Parameters.Add(ParameterStart);
                        //command.Parameters.Add(ParameterEnd);
                        command.CommandType = CommandType.Text;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new datas
                                {
                                    Name = result.GetString(0),
                                    Jumlah = result.GetDecimal(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"An error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return users;
        }





        //SELECT tInteraction.*, msUser.NAME, CONVERT(varchar, dbo.tInteraction.DateCreate, 13) AS TanggalInteraction FROM tInteraction

        //    LEFT OUTER JOIN msUser ON tInteraction.AgentCreate = msUser.USERNAME
        //WHERE tInteraction.TicketNumber= '20250312023004905' ORDER BY tInteraction.ID DESC
    }
}
public class datas
{
    public string Name { get; set; }
    public decimal Jumlah { get; set; }
}

public class dataInteraction
{
    public string Name { get; set; }
    public string DateInteraction { get; set; }
    public string Jumlah { get; set; }
    public string Aht { get; set; }
}
public class dataInteractionDay
{
    public string Name { get; set; }
    public string DateInteraction { get; set; }
    public decimal Jumlah { get; set; }
    public decimal Aht { get; set; }
}

public class SosmedReportResponse
{
    public SosmedReportValue value { get; set; }
    public List<object> formatters { get; set; } = new();
    public List<object> contentTypes { get; set; } = new();
    public object declaredType { get; set; } = null;
    public int statusCode { get; set; } = 200;
}
public class Interaction
{

    public string Description { get; set; }
    public string Status { get; set; }
    public string TicketNumber { get; set; }
    public string CaseOwner { get; set; }
    public string Name { get; set; }
    public string TanggalInteraction { get; set; }

}

public class SosmedReportValue
{
    public List<string> header { get; set; }
    public Dictionary<string, string> totals { get; set; }
    public List<object> data { get; set; } = new();
    public List<object> totalPerDate { get; set; } = new();
}