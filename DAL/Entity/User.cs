﻿using System;
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

        public int UserId { get; set; }
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }
        [Required]
        [StringLength(maximumLength:255)]
        public string UserPassword { get; set; }
        [Required]
        [StringLength(maximumLength:255)]
        public string UserName { get; set; }
        
        public int ImageId { get; set; }
        public virtual Image Avatar { get; set; }

        public virtual ICollection<Role> Roles { get; private set; }
    }
}
