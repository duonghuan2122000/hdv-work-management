using HDV.Nhom2.Gateway.BL;
using HDV.Nhom2.Infrastructure.CallService;
using HDV.Nhom2.Infrastructure.Contracts.CallService;
using HDV.Nhom2.Infrastructure.Contracts.Queue;
using HDV.Nhom2.Infrastructure.Queue;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace HDV.Nhom2.Gateway.HttpApi
{
    public class Startup
    {
        private readonly string DefaultCORS = "Default";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                });
            services.AddCors(options =>
                options.AddPolicy(DefaultCORS, policy =>
                    policy.AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                )
            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HDV.Nhom2.Gateway.HttpApi", Version = "v1" });
            });

            services.Configure<KafkaProducerOption>(Configuration.GetSection("Kafka"));
            services.Configure<AuthServiceOption>(Configuration.GetSection("AuthService"));
            services.Configure<CompanyServiceOption>(Configuration.GetSection("CompanyService"));
            services.Configure<ProjectServiceOption>(Configuration.GetSection("ProjectService"));
            services.Configure<ProjectNetServiceOption>(Configuration.GetSection("ProjectNetService"));

            services.AddTransient<ICallService, CallService>();
            services.AddTransient(typeof(IKafkaProducer<,>), typeof(KafkaProducer<,>));

            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<ITaskService, TaskService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HDV.Nhom2.Gateway.HttpApi v1"));
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HDV.Nhom2.Gateway.HttpApi v1"));

            app.UseHttpsRedirection();

            app.UseMiddleware<Nhom2Middleware>();

            app.UseRouting();
            app.UseCors(DefaultCORS);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}