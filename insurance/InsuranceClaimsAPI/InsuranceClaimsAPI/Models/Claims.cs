using InsuranceClaimsAPI.Enums;

namespace InsuranceClaimsAPI.Models
{
    public class Claims
    {
        public string PolicyId { get; set; } = string.Empty;
        public IncidentType IncidentType { get; set; }
        public DateTime IncidentDate { get; set; }
        public double AmountClaimed { get; set; }

    }
}
