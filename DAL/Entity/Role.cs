﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class Role
    {
        public Role()
        {
            Users = new List<User>();
        }

        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; private set; }
    }
}
