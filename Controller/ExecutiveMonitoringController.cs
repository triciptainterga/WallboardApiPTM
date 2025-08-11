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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using MySqlX.XDevAPI.Common;
using System.Net;


namespace WEBAPI_Bravo.Controller
{

    
        [Route("api/[controller]")]
        [ApiController]
        public class ExecutiveMonitoringController : ControllerBase
        {

        
            private readonly sqlContext _context;
            private readonly IConfiguration _configuration;
            private readonly string _connectionString2;
            private readonly string _AvayaconnectionString;
            private readonly string _CrmconnectionString;

        public ExecutiveMonitoringController(sqlContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
                _AvayaconnectionString = _configuration.GetConnectionString("AvayaConnection");
                _CrmconnectionString = _configuration.GetConnectionString("CrmConnection");
        }




        [HttpGet("GetDataInteraction")]
        public async Task<ActionResult<IEnumerable<object>>> GetDataInteraction(string startDate, string EndDate, string Tenant)
        {
            var ocmConnectionString = _configuration.GetConnectionString("DefaultConnection2");
            var result = new List<object>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ocmConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetDataMultichat", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@p_startdate", startDate));
                        command.Parameters.Add(new SqlParameter("@p_enddate", EndDate));
                        command.Parameters.Add(new SqlParameter("@Tenant", Tenant));

