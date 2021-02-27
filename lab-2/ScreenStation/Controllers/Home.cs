using Microsoft.AspNetCore.Mvc;

namespace ScreenStation.Controllers
{
    public class Home : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}