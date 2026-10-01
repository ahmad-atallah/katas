using InsuranceClaimsAPI.Enums;

namespace InsuranceClaimsAPI.Models
{
    public class Policies
    {
        public string PolicyId { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Deductible { get; set; }
        public double CoverageLimit { get; set; }
        public List<IncidentType> CoveredIncidents { get; set; } = new();
    }
}
