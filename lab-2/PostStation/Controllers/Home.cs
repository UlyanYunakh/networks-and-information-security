using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PostStationModels;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace PostStation.Controllers
{
    public class Home : Controller
    {
        private IHttpClientFactory _clientFactory;
        public Home(IHttpClientFactory clientFactory)
        {
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
        public async Task<IActionResult> Platforms()
        {
            try
            {
                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.GetAsync("api/platforms");
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

        [HttpGet]
        public IActionResult PlatformsAdd() => View();

        [HttpPost]
        public async Task<IActionResult> PlatformsAdd(Platform platform)
        {
            try
            {
                var platformJson = new StringContent(
                    JsonConvert.SerializeObject(platform),
                    Encoding.UTF8,
                    "application/json"
                );

                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.PostAsync("/api/platforms", platformJson);
                responce.EnsureSuccessStatusCode();

                return RedirectToAction("Platforms");
            }
            catch
            {
                return NoContent();
            }
        }

        [HttpGet]
        public async Task<IActionResult> PlatformsEdit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NoContent();
                }

                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.GetAsync($"api/platforms/{id}");
                responce.EnsureSuccessStatusCode();

                var platform = JsonConvert
                    .DeserializeObject<Platform>(
                        responce.Content.ReadAsStringAsync().Result
                    );

                return View(platform);
            }
            catch
            {
                return NoContent();
            }
        }

        [HttpPost]
        public async Task<IActionResult> PlatformsEdit(Platform platform)
        {
            try
            {
                var platformJson = new StringContent(
                    JsonConvert.SerializeObject(platform),
                    Encoding.UTF8,
                    "application/json"
                );

                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.PutAsync("/api/platforms", platformJson);
                responce.EnsureSuccessStatusCode();

                return RedirectToAction("Platforms");
            }
            catch
            {
                return NoContent();
            }
        }

        [HttpGet]
        public async Task<IActionResult> PlatformsDelete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NoContent();
                }

                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.DeleteAsync($"api/platforms/{id}");
                responce.EnsureSuccessStatusCode();

                return RedirectToAction("Platforms");
            }
            catch
            {
                return NoContent();
            }
        }

        // *** "Countries" actions section ***

        [HttpGet]
        public async Task<IActionResult> Countries()
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
        
        [HttpGet]
        public IActionResult CountriesAdd() => View();
        
        [HttpPost]
        public async Task<IActionResult> CountriesAdd(Country country)
        {
            try
            {
                var countryJson = new StringContent(
                    JsonConvert.SerializeObject(country),
                    Encoding.UTF8,
                    "application/json"
                );

                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.PostAsync("/api/countries", countryJson);
                responce.EnsureSuccessStatusCode();

                return RedirectToAction("Countries");
            }
            catch
            {
                return NoContent();
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> CountriesEdit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NoContent();
                }

                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.GetAsync($"api/countries/{id}");
                responce.EnsureSuccessStatusCode();

                var country = JsonConvert
                    .DeserializeObject<Country>(
                        responce.Content.ReadAsStringAsync().Result
                    );

                return View(country);
            }
            catch
            {
                return NoContent();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CountriesEdit(Country country)
        {
            try
            {
                var countryJson = new StringContent(
                    JsonConvert.SerializeObject(country),
                    Encoding.UTF8,
                    "application/json"
                );

                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.PutAsync("/api/countries", countryJson);
                responce.EnsureSuccessStatusCode();

                return RedirectToAction("Countries");
            }
            catch
            {
                return NoContent();
            }
        }

        [HttpGet]
        public async Task<IActionResult> CountriesDelete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NoContent();
                }

                var client = _clientFactory.CreateClient("poststation");
                var responce = await client.DeleteAsync($"api/countries/{id}");
                responce.EnsureSuccessStatusCode();

                return RedirectToAction("Countries");
            }
            catch
            {
                return NoContent();
            }
        }
    }
}