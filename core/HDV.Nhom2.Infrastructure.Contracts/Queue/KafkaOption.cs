using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Infrastructure.Contracts.Queue
{
    public class KafkaProducerOption
    {
        public string Host { get; set; }
    }

    public class KafkaConsumerOption
    {
        public string Host { get; set; }
    }
}
