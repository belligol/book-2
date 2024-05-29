using BookStore_BL.Interfaces;
using BookStore_DL.Interfaces;
using BookStore_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_BL.Services
{
    public class BookService : IBookService
    {
       
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task Add(Book book)
        {
             await _bookRepository.Add(book);
        }

        public async Task Delete(Guid id)
        {
            await _bookRepository.Delete(id);
        }

   
        public async Task<List<Book>> GetAll()
        {
            return await _bookRepository.GetAll();
        }

        public async Task<List<Book>> GetAllByAuthorId(int authorId)
        {
            return await _bookRepository.GetAllByAuthorId(authorId);
        }

        public async Task<Book?> GetById(Guid id)
        {
            return await _bookRepository.GetById(id);
        }
    }
}

