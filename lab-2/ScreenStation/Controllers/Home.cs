using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScreenStation.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ScreenStation.Controllers
{
    public class Home : Controller
    {
        private StationContext db;
        public Home(StationContext context)
        {
            db = context;
        }

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
        public IActionResult DevelopersAdd()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DevelopersSave(Developer developer)
        {
            db.Developers.Update(developer);
            await db.SaveChangesAsync();
            return RedirectToAction("Developers");
        }
        [HttpGet]
        public async Task<IActionResult> DevelopersEdit(int? id)
        {
            if (id != null)
            {
                Developer developer = await db.Developers.FirstOrDefaultAsync(d => d.Id == id);
                if (developer != null)
                {
                    return View(developer);
                }
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> DevelopersDelete(int? id)
        {
            if (id != null)
            {
                Developer developer = await db.Developers.FirstOrDefaultAsync(d => d.Id == id);
                if (developer != null)
                {
                    db.Remove(developer);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Developers");
                }
            }
            return NotFound();
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
        [HttpGet]
        public IActionResult CountriesAdd()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CountriesSave(Country country)
        {
            db.Countries.Update(country);
            await db.SaveChangesAsync();
            return RedirectToAction("Countries");
        }
        [HttpGet]
        public async Task<IActionResult> CountriesEdit(int? id)
        {
            if (id != null)
            {
                Country country = await db.Countries.FirstOrDefaultAsync(c => c.Id == id);
                if (country != null)
                {
                    return View(country);
                }
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> CountriesDelete(int? id)
        {
            if (id != null)
            {
                Country country = await db.Countries.FirstOrDefaultAsync(c => c.Id == id);
                if (country != null)
                {
                    var devs = db.Developers.Where(d => d.CountryId == country.Id);
                    foreach(Developer dev in devs)
                    {
                        dev.CountryId = null;
                    }

                    db.Remove(country);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Countries");
                }
            }
            return NotFound();
        }
    }
}