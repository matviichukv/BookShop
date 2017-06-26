using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    //hello guys first commit "serg commit" 
    public class Author
    {
        public Author()
        {
            WrittenBooks = new List<Book>();
        }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }

        public int NationalityId { get; set; }
        public Nationality AuthorNationality { get; set; }
        [DefaultValue(44)]
        public int ImageId { get; set; }
        public virtual Image AuthorImage { get; set; }

        public ICollection<Book> WrittenBooks { get; set; }
    }
}
