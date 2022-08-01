using System;
using System.Collections.Generic;
using System.Text;
using SkillTrackerIdentity.API.Models;

namespace SkillTrackerIdentity.API.Services
{
    public interface IUserService
    {
        User GetUser(string Username, string Password);
    }
}
