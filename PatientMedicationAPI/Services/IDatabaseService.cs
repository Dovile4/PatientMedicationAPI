using PatientMedicationAPI.Data;
using PatientMedicationAPI.Models.Database;

namespace PatientMedicationAPI.Services
{
    public interface IDatabaseService
    {
        Task<MedicationRequest> AddMedicationRequest(MedicationRequest request);
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
    }
}
