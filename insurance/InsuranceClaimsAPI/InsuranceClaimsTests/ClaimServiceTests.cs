using InsuranceClaimsAPI.Enums;
using InsuranceClaimsAPI.Models;
using InsuranceClaimsAPI.Services;

namespace InsuranceClaimsTests
{
    public class ClaimServiceTests
    {
        [Fact]
        public void EvaluateClaim_ValidClaim_ReturnsApproved()
        {
            // Arrange
            var policy = new Policies
            {
                PolicyId = "POL123",
                StartDate = new DateTime(2023, 1, 1),
                EndDate = new DateTime(2024, 1, 1),
                Deductible = 500,
                CoverageLimit = 10000,
                CoveredIncidents = [IncidentType.Accident, IncidentType.Fire]
            };

            var claim = new Claims
            {
                PolicyId = "POL123",
                IncidentType = IncidentType.Fire,
                IncidentDate = new DateTime(2023, 6, 15),
                AmountClaimed = 3000
            };

            var service = new ClaimService();

            // Act
            var result = service.EvaluateClaim(claim, policy);

            // Assert
            Assert.True(result.Approved);
            Assert.Equal(2500m, result.Payout);
            Assert.Equal(ReasonCode.Approved, result.ReasonCode);
        }
    }
}
