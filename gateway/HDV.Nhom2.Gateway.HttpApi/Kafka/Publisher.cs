using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HDV.Nhom2.Gateway.HttpApi
{
    public class Publisher
    {
        private readonly ProducerConfig _config;

        public Publisher()
        {
            _config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                ClientId = Dns.GetHostName()
            };


        }

        public IProducer<Null, string> Connect()
        {
            var producer = new ProducerBuilder<Null, string>(_config).Build();
            return producer;
        }
    }
}
