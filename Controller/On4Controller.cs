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
    public class On4Controller : ControllerBase
    {
        private readonly on4Context _context;
        private readonly CrmContext _Crmcontext;
        private readonly IConfiguration _configuration;

        public On4Controller(on4Context context, CrmContext Crmcontext, IConfiguration configuration)
        {
            _context = context;
            _Crmcontext = Crmcontext;
            _configuration = configuration;



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


