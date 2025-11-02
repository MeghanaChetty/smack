using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using smack.application.DTOs.Restaurant;
namespace smack.application.Validators
{
    public class UpdateRestaurantValidator : AbstractValidator<UpdateRestaurantDto>
    {
        public UpdateRestaurantValidator()
        {
            RuleSet("UpdateRestaurant", () =>
                {
                    RuleFor(x => x.RestaurantId).GreaterThan(0);
                    RuleFor(x => x.RestaurantName).MaximumLength(100).NotEmpty();
                    RuleFor(x => x.Email).EmailAddress().MaximumLength(255).NotEmpty();
                    RuleFor(x => x.PhoneNumber).MaximumLength(20);
                    RuleFor(x => x.Address).MaximumLength(500);
                });
        }
    }
}
