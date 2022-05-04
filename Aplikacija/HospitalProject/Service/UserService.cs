using HospitalProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Service
{
    public class UserService
    {

        private UserRepository userRepository;

        public UserService(UserRepository userRepo)
        {
            userRepository = userRepo;
        }

        public void Create(User user)
        {
            userRepository.Create(user);
        }

        public User Login(string username, string password)
        {
            return userRepository.Login(username, password);
        }
        

        public User GetLoggedUser()
        {
            return userRepository.GetLoggedUser();
        }

        public void Logout()
        {
            userRepository.Logout();
        }
        public void Delete(string username) => userRepository.Delete(username);

    }
}
