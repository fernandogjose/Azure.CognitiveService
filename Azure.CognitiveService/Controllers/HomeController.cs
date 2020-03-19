using Microsoft.AspNetCore.Mvc;

namespace Azure.CognitiveService.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
