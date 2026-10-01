using InsuranceClaimsAPI.Enums;

namespace InsuranceClaimsAPI.Models
{
    public class ClaimsEvaluation
    {
        public bool Approved { get; set; }
        public decimal Payout { get; set; }
        public ReasonCode ReasonCode { get; set; }
    }
}
