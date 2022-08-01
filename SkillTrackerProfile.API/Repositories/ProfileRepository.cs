using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillTrackerProfile.API.Services;
using SkillTrackerProfile.API.Models;

namespace SkillTrackerProfile.API.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationContext _context;

        public ProfileRepository(ApplicationContext dbcontext)
        {
            this._context = dbcontext;
        }

        public void DeleteProfile(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProfileEntity> GetAllProfiles()
        {
            return _context.Profile.ToList();
        }

        public ProfileEntity GetProfile(string id)
        {
            return _context.Profile.FirstOrDefault(s => s.EmpId == id);
        }

        public async Task<ProfileEntity> SaveProfile(ProfileEntity profile)
        {
            await this._context.Profile.AddAsync(profile);
            await this._context.SaveChangesAsync();
            return profile;
        }

        public async Task<ProfileEntity> UpdateProfile(ProfileEntity profile)
        {
            ProfileEntity existingProfile = _context.Profile.FirstOrDefault(s => s.EmpId == profile.EmpId || s.UserId == profile.UserId);
            if (existingProfile != null)
            {
                foreach (var skill in profile.Skills)
                {
                    var exSkill = existingProfile.Skills.FirstOrDefault(s => s.Name.Equals(skill.Name, StringComparison.OrdinalIgnoreCase));
                    if (exSkill != null)
                        exSkill.Proficiency = skill.Proficiency;
                    else 
                    {
                        Skill newSkill = new Skill();
                        newSkill.Name = skill.Name;
                        newSkill.IsTechnical = skill.IsTechnical;
                        newSkill.Proficiency = skill.Proficiency;
                        existingProfile.Skills.Add(newSkill);
                    }
                }

                existingProfile.LastModifiedDate = DateTime.Now;

                this._context.Update(existingProfile);
                await this._context.SaveChangesAsync();
                return profile;
            }
            else
                return null;           
        }

        public ProfileEntity SearchByEmpId(string empId)
        {
            return _context.Profile.AsEnumerable().FirstOrDefault(s => s.EmpId.ToLower() == empId.ToLower());
        }

        public List<ProfileEntity> GetProfilesByname(string name)
        {
            var pro = _context.Profile;
            Console.Write(_context.Profile.ToList());

            return _context.Profile.AsEnumerable().Where(s => s.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public IEnumerable<ProfileEntity> SearchBySkillName(string skillName)
        {
            return _context.Profile.AsEnumerable().Where(s => s.Skills.Any(k => k.Name.Equals(skillName, StringComparison.OrdinalIgnoreCase)));
        }
    }
}


