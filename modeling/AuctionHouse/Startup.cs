using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionHouse.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AuctionHouse
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
            // Changes made during the video on deploying and using a SQL Server database in Azure
            // Store the connection string in appsettings.json (but take out the password part)
            // then build it here and add in the password which we store in user-secrets
            var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("AuctionHouseConnectionAzure"));
            builder.Password = Configuration["AuctionHouse:DBPassword"];

            services.AddControllersWithViews();
            services.AddDbContext<AuctionHouseDbContext>(options =>
                //options.UseSqlServer(Configuration.GetConnectionString("AuctionHouseConnection"))
                options.UseSqlServer(builder.ConnectionString)      // switch back and forth here between a local db and cloud db
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
