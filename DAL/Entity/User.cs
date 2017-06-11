using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entity
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:20,MinimumLength = 3)]
        public string Login { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength:255)]
        public string Password { get; set; }
    }
}
