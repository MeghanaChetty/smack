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
        public MappingProfile() {
            CreateMap<Restaurant, RestaurantDto>();
            CreateMap<Restaurant, RestaurantDetailDto>()
                .ForMember(
        dest => dest.MenuItems,
        opt => opt.MapFrom(src => src.Menuitems)
    );
            CreateMap<Menuitem, MenuItemDto>()
                .ForMember(dest => dest.CategoryName,
        opt => opt.MapFrom(src => src.CategoryNavigation.Categoryname)
    );
            CreateMap<Menuitem, CreateMenuItemDto>();
            CreateMap<Menuitem, UpdateMenuItemDto>();
            CreateMap<User, UserDto>();
            CreateMap<User, UserRestaurantDto>();
        }
        
        // Mapping profile implementation goes here
       
    }
}
