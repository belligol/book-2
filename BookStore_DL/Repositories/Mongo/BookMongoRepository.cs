using BookStore_DL.Interfaces;
using BookStore_Models.Configurations;
using BookStore_Models.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_DL.Repositories.Mongo
{
    public class BookMongoRepository : IBookRepository
    {
        private IOptions<MongoConfiguration> _mongoConfig;
        private readonly IMongoCollection<Book> _books;

        public BookMongoRepository(
            IOptions<MongoConfiguration> mongoConfig)
        {
            _mongoConfig = mongoConfig;

            var client = new MongoClient(mongoConfig.Value.ConnectionString);

            var db = client.GetDatabase(mongoConfig.Value.DatabaseName);

            _books = db.GetCollection<Book>("Books");

        }

        public async Task Add(Book book)
        {
            await _books.InsertOneAsync(book);
        }

        public async Task Delete(Guid id)
        {
           await _books.DeleteOneAsync(b => b.Id == id);

        }

        public async Task<List<Book>> GetAll()
        {
            return await _books.Find(b => true).ToListAsync();
      
        }

        public async Task<List<Book>> GetAllByAuthorId(int authorId)
        {
            return await _books.Find(a => a.AuthorId == authorId).ToListAsync();
        }

        public async Task<Book?> GetById(Guid id)
        {
            var book = await _books.FindAsync(b => b.Id==id);
            return book.FirstOrDefault();
        }

       
    }
}
