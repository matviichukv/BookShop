using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class Author
    {
        public Author()
        {
            WrittenBooks = new List<Book>();
        }

        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public ICollection<Book> WrittenBooks { get; set; }
        public int NationalityId { get; set; }
        public Nationality AuthorNationality { get; set; }
    }
}
