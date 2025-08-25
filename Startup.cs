using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WEBAPI_Bravo.Model;
//using WEBAPI_Bravo.Model.Omnix;
using WEBAPI_Bravo.Services;


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

            services.AddHttpClient(); // <--- ini penting
            services.AddControllers();
            services.AddDbContext<OmnixContext>(options =>
                 options.UseMySQL(
                     Configuration.GetConnectionString("OmnixConnection")
                    
                 )
             );

            //services.AddDbContext<OmixDbContext>(options =>
            //     options.UseMySQL(
            //         Configuration.GetConnectionString("OmnixConnection")

            //     )
            // );

            services.AddDbContext<sqlContext>(options =>
      options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection2")));

            services.AddDbContext<CrmContext>(options =>
   options.UseSqlServer(Configuration.GetConnectionString("CrmConnection")));

            



            // Menambahkan layanan untuk controller
            services.AddControllers();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options =>
       {
           options.RequireHttpsMetadata = false;
           options.SaveToken = true;
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
               ValidateIssuer = false,
               ValidateAudience = false,
               ClockSkew = TimeSpan.Zero
           };
       });

            // Tambahkan Authorization
            services.AddAuthorization();
            services.AddScoped<IJwtService, JwtService>(); 
            services.AddScoped<iDetailServices, DetailServices>(); 

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


            //// Menambahkan Swagger untuk dokumentasi API
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Wallboard  Api", Version = "v1" });
            //    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            //});
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Bravo", Version = "v1" });

                // Konfigurasi Authentication di Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Masukkan token dengan format: Bearer {your_token_here}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
            });

         

            // Tambahkan Authorization
            services.AddAuthorization();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {



            app.UseCors("AllowAllOrigins");
            app.UseRouting();



            app.UseAuthentication();
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
            // c.SwaggerEndpoint("/APIWallboardPtm/swagger/v1/swagger.json", "Syntera API V1");
            //   c.SwaggerEndpoint("/crm-pertamina-api/swagger/v1/swagger.json", "Syntera API V1");
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Pertamina");
            });
        }
    }


}
