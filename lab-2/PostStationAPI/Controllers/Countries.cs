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
    public class Countries : ControllerBase
    {
        private StationContext db;
        public Countries(StationContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> Get()
            => await db.Countries.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> Get(int id)
        {
            Country country = await db.Countries.FirstOrDefaultAsync(c => c.Id == id);

            if (country == null)
            {
                return NotFound();
            }

            return new ObjectResult(country);
        }

        [HttpPost]
        public async Task<ActionResult<Country>> Post(Country country)
        {
            if (country == null)
            {
                return BadRequest();
            }

            db.Countries.Add(country);
            await db.SaveChangesAsync();
            return Ok(country);
        }

        [HttpPut]
        public async Task<ActionResult<Country>> Put(Country country)
        {
            if (country == null)
            {
                return BadRequest();
            }

            if (!db.Countries.Any(c => c.Id == country.Id))
            {
                return NotFound();
            }

            db.Update(country);
            await db.SaveChangesAsync();
            return Ok(country);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Country>> Delete(int id)
        {
            Country country = db.Countries.FirstOrDefault(c => c.Id == id);

            if (country == null)
            {
                return NotFound();
            }

            var devs = db.Developers.Where(d => d.CountryId == country.Id);
            foreach (Developer dev in devs)
            {
                dev.CountryId = null;
            }

            db.Countries.Remove(country);
            await db.SaveChangesAsync();
            return Ok(country);
        }
    }
}