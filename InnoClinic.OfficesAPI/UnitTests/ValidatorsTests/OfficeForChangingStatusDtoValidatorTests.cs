using BLL.DTO;
using BLL.Validators;
using FluentValidation.TestHelper;

namespace UnitTests.ValidatorsTests;

public class OfficeForChangingStatusDtoValidatorTests
{
    private readonly OfficeForChangingStatusDtoValidator _validator = new();

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ShouldNotHaveErrorWhenIsActiveIsSpecified(bool isActive)
    {
        var dto = new OfficeForChangingStatusDto { IsActive = isActive };
        _validator.TestValidate(dto).ShouldNotHaveValidationErrorFor(x => x.IsActive);
    }
}