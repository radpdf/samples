using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RadPdf;

namespace RadPdfCoreDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Default RAD PDF session provider relies on ASP.NET session state.
            //A custom session provider can be used to avoid use of this.
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();

            // Default RAD PDF session provider relies on ASP.NET session state, so call this before .UseRadPdf()
            // A custom session provider can be used to avoid use of this.
            app.UseSession();


            // Create middleware settings
            RadPdfCoreMiddlewareSettings settings = new RadPdfCoreMiddlewareSettings()
            {
                // Add SQL Server Connection String, if not using Lite Documents
                // Sample connection string below connects to a SQL Server Express instance on localhost
                // TrustServerCertificate=True is set to avoid a trust exception (e.g. "The certificate chain was issued by an authority that is not trusted.")
                ConnectionString = @"Server=.\SQLExpress;Database=RadPdf;Trusted_Connection=Yes;TrustServerCertificate=True;",

                // Add License Key
                LicenseKey = "DEMO"

                // To run RAD PDF without the System Service, add UseService = false
                // If using Lite Documents without the System Service, a LiteStorageProvider must also be implemented
            };

            // Add RAD PDF's middleware to app
            app.UseRadPdf(settings);

            app.UseMvc();
        }
    }
}
