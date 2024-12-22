using Microsoft.AspNetCore.Mvc;

namespace VivesRental.Api.Controllers
{
    public class IdentityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
