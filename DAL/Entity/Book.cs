using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
<<<<<<< HEAD
    class Book
    {
        //Vitaliy
=======
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
>>>>>>> 35a9c71ee00c61d06a9fa196198bd006a159f43d
    }
}
