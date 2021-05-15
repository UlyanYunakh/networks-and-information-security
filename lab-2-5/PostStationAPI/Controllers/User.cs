using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace PostStationAPI.Controllers
{
    [ApiController]
    [Route("user")]
    public class User : ControllerBase
    {
        private HttpClient _client;
        public User(IHttpClientFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Route("github/login")]
        public IActionResult GitHubLogIn()
        {
            return Redirect("https://github.com/login/oauth/authorize?client_id=2f95aa0ec701c748b602");
        }

        [Route("github/singin")]
        public async Task<IActionResult> GitHubSingIn()
        {
            var code = Request.Query["code"];
            var content = JsonContent.Create(new { code = code, client_id = "2f95aa0ec701c748b602", client_secret = "030e8d08f97f22b209ca9a68254f06982955a394" });

            var response = await _client.PostAsync($"https://github.com/login/oauth/access_token?code={code}", content);
            var access_token = await response.Content.ReadAsStringAsync();

            return Redirect($"http://localhost:5000/user?access_token={CreateToken()}");
        }

        [Route("instagram/login")]
        public IActionResult InstagramLogIn()
        {
            return Redirect(@"https://api.instagram.com/oauth/authorize?client_id=197359482111013&redirect_uri=https://localhost:5003/user/instagram/singin&response_type=code&scope=user_profile");
        }

        [Route("instagram/singin")]
        public IActionResult InstagramSingIn()
        {
            var code = Request.Query["code"];

            return Redirect($"http://localhost:5000/user?access_token={CreateToken()}");
        }

        [Route("yandex/login")]
        public IActionResult YandexLogIn()
        {
            return Redirect(@"https://oauth.yandex.ru/authorize?response_type=token&client_id=fdd30a822feb488cb1885f21b0178e8f");
        }

        [Route("yandex/singin")]
        public IActionResult YandexSingIn()
        {
            var code = Request.Query["access_token"];
            Console.WriteLine(code);

            return Redirect($"http://localhost:5000/user?access_token={CreateToken()}");
        }

        private string CreateToken()
        {
            var jwtToken = new JwtSecurityToken
            (
                "http://localhost:5000",
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: new SigningCredentials
                (
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("wdacawd22d1awd1325")),
                    SecurityAlgorithms.HmacSha256
                )
            );

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtTokenWritten = jwtTokenHandler.WriteToken(jwtToken);

            return jwtTokenWritten;
        }
    }
}