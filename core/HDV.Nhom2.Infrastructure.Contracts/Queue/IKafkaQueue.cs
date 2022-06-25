using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HDV.Nhom2.Infrastructure.Contracts.Queue
{
    /// <summary>
    /// Producer kafka
    /// </summary>
    /// CreatedBy: dbhuan 11/06/2022
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface IKafkaProducer<TKey, TValue>
    {
        /// <summary>
        /// Thêm một message vào topic
        /// </summary>
        /// CreatedBy: dbhuan 11/06/2022
        /// <param name="topic"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task ProduceAsync(string topic, TKey key, TValue value);
    }

    public interface IKafkaConsumer<TKey, TValue>
    {
        /// <summary>
        /// Lấy message từ trong queue ra
        /// </summary>
        /// CreatedBy: dbhuan 11/06/2022
        /// <param name="topic"></param>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        Task ConsumeAsync(string topic, CancellationToken stoppingToken);
    }

    public interface IKafkaHandler<TKey, TValue>
    {
        /// <summary>
        /// Xử lý msg từ queue
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task HandlerAsync(TKey key, TValue value);
    }
}
