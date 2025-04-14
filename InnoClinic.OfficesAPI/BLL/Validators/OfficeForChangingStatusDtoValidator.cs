using BLL.DTO;
using FluentValidation;

namespace BLL.Validators;

public class OfficeForChangingStatusDtoValidator : AbstractValidator<OfficeForChangingStatusDto>
{
    public OfficeForChangingStatusDtoValidator()
    {
        RuleFor(o => o.IsActive)
            .NotNull().WithMessage("Please, select the office's activity");
    }
}