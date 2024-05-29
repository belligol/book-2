using BookStore_DL.Kafka;
using BookStore_Models.Models;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_BL.BackgroundJobs
{
    public class BookConsumerService : IHostedService
    {
        public IConsumer<Guid, Book> _consumer;
        static Dictionary<Guid, Book> _books = new Dictionary<Guid, Book>();

        public BookConsumerService()
        {
            var config = new ConsumerConfig()
            {
                BootstrapServers = "sulky.srvs.cloudkafka.com:9094",
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslUsername = "wrlgoryr",
                SaslPassword = "cfcSFJUaHz7tVE-wButfZIb51GTR-3lx",
                SaslMechanism = SaslMechanism.ScramSha512,
                GroupId = $"Nacho.{Guid.NewGuid()}",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Guid, Book>(config)
                .SetKeyDeserializer(new MessagePackDeserializer<Guid>())
                .SetValueDeserializer(new MessagePackDeserializer<Book>())
                .Build();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                _consumer.Subscribe("wrlgoryr-books");
                while (!cancellationToken.IsCancellationRequested)
                {
                   var result = _consumer.Consume();
                   if (result == null)
                   {
                        continue;
                   }
                   _books.Add(result.Key, result.Value);
                }
            });


            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
