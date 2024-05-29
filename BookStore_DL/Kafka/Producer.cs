using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_DL.Kafka
{
    public class ProducerOrigin<TKey, TValue> 
    {
        public  IProducer<TKey, TValue> _producer;

        public ProducerOrigin()
        {
            var config = new ProducerConfig()
            {
                BootstrapServers = "sulky.srvs.cloudkafka.com:9094",
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslUsername = "wrlgoryr",
                SaslPassword = "cfcSFJUaHz7tVE-wButfZIb51GTR-3lx",
                SaslMechanism = SaslMechanism.ScramSha512
            };
            _producer = new ProducerBuilder<TKey, TValue>(config)
                .SetKeySerializer(new MsgPackSerializer<TKey>())
                .SetValueSerializer(new MsgPackSerializer<TValue>())
                .Build();
        }

        public async Task ProduceMessage(string topic, Message<TKey, TValue> msg)
        {
            await _producer.ProduceAsync(topic, msg);
        }



    }
}
