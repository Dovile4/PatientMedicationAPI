namespace PatientMedicationAPI.Models
{
    public class Medication
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string CodeName { get; set; }
        public string CodeSystem { get; set; }
        public int StrengthValue { get; set; }
        public int StrengthUnit { get; set; }
        public enum Form
        {
            Tablet,
            Powder,
            Capsule,
            Syrup
        }
    }
        
}
