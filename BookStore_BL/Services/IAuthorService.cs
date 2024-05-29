
using BookStore_BL.Interfaces;
using BookStore_DL.Interfaces;
using BookStore_DL.Repositories;
using BookStore_Models.Models;

namespace BookStore_BL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private AuthorRepository authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public AuthorService(AuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        public async Task Add(Author author)
        {
            await _authorRepository.Add(author);
        }

        public async Task Delete(int id)
        {
            await _authorRepository.Delete(id);
        }

        public async Task<List<Author>> GetAll()
        {
            return await _authorRepository.GetAll();  
        }

        public async Task<Author?> GetById(int id)
        {
            return await _authorRepository.GetById(id);
        }
    }
}
