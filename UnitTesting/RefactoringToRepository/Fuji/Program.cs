using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Fuji.Utilities;
using Fuji.Data;

namespace Fuji
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // After Build has been called, all services have been registered (by running Startup)
            // By using a scope for the services to be requested below, we limit their lifetime to this set of calls.
            // See: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-5.0#call-services-from-main
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    // Get the IConfiguration service that allows us to query user-secrets and 
                    // the configuration on Azure
                    var config = host.Services.GetRequiredService<IConfiguration>();
                    // Set password with the Secret Manager tool, or store in Azure app configuration
                    // dotnet user-secrets set SeedUserPW <pw>

                    var testUserPw = config["SeedUserPW"];
                    var adminPw = config["SeedAdminPW"];

                    SeedUsers.Initialize(services, SeedData.UserSeedData, testUserPw).Wait();
                    SeedUsers.InitializeAdmin(services, "admin@example.com", "admin", adminPw, "The", "Admin").Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
            // Go ahead and run the app, everything should be ready
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}


/*
   public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

*/

/*
 * var host = CreateHostBuilder(args).Build();
            // After Build has been called, all services have been registered (by running Startup)
            // By using a scope for the services to be requested below, we limit their lifetime to this set of calls.
            // See: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-5.0#call-services-from-main
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    // Get the IConfiguration service that allows us to query user-secrets and 
                    // the configuration on Azure
                    var config = host.Services.GetRequiredService<IConfiguration>();
                    // Set password with the Secret Manager tool, or store in Azure app configuration
                    // dotnet user-secrets set SeedUserPW <pw>

                    var testUserPw = config["SeedUserPW"];

                    SeedUsers.Initialize(services, SeedData.UserSeedData, testUserPw).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
            // Go ahead and run the app, everything should be ready
            host.Run();
 */