

using BookStore_DL.Interfaces;
using BookStore_DL.MemoryDB;
using BookStore_Models.Models;

namespace BookStore_DL.Repositories
{
    public class BookRepository 
    {     

        public void Add(Book book)
        {
            InMemoryDB.BookData.Add(book);
        }

        public void Delete(Guid id)
        {
            var book = GetById(id);
            if (book != null) 
            {
                InMemoryDB.BookData.Remove(book);
            }
        }

        public List<Book> GetAll()
        {
            return InMemoryDB.BookData;
        }

        public List<Book> GetAllByAuthorId(int authorId)
        {
            return InMemoryDB.BookData.Where(b => b.AuthorId == authorId).ToList();
        }

        public Book? GetById(Guid id)
        {
            return InMemoryDB.BookData.FirstOrDefault(b => b.Id == id);
        }
    }
}
