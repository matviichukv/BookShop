using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entity
{
    public class Review
    {
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public int ThumbUp { get; set; }
        public int ThumbDown { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
