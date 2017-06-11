using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class Book
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public DateTime DatePublished { get; set; }
        public int PublisherId { get; set; }
        public Publisher BookPublisher;
        public int AuthorId { get; set; }
        public Author BookAuthor { get; set; }
        public int Volume { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
    }
}
