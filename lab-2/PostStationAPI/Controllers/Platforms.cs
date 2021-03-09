using System.Collections.Generic;
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
        public async Task<ActionResult<IEnumerable<Platform>>> Get() => await db.Platforms.ToListAsync();
    }
}