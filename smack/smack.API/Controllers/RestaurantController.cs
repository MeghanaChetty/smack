using Microsoft.AspNetCore.Mvc;
using smack.core.Interfaces;
using AutoMapper;
using smack.application.DTOs.Restaurant;
namespace smack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        public IUnitOfWork _context;
        public IMapper _mapper;
        public RestaurantController(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);  // Single object
            return Ok(restaurantDto);
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
