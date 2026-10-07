using InsuranceClaimsAPI.Models;
using InsuranceClaimsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceClaimsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClaimsController : ControllerBase
    {
        private readonly ClaimService _claimService;
        private readonly PolicyService _policyService;

        public ClaimsController(ClaimService claimService, PolicyService policyService)
        {
            _claimService = claimService;
            _policyService = policyService;
        }

        [HttpPost]
        public ActionResult<ClaimsEvaluation> EvaluateClaim(Claims claim)
        {
            var policy = _policyService.GetPolicyById(claim.PolicyId);

            if (policy == null)
            {
                return NotFound("Policy not found.");
            }

            var result = _claimService.EvaluateClaim(claim, policy);

            return Ok(result);
        }

    }
}
