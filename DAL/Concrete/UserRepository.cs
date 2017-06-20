using DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entity;

namespace DAL.Concrete
{
    public class UserRepository : IUserRepository
    {
        BookShopContext ctx = new BookShopContext();

        public User CreateUser(User user)
        {
            ctx.Users.Add(user);
            ctx.SaveChanges();
            return user;
        }

        public void UpdateUser(User updateUser)
        {
            var oldUser = GetUserByEmail(updateUser.UserEmail);
            oldUser = updateUser;
            ctx.SaveChanges();
        }

        public List<string> GetEmails()
        {
            List<string> result = new List<string>();

            foreach (var user in ctx.Users)
            {
                result.Add(user.UserEmail);
            }

            return result;
        }

        public User GetUserByEmail(string Email)
        {
            return ctx.Users
                .Include(u => u.Avatar)
                .FirstOrDefault(t => t.UserEmail == Email);
        }
    }
}
