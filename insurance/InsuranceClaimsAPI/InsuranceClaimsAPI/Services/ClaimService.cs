using InsuranceClaimsAPI.Enums;
using InsuranceClaimsAPI.Models;
using System.Security.Claims;

namespace InsuranceClaimsAPI.Services
{
    public class ClaimService
    {
        public ClaimsEvaluation EvaluateClaim(Claims claim, Policies policy)
        {
            var claimResult = new ClaimsEvaluation();

            //Check if the policy is active
            if (claim.IncidentDate < policy.StartDate || claim.IncidentDate > policy.EndDate)
            {
                claimResult = new ClaimsEvaluation
                {
                    Approved = false,
                    Payout = 0,
                    ReasonCode = ReasonCode.PolicyInactive
                };
            }

            //Check if the incident type is covered
            else if (!policy.CoveredIncidents.Contains(claim.IncidentType))
            {
                claimResult = new ClaimsEvaluation
                {
                    Approved = false,
                    Payout = 0,
                    ReasonCode = ReasonCode.NotCovered
                };
            }

            //Otherwise calculate payout
            else
            {
                var payout = claim.AmountClaimed - policy.Deductible;

                if (payout <= 0)
                {
                    claimResult = new ClaimsEvaluation
                    {
                        Approved = false,
                        Payout = 0,
                        ReasonCode = ReasonCode.ZeroPayout
                    };
                }

                else
                {

                    claimResult = new ClaimsEvaluation
                    {
                        Approved = true,
                        Payout = payout,
                        ReasonCode = ReasonCode.Approved
                    };
                }
            }
            return claimResult;
        }

    }
}
