using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PostStation.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PostStation.Controllers
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
        public IActionResult MainAdd()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> MainSave(Post post)
        {
            db.Posts.Update(post);
            await db.SaveChangesAsync();
            return RedirectToAction("Main");
        }
        [HttpGet]
        public async Task<IActionResult> MainEdit(int? id)
        {
            if (id != null)
            {
                Post post = await db.Posts.FirstOrDefaultAsync(p => p.Id == id);
                if (post != null)
                {
                    return View(post);
                }
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> MainDelete(int? id)
        {
            if (id != null)
            {
                Post post = await db.Posts.FirstOrDefaultAsync(p => p.Id == id);
                if (post != null)
                {
                    db.Remove(post);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Main");
                }
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Games()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GamesAdd()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GamesSave(Game game)
        {
            db.Games.Update(game);
            await db.SaveChangesAsync();
            return RedirectToAction("Games");
        }
        [HttpGet]
        public async Task<IActionResult> GamesEdit(int? id)
        {
            if (id != null)
            {
                Game game = await db.Games.FirstOrDefaultAsync(g => g.Id == id);
                if (game != null)
                {
                    return View(game);
                }
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> GamesDelete(int? id)
        {
            if (id != null)
            {
                Game game = await db.Games.FirstOrDefaultAsync(g => g.Id == id);
                if (game != null)
                {
                    var posts = db.Posts.Where(p => p.GameId == game.Id);
                    foreach(Post screenshot in posts)
                    {
                        screenshot.GameId = null;
                    }

                    db.Remove(game);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Games");
                }
            }
            return NotFound();
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
                    var games = db.Games.Where(g => g.DeveloperId == developer.Id);
                    foreach (Game game in games)
                    {
                        game.DeveloperId = null;
                    }

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
        public IActionResult PlatformsAdd()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PlatformsSave(Platform platform)
        {
            db.Platforms.Update(platform);
            await db.SaveChangesAsync();
            return RedirectToAction("Platforms");
        }
        [HttpGet]
        public async Task<IActionResult> PlatformsEdit(int? id)
        {
            if (id != null)
            {
                Platform platform = await db.Platforms.FirstOrDefaultAsync(p => p.Id == id);
                if (platform != null)
                {
                    return View(platform);
                }
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> PlatformsDelete(int? id)
        {
            if (id != null)
            {
                Platform platform = await db.Platforms.FirstOrDefaultAsync(p => p.Id == id);
                if (platform != null)
                {
                    var games = db.Games.Where(g => g.PlatformId == platform.Id);
                    foreach (Game game in games)
                    {
                        game.PlatformId = null;
                    }

                    db.Remove(platform);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Platforms");
                }
            }
            return NotFound();
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
                    foreach (Developer dev in devs)
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