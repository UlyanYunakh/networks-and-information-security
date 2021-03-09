using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostStationAPI.Models;

namespace PostStationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Platforms : ControllerBase
    {
        private StationContext db;
        public Platforms(StationContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Platform>>> Get()
            => await db.Platforms.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Platform>> Get(int id)
        {
            Platform platform = await db.Platforms.FirstOrDefaultAsync(p => p.Id == id);

            if (platform == null)
            {
                return NotFound();
            }

            return new ObjectResult(platform);
        }

        [HttpPost]
        public async Task<ActionResult<Platform>> Post(Platform platform)
        {
            if (platform == null)
            {
                return BadRequest();
            }

            db.Platforms.Add(platform);
            await db.SaveChangesAsync();
            return Ok(platform);
        }

        [HttpPut]
        public async Task<ActionResult<Platform>> Put(Platform platform)
        {
            if (platform == null)
            {
                return BadRequest();
            }

            if (!db.Platforms.Any(p => p.Id == platform.Id))
            {
                return NotFound();
            }

            db.Update(platform);
            await db.SaveChangesAsync();
            return Ok(platform);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Platform>> Delete(int id)
        {
            Platform platform = db.Platforms.FirstOrDefault(p => p.Id == id);

            if (platform == null)
            {
                return NotFound();
            }

            var games = db.Games.Where(g => g.PlatformId == platform.Id);
            foreach (Game game in games)
            {
                game.PlatformId = null;
            }

            db.Platforms.Remove(platform);
            await db.SaveChangesAsync();
            return Ok(platform);
        }
    }
}