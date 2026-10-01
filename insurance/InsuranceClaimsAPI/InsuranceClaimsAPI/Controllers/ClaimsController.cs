using Microsoft.AspNetCore.Mvc;

namespace InsuranceClaimsAPI.Controllers
{
    public class ClaimsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
