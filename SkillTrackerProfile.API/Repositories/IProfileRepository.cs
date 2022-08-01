using SkillTrackerProfile.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillTrackerProfile.API.Repositories
{
    public interface IProfileRepository
    {
        Task<ProfileEntity> SaveProfile(ProfileEntity profile);
        ProfileEntity GetProfile(string id);
        IEnumerable<ProfileEntity> GetAllProfiles();
        void DeleteProfile(string id);
        Task<ProfileEntity> UpdateProfile(ProfileEntity profile);
        ProfileEntity SearchByEmpId(string empId);
        IEnumerable<ProfileEntity> SearchBySkillName(string skillName);
        List<ProfileEntity> GetProfilesByname(string name);
    }
}
