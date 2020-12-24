using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Warehouse.LegacyData;
using Warehouse.Data;
using Warehouse.Domain.StoreAbstractions;
using Warehouse.Queries;
using Warehouse.Queries.Abstractions;
using Warehouse.Repositories;
using System;
using System.Text;

namespace Warehouse.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = "ValidUserComp",
                            ValidAudience = "warehouse",
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtAuth:SecretKey"])),
                            ClockSkew = TimeSpan.Zero
                        };
                    });

            services.AddControllers()
                .AddNewtonsoftJson(json => { json.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; });

            services.AddDbContext<WarehouseContext>(o =>
            o.UseNpgsql(this.Configuration.GetConnectionString("Warehouse"),
            b =>
            {
                b.MigrationsAssembly("Warehouse.WebApi");
            })
             .UseSnakeCaseNamingConvention()
             .EnableSensitiveDataLogging()
            );

            services.AddDbContext<LegacyWarehouseContext>(o =>
                o.UseSqlServer(this.Configuration.GetConnectionString("LegacyWarehouse"))
                 .EnableSensitiveDataLogging()
            );

            services.AddTransient<ITerminalStore, TerminalStore>();
            services.AddTransient<IWarehouseStore, WarehouseStore>();
            services.AddTransient<IRouteStore, RouteStore>();
            services.AddTransient<UnitOfWork>();
            services.AddTransient<IItineraryQueryService, ItineraryQueryService>();

            services.AddSwaggerGen(swagger =>
            {
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Warehouse",
                    Version = this.GetType().Assembly.GetName().Version.ToString()
                });

                swagger.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    { securityScheme, new string[] { } }
                });
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Warehouse"); });

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                    var error = Error.Interpret(exceptionHandlerPathFeature.Error, true);

                    context.Response.StatusCode = (int)error.Code;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(error, new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        NullValueHandling = NullValueHandling.Ignore
                    }));
                });
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}