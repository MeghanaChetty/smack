using Microsoft.AspNetCore.Mvc;
using smack.core.Interfaces;

namespace smack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        public IUnitOfWork _context;
        public RestaurantController(IUnitOfWork context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> GetRestaurants()
        {
            var restauranslist = await _context.Restaurants.GetAllAsync();
            return Ok(restauranslist);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetRestaurantById(int id)
        {
            var restaurant = await _context.Restaurants.GetByIdAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

        [HttpGet("{id}/menu")] 
        public async Task<ActionResult> GetMenuItemsByRestaurantId(int id)
        {
            var menuItems = await _context.Restaurants.GetRestaurantWithMenuItemsAsync(id);
            return Ok(menuItems.Menuitems);
        }

        [HttpGet("{id}/tables")]
        public async Task<ActionResult> GetTableByRestaurantId(int id)
        {
            var restaurant = await _context.Restaurants.GetRestaurantWithTablesAsync(id);
            return Ok(restaurant.Restauranttables);
        }


        [HttpGet("active")]
        public async Task<ActionResult> GetActiveRestaurants()
        {
            var Restaurants = await _context.Restaurants.GetActiveRestaurantsAsync();
            return Ok(Restaurants);
        }
    }
}
