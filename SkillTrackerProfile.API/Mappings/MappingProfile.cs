using SkillTrackerProfile.API.Features.Commands;
using SkillTrackerProfile.API.Models;

namespace Profile.Application.Mappings
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<PersonalInfoEntity, AddProfileCommand>().ReverseMap();           
            CreateMap<ProfileEntity, AddProfileCommand>().ReverseMap();
            CreateMap<SkillEntity, Skill>().ReverseMap();
        }
    }
}
