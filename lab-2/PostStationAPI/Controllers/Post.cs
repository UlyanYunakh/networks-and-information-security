using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostStationAPI.Models;

namespace PostStationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Post : ControllerBase
    {
        private StationContext db;
        public Post(StationContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Post>>> Get()
        {
            var posts = db.Posts
                .Include(p => p.Game)
                    .ThenInclude(g => g.Platform)
                .Include(p => p.Game)
                    .ThenInclude(g => g.Developer)
                        .ThenInclude(d => d.Country);
            return await posts.ToListAsync();
        }
    }
}