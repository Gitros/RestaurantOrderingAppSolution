using Application.Dtos.MenuItems;
using FluentValidation;

namespace Application.Validators.MenuItems;

public class MenuItemCreateDtoValidator : AbstractValidator<MenuItemCreateDto>
{
    public MenuItemCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.")
            .LessThan(10000).WithMessage("Price must be less than 10,000.");

        RuleFor(x => x.MenuTypeId)
            .NotEmpty().WithMessage("MenuTypeId is required.");
    }
}
