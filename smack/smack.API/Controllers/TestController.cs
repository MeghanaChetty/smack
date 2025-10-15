using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using smack.core.Entities;
using smack.infrastructure.Data;

namespace smack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase

    {
        public SmackDbContext _context;
        public TestController()
        {
            _context = new SmackDbContext();
        }
        [HttpGet("connection")]
        public async Task<IActionResult> GetDbConnectionStatus()
        {
            var canConnect = await _context.Database.CanConnectAsync();
            return Ok(new { connected = canConnect });
        }
        [HttpGet("restaurants")]
        public async Task<ActionResult> GetRestaurants()
        {
            // Note: Ensure your 'Restaurant' entity is visible (using Smack.Core.Entities;)
            var restauranslist = await _context.Restaurants.ToListAsync();
            return Ok(restauranslist);
        }
    }
}
