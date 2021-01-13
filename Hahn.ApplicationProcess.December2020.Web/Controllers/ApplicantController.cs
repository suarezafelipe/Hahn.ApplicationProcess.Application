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

        /// <summary>
        /// Retrieves a specific applicant by id
        /// </summary>
        /// <param name="id" example="1">The applicant id</param>
        /// <response code="200">Applicant found</response>
        /// <response code="404">Applicant was not found in the database</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var applicant = await _applicantService.GetAsync(id);
            return applicant != null ? (IActionResult) Ok(applicant) : NotFound();
        }

        /// <summary>
        /// Creates a new applicant
        /// </summary>
        /// <param name="applicant"></param>
        /// <response code="201">Applicant successfully created</response>
        /// <response code="400">There were validation errors. Applicant not created</response>
        [HttpPost]
        public async Task<IActionResult> PostAsync(Applicant applicant)
        {
            var createdApplicant = await _applicantService.CreateAsync(applicant);
            return Created(
                new Uri($"{Request.Scheme}://{Request.Host}{Request.PathBase}/applicant/{createdApplicant.Id}",
                    UriKind.Absolute), createdApplicant);
        }

        /// <summary>
        /// Updates an existing applicant
        /// </summary>
        /// <param name="applicant"></param>
        /// <response code="200">Applicant successfully updated</response>
        /// <response code="204">The provided applicant id was not found in database. No Applicant was updated</response>
        /// <response code="400">There were validation errors. Applicant not updated</response>
        [HttpPut]
        public async Task<IActionResult> PutAsync(Applicant applicant)
        {
            var success = await _applicantService.UpdateAsync(applicant);
            return success ? (IActionResult) Ok() : NoContent();
        }

        /// <summary>
        /// Removes an existing applicant
        /// </summary>
        /// <param name="id" example="1"></param>
        /// <response code="200">Applicant successfully deleted</response>
        /// <response code="204">The provided applicant id was not found in database. No Applicant was removed</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var success = await _applicantService.DeleteAsync(id);
            return success ? (IActionResult) Ok() : NoContent();
        }
    }
}