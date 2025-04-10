using BLL.DTO;
using BLL.Helpers;
using FluentValidation;

namespace BLL.Validators;

public class OfficeForUpdatingDtoValidator : AbstractValidator<OfficeForUpdatingDto>
{
    public OfficeForUpdatingDtoValidator()
    {
        RuleFor(o => o.Photo)
            .Must(ValidationHelper.BeAValidImage).WithMessage("Invalid image format")
            .When(o => o.Photo is not null);

        RuleFor(o => o.City)
            .NotEmpty().WithMessage("Please, enter the office's city")
            .MaximumLength(100).WithMessage("City name cannot exceed 100 characters")
            .Must(ValidationHelper.BeAValidName).WithMessage("City contains invalid characters");

        RuleFor(o => o.Street)
            .NotEmpty().WithMessage("Please, enter the office's street")
            .MaximumLength(100).WithMessage("Street name cannot exceed 100 characters")
            .Must(ValidationHelper.BeAValidName).WithMessage("Street contains invalid characters");

        RuleFor(o => o.HouseNumber)
            .NotEmpty().WithMessage("Please, enter the office's house number")
            .MaximumLength(20).WithMessage("House number cannot exceed 20 characters")
            .Must(ValidationHelper.BeAValidNumber).WithMessage("House number must contain only digits and optional letters");

        RuleFor(o => o.OfficeNumber)
            .MaximumLength(20).WithMessage("Office number cannot exceed 20 characters")
            .Must(ValidationHelper.BeAValidNumberOrEmpty).WithMessage("Office number must contain only digits and optional letters")
            .When(o => !string.IsNullOrEmpty(o.OfficeNumber));

        RuleFor(o => o.IsActive)
            .NotNull().WithMessage("Please, select the office's activity");

        RuleFor(o => o.RegistryPhoneNumber)
            .NotEmpty().WithMessage("Please, enter the phone number")
            .Matches(@"^\+\d{5,15}$").WithMessage("You've entered an invalid phone number");
    }
}