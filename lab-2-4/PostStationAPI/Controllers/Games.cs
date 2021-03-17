using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostStationAPI.Models;
using PostStationModels;

namespace PostStationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Games : ControllerBase
    {
        private StationContext db;
        public Games(StationContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> Get()
        {
            var games = db.Games
                .Include(g => g.Developer)
                .Include(g => g.Platform);
            return await games.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> Get(int id)
        {
            Game game = await db.Games.FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            return new ObjectResult(game);
        }

        [HttpPost]
        public async Task<ActionResult<Game>> Post(Game game)
        {
            if (game == null)
            {
                return BadRequest();
            }

            db.Games.Add(game);
            await db.SaveChangesAsync();
            return Ok(game);
        }

        [HttpPut]
        public async Task<ActionResult<Game>> Put(Game game)
        {
            if (game == null)
            {
                return BadRequest();
            }

            if (!db.Games.Any(g => g.Id == game.Id))
            {
                return NotFound();
            }

            db.Update(game);
            await db.SaveChangesAsync();
            return Ok(game);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Game>> Delete(int id)
        {
            Game game = await db.Games.FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            var posts = db.Posts.Where(p => p.GameId == game.Id);
            foreach (Post screenshot in posts)
            {
                screenshot.GameId = null;
            }

            db.Games.Remove(game);
            await db.SaveChangesAsync();
            return Ok(game);
        }
    }
}