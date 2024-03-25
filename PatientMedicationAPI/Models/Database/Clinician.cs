using System.ComponentModel.DataAnnotations;

namespace PatientMedicationAPI.Models.Database
{
    public class Clinician
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RegistrationId { get; set; }

        public List<MedicationRequest> MedicationRequests { get; set; }
    }
}
