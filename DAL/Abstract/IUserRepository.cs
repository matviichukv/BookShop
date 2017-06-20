using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        List<string> GetEmails();
        User GetUserByEmail(string Email);
        void UpdateUser(User updateUser);
    }
}
