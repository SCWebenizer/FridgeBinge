using FridgeBinge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgeBinge.Services
{
    public class SecurityService
    {
        readonly UserDAO userDAO = new();

        public SecurityService()
        {
           
        }

        public bool IsValid(UserModel user)
        {
            return userDAO.FindUserByNameAndPassword(user);
        }
    }
}
