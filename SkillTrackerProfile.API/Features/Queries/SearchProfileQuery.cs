using SkillTrackerProfile.API.Models;
using System.Collections.Generic;

namespace SkillTrackerProfile.API.Features.Queries
{
    public class SearchProfileQuery
    {
        public string EmpId { get; set; }

        public string Name { get; set; }

        public string Skill { get; set; }
    }
}
