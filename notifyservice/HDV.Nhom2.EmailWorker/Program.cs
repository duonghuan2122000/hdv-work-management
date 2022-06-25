using HDV.Nhom2.Infrastructure.CallService;
using HDV.Nhom2.Infrastructure.Contracts.CallService;
using HDV.Nhom2.Infrastructure.Contracts.Queue;
using HDV.Nhom2.Infrastructure.Queue;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.Nhom2.EmailWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Logger.Debug("Start");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureServices(ConfigureServices);

        public static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            IConfiguration configuration = hostContext.Configuration;

            services.Configure<KafkaConsumerOption>(configuration.GetSection("Kafka"));
            services.Configure<EmailServiceOption>(configuration.GetSection("EmailService"));

            services.AddTransient<ICallService, CallService>();
            services.AddTransient(typeof(IKafkaConsumer<,>), typeof(KafkaConsumer<,>));
            services.AddTransient(typeof(IKafkaHandler<,>), typeof(EmailKafkaHandler<,>));


            services.AddHostedService<EmailWorkerHandle>();
        }
    }
}
