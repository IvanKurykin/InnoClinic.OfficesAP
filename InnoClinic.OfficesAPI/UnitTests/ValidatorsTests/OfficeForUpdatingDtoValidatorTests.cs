using BLL.DTO;
using BLL.Validators;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using Moq;

namespace UnitTests.ValidatorsTests;

public class OfficeForUpdatingDtoValidatorTests
{
    private readonly OfficeForUpdatingDtoValidator _validator = new();
    private readonly Mock<IFormFile> _fileMock = new();

    [Theory]
    [InlineData(null, true)]
    [InlineData("image.jpg", true)]
    [InlineData("document.pdf", false)]
    public void PhotoValidation(string? filename, bool expectedValid)
    {
        var dto = new OfficeForUpdatingDto();

        if (filename is not null)
        {
            _fileMock.Setup(f => f.FileName).Returns(filename);
            dto.Photo = _fileMock.Object;
        }

        if (expectedValid) _validator.TestValidate(dto).ShouldNotHaveValidationErrorFor(x => x.Photo);
        else _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.Photo);
    }

    [Theory]
    [InlineData("", false)]
    [InlineData("London", true)]
    [InlineData("New York", true)]
    [InlineData("123", false)]
    public void CityValidation(string city, bool expectedValid)
    {
        var dto = new OfficeForUpdatingDto { City = city };

        if (expectedValid) _validator.TestValidate(dto).ShouldNotHaveValidationErrorFor(x => x.City);
        else _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.City);
    }

    [Theory]
    [InlineData("", false)]
    [InlineData("Labrend", true)]
    [InlineData("Main Street", true)]
    [InlineData("123", false)]
    public void StreetValidation(string street, bool expectedValid)
    {
        var dto = new OfficeForUpdatingDto { Street = street };

        if (expectedValid) _validator.TestValidate(dto).ShouldNotHaveValidationErrorFor(x => x.Street);
        else _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.Street);
    }

    [Theory]
    [InlineData("", false)]
    [InlineData("123", true)]
    [InlineData("12A", true)]
    [InlineData("12/3", false)]
    public void HouseNumberValidation(string houseNumber, bool expectedValid)
    {
        var dto = new OfficeForUpdatingDto { HouseNumber = houseNumber };

        if (expectedValid) _validator.TestValidate(dto).ShouldNotHaveValidationErrorFor(x => x.HouseNumber);
        else _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.HouseNumber);
    }

    [Theory]
    [InlineData("", true)]
    [InlineData("45", true)]
    [InlineData("45B", true)]
    [InlineData("45/B", false)]
    public void OfficeNumberValidation(string officeNumber, bool expectedValid)
    {
        var dto = new OfficeForUpdatingDto { OfficeNumber = officeNumber };

        if (expectedValid) _validator.TestValidate(dto).ShouldNotHaveValidationErrorFor(x => x.OfficeNumber);
        else _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.OfficeNumber);
    }

    [Theory]
    [InlineData("", false)]
    [InlineData("+12345", true)]
    [InlineData("+380501234567", true)]
    [InlineData("12345", false)]
    [InlineData("+1234567890123456", false)]
    public void RegistryPhoneNumberValidation(string phone, bool expectedValid)
    {
        var dto = new OfficeForUpdatingDto { RegistryPhoneNumber = phone };

        if (expectedValid) _validator.TestValidate(dto).ShouldNotHaveValidationErrorFor(x => x.RegistryPhoneNumber);
        else _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.RegistryPhoneNumber);
    }
}