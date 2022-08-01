using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore.Cosmos;

namespace SkillTrackerProfile.API.Models
{
    public class ProfileEntity : EntityBase
    {
        [Key]
        public string EmpId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string UserId { get; set; }

        public List<Skill> Skills { get; set; }
    }
}
