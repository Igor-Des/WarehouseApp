using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using WarehouseApp.Data;
using WarehouseApp.Middleware;
using WarehouseApp.Models;
using WarehouseApp.Services;

namespace WarehouseApp
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
            services.AddMvc();
            services.AddControllersWithViews();

            string connectionDB = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<WarehouseContext>(options => options.UseSqlServer(connectionDB));

            services.AddMemoryCache();
            services.AddDistributedMemoryCache();

            services.AddResponseCompression(options => options.EnableForHttps = true);
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddSession();
            services.AddScoped<ICached<Supplier>, CachedSupplier>();
            services.AddScoped<ICached<TypeComponent>, CachedTypeComponent>();
            services.AddScoped<ICached<Component>, CachedComponent>();
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
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();
            app.UseRouting();


            // добавляем компонента miidleware по инициализации базы данных
            // app.UseDbInitializer();

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
