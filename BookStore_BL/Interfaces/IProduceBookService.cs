using BookStore_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_BL.Interfaces
{
    public interface IProduceBookService
    {
        public Task ProduceBook(Book book);
    }
}
