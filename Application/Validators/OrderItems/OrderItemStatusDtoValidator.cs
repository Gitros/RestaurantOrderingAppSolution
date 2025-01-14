using Application.Dtos.OrderItems;
using FluentValidation;

namespace Application.Validators;

public class OrderItemStatusDtoValidator : AbstractValidator<OrderItemStatusDto>
{
    public OrderItemStatusDtoValidator()
    {
        RuleFor(x => x.OrderItemStatus)
            .IsInEnum().WithMessage("Invalid order item status.");
    }
}
