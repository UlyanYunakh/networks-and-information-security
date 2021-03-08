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
    public class Posts : ControllerBase
    {
        private StationContext db;
        public Posts(StationContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> Get()
        {
            var posts = db.Posts
                .Include(p => p.Game)
                    .ThenInclude(g => g.Platform)
                .Include(p => p.Game)
                    .ThenInclude(g => g.Developer)
                        .ThenInclude(d => d.Country);
            return await posts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> Get(int id)
        {
            Post post = await db.Posts.FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return new ObjectResult(post);
        }

        [HttpPost]
        public async Task<ActionResult<Post>> Post(Post post)
        {
            if (post == null)
            {
                return BadRequest();
            }

            db.Posts.Add(post);
            await db.SaveChangesAsync();
            return Ok(post);
        }

        [HttpPut]
        public async Task<ActionResult<Post>> Put(Post post)
        {
            if (post == null)
            {
                return BadRequest();
            }

            if (!db.Posts.Any(p => p.Id == post.Id))
            {
                return NotFound();
            }

            db.Update(post);
            await db.SaveChangesAsync();
            return Ok(post);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> Delete(int id)
        {
            Post post = db.Posts.FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            db.Posts.Remove(post);
            await db.SaveChangesAsync();
            return Ok(post);
        }
    }
}