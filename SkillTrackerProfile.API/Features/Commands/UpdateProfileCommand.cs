using SkillTrackerProfile.API.Models;
using System.Collections.Generic;

namespace SkillTrackerProfile.API.Features.Commands
{
    public class UpdateProfileCommand
    {
        public string EmpId { get; set; }

        public List<Skill> Skills { get; set; }
    }
}
