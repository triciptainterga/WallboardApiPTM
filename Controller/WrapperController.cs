using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WEBAPI_Bravo.Model;


namespace WEBAPI_Bravo.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class WrapperController : ControllerBase
    {
        private readonly OmnixContext _context;
        private readonly CrmContext _Crmcontext;
        private readonly IConfiguration _configuration;
        private readonly IJwtService _jwtService;

        public WrapperController(OmnixContext context, CrmContext Crmcontext, IConfiguration configuration, IJwtService jwtService)
        {
            _context = context;
            _Crmcontext = Crmcontext;
            _configuration = configuration;
            _jwtService = jwtService;
            // _omnixDbContext = OmixDbContext;



        }
        [HttpPost("AuthMyPertamina")]
        public async Task<IActionResult> GetToken([FromBody] LoginMyPertamina request)
        {
            try
            {
                // Force TLS 1.2
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (var httpClient = new HttpClient())
                {
                    var url = "https://api-dev.mypertamina.id/partner/api/v1/auth/token";

                    var payload = new
                    {
                        mobileNumber = request.mobileNumber,
                        apiKey = request.apiKey
                    };

                    var json = JsonConvert.SerializeObject(payload);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(url, content);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        return StatusCode((int)response.StatusCode, new { error = responseContent });
                    }

                    var result = JsonConvert.DeserializeObject<MyPertaminaResponse>(responseContent);

                    if (result != null && result.Success && result.Data != null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest(new { error = "Invalid response from MyPertamina API." });
                    }
                }
            }
            catch (Exception ex)
            {
                var baseEx = ex.GetBaseException();
                return StatusCode(500, new
                {
                    error = ex.Message,
                    innerException = baseEx?.Message
                });
            }
        }


        public class LoginMyPertamina
        {
            public string mobileNumber { get; set; }
            public string apiKey { get; set; }

        }

        public class MyPertaminaResponse
        {
            public bool Success { get; set; }
            public MyPertaminaData Data { get; set; }
        }

        public class MyPertaminaData
        {
            public string Token { get; set; }
            public long TokenExpireIn { get; set; }
        }
    }
}







