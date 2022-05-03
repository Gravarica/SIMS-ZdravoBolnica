﻿using HospitalProject.FileHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Repository
{
    public class UserRepository
    {

        private UserFileHandler userFileHandler;
        private User _user;
        private List<User> users;

        public UserRepository(UserFileHandler userFH)
        {
            userFileHandler = userFH;
            users = userFileHandler.ReadAll().ToList();
            _user = null;
        }
     
        public User Login(String username, String password)
        {
            foreach(User user in users)
            {
                if(user.Username.Equals(username) && user.Password.Equals(password))
                {
                    _user = user;
                    break;
                }
            }

            return _user;
        }

        public User GetUser(string username)
        {
            return users.Find(user => user.Username.Equals(username));
        }

        public User GetLoggedUser()
        {
            return _user; 
        }

        public List<User> GetAll()
        {
            return users;
        }

        public void Create(User user)
        {
            users.Add(user);
            userFileHandler.AppendLineToFile(user);
        }

        public void Delete(String username)
        {
            users.Remove(GetUser(username));
            userFileHandler.Save(users);
        }

        public void Update(User user)
        {
            User updateUser = GetUser(user.Username);
            
            updateUser.Password = user.Password;
            
        }

        public void Logout()
        {
            _user = null;
        }

    }
}
