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
            Assert.Equal(2500, result.Payout);
            Assert.Equal(ReasonCode.Approved, result.ReasonCode);
        }

        [Fact]
        public void EvaluateClaim_InactivePolicy_ReturnsPolicyInactive()
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
                IncidentDate = new DateTime(2024, 6, 15), //expired policy
                AmountClaimed = 3000
            };

            var service = new ClaimService();

            // Act
            var result = service.EvaluateClaim(claim, policy);

            // Assert
            Assert.False(result.Approved);
            Assert.Equal(0, result.Payout);
            Assert.Equal(ReasonCode.PolicyInactive, result.ReasonCode);
        }

        [Fact]
        public void EvaluateClaim_IncidentNotCovered_ReturnsNotCovered()
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

                IncidentType = IncidentType.Theft, //Theft is NOT covered for this policy

                IncidentDate = new DateTime(2023, 6, 15),
                AmountClaimed = 3000
            };

            var service = new ClaimService();

            // Act
            var result = service.EvaluateClaim(claim, policy);

            // Assert
            Assert.False(result.Approved);
            Assert.Equal(0, result.Payout);
            Assert.Equal(ReasonCode.NotCovered, result.ReasonCode);
        }

        [Fact]
        public void EvaluateClaim_PayoutIsZeroOrNegative_ReturnsZeroPayout()
        {
            // Arrange
            var policy = new Policies
            {
                PolicyId = "POL123",
                StartDate = new DateTime(2023, 1, 1),
                EndDate = new DateTime(2024, 1, 1),
                Deductible = 500,
                CoverageLimit = 10000,
                CoveredIncidents = [IncidentType.Fire]
            };

            var claim = new Claims
            {
                PolicyId = "POL123",
                IncidentType = IncidentType.Fire,
                IncidentDate = new DateTime(2023, 6, 15),
                AmountClaimed = 300
            };

            var service = new ClaimService();

            // Act
            var result = service.EvaluateClaim(claim, policy);

            // Assert
            Assert.False(result.Approved);
            Assert.Equal(0, result.Payout);
            Assert.Equal(ReasonCode.ZeroPayout, result.ReasonCode);
        }

        [Fact]
        public void EvaluateClaim_PayoutExceedsCoverageLimit_ReturnsCovreageLimit()
        {
            // Arrange
            var policy = new Policies
            {
                PolicyId = "POL123",
                StartDate = new DateTime(2023, 1, 1),
                EndDate = new DateTime(2024, 1, 1),
                Deductible = 500,
                CoverageLimit = 10000,
                CoveredIncidents = [IncidentType.Fire]
            };

            var claim = new Claims
            {
                PolicyId = "POL123",
                IncidentType = IncidentType.Fire,
                IncidentDate = new DateTime(2023, 6, 15),
                AmountClaimed = 20000
            };

            var service = new ClaimService();

            // Act
            var result = service.EvaluateClaim(claim, policy);

            // Assert
            Assert.True(result.Approved);
            Assert.Equal(10000, result.Payout);
            Assert.Equal(ReasonCode.Approved, result.ReasonCode);
        }
    }
}
