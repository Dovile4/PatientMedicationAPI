using System.ComponentModel.DataAnnotations;

namespace PatientMedicationAPI.Models.Database
{
    public class Medication
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string CodeName { get; set; }
        public string CodeSystem { get; set; }
        public int StrengthValue { get; set; }
        public string StrengthUnit { get; set; }
        public string Form { get; set; }

        public List<MedicationRequest> MedicationRequests { get; set; }
    }

}
