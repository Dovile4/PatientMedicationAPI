using Microsoft.AspNetCore.Mvc;
using PatientMedicationAPI.Models.API;
using PatientMedicationAPI.Models.Database;
using PatientMedicationAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PatientMedicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class MedicationRequestController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;

        public MedicationRequestController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// Adds a new MedicationRequest
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Status Code</returns>
        /// <response code="201">If medication request gets added correctly</response>
        /// <response code="400">If a problem occurs when trying to add a MedicationRequest</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] AddMedicationRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            if (request == null) 
            {
                return new BadRequestObjectResult("Request cannot be null");
            }

            var createdMedicationRequest = await _databaseService.AddMedicationRequest(new MedicationRequest 
            { 
                ClinicianId = request.ClinicianReference,
                PatientId = request.PatientReference,
                MedicationId = request.MedicationReference,
                Frequency = request.Frequency,
                Reason = request.Reason,
                PrescribedDate = request.PrescribedDate,
                StartDate = request.StartDate,
                EndDate = request.EndDate.HasValue ? request.EndDate.Value : null,
                Status = request.Status               
            });

            if (createdMedicationRequest != null) 
            {
                return new CreatedResult($"/api/medicationRequest", createdMedicationRequest.Id);
            }

            return new BadRequestObjectResult("Could not add medication request");
        }

        /// <summary>
        /// Gets MedicationRequests for a given Patient ID with some optional filtering
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A list of Medication Requests</returns>
        /// <response code="200">If medication requests are found</response>
        /// <response code="400">If a problem occurs when trying to lookup MedicationRequests</response>
        /// <response code="404">If no items are found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id, string status = null, DateOnly? startDate = null, DateOnly? endDate = null)
        {
            var results = await _databaseService.GetFilteredMedicationRequests(id, status, startDate, endDate);

            if (results == null)
            {
                return new BadRequestObjectResult("An error occured when trying to retrieve medication requests");
            }

            if (results.Count == 0)
            {
                return new NotFoundObjectResult("Could not find any medication requests with filters provided");
            }
            

            return new OkObjectResult(results);
        }
    }
}
