using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkillTrackerProfile.API.Exceptions;
using SkillTrackerProfile.API.Features.Commands;
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
    public class EngineerController : ControllerBase
    {
        private readonly ILogger<EngineerController> _logger;
        private readonly ISkillProfileService _searchService;

        public EngineerController(ILogger<EngineerController> logger, ISkillProfileService searchService)
        {
            _logger = logger;
            _searchService = searchService;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> IsAlive()
        {
            _logger.LogInformation("Checking Profile API live status.");
            string response = "Profile API is in good health.";
            return Ok(response);
        }

        [HttpPost(Name = "AddProfile")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> AddProfile([FromBody] AddProfileCommand command)
        {
            var result = await _searchService.AddProfile(command);
            return Ok(result);
        }

        [HttpPut(Name = "UpdateProfile")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<int>> UpdateProfile([FromBody] UpdateProfileCommand command,
            [FromHeader(Name="x-userid")] string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                var errors = new List<ValidationFailure> { new ValidationFailure("", "UserId header missing") };
                throw new ValidationException(errors);
            }
            
            await _searchService.UpdateProfile(command,userId);
            return Ok();
        }
    }
}
