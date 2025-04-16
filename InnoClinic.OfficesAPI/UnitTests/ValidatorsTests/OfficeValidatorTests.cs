using BLL.DTO;
using BLL.Validators;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using Moq;

namespace UnitTests.ValidatorsTests;

public class OfficeValidatorTests
{
    private readonly OfficeValidator _validator = new();
    private readonly Mock<IFormFile> _fileMock = new();

    [Theory]
    [InlineData(null, true)]
    [InlineData("image.jpg", true)]
    [InlineData("image.png", true)]
    [InlineData("document.pdf", false)]
    [InlineData("script.exe", false)]
    public void PhotoValidation(string? filename, bool expectedValid)
    {
        var dto = new OfficeRequestDto();

        if (filename is not null)
        {
            _fileMock.Setup(f => f.FileName).Returns(filename);
            dto.Photo = _fileMock.Object;
        }

        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.Photo);
        else result.ShouldHaveValidationErrorFor(x => x.Photo).WithErrorMessage("Invalid image format");
    }

    [Theory]
    [InlineData("", false)]
    [InlineData("London", true)]
    [InlineData("New York", true)]
    [InlineData("123", false)]
    [InlineData("City!", false)]
    public void CityValidation(string city, bool expectedValid)
    {
        var dto = new OfficeRequestDto { City = city };

        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.City);
        else result.ShouldHaveValidationErrorFor(x => x.City);
    }

    [Theory]
    [InlineData("", false)]
    [InlineData("Main Street", true)]
    [InlineData("123", false)]
    [InlineData("Street!", false)]
    public void StreetValidation(string street, bool expectedValid)
    {
        var dto = new OfficeRequestDto { Street = street };

        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.Street);
        else result.ShouldHaveValidationErrorFor(x => x.Street);
    }

    [Theory]
    [InlineData("", false)]
    [InlineData("123", true)]
    [InlineData("12A", true)]
    [InlineData("12/3", false)]
    [InlineData("12-3", false)]
    public void HouseNumberValidation(string houseNumber, bool expectedValid)
    {
        var dto = new OfficeRequestDto { HouseNumber = houseNumber };

        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.HouseNumber);
        else result.ShouldHaveValidationErrorFor(x => x.HouseNumber).WithErrorMessage("House number must contain only digits and optional letters");
    }

    [Theory]
    [InlineData(null, true)]
    [InlineData("", true)]
    [InlineData("45", true)]
    [InlineData("45B", true)]
    [InlineData("45/B", false)]
    public void OfficeNumberValidation(string? officeNumber, bool expectedValid)
    {
        var dto = new OfficeRequestDto { OfficeNumber = officeNumber };

        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.OfficeNumber);
        else result.ShouldHaveValidationErrorFor(x => x.OfficeNumber).WithErrorMessage("Office number must contain only digits and optional letters");
    }

    [Theory]
    [InlineData("", false)]
    [InlineData("+12345", true)]
    [InlineData("+380501234567", true)]
    [InlineData("12345", false)]
    [InlineData("+123", false)]
    [InlineData("+1234567890123456", false)]
    public void RegistryPhoneNumberValidation(string phone, bool expectedValid)
    {
        var dto = new OfficeRequestDto { RegistryPhoneNumber = phone };

        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.RegistryPhoneNumber);
        else result.ShouldHaveValidationErrorFor(x => x.RegistryPhoneNumber).WithErrorMessage("You've entered an invalid phone number");
    }

    [Fact]
    public void IsActiveShouldBeRequired()
    {
        var dto = new OfficeRequestDto();

        _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.IsActive).WithErrorMessage("Please, select the office's activity");
    }
}