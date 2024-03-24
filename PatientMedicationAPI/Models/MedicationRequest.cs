using System.ComponentModel.DataAnnotations;

namespace PatientMedicationAPI.Models
{
    public class MedicationRequest
    {
        public int Id { get; set; }
        public string PatientReference { get; set; }
        public string ClinicianReference { get; set; }
        public string MedicationReference { get; set; }
        public string Reason { get; set; }

        [Required]
        public DateTime PrescribedDate { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int FrSequency { get; set; }
        public enum Status
        {
            Active,
            OnHold,
            Cancelled,
            Completed
        }
    }
}
