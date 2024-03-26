namespace PatientMedicationAPI.Models.API
{
    public class GetMedicationRequestsResponse
    {
        public int Id { get; set; }
        public int PatientReference { get; set; }
        public int ClinicianReference { get; set; }
        public int MedicationReference { get; set; }
        public string Reason { get; set; }
        public DateOnly PrescribedDate { get; set; }
        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }
        public string Frequency { get; set; }

        public string Status { get; set; }
        public string MedicationCodeName { get; set; }
        public string CliniciansFirstName { get; set; }
        public string CliniciansLastName { get; set; }
    }
}
