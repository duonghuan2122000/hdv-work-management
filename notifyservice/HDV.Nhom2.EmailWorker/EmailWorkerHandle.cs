using Confluent.Kafka;
using HDV.Nhom2.Infrastructure.Contracts.Queue;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HDV.Nhom2.EmailWorker
{
    /// <summary>
    /// Xử lý emailworker
    /// </summary>
    /// CreatedBy: dbhuan 11/06/2022
    public class EmailWorkerHandle : BackgroundService
    {
        private readonly IKafkaConsumer<Null, string> _consumer;
        public EmailWorkerHandle(IKafkaConsumer<Null, string> consumer)
        {
            _consumer = consumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _consumer.ConsumeAsync("HDV_EmailService", stoppingToken);
                }
                catch(Exception ex)
                {
                    Log.Logger.Debug("EmailWorkerHandle-Exception: {ex}", ex);
                }
            }
        }
    }
}