                        await conn.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync()) // Gunakan ReadAsync untuk async operation
                            {
                                result.Add(new
                                {
                                    Name = reader["Name"].ToString(),
                                    Jumlah = reader["Jumlah"].ToString()
                                });
                            }
                        }
                    }
                }

                return Ok(result);
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { message = "Database error occurred.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }


        [HttpGet("GetDataAnswer")]
        public async Task<IActionResult> GetDataAnswer(string startDate, string EndDate, string Tenant)
        {
            var avayaConnectionString = _configuration.GetConnectionString("AvayaConnection");
            var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var ocmConnectionString = _configuration.GetConnectionString("DefaultConnection2");

            int totalVoice = 0;
            int totalEmail = 0;
            int totalMultichat = 0;

            try
            {
                async Task<int> GetDataAsync(string connectionString, string procedureName, string startDateParam, string endDateParam, string TenantParam)
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = procedureName;
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter(startDateParam, startDate));
                            command.Parameters.Add(new SqlParameter(endDateParam, EndDate));
                            command.Parameters.Add(new SqlParameter(TenantParam, Tenant));

                            var result = await command.ExecuteScalarAsync();
                            return result != DBNull.Value ? Convert.ToInt32(result) : 0;
                        }

                    }
                }

                // Run database queries concurrently
                var taskMultichat = GetDataAsync(ocmConnectionString, "GetDataMultichatAns", "@p_startdate", "@p_enddate", "@Tenant");
                var taskEmail = GetDataAsync(ocmConnectionString, "GetDataEmailAns", "@p_startdate", "@p_enddate", "@Tenant");


                var results = await Task.WhenAll( taskEmail, taskMultichat);

                // Assign results
                totalEmail = results[0];
                totalMultichat = results[1];

            }
            catch (SqlException sqlEx)
            {
                return StatusCode(500, new { message = $"SQL Error: {sqlEx.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"General Error: {ex.Message}" });
            }

            // Return response in a structured format
            return Ok(new
            {
                TotalEmail = totalEmail,
                TotalMultichat = totalMultichat,
                GrandTotal =   totalEmail + totalMultichat
            });
        }



        [HttpGet("ChannelInteraktion")] 
        public async Task<ActionResult<IEnumerable<object>>> ChannelInteraktion(string startDate, string EndDate, string Tenant)
        {
            var ocmConnectionString = _configuration.GetConnectionString("DefaultConnection2");
            var result = new List<object>();

            using (SqlConnection conn = new SqlConnection(ocmConnectionString))
            {
                using (SqlCommand command = new SqlCommand("GetChannelInteraktion", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@p_startdate", startDate));
                    command.Parameters.Add(new SqlParameter("@p_enddate", EndDate));
                    command.Parameters.Add(new SqlParameter("@Tenant", Tenant));

                    conn.Open();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            result.Add(new
                            {
                                Name = reader["Name"].ToString(),
                                Jumlah = reader["Jumlah"].ToString()
                            });
                        }
                    }
                }
            }

            return Ok(result);
        }
        [HttpGet("CategoryInteraktionTicketDaily")] 
        public async Task<ActionResult<IEnumerable<object>>> CategoryInteraktionTicketDaily(string startDate, string EndDate, string Tenant, string Region)
        {
            var ocmConnectionString = _configuration.GetConnectionString("CrmConnection");
            var result = new List<object>();

            using (SqlConnection conn = new SqlConnection(ocmConnectionString))
            {
                using (SqlCommand command = new SqlCommand("CategoryInteraktionTicketDaily", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@p_startdate", startDate));
                    command.Parameters.Add(new SqlParameter("@p_enddate", EndDate));
                    command.Parameters.Add(new SqlParameter("@Tenant", Tenant));
                    command.Parameters.Add(new SqlParameter("@region", Region));

                    conn.Open();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            result.Add(new
                            {
                                Name = reader["Name"].ToString(),
                                Jumlah = reader["Jumlah"].ToString()
                            });
                        }
                    }
                }
            }

            return Ok(result);
        }  
        [HttpGet("CategoryInteraktionTicketByRegion")] 
        public async Task<ActionResult<IEnumerable<object>>> CategoryInteraktionTicketByRegion(string startDate, string EndDate, string Tenant,string region)
        {
            var ocmConnectionString = _configuration.GetConnectionString("CrmConnection");
            var result = new List<object>();

            using (SqlConnection conn = new SqlConnection(ocmConnectionString))
            {
                using (SqlCommand command = new SqlCommand("CategoryInteraktionTicketByRegion", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@p_startdate", startDate));
                    command.Parameters.Add(new SqlParameter("@p_enddate", EndDate));
                    command.Parameters.Add(new SqlParameter("@Tenant", Tenant));
                    command.Parameters.Add(new SqlParameter("@region", region));

                    conn.Open();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            result.Add(new
                            {
                                Name = reader["Name"].ToString(),
                                Jumlah = reader["Jumlah"].ToString()
                            });
                        }
                    }
                }
            }

            return Ok(result);
        } 
        [HttpGet("GetChannelInteraktionDetail")] 
        public async Task<ActionResult<IEnumerable<object>>> GetChannelInteraktionDetail(string startDate, string EndDate, string Tenant)
        {
            var ocmConnectionString = _configuration.GetConnectionString("DefaultConnection2");
            var result = new List<object>();

            using (SqlConnection conn = new SqlConnection(ocmConnectionString))
            {
                using (SqlCommand command = new SqlCommand("GetChannelInteraktionDetail", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@p_startdate", startDate));
                    command.Parameters.Add(new SqlParameter("@p_enddate", EndDate));
                    command.Parameters.Add(new SqlParameter("@Tenant", Tenant));

                    conn.Open();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            result.Add(new
                            {
                                Name = reader["Name"].ToString(),
                                Jumlah = reader["Jumlah"].ToString()
                            });
                        }
                    }
                }
            }

            return Ok(result);
        }
        [HttpGet("CategoriInteraktion")]
        public async Task<ActionResult<IEnumerable<object>>> CategoriInteraktion(string startDate, string EndDate, string Tenant,string region)
        {
            var ocmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var result = new List<object>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ocmConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("CategoryInteraktion", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@p_startdate", startDate));
                        command.Parameters.Add(new SqlParameter("@p_enddate", EndDate));
                        command.Parameters.Add(new SqlParameter("@Tenant", Tenant));
                        command.Parameters.Add(new SqlParameter("@params", region));

                        conn.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                result.Add(new
                                {
                                    Name = reader["Name"].ToString(),
                                    Jumlah = reader["Jumlah"].ToString()
                                });
                            }
                        }
                    }
                }
                return Ok(result);
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { message = "Database error occurred", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }

        }
        [HttpGet("CategoriInteraktionDetail")]
        public async Task<ActionResult<IEnumerable<object>>> CategoriInteraktionDetail(string startDate, string EndDate, string Tenant,string Params)
        {
            var ocmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var result = new List<object>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ocmConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("CategoriInteraktionDetail", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@p_startdate", startDate));
                        command.Parameters.Add(new SqlParameter("@p_enddate", EndDate));
                        command.Parameters.Add(new SqlParameter("@Tenant", Tenant));
                        command.Parameters.Add(new SqlParameter("@params", Params));

                        conn.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                result.Add(new
                                {
                                    Name = reader["Name"].ToString(),
                                    Jumlah = reader["Jumlah"].ToString()
                                });
                            }
                        }
                    }
                }
                return Ok(result);
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { message = "Database error occurred", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }

        }

        [HttpGet("Top5CategoryInteraktion")]
        public async Task<ActionResult<IEnumerable<object>>> Top5CategoryInteraktion(string startDate, string EndDate, string Tenant,string region )
        {
            var ocmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var result = new List<object>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ocmConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("Top5CategoryInteraktion", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@p_startdate", startDate));
                        command.Parameters.Add(new SqlParameter("@p_enddate", EndDate));
                        command.Parameters.Add(new SqlParameter("@Tenant", Tenant));
                        command.Parameters.Add(new SqlParameter("@region", region));

                        conn.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                result.Add(new
                                {
                                    Category = reader["Category"].ToString(),
                                    Name = reader["Name"].ToString(),
                                    Jumlah = reader["Percentage"].ToString()
                                });
                            }
                        }
                    }
                }
                return Ok(result);
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { message = "Database error occurred", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }

        }

        [HttpGet("Top10DailyReportInteraction")]
        public async Task<ActionResult<IEnumerable<object>>> Top10DailyReportInteraction(string startDate, string EndDate, string Tenant)
        {
            var ocmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var result = new List<object>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ocmConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("Top10DailyReportInteraction", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@p_startdate", startDate));
                        command.Parameters.Add(new SqlParameter("@p_enddate", EndDate));
                        command.Parameters.Add(new SqlParameter("@Tenant", Tenant));
                      
                        conn.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                result.Add(new
                                {
                                    Type = reader["type"].ToString(),
                                    Name = reader["Name"].ToString(),
                                    Jumlah = reader["Jumlah"].ToString()
                                });
                            }
                        }
                    }
                }
                return Ok(result);
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { message = "Database error occurred", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }

        }
        [HttpGet("CountDailyReportInteraction")]
        public async Task<ActionResult<IEnumerable<object>>> CountDailyReportInteraction(string startDate, string EndDate, string Tenant)
        {
            var ocmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var result = new List<object>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ocmConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("CountDailyReportInteraction", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@p_startdate", startDate));
                        command.Parameters.Add(new SqlParameter("@p_enddate", EndDate));
                        command.Parameters.Add(new SqlParameter("@Tenant", Tenant));
                      
                        conn.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                result.Add(new
                                {
                                    Name = reader["Name"].ToString(),
                                    Jumlah = reader["jumlah"].ToString()
                                });
                            }
                        }
                    }
                }
                return Ok(result);
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { message = "Database error occurred", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }

        }
        [HttpGet("Top5CategoryInteraktionDetail")]
        public async Task<ActionResult<IEnumerable<object>>> Top5CategoryInteraktionDetail(string startDate, string EndDate, string Tenant, string Params)
        {
            var ocmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var result = new List<object>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ocmConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("Top5CategoryInteraktionDetail", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@p_startdate", startDate));
                        command.Parameters.Add(new SqlParameter("@p_enddate", EndDate));
                        command.Parameters.Add(new SqlParameter("@Tenant", Tenant));
                        command.Parameters.Add(new SqlParameter("@params", Params));

                        conn.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                result.Add(new
                                {
                                    Category = reader["Category"].ToString(),
                                    Name = reader["Name"].ToString(),
                                    Jumlah = reader["Percentage"].ToString()
                                });
                            }
                        }
                    }
                }
                return Ok(result);
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { message = "Database error occurred", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }

        }

        //[HttpGet("GetInteraction")]
        //public async Task<ActionResult<List<Interaction>>> GetInteraction(string TicketNumber)
        //{
        //    var interactions = new List<Interaction>();

        //    try
        //    {
        //        using (var connection = _context.Database.GetDbConnection())
        //        {
        //            await connection.OpenAsync();

        //            using (var command = connection.CreateCommand())
        //            {
        //                command.CommandText = "sp_GetInteractions"; // Panggil SP langsung
        //                command.CommandType = CommandType.StoredProcedure;

        //                var parameter = command.CreateParameter();
        //                parameter.ParameterName = "@TicketNumber";
        //                parameter.Value = TicketNumber;
        //                command.Parameters.Add(parameter);

        //                using (var reader = await command.ExecuteReaderAsync())
        //                {
        //                    while (await reader.ReadAsync())
        //                    {
        //                        interactions.Add(new Interaction
        //                        {
        //                            TicketNumber = reader["TicketNumber"].ToString(),
        //                            Description = reader["ResponseComplaint"].ToString(),
        //                            Status = reader["Status"].ToString(),
        //                            CaseOwner = reader["DispatchValueName"].ToString(),
        //                            Name = reader["Name"].ToString(),
        //                            TanggalInteraction = reader["TanggalInteraction"].ToString()
        //                        });
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        return StatusCode(500, $"SQL Error: {sqlEx.Message}");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error: {ex.Message}");
        //    }

        //    return Ok(interactions);
        //}

        [HttpGet("GetInteraction")]
        public async Task<ActionResult<IEnumerable<object>>> CategoryInteraktion(string TicketNumber)
        {
            var ocmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var result = new List<object>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ocmConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_GetInteractions", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@TicketNumber", TicketNumber));
                      

                        conn.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                result.Add(new
                                {
                                    TicketNumber = reader["TicketNumber"].ToString(),
                                    Description = reader["ResponseComplaint"].ToString(),
                                    TanggalEskalasiBO = reader["TanggalEskalasiBO"].ToString(),
                                    keteranganBo = reader["keteranganBo"].ToString(),
                                    TanggalEskalasiSitika = reader["TanggalEskalasiSitika"].ToString(),
                                    keteranganSitika = reader["keteranganSitika"].ToString(),
                                    Status = reader["Status"].ToString(),
                                    CaseOwner = reader["DispatchValueName"].ToString(),
                                    Name = reader["Name"].ToString(),
                                    TanggalInteraction = reader["TanggalInteraction"].ToString()
                                });
                            }
                        }
                    }
                }
                return Ok(result);
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { message = "Database error occurred", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }

        }

        [HttpGet("CategoryInteraktion")]
        public async Task<ActionResult<IEnumerable<object>>> CategoryInteraktion(string startDate, string EndDate, string Tenant)
        {
            var ocmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var result = new List<object>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ocmConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("CategoryInteraktion", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@p_startdate", startDate));
                        command.Parameters.Add(new SqlParameter("@p_enddate", EndDate));
                        command.Parameters.Add(new SqlParameter("@Tenant", Tenant));

                        conn.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                result.Add(new
                                {
                                    Name = reader["Name"].ToString(),
                                    Jumlah = reader["Jumlah"].ToString()
                                });
                            }
                        }
                    }
                }
                return Ok(result);
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { message = "Database error occurred", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }

        }
        [HttpGet("CategoryInteraktionTicket")]
        public async Task<ActionResult<IEnumerable<object>>> CategoryInteraktionTicket(string startDate, string EndDate, string Tenant,string CategoryUrgent)
        {
            var ocmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var result = new List<object>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ocmConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("CategoryInteraktionTicket", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@p_startdate", startDate));
                        command.Parameters.Add(new SqlParameter("@p_enddate", EndDate));
                        command.Parameters.Add(new SqlParameter("@Tenant", Tenant));
                        command.Parameters.Add(new SqlParameter("@CategoryUrgent", CategoryUrgent));

                        conn.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                result.Add(new
                                {
                                    Name = reader["Name"].ToString(),
                                    Jumlah = reader["Jumlah"].ToString()
                                });
                            }
                        }
                    }
                }
                return Ok(result);
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { message = "Database error occurred", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }

        }
        [HttpGet("CategoryInteraktionTicketDetail")]
        public async Task<ActionResult<IEnumerable<object>>> CategoryInteraktionTicketDetail(string startDate, string EndDate, string Tenant, string Params)
        {
            var ocmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var result = new List<object>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ocmConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("CategoryInteraktionTicketDetail", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@p_startdate", startDate));
                        command.Parameters.Add(new SqlParameter("@p_enddate", EndDate));
                        command.Parameters.Add(new SqlParameter("@Tenant", Tenant));
                        command.Parameters.Add(new SqlParameter("@params", Params));

                        conn.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                result.Add(new
                                {
                                    Name = reader["Name"].ToString(),
                                    Jumlah = reader["Jumlah"].ToString()
                                });
                            }
                        }
                    }
                }
                return Ok(result);
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { message = "Database error occurred", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }

        }

        [HttpGet("Login")]
        public async Task<ActionResult<IEnumerable<object>>> Login(string UserName, string Password)
        {
            var ocmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var result = new List<object>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ocmConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_LOGIN_APPLIKASI", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        var ParameterStart = new SqlParameter("@USER", UserName);
                        var ParameterEnd = new SqlParameter("@PASSWORD", Password);

                        command.Parameters.Add(ParameterStart);
                        command.Parameters.Add(ParameterEnd);

                        conn.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                result.Add(new
                                {
                                    Name = reader["TenantName"].ToString()

                                });
                            }
                        }
                    }
                }
                return Ok(result);
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { message = "Database error occurred", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }

        }



        [HttpGet("GetDataDDlCategory")] // Endpoint: api/tickets/Top3SubCategory3
        public async Task<ActionResult<IEnumerable<object>>> GetDataDDlCategory(string Tenant)
        {
            var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var result = new List<object>();

            using (SqlConnection conn = new SqlConnection(crmConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetDataDDlCategory", conn))
                {
                    cmd.Parameters.AddWithValue("@Tenant", Tenant);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            result.Add(new
                            {
                                value = reader["ID"].ToString(),
                                text = reader["Name"].ToString()
                            });
                        }
                    }
                }
            }

            return Ok(result);
        }

        [HttpGet("GetDataDDlChannel")] // Endpoint: api/tickets/Top3SubCategory3
        public async Task<ActionResult<IEnumerable<object>>> GetDataDDlChannel()
        {
            var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var result = new List<object>();

            using (SqlConnection conn = new SqlConnection(crmConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetDataDDlChannel", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            result.Add(new
                            {
                                value = reader["TypeID"].ToString(),
                                text = reader["Name"].ToString()
                            });
                        }
                    }
                }
            }

            return Ok(result);
        }

        [HttpGet("Top3SubCategory3")] // Endpoint: api/tickets/Top3SubCategory3
        public async Task<ActionResult<IEnumerable<object>>> GetTop3SubCategory3()
        {
            var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var result = new List<object>();

            using (SqlConnection conn = new SqlConnection(crmConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetDataDdlTop3SubCategory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            result.Add(new
                            {
                                value = reader["SubCategory3ID"].ToString(),
                                text = reader["SubCategory3Name"].ToString()
                            });
                        }
                    }
                }
            }

            return Ok(result);
        }

        [HttpGet("GetDataTicketWb")] // Endpoint: api/tickets/Top3SubCategory3
        public async Task<ActionResult<IEnumerable<object>>> GetDataTicketWb(string StartDate, string EndDate)
        {
            var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var result = new List<object>();

            using (SqlConnection conn = new SqlConnection(crmConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetDataDdlTop3SubCategory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            result.Add(new
                            {
                                Name = reader["Name"].ToString(),
                                Jumlah = int.Parse(reader["jumlah"].ToString())
                            });
                        }
                    }
                }
            }

            return Ok(result);
        }

        [HttpGet("TicketCount")] // Endpoint: api/tickets/TicketCount?startDate=YYYY-MM-DD&endDate=YYYY-MM-DD
        public async Task<ActionResult<object>> GetTicketCount([FromQuery] string startDate, [FromQuery] string endDate)
        {
            var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            object result = null;

            using (SqlConnection conn = new SqlConnection(crmConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetDataTicketWb", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@date", startDate));
                    cmd.Parameters.Add(new SqlParameter("@enddate", endDate));

                    conn.Open();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            result = new
                            {
                                Name = reader["Name"].ToString(),
                                jumlah = reader["jumlah"].ToString()
                            };
                        }
                    }
                }
            }

            return Ok(result);
        }

        [HttpGet("GetDataIntertionByCategory")] // Endpoint: api/tickets/GetDataIntertionByCategory?startDate=YYYY-MM-DD&endDate=YYYY-MM-DD&channel=SomeChannel&category=SomeCategory
                public async Task<ActionResult<object>> GetDataIntertionByCategory(
              [FromQuery] string startDate,
              [FromQuery] string endDate,
              [FromQuery] string channel,
              [FromQuery] string category)
        {
            var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            object result = null;

            using (SqlConnection conn = new SqlConnection(crmConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetDataIntertionByCategory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters for the stored procedure
                    cmd.Parameters.Add(new SqlParameter("@date", startDate));
                    cmd.Parameters.Add(new SqlParameter("@enddate", endDate));
                    cmd.Parameters.Add(new SqlParameter("@Channel", channel));  // New parameter
                    cmd.Parameters.Add(new SqlParameter("@Category", category)); // New parameter

                    conn.Open();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        List<object> results = new List<object>();
                        while (await reader.ReadAsync())
                        {
                            var row = new
                            {
                                Name = reader["Name"].ToString(),
                                jumlah = reader["jumlah"].ToString()
                            };
                            results.Add(row);
                        }
                        result = results;
                    }
                }
            }

            return Ok(result);
        }
        [HttpGet("GetDataSubCategory3bytop5")] // Endpoint: api/tickets/GetDataIntertionByCategory?startDate=YYYY-MM-DD&endDate=YYYY-MM-DD&channel=SomeChannel&category=SomeCategory
                public async Task<ActionResult<object>> GetDataSubCategory3bytop5(
              [FromQuery] string startDate,
              [FromQuery] string endDate,
              [FromQuery] string channel,
              [FromQuery] string category)
        {
            var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            object result = null;

            using (SqlConnection conn = new SqlConnection(crmConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetDataSubCategory3bytop5", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters for the stored procedure
                    cmd.Parameters.Add(new SqlParameter("@date", startDate));
                    cmd.Parameters.Add(new SqlParameter("@enddate", endDate));
                    cmd.Parameters.Add(new SqlParameter("@Channel", channel));  // New parameter
                    cmd.Parameters.Add(new SqlParameter("@Category", category)); // New parameter

                    conn.Open();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        List<object> results = new List<object>();
                        while (await reader.ReadAsync())
                        {
                            var row = new
                            {
                                Name = reader["SubCategory3Name"].ToString(),
                                jumlah = reader["Periode"].ToString()
                            };
                            results.Add(row);
                        }
                        result = results;
                    }
                }
            }

            return Ok(result);
        }
        
      


        [HttpGet("GetDataDailyInteraction")]
        public async Task<IActionResult> GetDataDailyInteraction(string startDate, string EndDate)
        {
            var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            int totalVoice = 0;
            int totalEmail = 0;
            int totalMultichat = 0;

            try
            {
                async Task<int> GetDataAsync(string connectionString, string procedureName, string startDateParam, string endDateParam)
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = procedureName;
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter(startDateParam, startDate));
                            command.Parameters.Add(new SqlParameter(endDateParam, EndDate));

                            var result = await command.ExecuteScalarAsync();
                            return result != DBNull.Value ? Convert.ToInt32(result) : 0;
                        }
                    }
                }

                // Run database queries concurrently
                var taskEmail = await GetDataAsync(crmConnectionString, "GetDataDailyInteraction", "@date", "@enddate");




            }
            catch (SqlException sqlEx)
            {
                return StatusCode(500, new { message = $"SQL Error: {sqlEx.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"General Error: {ex.Message}" });
            }

            // Return response in a structured format
            return Ok(new
            {
                TotalVoice = totalVoice,
                TotalEmail = totalEmail,
                TotalMultichat = totalMultichat,
                GrandTotal = totalVoice + totalEmail + totalMultichat
            });
        }


        [HttpGet("GetDataSubCategory1bytop5")]
        public async Task<IActionResult> GetDataSubCategory1bytop5(
      string startDate,
      string endDate,
      string SubCategori1Name,string Params)
        {
            var crmConnectionString = _configuration.GetConnectionString("CRMConnection");

            // 🔧 Tambahkan deklarasi result
            var result = new List<object>();

            try
            {
                using (SqlConnection conn = new SqlConnection(crmConnectionString))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("GetDataSubCategory3", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_startdate", startDate);
                        cmd.Parameters.AddWithValue("@p_enddate", endDate);
                        cmd.Parameters.AddWithValue("@SubCategori1Name", SubCategori1Name);
                        cmd.Parameters.AddWithValue("@params", Params);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                result.Add(new
                                {
                                    Name = reader["SubCategory1Name"]?.ToString(),
                                    Periode = reader["Periode"]?.ToString(),
                                    History = reader["History"]?.ToString(),
                                    Selisih = reader["Selisih"]?.ToString()
                                });
                            }
                        }
                    }
                }

                return Ok(result);
            }
            catch (SqlException sqlEx)
            {
                return StatusCode(500, new { message = $"SQL Error: {sqlEx.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"General Error: {ex.Message}" });
            }
        }



    }



}


public class executiveMonitoring
{
    public int incoming { get; set; }
    public decimal answer { get; set; }
}
public class AvayaVoice
{
    public int Voice { get; set; }
    
}
public class OcmData
{
    public int multichat { get; set; }

}

public class EmailData
{
    public int Email { get; set; }

}
public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}


