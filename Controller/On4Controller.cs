
using Dapper;
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
    public class On4Controller : ControllerBase
    {
         private readonly CrmContext _Crmcontext;
        private readonly IConfiguration _configuration;

        public On4Controller(CrmContext Crmcontext, IConfiguration configuration)
        {
            _Crmcontext = Crmcontext;
            _configuration = configuration;



        }




        [HttpGet]
        [Route("GetTicketByCustomerId")]
        public async Task<ActionResult<IEnumerable<TransTicket>>> GetTickets(long CustomerId)
        {
            var connectionString = _configuration.GetConnectionString("On4Connection");

            try
            {
                using var connection = new MySqlConnection(connectionString);

                // Cek koneksi ke database
                try
                {
                    await connection.OpenAsync();
                    Console.WriteLine("Koneksi ke database berhasil.");
                }
                catch (Exception dbEx)
                {
                    // Gagal konek ke database
                    return StatusCode(500, new { message = "Gagal koneksi ke database.", error = dbEx.Message });
                }

                    var sql = @"
                    SELECT 
                        c.*
                    FROM interaction_header_history a
                    JOIN cwc b ON b.session_id = a.session_id
                    JOIN trans_ticket c ON c.ticket_id = b.ticket_id
                    JOIN m_category d ON d.category_id = c.category_id
                    JOIN m_sub_category e ON e.sub_category_id = c.sub_category_id
                    WHERE b.create_ticket = 1
                      AND d.is_active = 1
                      AND a.cust_id = @CustomerId
                    LIMIT 10;
                ";

                var result = await connection.QueryAsync<TransTicket>(
                    sql,
                    new { CustomerId }
                );

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Terjadi kesalahan saat mengambil data.", error = ex.Message });
            }

        }





    }
}


