using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class Publisher
    {
        public Publisher()
        {
            PublishedBooks = new List<Book>();
        }

        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public string PublisherCity { get; set; }

        public ICollection<Book> PublishedBooks { get; private set; }
    }
}
