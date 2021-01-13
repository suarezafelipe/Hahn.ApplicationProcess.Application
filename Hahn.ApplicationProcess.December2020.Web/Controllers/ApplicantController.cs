using System;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Domain.Entities;
using Hahn.ApplicationProcess.December2020.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hahn.ApplicationProcess.December2020.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantService _applicantService;

        public ApplicantController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var applicant = await _applicantService.GetAsync(id);
            return Ok(applicant);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Applicant applicant)
        {
            var createdApplicant = await _applicantService.PostAsync(applicant);
            return Created(
                new Uri($"{Request.Scheme}://{Request.Host}{Request.PathBase}/applicant/{createdApplicant.Id}",
                    UriKind.Absolute), createdApplicant);
        }
    }
}