using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostStationAPI.Models;

namespace PostStationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Devs : ControllerBase
    {
        private StationContext db;
        public Devs(StationContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Developer>>> Get() => await db.Developers.ToListAsync();
    }
}