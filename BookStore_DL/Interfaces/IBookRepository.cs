

using BookStore_Models.Models;

namespace BookStore_DL.Interfaces
{
    public interface IBookRepository
    {
        Task <List<Book>> GetAll();

        Task <List<Book>> GetAllByAuthorId(int authorId);

        Task <Book?> GetById(Guid id);

        Task Add(Book book);

        Task Delete(Guid id);
    }
}
