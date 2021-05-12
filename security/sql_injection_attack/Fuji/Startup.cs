using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Fuji.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;
using Fuji.Data.Abstract;
using Fuji.Data.Concrete;

namespace Fuji
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("AuthenticationConnection")));
            services.AddDbContext<FujiDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("FujiConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddScoped<IAppleRepository, AppleRepository>();
            services.AddScoped<IFujiUserRepository, FujiUserRepository>();
            //services.AddSingleton<>
            //services.AddTransient<>

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()                   // enable roles
                .AddEntityFrameworkStores<ApplicationDbContext>();
            // Customize some settings that Identity uses
            services.Configure<IdentityOptions>(opts => {
                // lines without comments are the default settings that we get by "AddDefault" above
                opts.Password.RequireDigit = true;
                opts.Password.RequireLowercase = true;
                opts.Password.RequireNonAlphanumeric = false;   // default is true
                opts.Password.RequireUppercase = true;
                opts.Password.RequiredLength = 8;           // default is 6
                opts.Password.RequiredUniqueChars = 5;      // default is 1
                opts.SignIn.RequireConfirmedEmail = false;
                opts.SignIn.RequireConfirmedPhoneNumber = false;
                opts.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzBCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";   // removed letter 'A'
                opts.User.RequireUniqueEmail = true;        // default is false
            });
            services.AddControllersWithViews();
            // Added to enable runtime compilation
            services.AddRazorPages().AddRazorRuntimeCompilation();

            // Change the overriding authorization strategy from 
            //     "allow unless explicitly authorized" (the default)
            // to
            //     "require authorization unless explicitly allowed for anyone"
            services.AddAuthorization(opts => {
                opts.FallbackPolicy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();
            });
      
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
