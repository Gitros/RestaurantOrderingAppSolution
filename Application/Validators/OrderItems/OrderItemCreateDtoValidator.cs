using Application.Dtos.OrderItems;
using FluentValidation;

public class OrderItemCreateDtoValidator : AbstractValidator<OrderItemCreateDto>
{
    public OrderItemCreateDtoValidator()
    {

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

        RuleFor(x => x.MenuItemId)
            .NotEmpty().WithMessage("MenuItemId is required.");

        RuleFor(x => x.SpecialInstructions)
            .MaximumLength(200).WithMessage("Special instructions must not exceed 200 characters.");
    }
}
