using C_bool.BLL.Config;
using C_bool.BLL.DAL.Context;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Helpers;
using C_bool.BLL.Repositories;
using C_bool.BLL.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using System.Net.Mail;
using C_bool.BLL.Config;
using C_bool.WebApp.Middleware;
using Serilog.Ui.MsSqlServerProvider;
using Serilog.Ui.Web;

namespace C_bool.WebApp
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            // app configuration
            services.Configure<GoogleAPISettings>(Configuration.GetSection("GoogleAPISettings"));

            // repository & database context
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("Database")));
            services.AddDatabaseDeveloperPageExceptionFilter();


            services.AddDefaultIdentity<User>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Lockout.MaxFailedAccessAttempts = 3;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = true;
                })
                .AddRoles<UserRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            // razor pages
            services.AddRazorPages();

            // services
            services.AddTransient<IPlaceService, PlaceService>();
            services.AddTransient<IGooglePlaceService, GooglePlaceService>();
            services.AddTransient<IGameTaskService, GameTaskService>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IEmailSenderService, EmailSenderService>();

            services.AddTransient<IMessagingService, MessagingService>();

            services.AddTransient<DatabaseSeeder>();

            services.AddSerilogUi(options => options
                .EnableAuthorization(authOptions =>
                {
                    authOptions.AuthenticationType = AuthenticationType.Cookie;
                    authOptions.Roles = new[] { "Admin" };
                })
                .UseSqlServer(Configuration.GetConnectionString("Database"), "Logs"));



            //Google API Http client
            services.AddHttpClient("GoogleMapsClient", client =>
            {
                client.BaseAddress = new Uri("https://maps.googleapis.com/");
                client.Timeout = new TimeSpan(0, 0, 30);
                client.DefaultRequestHeaders.Clear();
            });

            //automapper
            var profileAssembly = typeof(Startup).Assembly;
            services.AddAutoMapper(profileAssembly);

            //FluentEmail
            services
                .AddFluentEmail(Configuration["SmtpClient:UserName"])
                .AddSmtpSender(new SmtpClient()
                {
                    Host = Configuration["SmtpClient:Host"],
                    Port = int.Parse(Configuration["SmtpClient:Port"]),
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(
                        Configuration["SmtpClient:UserName"],
                        Configuration["SmtpClient:Password"])
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dataContext, DatabaseSeeder seeder)
        {
            if (env.IsDevelopment() || env.IsEnvironment("Maciej") || env.IsEnvironment("Andrzej") || env.IsEnvironment("Natalia"))
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

            app.UseSerilogUi();

            //Error handler middleware
            app.UseMiddleware<ErrorHandlerMiddleware>();

            dataContext.Database.Migrate();

            seeder.Seed().Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}