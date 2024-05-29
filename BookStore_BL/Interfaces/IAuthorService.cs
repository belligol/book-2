using BookStore_Models.Models;

namespace BookStore_BL.Interfaces
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAll();

        Task<Author?> GetById(int id);

        Task Add(Author author);

        Task Delete(int id);
    }
}
