using System.ComponentModel.DataAnnotations;

namespace PatientMedicationAPI.Models.API
{
    public class MedicationRequestModel
    {
        
        public int PatientReference { get; set; }
        public int ClinicianReference { get; set; }
        public int MedicationReference { get; set; }
        public string Reason { get; set; }
        [Required(ErrorMessage = "PrescribedDate is required")]
        public DateOnly PrescribedDate { get; set; }
        [Required(ErrorMessage = "StartDate is required")]
        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }
        public string Frequency { get; set; }

        [EnumDataType(typeof(StatusEnum))]
        public string Status { get; set; }
    }

    public enum StatusEnum
    {
        Active,
        OnHold,
        Cancelled,
        Completed
    }
}
