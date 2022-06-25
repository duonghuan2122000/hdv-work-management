using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HDV.Nhom2.Gateway.HttpApi.Controllers
{
    [Route("kafka-test")]
    [ApiController]
    public class KafkaTestController : ControllerBase
    {
        [HttpPost]
        public async Task PublishMessage()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                ClientId = Dns.GetHostName()
            };

            var producer = new ProducerBuilder<Null, string>(config).Build();

            await producer.ProduceAsync("test", new Message<Null, string> { Value = "Hello" });
        }

        [HttpPost("mail")]
        public async Task PublishMail(EmailDtoQueue emailDtoQueue)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                ClientId = Dns.GetHostName()
            };

            var producer = new ProducerBuilder<Null, string>(config).Build();

            await producer.ProduceAsync("EmailService", new Message<Null, string> { Value = JsonConvert.SerializeObject(emailDtoQueue) });
        }
    }
}
