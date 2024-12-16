using FluentValidation;
using FoodTruck.Common.Models;

namespace FoodTruck.Common.Validators;

public class GetFoodTrucksRequestValidator : AbstractValidator<GetFoodTrucksRequest>
{
    public GetFoodTrucksRequestValidator()
    {
        RuleFor(x => x.Latitude)
            .NotEmpty();
        RuleFor(x => x.Longitude)
            .NotEmpty();
        RuleFor(x => x.Radius)
            .NotEmpty()
            .InclusiveBetween(100, 1500);
    }
}