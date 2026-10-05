using InsuranceClaimsAPI.Enums;
using InsuranceClaimsAPI.Models;

namespace InsuranceClaimsAPI.Services
{
    public class PolicyService
    {
        private readonly List<Policies> _policies = new()
        {
            new Policies
            {
                PolicyId = "POL123",
                StartDate = new DateTime(2023, 1, 1),
                EndDate = new DateTime(2024, 1, 1),
                Deductible = 500,
                CoverageLimit = 10000,
                CoveredIncidents = new List<IncidentType>
                {
                    IncidentType.Accident,
                    IncidentType.Fire
                }
            },

            new Policies
            {
                PolicyId = "POL456",
                StartDate = new DateTime(2022, 6, 1),
                EndDate = new DateTime(2025, 6, 1),
                Deductible = 250,
                CoverageLimit = 50000,
                CoveredIncidents = new List<IncidentType>
                {
                    IncidentType.Accident,
                    IncidentType.Theft,
                    IncidentType.Fire,
                    IncidentType.WaterDamage
                }
            }
        };

        public Policies? GetPolicyById(string policyId)
        {
            return _policies.Find(policy => policy.PolicyId == policyId);
        }
    }
}
