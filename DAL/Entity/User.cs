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
        public User()
        {
            Roles = new List<Role>();
        }

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

        [Required]
        [StringLength(maximumLength:255)]
        public string UserName { get; set; }

        public virtual ICollection<Role> Roles { get; private set; }
        
        public int ImageId { get; set; }
        public virtual Image Avatar { get; set; }
    }
}
