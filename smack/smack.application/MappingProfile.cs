using smack.application.DTOs.Restaurant;
using smack.application.DTOs.User;
using smack.application.DTOs.MenuItem;

using smack.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using System.Threading.Tasks;

namespace smack.application
{
    public class MappingProfile : Profile

    {
        public MappingProfile()
        {
            // Read mappings (Entity -> DTO)
            CreateMap<Restaurant, RestaurantDto>();
            CreateMap<Restaurant, RestaurantDetailDto>()
                .ForMember(dest => dest.MenuItems, opt => opt.MapFrom(src => src.Menuitems));

            // Write mappings (DTO -> Entity) - ADD THIS
            CreateMap<CreateRestaurantDto, Restaurant>();

            // MenuItem mappings
            CreateMap<Menuitem, MenuItemDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryNavigation.Categoryname));
            CreateMap<CreateMenuItemDto, Menuitem>();  // DTO -> Entity
            CreateMap<UpdateMenuItemDto, Menuitem>();  // DTO -> Entity

            // User mappings
            CreateMap<User, UserDto>();
            CreateMap<User, UserRestaurantDto>();
        }
        // Mapping profile implementation goes here

    }
}
