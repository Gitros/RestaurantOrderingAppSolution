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
    }
}
