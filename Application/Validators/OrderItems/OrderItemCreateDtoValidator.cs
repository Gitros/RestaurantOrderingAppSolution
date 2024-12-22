using Application.Dtos.OrderItems;
using FluentValidation;
using Infrastructure.Database;

public class OrderItemCreateDtoValidator : AbstractValidator<OrderItemCreateDto>
{
    private readonly RestaurantOrderingContext _orderingContext;

    public OrderItemCreateDtoValidator(RestaurantOrderingContext orderingContext)
    {
        _orderingContext = orderingContext;

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

        RuleFor(x => x.MenuItemId)
            .NotEmpty().WithMessage("MenuItemId is required.")
            .Must(menuItemId => _orderingContext.MenuItems.Any(m => m.Id == menuItemId))
            .WithMessage("The specified MenuItemId does not exist.");

        RuleFor(x => x.SpecialInstructions)
            .MaximumLength(200).WithMessage("Special instructions must not exceed 200 characters.")
            .Matches(@"^[a-zA-Z0-9\s,.!?]*$").WithMessage("Special instructions contain invalid characters.");
    }
}
