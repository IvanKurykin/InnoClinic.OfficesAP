using BLL.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace UnitTests.Helpers;

public class ValidationHelperTests
{
    [Theory]
    [InlineData("test.jpg", true)]
    [InlineData("test.png", true)]
    [InlineData("test.jpeg", false)]
    [InlineData("test.gif", false)]
    [InlineData("test.pdf", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public void BeAValidImageValidatesImageExtensions(string? filename, bool expected)
    {
        IFormFile? file = null;
        if (!string.IsNullOrEmpty(filename))
        {
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.FileName).Returns(filename);
            file = fileMock.Object;
        }

        var result = ValidationHelper.BeAValidImage(file);

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("12345", true)]
    [InlineData("ABC123", true)]
    [InlineData("", false)]
    [InlineData(null, false)]
    [InlineData("123.45", false)]
    [InlineData("ABC!123", false)]
    public void BeAValidNumberValidatesNumberFormat(string? number, bool expected)
    {
        var result = ValidationHelper.IsNumberValid(number);

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("12345", true, true)]
    [InlineData("", true, true)]
    [InlineData(null, true, true)]
    [InlineData("ABC123", true, true)]
    [InlineData("", false, false)]
    [InlineData(null, false, false)]
    public void BeAValidNumberWithOrEmptyOptionValidatesCorrectly(string? number, bool orEmpty, bool expected)
    {
        var result = ValidationHelper.IsNumberValid(number, orEmpty);

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("John Doe", true)]
    [InlineData("Jane-Doe", true)]
    [InlineData("O'Reilly", false)]
    [InlineData("John123", false)]
    [InlineData("John_Doe", false)]
    [InlineData("", false)]
    public void BeAValidName_alidatesNameFormat(string name, bool expected)
    {
        var result = ValidationHelper.BeAValidName(name);

        result.Should().Be(expected);
    }
}