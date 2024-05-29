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
using ZstdSharp.Unsafe;

namespace BookStore_DL.Repositories.Mongo
{
    public class AuthorMongoRepository : IAuthorRepository
    {
        private IOptions<MongoConfiguration> _mongoConfig;
        private readonly IMongoCollection<Author> _author;
        public AuthorMongoRepository(IOptions<MongoConfiguration> mongoConfig) 
        {
            _mongoConfig = mongoConfig;

            var client = new MongoClient(mongoConfig.Value.ConnectionString);

            var db = client.GetDatabase(mongoConfig.Value.DatabaseName);

            _author = db.GetCollection<Author>("Authors");

        }

        public async Task<List<Author>> GetAll()
        {
            return await _author.Find(a => true).ToListAsync();
        }

        public async Task<Author?> GetById(int id)
        {
            var author = await _author.FindAsync(a => a.Id == id);
            return author.FirstOrDefault();
        }

        public async Task Add(Author author)
        {
            await _author.InsertOneAsync(author);
        }

        public async Task Delete(int id)
        {
           await _author.DeleteOneAsync(a => a.Id == id);   
        }
    }
}
