using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientMedicationAPI.Models.Database
{
    public class MedicationRequest
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("PatientReference")]
        public int PatientReference { get; set; }
        [ForeignKey("ClinicianReference")]
        public int ClinicianReference { get; set; }
        [ForeignKey("MedicationReference")]
        public int MedicationReference { get; set; }
        public string Reason { get; set; }

        [Required]
        public DateOnly PrescribedDate { get; set; }
        [Required]
        public DateOnly StartDate { get; set; }        
        public DateOnly? EndDate { get; set; }
        public string Frequency { get; set; }
        public string Status { get; set; }

        public Patient Patient { get; set; }
        public Clinician Clinician { get; set; }
        public Medication Medication { get; set; }
    }

}
