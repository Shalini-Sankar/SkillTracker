using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkillTrackerProfile.API.Exceptions;
using SkillTrackerProfile.API.Features.Queries;
using SkillTrackerProfile.API.Models;
using SkillTrackerProfile.API.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SkillTrackerProfile.API.Controllers
{
    [Route("skill-tracker/api/[controller]")]
    [ApiController]
    // [TypeFilter(typeof(GloabalExceptionFillter))]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ISkillProfileService _searchService;

        public AdminController(ILogger<AdminController> logger, ISkillProfileService searchService)
        {
            _logger = logger;
            _searchService = searchService;
        }
 
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_searchService.GetAllProfiles());
        }

        [HttpPost("search", Name = "Search")]
        public IEnumerable<ProfileEntity> Search(SearchProfileQuery query)
        {
           
            return _searchService.SearchProfile(query);
        }

       
    }
}
