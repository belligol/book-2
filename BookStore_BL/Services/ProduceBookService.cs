using BookStore_BL.Interfaces;
using BookStore_DL.Kafka;
using BookStore_Models.Models;
using Confluent.Kafka;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MongoDB.Driver.WriteConcern;

namespace BookStore_BL.Services
{
    public class ProduceBookService : IProduceBookService
    {
        private readonly ProducerOrigin<Guid, Book> _producer;

        public ProduceBookService()
        {
            _producer = new ProducerOrigin<Guid, Book>();
        }
        public async Task ProduceBook(Book book)
        {
            await _producer.ProduceMessage("wrlgoryr-books", new Message<Guid, Book>
            {
               Key = book.Id,
               Value = book
            });
        }

    }
}
