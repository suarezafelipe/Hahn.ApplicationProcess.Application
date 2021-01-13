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
            var createdApplicant = await _applicantService.CreateAsync(applicant);
            return Created(
                new Uri($"{Request.Scheme}://{Request.Host}{Request.PathBase}/applicant/{createdApplicant.Id}",
                    UriKind.Absolute), createdApplicant);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Applicant applicant)
        {
            var result = await _applicantService.UpdateAsync(applicant);
            return result ? (IActionResult) Ok() : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _applicantService.DeleteAsync(id);
            return result ? (IActionResult) Ok() : NoContent();
        }
    }
}