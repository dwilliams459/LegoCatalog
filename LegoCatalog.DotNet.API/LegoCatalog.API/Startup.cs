using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using LegoCatalog.Data;
using LegoCatalog.Service;
using AutoMapper;
using System.IO;
using LegoCatalog.API.Helpers;

namespace LegoCatalog.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AzureConfiguration = new AzureConfiguration(configuration);
        }

        public IConfiguration Configuration { get; }
        public AzureConfiguration AzureConfiguration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine($"Environment: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");
            string connectionString = AzureConfiguration.GetValue<string>("ConnectionStrings:LegoCatalogDatabase", "LegoCatalogDatabase");
            Console.WriteLine($"Connection String: {connectionString.Substring(0, 10)}");

            services.AddDbContext<PartsCatalogDbContext>(opt =>
                opt.UseSqlServer(connectionString)  //"Data Source=localhost;Initial Catalog=legocatalog;Integrated Security=True") //)
            );

            services.AddScoped<PartService>();

            services.AddCors();

            services.AddControllers();
        }

        public T GetValue<T>(string devKey, string azureKey) 
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                return Configuration.GetValue<T>(azureKey);
            else
                return Configuration.GetValue<T>(devKey);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.CreateDefaultBuilder();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseRouting();
            app.UseAuthorization();

            app.Use(async(context, next) => {
                await next();

                // If not found redirect to index page
                if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
                {
                    context.Request.Path = "/index.html";
                    await next();
                } 
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
