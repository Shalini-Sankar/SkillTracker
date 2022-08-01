using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillTrackerIdentity.API.Models;

namespace SkillTrackerIdentity.API.Services
{
    public class UserService : IUserService
    {
        //private readonly ApplicationContext _context;
        public UserService() {}
        //public UserService(ApplicationContext context)
        //{
        //    _context = context;
        //}
        public User GetUser(string UserName,string Password)
        {
            List<User> Users = new List<User>() {
                new User{ UserId = 1, UserName = "admin", Password = "admin", Role = "admin"},
                new User{ UserId = 2, UserName = "emp1", Password = "test123", Role = "employee"},
                new User{ UserId = 3, UserName = "emp2", Password = "test234", Role = "employee" }};

            return Users.Where(e => e.UserName == UserName && e.Password == Password).FirstOrDefault();
        }
    }
}
