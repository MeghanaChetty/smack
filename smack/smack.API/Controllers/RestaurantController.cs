using Microsoft.AspNetCore.Mvc;
using smack.core.Interfaces;
using AutoMapper;
using smack.application.DTOs.Restaurant;
using smack.application.DTOs.MenuItem;
using smack.core.Entities;
using smack.application.Common;
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
            var restaurantslist = await _context.Restaurants.GetAllAsync();
            var restaurantDtos = _mapper.Map<IEnumerable<RestaurantDto>>(restaurantslist); // Map to DTOs
            return Ok(restaurantDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetRestaurantById(int id)
        {
            var restaurant = await _context.Restaurants.GetByIdAsync(id);
            if (restaurant == null)
            {
                return NotFound(ResponseHelper.ErrorResponse("Restaurant not found", null));
            }

            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);  // Single object
            //return Ok(restaurantDto);
            return Ok(ResponseHelper.SuccessResponse(restaurantDto, "Restaurant retrieved successfully"));

        }

        [HttpGet("{id}/menu")]
        public async Task<ActionResult> GetMenuItemsByRestaurantId(int id)
        {
            var restaurant = await _context.Restaurants.GetRestaurantWithMenuItemsAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            var RestaurantMenuItems = _mapper.Map<IEnumerable<MenuItemDto>>(restaurant.Menuitems);
            // Map menu items if you have a MenuItemDto, otherwise return as is
            return Ok(RestaurantMenuItems);
        }

        [HttpGet("{id}/tables")]
        public async Task<ActionResult> GetTableByRestaurantId(int id)
        {
            var restaurant = await _context.Restaurants.GetRestaurantWithTablesAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            // Map tables if you have a TableDto, otherwise return as is
            return Ok(restaurant.Restauranttables);
        }

        [HttpGet("active")]
        public async Task<ActionResult> GetActiveRestaurants()
        {
            var restaurants = await _context.Restaurants.GetActiveRestaurantsAsync();
            var restaurantDtos = _mapper.Map<List<RestaurantDto>>(restaurants);
            return Ok(restaurantDtos);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRestaurant([FromBody] CreateRestaurantDto createRestaurantDto)
        {
            var restaurant = _mapper.Map<Restaurant>(createRestaurantDto);
            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
            return CreatedAtAction(nameof(GetRestaurantById), new { id = restaurant.RestaurantId }, restaurantDto);
        }
    }
}