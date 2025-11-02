using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using smack.application.DTOs.MenuItem;

namespace smack.application.Validators
{
    public class CreateMenuItemValidator : AbstractValidator <CreateMenuItemDto>
    {
        public CreateMenuItemValidator()
        {

            //    public string? ItemName { get; set; }
            //public string? Description { get; set; }
            //public decimal Price { get; set; }
            //public int CategoryId { get; set; }
            //public int RestaurantId { get; set; }
            RuleSet("CreateMenuItem", () =>
            {
                RuleFor(x => x.ItemName).MaximumLength(100);
                RuleFor(x => x.Description).MaximumLength(500);
                RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
                RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("CategoryId must be greater than 0");
                RuleFor(x => x.RestaurantId).GreaterThan(0).WithMessage("RestaurantId must be greater than 0");
            });
        }
    }
}

