using Application.Dtos.Orders;
using FluentValidation;

namespace Application.Validators.Orders;

public class OrderUpdateDtoValidator : AbstractValidator<OrderUpdateDto>
{
    public OrderUpdateDtoValidator()
    {
        RuleFor(x => x.TotalAmount)
            .GreaterThan(0).WithMessage("Total amount must be greater than zero.");

        RuleFor(x => x.TableId)
            .NotEmpty().WithMessage("TableId is required.");
    }
}
