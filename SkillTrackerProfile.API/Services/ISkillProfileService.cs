using System;
using System.Collections.Generic;
using System.Text;
using SkillTrackerProfile.API.Models;
using SkillTrackerProfile.API.Features.Commands;
using SkillTrackerProfile.API.Features.Queries;
using System.Threading.Tasks;

namespace SkillTrackerProfile.API.Services

{
    public interface ISkillProfileService
    {
        Task<string> AddProfile(AddProfileCommand request);

        Task<string> UpdateProfile(UpdateProfileCommand request,string userId);

        IEnumerable<ProfileEntity> GetAllProfiles();

        IEnumerable<ProfileEntity> SearchProfile(SearchProfileQuery request);
    }
}
