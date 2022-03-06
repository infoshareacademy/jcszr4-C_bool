using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_bool.API.DAL.Context;
using C_bool.API.Repositories;
using C_Bool.API.Services;
using Microsoft.EntityFrameworkCore;

namespace C_Bool.API
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
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddDbContext<ApiDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("Database")));
            services.AddTransient<IApReportingGameTaskService, ApiReportingGameTaskService>();
            services.AddTransient<IApiReportingPlaceService, ApiReportingPlaceService>();
            services.AddTransient<IApiReportingUserService, ApiReportingUserService>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "C_Bool.API", Version = "v1" });
            });

            var profileAssembly = typeof(Startup).Assembly;
            services.AddAutoMapper(profileAssembly);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApiDbContext dataContext)
        {
            if (env.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                dataContext.Database.Migrate();
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "C_Bool.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
