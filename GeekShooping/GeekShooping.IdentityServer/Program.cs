using Duende.IdentityServer.AspNetIdentity;
using Duende.IdentityServer.Services;
using GeekShooping.IdentityServer.Model;
using GeekShooping.IdentityServer.Model.Context;
using GeekShopping.IdentityServer.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GeekShooping.IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<SqlContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlContext") ?? throw new InvalidOperationException("Connection string 'SqlContext' not found.")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
              .AddEntityFrameworkStores<SqlContext>()
              .AddDefaultTokenProviders();

            var uilder = builder.Services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;
            }).AddInMemoryIdentityResources(
                       IdentityConfiguration.IdentityResources)
                   .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
                   .AddInMemoryClients(IdentityConfiguration.Clients)
                   .AddAspNetIdentity<ApplicationUser>();

           
            uilder.AddDeveloperSigningCredential();

            builder.Services.AddControllersWithViews();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}