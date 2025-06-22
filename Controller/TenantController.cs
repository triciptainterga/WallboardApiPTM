using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
    public class TenantController : ControllerBase
    {
        private readonly pcc135Context _context;
        private readonly CrmContext _Crmcontext;
        private readonly IConfiguration _configuration;

        public TenantController(pcc135Context context, CrmContext Crmcontext, IConfiguration configuration)
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
                                    TenantApiKey = result.GetString(3),
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

    }
}

public class dataTenant
{
    public int ID { get; set; }
    public string TenantID { get; set; }
    public string DomainAPI { get; set; }
    public string TenantApiKey { get; set; }
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

