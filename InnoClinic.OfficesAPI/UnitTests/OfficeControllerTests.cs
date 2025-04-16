using API.Controllers;
using BLL.DTO;
using BLL.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UnitTests.TestCases;

namespace UnitTests
{
    public class OfficeControllerTests
    {
        private readonly Mock<IOfficeService> _officeServiceMock;
        private readonly OfficeController _controller;

        public OfficeControllerTests()
        {
            _officeServiceMock = new Mock<IOfficeService>();
            _controller = new OfficeController(_officeServiceMock.Object);
        }

        [Fact]
        public async Task CreateOfficeAsyncReturnsOkResult()
        {
            var dto = new OfficeRequestDto();
            var expectedResult = new OfficeResultDto();

            _officeServiceMock.Setup(s => s.CreateOfficeAsync(dto, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            var result = await _controller.CreateOfficeAsync(dto, CancellationToken.None);

            var actionResult = Assert.IsType<ActionResult<OfficeResultDto>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            okResult.Value.Should().Be(expectedResult);

            _officeServiceMock.Verify(s => s.CreateOfficeAsync(dto, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetOfficesAsyncReturnsListOfOffices()
        {
            var expectedResult = new List<OfficeResultDto> { new OfficeResultDto() };

            _officeServiceMock.Setup(s => s.GetOfficesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            var result = await _controller.GetOfficesAsync(CancellationToken.None);

            var actionResult = Assert.IsType<ActionResult<List<OfficeResultDto>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            okResult.Value.Should().BeEquivalentTo(expectedResult);

            _officeServiceMock.Verify(s => s.GetOfficesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetOfficeByIdAsyncReturnsOffice()
        {
            var id = TestConstants.TestRequestId;
            var expectedResult = new OfficeResultDto();

            _officeServiceMock.Setup(s => s.GetOfficeByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            var result = await _controller.GetOfficeByIdAsync(id, CancellationToken.None);

            var actionResult = Assert.IsType<ActionResult<OfficeResultDto>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            okResult.Value.Should().Be(expectedResult);

            _officeServiceMock.Verify(s => s.GetOfficeByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateOfficeAsyncReturnsUpdatedOffice()
        {
            var id = TestConstants.TestRequestId;
            var dto = new OfficeRequestDto();
            var expectedResult = new OfficeResultDto();

            _officeServiceMock.Setup(s => s.UpdateOfficeAsync(id, dto, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            var result = await _controller.UpdateOfficeAsync(id, dto, CancellationToken.None);

            var actionResult = Assert.IsType<ActionResult<OfficeResultDto>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            okResult.Value.Should().Be(expectedResult);

            _officeServiceMock.Verify(s => s.UpdateOfficeAsync(id, dto, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateOfficeStatusAsyncReturnsUpdatedOffice()
        {
            var id = TestConstants.TestRequestId;
            var isActive = true;
            var expectedResult = new OfficeResultDto();

            _officeServiceMock.Setup(s => s.UpdateOfficeStatusAsync(id, isActive, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            var result = await _controller.UpdateOfficeStatusAsync(id, isActive, CancellationToken.None);

            var actionResult = Assert.IsType<ActionResult<OfficeResultDto>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            okResult.Value.Should().Be(expectedResult);

            _officeServiceMock.Verify(s => s.UpdateOfficeStatusAsync(id, isActive, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteOfficeAsyncReturnsSuccessMessage()
        {
            var id = TestConstants.TestRequestId;
            _officeServiceMock.Setup(s => s.DeleteOfficeAsync(id, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var result = await _controller.DeleteOfficeAsync(id, CancellationToken.None);

            var okResult = Assert.IsType<OkObjectResult>(result);
            okResult.Value.Should().Be("The office was successfully deleted.");

            _officeServiceMock.Verify(s => s.DeleteOfficeAsync(id, It.IsAny<CancellationToken>()),Times.Once);
        }
    }
}