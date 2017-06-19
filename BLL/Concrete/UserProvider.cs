using BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using DAL.Concrete;
using DAL.Entity;
using DAL.Abstract;

namespace BLL.Concrete
{
    public class UserProvider : IUserProvider
    {
        IUserRepository userRepository = new UserRepository();

        public UserStatus CreateUser(UserCreateViewModel userModel)
        {
            if (userRepository.GetEmails().Contains(userModel.Email))
                return UserStatus.DublicationEmail;

            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            User newUser = new User()
            {
                UserEmail = userModel.Email,
                UserName = userModel.UserName,
                Salt = salt,
                UserPassword = BCrypt.Net.BCrypt.HashPassword(userModel.Password, salt)
            };

            userRepository.CreateUser(newUser);
            return UserStatus.Success;
        }

        public UserInfoViewModel GetUserInfo(string Email, string password)
        {
            User user = userRepository.GetUserByEmail(Email);

            if(user == null)
            {
                return null;
            }

            UserInfoViewModel userInfo = null;

            if(BCrypt.Net.BCrypt.Verify(password, user.UserPassword))
            {
                userInfo = new UserInfoViewModel()
                {
                    UserId = user.UserId,
                    AvatarPath = user.Avatar == null ? null : user.Avatar.PathToImageFile,
                    UserEmail = user.UserEmail,
                    UserName = user.UserName,
                    UserPassword = user.UserPassword
                };
            }

            return userInfo;
        }
    }
}
