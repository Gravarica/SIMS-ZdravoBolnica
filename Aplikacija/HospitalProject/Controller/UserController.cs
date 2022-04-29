using HospitalProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Controller
{
    public class UserController
    {
        private UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }
        
        public User Login(string username, string password)
        {
            return userService.Login(username, password);
        }

        public User GetLoggedUser()
        {
            return userService.GetLoggedUser();
        }

        public void Logout()
        {
            userService.Logout();
        }
    }
}
