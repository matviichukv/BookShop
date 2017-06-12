using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class Book
    {
        public int BookId { get; set; }
        [Required]
        public string BookName { get; set; }
        public DateTime DatePublished { get; set; }
        public int Volume { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }

        public int PublisherId { get; set; }
        public Publisher BookPublisher;
        
        public int ImageId { get; set; }
        public Image BookImage { get; set; }

        public int CategoryId { get; set; }
        public virtual Category BookCategory { get; set; }

        public int AuthorId { get; set; }
        public Author BookAuthor { get; set; }
    }
}
