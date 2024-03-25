using Microsoft.AspNetCore.Mvc;
using PatientMedicationAPI.Models.API;
using PatientMedicationAPI.Models.Database;
using PatientMedicationAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PatientMedicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationRequestController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;

        public MedicationRequestController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        // POST api/<MedicationRequestController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] MedicationRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdMedicationRequest = await _databaseService.AddMedicationRequest(new MedicationRequest 
            { 
                ClinicianReference = request.ClinicianReference,
                PatientReference = request.PatientReference,
                MedicationReference = request.MedicationReference,
                Frequency = request.Frequency,
                Reason = request.Reason,
                PrescribedDate = request.PrescribedDate,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Status = request.Status               
            });

            if (createdMedicationRequest != null) 
            {
                return new CreatedResult($"/api/medicationRequest", createdMedicationRequest.Id);
            }

            return BadRequest();
        }
    }
}
