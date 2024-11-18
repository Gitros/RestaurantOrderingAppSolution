using Application.Dtos.MenuTypes;
using FluentValidation;

namespace Application.Validators.MenuTypes;

public class MenuTypeCreateDtoValidator : AbstractValidator<MenuTypeCreateDto>
{
    public MenuTypeCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
    }
}
