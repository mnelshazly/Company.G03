using Company.G03.BLL.Interfaces;
using Company.G03.BLL.Repositories;
using Company.G03.DAL.Data.Contexts;
using Company.G03.DAL.Models;
using Company.G03.PL.Helpers;
using Company.G03.PL.Mapping;
using Company.G03.PL.Services;
using Company.G03.PL.Settings;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company.G03.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>(); // Allow DI for DepartmentRepository
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); // Allow DI for EmployeeRepository
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }); // Allow DI for CompanyDbContext

            //builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new DepartmentProfile()));

            #region DI Example

            // Life time
            // builder.Services.AddScoped();    // Create Object (Life Time Per Request) - Unreachable Object
            // builder.Services.AddTransient(); // Create Object (Life Time Per Operation)
            // builder.Services.AddSingleton(); // Create Object (Life Time Per App) 

            builder.Services.AddScoped<IScopedService, ScopedService>(); // Per Request
            builder.Services.AddTransient<ITransientService, TransientService>(); // Per Operation
            builder.Services.AddSingleton<ISingletonService, SingletonService>(); // Per App

            #endregion

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                            .AddEntityFrameworkStores<CompanyDbContext>()
                            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/SignIn";
            });

            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));

            builder.Services.AddScoped<IMailService, MailService>();

            builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection(nameof(TwilioSettings)));

            builder.Services.AddScoped<ITwilioService, TwilioService>();

            builder.Services.AddAuthentication(O =>
            {
                O.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
                O.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            }).AddGoogle( O => {
                O.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                O.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
