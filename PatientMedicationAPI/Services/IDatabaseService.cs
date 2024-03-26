using PatientMedicationAPI.Data;
using PatientMedicationAPI.Models.API;
using PatientMedicationAPI.Models.Database;

namespace PatientMedicationAPI.Services
{
    public interface IDatabaseService
    {
        Task<MedicationRequest> AddMedicationRequest(MedicationRequest request);
        Task<List<GetMedicationRequestsResponse>> GetFilteredMedicationRequests(int patientId, string status = null, DateOnly? startDate = null, DateOnly? endDate = null);
    }

    public class DatabaseService : IDatabaseService
    {
        private readonly AppDbContext _dbContext;
        public DatabaseService(AppDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public async Task<MedicationRequest> AddMedicationRequest(MedicationRequest request)
        {
            try
            {
                var entity = await _dbContext.MedicationRequests.AddAsync(request);
                await _dbContext.SaveChangesAsync();

                return entity.Entity;
            }
            catch
            { 
                return null;
            }
            
        }

        public async Task<List<GetMedicationRequestsResponse>> GetFilteredMedicationRequests(int patientId, string status = null, DateOnly? startDate = null, DateOnly? endDate = null)
        {
            try
            {
                var results = from medicationRequest in _dbContext.MedicationRequests
                              join medication in _dbContext.Medications
                              on medicationRequest.MedicationReference equals medication.Id
                              join clinician in _dbContext.Clinicians
                              on medicationRequest.ClinicianReference equals clinician.Id
                              where medicationRequest.PatientReference == patientId &&
                              (status == null || medicationRequest.Status == status) &&
                              (startDate == null || medicationRequest.StartDate >= startDate) &&
                              (endDate == null || medicationRequest.EndDate <= endDate)
                              select new GetMedicationRequestsResponse
                              {
                                  Id = medicationRequest.Id,
                                  Status = medicationRequest.Status,
                                  StartDate = medicationRequest.StartDate,
                                  EndDate = medicationRequest.EndDate,
                                  MedicationReference = medicationRequest.MedicationReference,
                                  MedicationCodeName = medication.CodeName,
                                  ClinicianReference = medicationRequest.ClinicianReference,
                                  Frequency = medicationRequest.Frequency,
                                  PatientReference = medicationRequest.PatientReference,
                                  PrescribedDate = medicationRequest.PrescribedDate,
                                  Reason = medicationRequest.Reason,
                                  CliniciansFirstName = clinician.FirstName,
                                  CliniciansLastName = clinician.LastName
                              };

                return results.ToList();
            }
            catch
            {
                return null;
            }

        }
    }
}
