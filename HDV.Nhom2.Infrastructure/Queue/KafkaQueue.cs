using Confluent.Kafka;
using HDV.Nhom2.Infrastructure.Contracts.Queue;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HDV.Nhom2.Infrastructure.Queue
{
    /// <summary>
    /// Kafka queue
    /// </summary>
    /// CreatedBy: dbhuan 11/06//2022
    public class KafkaProducer<TKey, TValue> : IDisposable, IKafkaProducer<TKey, TValue>
    {
        private readonly IOptions<KafkaProducerOption> _kafkaProducerOptions;

        private readonly IProducer<TKey, TValue> _producer;

        public KafkaProducer(IOptions<KafkaProducerOption> kafkaProducerOptions)
        {
            _kafkaProducerOptions = kafkaProducerOptions;
            var config = new ProducerConfig
            {
                BootstrapServers = _kafkaProducerOptions.Value.Host,
                ClientId = Dns.GetHostName()
            };

            _producer = new ProducerBuilder<TKey, TValue>(config).Build();
        }

        /// <summary>
        /// Thêm một message vào topic
        /// </summary>
        /// CreatedBy: dbhuan 11/06/2022
        /// <param name="topic"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task ProduceAsync(string topic, TKey key, TValue value)
        {
            await _producer.ProduceAsync(topic, new Message<TKey, TValue>
            {
                Key = key,
                Value = value
            });
            Log.Logger.Debug("KafkaProducer-ProduceAsync: topic={topic}, key={@key}, value={@value}", topic, key, value);
        }

        public void Dispose()
        {
            _producer.Flush();
            _producer.Dispose();
        }
    }

    public class KafkaConsumer<TKey, TValue> : IDisposable, IKafkaConsumer<TKey, TValue>
    {
        private readonly IConsumer<TKey, TValue> _consumer;

        private readonly IKafkaHandler<TKey, TValue> _handler;

        public KafkaConsumer(IOptions<KafkaConsumerOption> kafkaConsumerOptions, IKafkaHandler<TKey, TValue> handler) 
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = kafkaConsumerOptions.Value.Host,
                GroupId = "HDV.Nhom2"
            };

            _consumer = new ConsumerBuilder<TKey, TValue>(config).Build();

            _handler = handler;
        }

        /// <summary>
        /// Lấy message từ trong queue ra
        /// </summary>
        /// CreatedBy: dbhuan 11/06/2022
        /// <param name="topic"></param>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public async Task ConsumeAsync(string topic, CancellationToken stoppingToken)
        {
            _consumer.Subscribe(topic);
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = _consumer.Consume(stoppingToken);

                    if(result != null)
                    {
                        Log.Logger.Debug("KafkaConsumer-ConsumeAsync: topic={topic}, key={@key}, value={@value}", topic, result.Message.Key, result.Message.Value);
                        //result.Message.Value;
                        await _handler.HandlerAsync(result.Message.Key, result.Message.Value);
                    }
                }
                catch (Exception ex)
                {
                    Log.Logger.Error("KafkaConsumer-ConsumeAsync-Exception: {ex}", ex);
                }
            }
        }

        public void Dispose()
        {
            _consumer.Close();
        }
    }
}
