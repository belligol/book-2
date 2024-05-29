using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_Models.Models.Responses
{
    public class AuthenticationResult
    {
        public string Token {get;set;}  = string.Empty; 
        public bool isSuccessfull { get;set;}
        public IEnumerable<string> Errors { get;set;}
    }
}
