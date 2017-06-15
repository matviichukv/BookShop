using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class UserCreateViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public enum UserStatus
    {
        Success = 1,
        DublicationEmail = 2
    }
}
