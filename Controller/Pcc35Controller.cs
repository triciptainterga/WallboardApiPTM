using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class Pcc35Controller : ControllerBase
    {
        private readonly pcc135Context _context;
        private readonly CrmContext _Crmcontext;
        private readonly IConfiguration _configuration;

        public Pcc35Controller(pcc135Context context, CrmContext Crmcontext, IConfiguration configuration)
        {
            _context = context;
            _Crmcontext = Crmcontext;
            _configuration = configuration;



        }

        [HttpPost]
        [Route("createTicket")]
        public async Task<IActionResult> CreateTicket([FromBody] TicketRequest requestBody)
        {
            var apiSettings = _configuration.GetSection("ApiSettings").Get<ApiSettings>();

            var accessToken = await GetAccessToken(apiSettings);

            if (string.IsNullOrEmpty(accessToken))
            {
                return BadRequest("Failed to get access token.");
            }

            //using (var client = new HttpClient())
            //{
            //    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            //    var jsonContent = JsonConvert.SerializeObject(requestBody);
            //    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            //    // Kirim POST request ke ticketing system
            //    var response = await client.PostAsync(apiSettings.UrlTicket, content);

            //    if (response.IsSuccessStatusCode)
            //    {
            //        var result = await response.Content.ReadAsStringAsync();
            //        return Ok(result);
            //    }
            //    else
            //    {
            //        return BadRequest("Failed to create ticket.");
            //    }
            //}
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var jsonContent = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync(apiSettings.UrlTicket, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return Ok(result);
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        return BadRequest($"Failed to create ticket. Error: {errorMessage}");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }


        [HttpGet]
        [Route("GetTicketList")]
        public async Task<IActionResult> GetTickets([FromQuery] string category, [FromQuery] int page, [FromQuery] int size)
        {
            var apiSettings = _configuration.GetSection("ApiSettings").Get<ApiSettings>();
            var accessToken = await GetAccessToken(apiSettings);

            if (string.IsNullOrEmpty(accessToken))
            {
                return BadRequest("Failed to get access token.");
            }

            using (var client = new HttpClient())
            {
                // Add Authorization header with Bearer token
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                // Create the query parameters entity using the inputs directly from the URL
                var queryParams = new TicketQueryParameters(category, page, size);

                // Build the query string from the TicketQueryParameters
                var queryString = BuildQueryString(queryParams);

                var requestUri = $"{apiSettings.UrlTicketList}{queryString}";

                try
                {
                    // Send the GET request to the ticketing system API
                    var response = await client.GetAsync(requestUri);

                    if (response.IsSuccessStatusCode)
                    {
                        // If the response is successful, read the content
                        var result = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Response: " + result); // Log the response for debugging

                        // Optionally, you could deserialize the result into an object
                        //var tickets = JsonConvert.DeserializeObject<List<TicketResponse>>(result);

                        var formattedJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(result), Formatting.Indented);

                        return Ok(formattedJson);  // Returning the list of tickets in the response
                    }
                    else
                    {
                        // If the response indicates failure, read the error message
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Error: " + errorMessage); // Log the error for debugging
                        return BadRequest($"Failed to fetch tickets. Error: {errorMessage}");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any unexpected errors, such as network issues
                    Console.WriteLine("Exception: " + ex.Message); // Log the exception for debugging
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }

        [HttpGet]
        [Route("GetTicketDetail")]
        public async Task<IActionResult> GetTicketDetail([FromQuery] string ticket_id)
        {
            var apiSettings = _configuration.GetSection("ApiSettings").Get<ApiSettings>();
            var accessToken = await GetAccessToken(apiSettings);

            if (string.IsNullOrEmpty(accessToken))
            {
                return BadRequest("Failed to get access token.");
            }

            using (var client = new HttpClient())
            {
                // Add Authorization header with Bearer token
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                // Create the query parameters entity using the inputs directly from the URL
            
                var requestUri = $"{apiSettings.UrlTicketList}" +ticket_id;

                try
                {
                    // Send the GET request to the ticketing system API
                    var response = await client.GetAsync(requestUri);

                    if (response.IsSuccessStatusCode)
                    {
                        // If the response is successful, read the content
                        var result = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Response: " + result); // Log the response for debugging

                        // Optionally, you could deserialize the result into an object
                        //var tickets = JsonConvert.DeserializeObject<List<TicketResponse>>(result);

                        var formattedJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(result), Formatting.Indented);

                        return Ok(formattedJson);  // Returning the list of tickets in the response
                    }
                    else
                    {
                        // If the response indicates failure, read the error message
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Error: " + errorMessage); // Log the error for debugging
                        return BadRequest($"Failed to fetch tickets. Error: {errorMessage}");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any unexpected errors, such as network issues
                    Console.WriteLine("Exception: " + ex.Message); // Log the exception for debugging
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }


        private string BuildQueryString(TicketQueryParameters parameters)
        {
            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(parameters.Category))
            {
                queryParams.Add($"category={parameters.Category}");
            }

            queryParams.Add($"page={parameters.Page}");
            queryParams.Add($"size={parameters.Size}");

            return string.Join("&", queryParams);
        }
        private async Task<string> GetAccessToken(ApiSettings apiSettings)
        {
            using (var client = new HttpClient())
            {
                var requestBody = new
                {
                    grant_type = apiSettings.GrantType,
                    client_id = apiSettings.ClientId,
                    client_secret = apiSettings.ClientSecret
                };

                var jsonContent = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Kirim POST request untuk mendapatkan access token
                var response = await client.PostAsync(apiSettings.Url, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(result);
                    return tokenResponse?.AccessToken;
                }
                else
                {
                    return null;
                }
            }
        }

        [HttpPost("UpdateTicket")]
        public async Task<IActionResult> PTM_ResolveTicketSitika([FromBody] ResolveTicket request)
        {
            var listTickets = new List<ResolveTicket>();
            var _strTime = new SqlParameter("@TicketNumber", request.TicketNumber);
            var _strStatus = new SqlParameter("@Status", request.Status);
            var _strGenesysNumber = new SqlParameter("@CreatedBy", request.CreatedBy);
            var _strThreadID = new SqlParameter("@Description", request.Description);
           





            try
            {


              
               

                var  result = await _Crmcontext.Database.ExecuteSqlRawAsync(
                    "EXEC PTM_ResolveTicketSitika @TicketNumber,@Status, @CreatedBy, @Description",
                                              _strTime,_strStatus, _strGenesysNumber, _strThreadID

                );
                return Ok(request);

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());

            }


            //var js = new JavaScriptSerializer();
            
        }


        [HttpGet]
        [Route("GetTicketByCustomerId")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets(int CustomerId, int limit = 10, int offset = 0)
                        {
                          try
                {
                    var result = await _context.Tickets
                        .Where(t => t.CustId == CustomerId)  
                        .Include(t => t.Status)     
                        .Include(t => t.CreatedByNavigation)
                        .Select(t => new TaskDisplayDto
                        {
                            Id = t.Id,
                            StatusId = t.StatusId,
                            Status = t.Status.Name,
                            CreatedBy = t.CreatedBy.ToString(),
                            DateCreated = t.DateCreate
                        })
                        .Skip(offset)  
                        .Take(limit)  
                        .ToListAsync();

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    // Log the exception (you can use a logging framework like Serilog or log to a file)
                    // For example, if you're using console logging, you can do something like:
                    Console.WriteLine($"Error occurred: {ex.Message}");

                    // Return an appropriate error message to the client
                    return StatusCode(500, new { message = "An error occurred while fetching the data.", error = ex.Message });
                }
        }


        [HttpGet]
        [Route("GetTicketDetailByTicketId")]


        public async Task<ActionResult<IEnumerable<Ticket>>> GetDetailTickets(int ticketId, int limit = 10, int offset = 0)
        {
            var result = await _context.Tickets
     .Join(_context.TicketHistories,
           ticket => ticket.Id,
           history => history.TicketId,
           (ticket, history) => new { ticket, history })
     .Join(_context.MTicketStatuses,
           ticketHistory => ticketHistory.ticket.StatusId,
           status => status.Id,
           (ticketHistory, status) => new { ticketHistory.ticket, ticketHistory.history, status })
     .Join(_context.Users,
           ticketHistoryStatus => ticketHistoryStatus.ticket.CreatedBy,
           user => user.Id,
           (ticketHistoryStatus, user) => new { ticketHistoryStatus.ticket, ticketHistoryStatus.history, ticketHistoryStatus.status, user })
     .Join(_context.MCategories,
           ticketHistoryStatusUser => ticketHistoryStatusUser.ticket.CategoryId,
           category => category.Id,
           (ticketHistoryStatusUser, category) => new { ticketHistoryStatusUser.ticket, ticketHistoryStatusUser.history, ticketHistoryStatusUser.status, ticketHistoryStatusUser.user, category })
     .Join(_context.MCategoryMains,
           category => category.category.MainCategoryId,
           mainCategory => mainCategory.Id,
           (ticketHistoryStatusUserCategory, mainCategory) => new
           {
               ticketHistoryStatusUserCategory.ticket.Id,
               ticketHistoryStatusUserCategory.history.TicketId,
               ticketHistoryStatusUserCategory.ticket.CategoryId,
               Category = ticketHistoryStatusUserCategory.category.Level2, 
               MainCategory = mainCategory.Name,  
               ticketHistoryStatusUserCategory.ticket.Subject,
               ticketHistoryStatusUserCategory.ticket.Remark,
               ticketHistoryStatusUserCategory.ticket.StatusId,
               Status = ticketHistoryStatusUserCategory.status.Name, 
               ticketHistoryStatusUserCategory.ticket.CreatedBy,
               UserName = ticketHistoryStatusUserCategory.user.Fullname, 
               ticketHistoryStatusUserCategory.ticket.DateCreate,
               ticketHistoryStatusUserCategory.ticket.DateClose
           })
     .Where(t => t.TicketId == ticketId)
      .Skip(offset)  
        .Take(limit)   
     .ToListAsync();
            return Ok(result);
        }
    }
}


public class TicketQueryParameters
{
    public string Category { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }

    public TicketQueryParameters(string category, int page, int size)
    {
        Category = category;
        Page = page;
        Size = size;
    }
}
public class TaskDisplayDto
{
    public int Id { get; set; }
    public int StatusId { get; set; }
    public string Status { get; set; }
    public string CreatedBy { get; set; }
    public string UserName { get; set; }
    public DateTime? DateCreated { get; set; }
}
public class ResolveTicket
{
   
    public string TicketNumber { get; set; }
    public string Status { get; set; }
    public string CreatedBy { get; set; }
    public string Description { get; set; }
   
}
public class ApiSettings
{
    public string GrantType { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Url { get; set; }
    public string UrlTicket { get; set; }
    public string UrlTicketList { get; set; }
    public string UrlDetail { get; set; }
}



public class CustomField
{
    public string custom_field_id { get; set; }
    public string category_id { get; set; }
    public string field_name { get; set; }
    public int order_number { get; set; }
    public int field_type { get; set; }
    public string value { get; set; }
}

public class TicketRequest
{
    public string category_id { get; set; }
    public string title { get; set; }
    public string assigne_group { get; set; }
    public string assigne_user { get; set; }
    public string reference_ticket { get; set; }
    public string ticket_description { get; set; }
    public string sortfield { get; set; }
    public string created_by { get; set; }
    public List<CustomField> custom_field { get; set; }
    public List<string> File { get; set; }
}
public class TokenResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
}


public class TicketResponse
{
    public PaginationInfo Pages { get; set; }
    public List<TicketData> Data { get; set; }
}

public class PaginationInfo
{
    public int CurrentPage { get; set; }
    public int LastPage { get; set; }
    public int Size { get; set; }
    public int TotalData { get; set; }
}

public class TicketData
{
    public string TicketId { get; set; }
    public string CategoryId { get; set; }
    public string Title { get; set; }
    public string Status { get; set; }
    public string StatusNotes { get; set; }
    public string AssigneGroup { get; set; }
    public string AssigneUser { get; set; }
    public string Created { get; set; }
    public string CreatedBy { get; set; }
    public string ClosedTargetTime { get; set; }
    public string ActualClosedTime { get; set; }
    public int SlaStatus { get; set; }
    public string LastAssigneGroup { get; set; }
    public string LastClosedTargetTime { get; set; }
    public int? LastSlaStatus { get; set; }
    public string TicketDescription { get; set; }
    public bool IsRead { get; set; }
}