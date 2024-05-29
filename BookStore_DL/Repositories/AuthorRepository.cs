using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore_DL.Interfaces;
using BookStore_DL.MemoryDB;
using BookStore_Models.Models;

namespace BookStore_DL.Repositories
{
    public class AuthorRepository //: IAuthorRepository
    {
        public  void Add(Author author)
        {
            InMemoryDB.AuthorData.Add(author);
        }

        public void Delete(int id)
        {
            var author = GetById(id);
            if (author != null)
            {
                InMemoryDB.AuthorData.Remove(author);
            }
            
        }

        public List<Author> GetAll()
        {
            return InMemoryDB.AuthorData;
        }

        public Author? GetById(int id)
        {
            return InMemoryDB.AuthorData.FirstOrDefault(a => a.Id == id);
        }
    }
}
