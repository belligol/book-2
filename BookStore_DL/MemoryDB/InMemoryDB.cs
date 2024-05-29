using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore_Models.Models;

namespace BookStore_DL.MemoryDB
{
    public static class InMemoryDB
    {
        public static List<Author> AuthorData = new List<Author>()
        {
            new Author()
            {
                Id = 1,
                Name = "Author 1",
                BirthDay = DateTime.Now
            },
              new Author()
            {
                Id = 2,
                Name = "Author 2",
                BirthDay = DateTime.Now
            },
                new Author()
            {
                Id = 3,
                Name = "Author 3",
                BirthDay = DateTime.Now
            }
        };


        public static List<Book> BookData = new List<Book>()
        {
            new Book()
            {
                Id = Guid.NewGuid(),
                Title = "Book 1",
                AuthorId = 1
            },
            new Book()
            {
                Id = Guid.NewGuid(),
                Title = "Book 2",
                AuthorId = 2
            },
            new Book() 
            {
                Id = Guid.NewGuid(),
                Title = "Book 3",
                AuthorId = 3
            }
        };
    }
}
