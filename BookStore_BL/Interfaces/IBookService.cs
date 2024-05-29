using BookStore_Models.Models;

namespace BookStore_BL.Interfaces
{
    public interface IBookService
    {
        Task<List<Book>>GetAll();

        Task<List<Book>> GetAllByAuthorId(int authorId);

        Task<Book?> GetById(Guid id);

        Task Add(Book book);

        Task Delete(Guid id);
    }
}
