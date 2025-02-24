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
using System.Configuration;

namespace WEBAPI_Bravo.Controller
{


    [Route("api/[controller]")]
    [ApiController]
    public class DashboardMonitoringController : ControllerBase
    {


        private readonly sqlContext _context;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString2;
        private readonly string _AvayaconnectionString;
        private readonly string _CrmconnectionString;

        public DashboardMonitoringController(sqlContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _AvayaconnectionString = _configuration.GetConnectionString("AvayaConnection");
            _CrmconnectionString = _configuration.GetConnectionString("CrmConnection");
        }




        [HttpGet("GetDataInteractionDashboard")]
        public async Task<IActionResult> GetDataTotalInteraction(string startDate, string EndDate, string Tenant)
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
                var taskMultichat = GetDataAsync(ocmConnectionString, "GetDataMultichat", "@p_startdate", "@p_enddate", "@Tenant");
                var taskVoice = GetDataAsync(avayaConnectionString, "GetDataVoice", "@StartDate", "@EndDate", "@Tenant");
                var taskEmail = GetDataAsync(ocmConnectionString, "GetDataEmail", "@p_startdate", "@p_enddate", "@Tenant");


                var results = await Task.WhenAll(taskVoice, taskEmail, taskMultichat);

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

        [HttpGet("GetDataDDlCaseOwner")]
        public async Task<ActionResult<IEnumerable<object>>> GetDataDDlCaseOwner()
        {
            var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            var result = new List<object>();

            using (SqlConnection conn = new SqlConnection(crmConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetDataDDlCaseOwner", conn))
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
    }
   






}


