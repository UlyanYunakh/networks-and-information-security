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
    public class Developers : ControllerBase
    {
        private StationContext db;
        public Developers(StationContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Developer>>> Get()
        {
            var developers = db.Developers
                .Include(d => d.Country);
            return await developers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Developer>> Get(int id)
        {
            Developer developer = await db.Developers.FirstOrDefaultAsync(d => d.Id == id);

            if (developer == null)
            {
                return NotFound();
            }

            return new ObjectResult(developer);
        }

        [HttpPost]
        public async Task<ActionResult<Developer>> Post(Developer developer)
        {
            if (developer == null)
            {
                return BadRequest();
            }

            db.Developers.Add(developer);
            await db.SaveChangesAsync();
            return Ok(developer);
        }

        [HttpPut]
        public async Task<ActionResult<Developer>> Put(Developer developer)
        {
            if (developer == null)
            {
                return BadRequest();
            }

            if (!db.Developers.Any(d => d.Id == developer.Id))
            {
                return NotFound();
            }

            db.Update(developer);
            await db.SaveChangesAsync();
            return Ok(developer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Developer>> Delete(int id)
        {
            Developer developer = await db.Developers.FirstOrDefaultAsync(d => d.Id == id);

            if (developer == null)
            {
                return NotFound();
            }

            var games = db.Games.Where(g => g.DeveloperId == developer.Id);
            foreach (Game game in games)
            {
                game.DeveloperId = null;
            }

            db.Developers.Remove(developer);
            await db.SaveChangesAsync();
            return Ok(developer);
        }
    }
}