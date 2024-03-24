using System.ComponentModel.DataAnnotations;

namespace PatientMedicationAPI.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Sex { get; set; }

        public List<MedicationRequest> MedicationRequests { get; set; }
    }
}
