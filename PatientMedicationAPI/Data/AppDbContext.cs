using Microsoft.EntityFrameworkCore;
using PatientMedicationAPI.Models.Database;

namespace PatientMedicationAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Clinician> Clinicians { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<MedicationRequest> MedicationRequests { get; set; }        
    }
}
