﻿using BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using DAL.Concrete;
using DAL.Entity;

namespace BLL.Concrete
{
    class UserProvider : IUserProvider
    {
        UserRepository userRepository = new UserRepository();

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
    }
}