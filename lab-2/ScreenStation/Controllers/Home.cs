using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ScreenStation.Controllers
{
    public class Home : Controller
    {
        [HttpGet]
        public IActionResult Main()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Games()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Developers()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Platforms()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Countries()
        {
            return View();
        }
    }
}