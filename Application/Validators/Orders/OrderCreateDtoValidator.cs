using Application.Dtos.Orders;
using FluentValidation;

namespace Application.Validators.Orders;

public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
{
    public OrderCreateDtoValidator()
    {
        RuleFor(x => x.OrderDateTime)
            .NotNull()
            .GreaterThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Order date and time must not be in the past.");

        RuleFor(x => x.TotalAmount)
                .GreaterThan(0)
                .WithMessage("Total amount must be greater than zero.");

        RuleFor(x => x.OrderItems)
            .NotEmpty()
            .WithMessage("An order must have at least one item.");

        RuleForEach(x => x.OrderItems).ChildRules(items =>
        {
            items.RuleFor(i => i.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
            items.RuleFor(i => i.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        });

        RuleFor(x => x.TableId)
            .NotEmpty()
            .WithMessage("TableId is required.");
    }
}
