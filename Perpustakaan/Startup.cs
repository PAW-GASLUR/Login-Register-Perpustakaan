using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Perpustakaan.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Perpustakaan.Models;

namespace Perpustakaan
{
    /// <summary>
    /// Main Class
    /// </summary>
    /// <remarks>
    /// Class program
    /// </remarks>
    public class Startup
    {
        /// <summary>
        /// Class startup
        /// </summary>
        /// <param name="configuration">Parameter configuration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Function Configuration
        /// </summary>
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

            services.AddDbContext<Models.PERPUSTAKAAN_PAWContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //services.AddDefaultIdentity<IdentityUser>()
            //.AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultUI()
            .AddEntityFrameworkStores<PERPUSTAKAAN_PAWContext>().AddDefaultTokenProviders();


            services.AddAuthorization(options =>
            {
                options.AddPolicy("readonlypolicy", builder => builder.RequireRole("Staff", "Mahasiswa"));
                options.AddPolicy("writepolicy", builder => builder.RequireRole("Staff" ));
                options.AddPolicy("editpolicy", builder => builder.RequireRole("Staff"));
                options.AddPolicy("deletepolicy", builder => builder.RequireRole("Staff"));

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Function Configure App dan Env
        /// </summary>
        /// <param name="app">Parameter app</param>
        /// <param name="env">Parameter env</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
