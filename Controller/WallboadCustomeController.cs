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
        public class WallboadCustomeController : ControllerBase
        {

        
            private readonly sqlContext _context;
            private readonly IConfiguration _configuration;
            private readonly string _connectionString2;

            public WallboadCustomeController(sqlContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
                _connectionString2 = _configuration.GetConnectionString("DefaultConnection2");
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
        [HttpGet("GetDataEmailMonth")]
        public async Task<List<datas>> GetDataEmailMonth(string startDate, string EndDate)
        {
            var users = new List<datas>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataEmailMonth @p_startdate,@p_enddate";
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
          [HttpGet("GetDataEmail1")]
        public async Task<List<datas>> GetDataEmail1()
        {
            var users = new List<datas>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataEmail1";
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
        public async Task<List<dataInteractionDay>> GetDataEmailIntervalHari(string startDate, string EndDate)
        {
            var users = new List<dataInteractionDay>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataEmailIntervalHari @p_startdate,@p_enddate";
                        var ParameterStart = new SqlParameter("@p_startdate", startDate);
                        var ParameterEnd = new SqlParameter("@p_enddate", EndDate);

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
                                    Jumlah =    result.GetDecimal(2)
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
        public async Task<List<datas>> GetDataLivechatMonth(string startDate, string EndDate)
        {
            var users = new List<datas>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataLivechatMonth @p_startdate,@p_enddate ";
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
        public async Task<List<dataInteraction>> GetDataEmailInterval()
        {
            var users = new List<dataInteraction>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataEmailInterval";
                        //var ParameterStart = new SqlParameter("@p_startdate", startDate);
                        //var ParameterEnd = new SqlParameter("@p_enddate", EndDate);

                        //command.Parameters.Add(ParameterStart);
                        //command.Parameters.Add(ParameterEnd);
                        command.CommandType = CommandType.Text;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new dataInteraction
                                {
                                    Name = result.GetString(0),
                                    DateInteraction = result.GetDateTime(1).ToString(),
                                    Jumlah = result.GetDecimal(2).ToString()
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
        public async Task<List<dataInteraction>> GetDataSosmedInterval()
        {
            var users = new List<dataInteraction>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataSosmedInterval";
                        //var ParameterStart = new SqlParameter("@p_startdate", startDate);
                        //var ParameterEnd = new SqlParameter("@p_enddate", EndDate);

                        //command.Parameters.Add(ParameterStart);
                        //command.Parameters.Add(ParameterEnd);
                        command.CommandType = CommandType.Text;
                     
                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new dataInteraction
                                {
                                    Name = result.GetString(0),
                                    DateInteraction = result.GetDateTime(1).ToString(),
                                    Jumlah = result.GetDecimal(2).ToString()
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
        public async Task<List<dataInteraction>> GetDataSosmedIntervalHari(string startDate, string EndDate)
        {
            var users = new List<dataInteraction>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataSosmedIntervalHari @p_startdate,@p_enddate";
                        var ParameterStart = new SqlParameter("@p_startdate", startDate);
                        var ParameterEnd = new SqlParameter("@p_enddate", EndDate);

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
                                    Jumlah = result.GetDecimal(2).ToString()
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

        [HttpGet("GetDataSosmed1")]
        public async Task<List<datas>> GetDataSosmed1()
        {
            var users = new List<datas>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC  GetDataSosmed1";
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
        
        [HttpGet("GetDataLivechat1")]
        public async Task<List<datas>> GetDataLivechat1()
        {
            var users = new List<datas>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC  GetDataLivechat1";
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
}
public class dataInteractionDay
{
    public string Name { get; set; }
    public string DateInteraction { get; set; }
    public decimal Jumlah { get; set; }
}
