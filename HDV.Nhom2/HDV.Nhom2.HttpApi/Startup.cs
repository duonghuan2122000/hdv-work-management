using Autofac;
using HDV.Nhom2.Application.Contracts;
using HDV.Nhom2.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.Nhom2.HttpApi
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
            services.Configure<JwtSetting>(Configuration.GetSection("JwtSettings"));
            services.Configure<MailSetting>(Configuration.GetSection("MailSettings"));

            string connectionString = Configuration.GetConnectionString("Default");
            services.AddDbContext<Nhom2DbContext>(
                options =>
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HDV.Nhom2.HttpApi", Version = "v1" });
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new DIModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HDV.Nhom2.HttpApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<Nhom2Middleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
