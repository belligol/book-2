using AspNetCore.Identity.MongoDbCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookStore_Models.Models
{
    public class User : MongoIdentityUser<Guid>
    {
        public User() { }
        public User(string userName, string password) : base(userName, password)
        {
           
        }
        public string AuthorId { get; set; } = string.Empty;
    }
}
