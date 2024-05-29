using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_Models.Configurations
{
    public class MongoConfiguration
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
