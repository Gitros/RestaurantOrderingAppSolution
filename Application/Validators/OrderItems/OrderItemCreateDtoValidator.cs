using Application.Dtos.OrderItems;
using FluentValidation;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

public class OrderItemCreateDtoValidator : AbstractValidator<OrderItemCreateDto>
{
    private readonly RestaurantOrderingContext _orderingContext;

    public OrderItemCreateDtoValidator(RestaurantOrderingContext orderingContext)
    {
        _orderingContext = orderingContext;

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.")
            .LessThan(10000).WithMessage("Price must be less than 10,000.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

        RuleFor(x => x.MenuItemId)
            .NotEmpty().WithMessage("MenuItemId is required.")
            .MustAsync(async (menuItemId, cancellation) =>
                await _orderingContext.MenuItems.AnyAsync(m => m.Id == menuItemId))
            .WithMessage("The specified MenuItemId does not exist.");

        RuleFor(x => x.SpecialInstructions)
            .MaximumLength(200).WithMessage("Special instructions must not exceed 200 characters.")
            .Matches(@"^[a-zA-Z0-9\s,.!?]*$").WithMessage("Special instructions contain invalid characters.");
    }
}
