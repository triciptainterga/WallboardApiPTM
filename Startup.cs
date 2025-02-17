using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WEBAPI_Bravo.Model;


namespace WEBAPI_Bravo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<pcc135Context>(options =>
                 options.UseMySQL(
                     Configuration.GetConnectionString("DefaultConnection")
                    
                 )
             );

            services.AddDbContext<sqlContext>(options =>
      options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection2")));

            services.AddDbContext<CrmContext>(options =>
   options.UseSqlServer(Configuration.GetConnectionString("CrmConnection")));

            // Menambahkan layanan untuk controller
            services.AddControllers();

            // Menambahkan konfigurasi CORS untuk mengizinkan domain tertentu
            //services.AddCors(options =>
            //{
            //    //options.AddPolicy("AllowSpecificOrigin", builder =>
            //    //{
            //    //    builder.WithOrigins("http://192.168.83.26:3000") // Ganti dengan domain frontend Anda
            //    //           .AllowAnyHeader()  // Mengizinkan semua header
            //    //           .AllowAnyMethod(); // Mengizinkan semua metode HTTP (GET, POST, dll)
            //    //});
            //});

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                    builder.AllowAnyOrigin()  // Allow any origin (use with caution, could be restrictive for production)
                           .AllowAnyMethod()  // Allow all methods (GET, POST, DELETE, PUT, etc.)
                           .AllowAnyHeader()); // Allow any header
            });

       
            // Menambahkan Swagger untuk dokumentasi API
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Wallboard  Api", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors("AllowAllOrigins");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllers();


                // Schedule the job

            });

            // Enable Swagger
            app.UseSwagger();

            // Enable CORS

            // Enable Swagger UI
            app.UseSwaggerUI(c =>
            {
               //c.SwaggerEndpoint("/APIWallboardPtm/swagger/v1/swagger.json", "Syntera API V1");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Pertamina");
            });
        }
    }


}
