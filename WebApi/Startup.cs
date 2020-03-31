using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace WebApi
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;

            _env = env;

            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Information()
              .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
              .Enrich.FromLogContext()
              .Enrich.WithMachineName()
              .Enrich.WithProcessId()
              .Enrich.WithProcessName()
              .WriteTo.RollingFile(Configuration.GetSection("LogPath").Value + "app-{Date}.log", outputTemplate: "[{MachineName}] {ProcessName} {Timestamp} - [{Level}] {Message}{NewLine}{Exception}")
              .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            //Enable Cors on DevEnv
            if (_env.IsDevelopment() || Configuration.GetValue<bool>("EnableCors"))
            {
                //Add Cors policy
                services.AddCors(options =>
                {
                    options.AddPolicy("AllowedOrigin", builder =>
                    {
                        builder.SetIsOriginAllowedToAllowWildcardSubdomains().WithOrigins(Configuration.GetSection("AllowedHosts").Value)
                            .AllowAnyHeader()
                            .AllowAnyMethod().Build();
                    });
                });
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("AllowedOrigin");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
