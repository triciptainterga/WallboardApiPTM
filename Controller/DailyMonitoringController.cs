﻿using Microsoft.AspNetCore.Mvc;
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

namespace WEBAPI_Bravo.Controller
{


    [Route("api/[controller]")]
    [ApiController]
    public class DailyMonitoringController : ControllerBase
    {


        private readonly sqlContext _context;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString2;
        private readonly string _AvayaconnectionString;
        private readonly string _CrmconnectionString;

        public DailyMonitoringController(sqlContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _AvayaconnectionString = _configuration.GetConnectionString("AvayaConnection");
            _CrmconnectionString = _configuration.GetConnectionString("CrmConnection");
        }


        [HttpGet("GetDataTickeyByRegion")] 
        public async Task<ActionResult<object>> GetDataTickeyByRegion(
             [FromQuery] string startDate,
             [FromQuery] string endDate,
             [FromQuery] string Tenant)
        {
            var crmConnectionString = _configuration.GetConnectionString("CRMConnection");
            object result = null;

            using (SqlConnection conn = new SqlConnection(crmConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetDataTickeyByRegion", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters for the stored procedure
                    cmd.Parameters.Add(new SqlParameter("@p_startdate", startDate));
                    cmd.Parameters.Add(new SqlParameter("@p_enddate", endDate));
                    cmd.Parameters.Add(new SqlParameter("@Tenant", Tenant));  // New parameter
                   
                    conn.Open();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        List<object> results = new List<object>();
                        while (await reader.ReadAsync())
                        {
                            var row = new
                            {
                                Category = reader["CategoryName"].ToString(),
                                SubCategory = reader["SubCategory1Name"].ToString(),
                                Jumlah = reader["Jumlah"].ToString(),
                                More1 = reader["MORE1"].ToString(),
                                More2 = reader["MORE2"].ToString(),
                                More3 = reader["MORE3"].ToString(),
                                More4 = reader["MORE4"].ToString(),
                                More5 = reader["MORE5"].ToString(),
                                More6 = reader["MORE6"].ToString(),
                                More7 = reader["MORE7"].ToString(),
                                More8 = reader["MORE8"].ToString(),
                                Other = reader["Other"].ToString()


                            };
                            results.Add(row);
                        }
                        result = results;
                    }
                }
            }

            return Ok(result);
        }

        [HttpGet("GetDataInteraction")]
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
}


