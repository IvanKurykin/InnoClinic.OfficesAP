using BLL.Exceptions;
using BLL.Helpers;
using FluentAssertions;
using MongoDB.Bson;

namespace UnitTests.Helpers;

public class ParseHelperTests
{
    [Fact]
    public void ParseIdValidObjectIdStringReturnsObjectId()
    {
        var validId = ObjectId.GenerateNewId().ToString();

        var result = ParseHelper.ParseId(validId);

        result.Should().BeOfType<ObjectId>();
        result.ToString().Should().Be(validId);
    }

    [Fact]
    public void ParseIdInvalidObjectIdStringThrowsInvalidIdException()
    {
        var invalidId = "not-a-valid-id";

        Action act = () => ParseHelper.ParseId(invalidId);

        act.Should().Throw<InvalidIdException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData("123")]
    [InlineData("507f1f77bcf86cd79943901")]
    [InlineData("507f1f77bcf86cd7994390111")]
    public void ParseId_VariousInvalidInputsThrowsInvalidIdException(string invalidId)
    {
        Action act = () => ParseHelper.ParseId(invalidId);

        act.Should().Throw<InvalidIdException>();
    }
}