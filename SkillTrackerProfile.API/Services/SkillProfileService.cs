using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using SkillTrackerProfile.API.Models;
using SkillTrackerProfile.API.Repositories;
using SkillTrackerProfile.API.Features.Commands;
using SkillTrackerProfile.API.Features.Queries;

namespace SkillTrackerProfile.API.Services
{
    public class SkillProfileService : ISkillProfileService
    {        
        private readonly IProfileRepository _profileRepository;
        private readonly ILogger<SkillProfileService> _logger;

        public SkillProfileService(ILogger<SkillProfileService> logger, IProfileRepository profileRepository)
        {
            _logger = logger;
            _profileRepository = profileRepository;
        }
        public IEnumerable<ProfileEntity> GetAllProfiles()
        {
            return _profileRepository.GetAllProfiles();
        }

        public IEnumerable<ProfileEntity> SearchProfile(SearchProfileQuery request)
        {
            _logger.LogInformation($"Search with EmpId={request.EmpId} Name={request.Name} Skill={request.Skill}");
            return  Search(request);
        }

        public async Task<string> AddProfile(AddProfileCommand request)
        {
            var userId1 = await SaveProfileInfo(request);
            var userId =  SavePersonalInfo(request);
             SaveSkills(request);

            _logger.LogInformation($"Profile {request.EmpId} is successfully created.");

            return userId;
        }

        public async Task<string> UpdateProfile(UpdateProfileCommand request, string userId)
        {
            ProfileEntity profileInfo = new ProfileEntity
            {
                EmpId = request.EmpId,
                UserId = userId,
                Skills = request.Skills
            };
            var profile = await _profileRepository.UpdateProfile(profileInfo);
            if (profile != null)
            {
                _logger.LogInformation($"Profile {request.EmpId} is successfully updated.");

                return request.EmpId;
            }
            else 
                return null;
        }

        private async Task<string> SaveProfileInfo(AddProfileCommand request)
        {
            var profileInfo = new ProfileEntity();
            profileInfo.Name = request.Name;
            profileInfo.Email = request.Email;
            profileInfo.Mobile = request.Mobile;
            profileInfo.EmpId = request.EmpId;
            profileInfo.UserId = $"user{profileInfo.EmpId}";
            profileInfo.CreatedDate = System.DateTime.UtcNow;
            profileInfo.LastModifiedDate = System.DateTime.UtcNow;
            profileInfo.Skills = request.Skills;

            await _profileRepository.SaveProfile(profileInfo);

            return profileInfo.UserId;
        }

        private string SavePersonalInfo(AddProfileCommand request)
        {
            var personalInfo = new PersonalInfoEntity();
            personalInfo.Name = request.Name;
            personalInfo.Email = request.Email;
            personalInfo.Mobile = request.Mobile;
            personalInfo.EmpId = request.EmpId;
            personalInfo.UserId = $"user{personalInfo.EmpId}";
            personalInfo.CreatedDate = System.DateTime.UtcNow;
            personalInfo.LastModifiedDate = System.DateTime.UtcNow;

            return personalInfo.UserId;
        }

        private void SaveSkills(AddProfileCommand request)
        {
            var skills = request.Skills.Select(s => new SkillEntity
            {
                Name = s.Name,
                IsTechnical = s.IsTechnical,
                Proficiency = s.Proficiency,
                EmpId = request.EmpId,
                CreatedDate = System.DateTime.UtcNow,
                LastModifiedDate = System.DateTime.UtcNow
            }).ToList();

        }
        public IEnumerable<ProfileEntity> Search(SearchProfileQuery query)
        {
             
            if (!string.IsNullOrWhiteSpace(query.EmpId) && (query.EmpId != "string"))
            {
                var response = new List<ProfileEntity>();
                var profile =  SearchById(query.EmpId);
                if (profile != null)
                {
                    response.Add(profile);
                }
                return response;
            }
            else if (!string.IsNullOrWhiteSpace(query.Name) && (query.Name != "string"))
            {
                return  SearchByName(query.Name);
            }
            else if (!string.IsNullOrWhiteSpace(query.Skill) && (query.Skill != "string"))
            {
                return  SearchBySkillName(query.Skill);
            }
            return new List<ProfileEntity>();
        }

        private  ProfileEntity SearchById(string empId)
        {
           var profile = MapToProfile( _profileRepository.SearchByEmpId(empId));
           return profile;
        }

        private ProfileEntity MapToProfile(ProfileEntity profileEntity)
        {
            if (profileEntity != null)
                return new ProfileEntity
                {
                    EmpId = profileEntity.EmpId,
                    Name = profileEntity.Name,
                    Email = profileEntity.Email,
                    UserId = profileEntity.UserId,
                    Mobile = profileEntity.Mobile,
                    Skills = profileEntity.Skills
                };
            else
            {
                return new ProfileEntity();
            }
        }

        private IEnumerable<ProfileEntity> SearchByName(string name)
        {

            var profileData =  _profileRepository.GetProfilesByname(name);


            var plist = profileData.Select(x => new ProfileEntity
            {
                Name = x.Name,
                Email = x.Email,
                Mobile = x.Mobile,
                UserId = x.UserId,
                EmpId = x.EmpId,
                Skills = x.Skills
            });
            return plist;
        }

        private IEnumerable<ProfileEntity> SearchBySkillName(string skillName)
        {
            var profileData =  _profileRepository.SearchBySkillName(skillName);
            var plist = profileData.Select(x => new ProfileEntity
            {
                Name = x.Name,
                Email = x.Email,
                Mobile = x.Mobile,
                UserId = x.UserId,
                EmpId = x.EmpId,
                Skills = x.Skills
            });
            return plist;
            //return profiles;
        }
    }
}
