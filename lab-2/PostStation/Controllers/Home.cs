using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PostStation.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace PostStation.Controllers
{
    public class Home : Controller
    {
        private StationContext db;
        private IHttpClientFactory _clientFactory;
        public Home(StationContext context, IHttpClientFactory clientFactory)
        {
            db = context;
            _clientFactory = clientFactory;
        }

        // *** "Main" actions section ***

        [HttpGet]
        public async Task<IActionResult> Main()
        {
            try
            {
                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.GetAsync("api/posts");
                responce.EnsureSuccessStatusCode();

                ViewBag.Posts = JsonConvert
                    .DeserializeObject<List<Post>>(
                        responce.Content.ReadAsStringAsync().Result
                    );

                return View();
            }
            catch
            {
                return NoContent();
            }
        }

        [HttpGet]
        public async Task<IActionResult> MainAdd()
        {
            try
            {
                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.GetAsync("api/games");
                responce.EnsureSuccessStatusCode();

                ViewBag.Games = JsonConvert
                    .DeserializeObject<List<Game>>(
                        responce.Content.ReadAsStringAsync().Result
                    );

                return View();
            }
            catch
            {
                return NoContent();
            }
        }

        [HttpPost]
        public async Task<IActionResult> MainAdd(Post post)
        {
            try
            {
                var postJson = new StringContent(
                    JsonConvert.SerializeObject(post),
                    Encoding.UTF8,
                    "application/json"
                );

                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.PostAsync("/api/posts", postJson);
                responce.EnsureSuccessStatusCode();

                return RedirectToAction("Main");
            }
            catch
            {
                return NoContent();
            }
        }

        [HttpGet]
        public async Task<IActionResult> MainEdit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NoContent();
                }

                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.GetAsync($"api/posts/{id}");
                responce.EnsureSuccessStatusCode();

                var post = JsonConvert
                    .DeserializeObject<Post>(
                        responce.Content.ReadAsStringAsync().Result
                    );

                responce = await client.GetAsync("api/games");
                responce.EnsureSuccessStatusCode();

                ViewBag.Games = JsonConvert
                    .DeserializeObject<List<Game>>(
                        responce.Content.ReadAsStringAsync().Result
                    );

                return View(post);
            }
            catch
            {
                return NoContent();
            }
        }

        [HttpPost]
        public async Task<IActionResult> MainEdit(Post post)
        {
            try
            {
                var postJson = new StringContent(
                    JsonConvert.SerializeObject(post),
                    Encoding.UTF8,
                    "application/json"
                );

                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.PutAsync("/api/posts", postJson);
                responce.EnsureSuccessStatusCode();

                return RedirectToAction("Main");
            }
            catch
            {
                return NoContent();
            }
        }

        [HttpGet]
        public async Task<IActionResult> MainDelete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NoContent();
                }

                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.DeleteAsync($"api/posts/{id}");
                responce.EnsureSuccessStatusCode();

                return RedirectToAction("Main");
            }
            catch
            {
                return NoContent();
            }
        }

        // *** "Games" actions section ***

        [HttpGet]
        public async Task<IActionResult> Games()
        {
            try
            {
                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.GetAsync("api/games");
                responce.EnsureSuccessStatusCode();

                ViewBag.Games = JsonConvert
                    .DeserializeObject<List<Game>>(
                        responce.Content.ReadAsStringAsync().Result
                    );

                return View();
            }
            catch
            {
                return NoContent();
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> GamesAdd()
        {
            try
            {
                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.GetAsync("api/developers");
                responce.EnsureSuccessStatusCode();

                ViewBag.Developers = JsonConvert
                    .DeserializeObject<List<Developer>>(
                        responce.Content.ReadAsStringAsync().Result
                    );

                responce = await client.GetAsync("api/platforms");
                responce.EnsureSuccessStatusCode();

                ViewBag.Platforms = JsonConvert
                    .DeserializeObject<List<Platform>>(
                        responce.Content.ReadAsStringAsync().Result
                    );

                return View();
            }
            catch
            {
                return NoContent();
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> GamesAdd(Game game)
        {
            try
            {
                var gameJson = new StringContent(
                    JsonConvert.SerializeObject(game),
                    Encoding.UTF8,
                    "application/json"
                );

                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.PostAsync("/api/games", gameJson);
                responce.EnsureSuccessStatusCode();

                return RedirectToAction("Games");
            }
            catch
            {
                return NoContent();
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> GamesEdit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NoContent();
                }

                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.GetAsync($"api/games/{id}");
                responce.EnsureSuccessStatusCode();

                var game = JsonConvert
                    .DeserializeObject<Game>(
                        responce.Content.ReadAsStringAsync().Result
                    );

                responce = await client.GetAsync("api/developers");
                responce.EnsureSuccessStatusCode();

                ViewBag.Developers = JsonConvert
                    .DeserializeObject<List<Developer>>(
                        responce.Content.ReadAsStringAsync().Result
                    );

                responce = await client.GetAsync("api/platforms");
                responce.EnsureSuccessStatusCode();

                ViewBag.Platforms = JsonConvert
                    .DeserializeObject<List<Platform>>(
                        responce.Content.ReadAsStringAsync().Result
                    );

                return View(game);
            }
            catch
            {
                return NoContent();
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> GameEdit(Game game)
        {
            try
            {
                var gameJson = new StringContent(
                    JsonConvert.SerializeObject(game),
                    Encoding.UTF8,
                    "application/json"
                );

                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.PutAsync("/api/games", gameJson);
                responce.EnsureSuccessStatusCode();

                return RedirectToAction("Games");
            }
            catch
            {
                return NoContent();
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> GamesDelete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NoContent();
                }

                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.DeleteAsync($"api/games/{id}");
                responce.EnsureSuccessStatusCode();

                return RedirectToAction("Games");
            }
            catch
            {
                return NoContent();
            }
        }

        // *** "Developers" actions section ***

        [HttpGet]
        public async Task<IActionResult> Developers()
        {
            try
            {
                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.GetAsync("api/developers");
                responce.EnsureSuccessStatusCode();

                ViewBag.Developers = JsonConvert
                    .DeserializeObject<List<Developer>>(
                        responce.Content.ReadAsStringAsync().Result
                    );

                return View();
            }
            catch
            {
                return NoContent();
            }
        }

        [HttpGet]
        public async Task<IActionResult> DevelopersAdd()
        {
            try
            {
                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.GetAsync("api/countries");
                responce.EnsureSuccessStatusCode();

                ViewBag.Countries = JsonConvert
                    .DeserializeObject<List<Country>>(
                        responce.Content.ReadAsStringAsync().Result
                    );

                return View();
            }
            catch
            {
                return NoContent();
            }
        }

        [HttpPost]
        public async Task<IActionResult> DevelopersAdd(Developer developer)
        {
            try
            {
                var developersJson = new StringContent(
                    JsonConvert.SerializeObject(developer),
                    Encoding.UTF8,
                    "application/json"
                );

                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.PostAsync("/api/developers", developersJson);
                responce.EnsureSuccessStatusCode();

                return RedirectToAction("Developers");
            }
            catch
            {
                return NoContent();
            }
        }

        [HttpGet]
        public async Task<IActionResult> DevelopersEdit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NoContent();
                }

                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.GetAsync($"api/developers/{id}");
                responce.EnsureSuccessStatusCode();

                var developer = JsonConvert
                    .DeserializeObject<Developer>(
                        responce.Content.ReadAsStringAsync().Result
                    );

                responce = await client.GetAsync("api/countries");
                responce.EnsureSuccessStatusCode();

                ViewBag.Countries = JsonConvert
                    .DeserializeObject<List<Country>>(
                        responce.Content.ReadAsStringAsync().Result
                    );

                return View(developer);
            }
            catch
            {
                return NoContent();
            }
        }

        [HttpPost]
        public async Task<IActionResult> DevelopersEdit(Developer developer)
        {
            try
            {
                var developerJson = new StringContent(
                    JsonConvert.SerializeObject(developer),
                    Encoding.UTF8,
                    "application/json"
                );

                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.PutAsync("/api/developers", developerJson);
                responce.EnsureSuccessStatusCode();

                return RedirectToAction("Developers");
            }
            catch
            {
                return NoContent();
            }
        }

        [HttpGet]
        public async Task<IActionResult> DevelopersDelete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NoContent();
                }

                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.DeleteAsync($"api/developers/{id}");
                responce.EnsureSuccessStatusCode();

                return RedirectToAction("Developers");
            }
            catch
            {
                return NoContent();
            }
        }

// *** "Platforms" actions section ***

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

// *** "Countries" actions section ***

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