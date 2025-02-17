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
        public async Task<IActionResult> GetDataTotalInteraction(string startDate, string EndDate)
        {
            var avayaConnectionString = _configuration.GetConnectionString("AvayaConnection");
            var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var ocmConnectionString = _configuration.GetConnectionString("DefaultConnection2");

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
                var taskMultichat = GetDataAsync(ocmConnectionString, "GetDataMultichat", "@p_startdate", "@p_enddate");
                var taskVoice = GetDataAsync(avayaConnectionString, "GetDataVoice", "@StartDate", "@EndDate");
                var taskEmail = GetDataAsync(crmConnectionString, "GetDataEmail", "@date", "@enddate");
               

                var results = await Task.WhenAll(taskVoice, taskEmail,taskMultichat);

                // Assign results
                totalVoice = results[0];
                totalEmail = results[1];
                totalMultichat = results[2];

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

        
        [HttpGet("GetDataDDlCategory")] // Endpoint: api/tickets/Top3SubCategory3
        public async Task<ActionResult<IEnumerable<object>>> GetDataDDlCategory()
        {
            var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var result = new List<object>();

            using (SqlConnection conn = new SqlConnection(crmConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetDataDDlCategory", conn))
                {
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


