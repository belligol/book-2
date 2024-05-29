using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore_Models.Models;

namespace BookStore_DL.Interfaces
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAll();

        Task<Author?> GetById(int id);

        Task Add(Author author);

        Task Delete(int id);
    }
}
