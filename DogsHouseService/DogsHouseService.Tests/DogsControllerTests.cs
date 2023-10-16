using Castle.Core.Logging;
using DogsHouseService.BLL.Abstractions;
using DogsHouseService.BLL.DTOs;
using DogsHouseService.BLL.DTOs.Common;
using DogsHouseService.Controllers;
using DogsHouseService.DAL.Models.Common;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace DogsHouseService.Tests
{
    public class DogsControllerTests
    {
        private readonly Mock<IDogsService> _dogsServiceMock = new();
        private readonly Mock<ILogger<DogsController>> _loggerMock = new();
        private readonly DogsController _dogsController;
        private readonly List<DogDto> _listOfDogs;
        private readonly DogDto _dogDto;

        public DogsControllerTests()
        {
            _dogsController = new DogsController(_dogsServiceMock.Object, _loggerMock.Object);

            _listOfDogs = new List<DogDto>
            {
                new DogDto
                {
                    Name = "Neo",
                    Color = "red&amber",
                    TailLength = 22,
                    Weight = 32,
                },
                new DogDto
                {
                    Name = "Jessy",
                    Color = "black&white",
                    TailLength = 7,
                    Weight = 14,
                },
            };

            _dogDto = new DogDto
            {
                Name = "Ricky",
                Color = "brown&white",
                TailLength = 9,
                Weight = 25,
            };
        }

        [Fact]
        public async Task GetDogs_Returns200Ok()
        {
            _dogsServiceMock.Setup(r =>
                r.GetDogsAsync(
                    It.IsAny<PagingParamsDto>(),
                    It.IsAny<SortingParamsDto>()))
                .ReturnsAsync(_listOfDogs);

            ActionResult<List<DogDto>> result = await _dogsController.GetDogs(new(), new());

            result.Should().NotBeNull();
            result.Result.Should().NotBeNull();
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetDogs_Returns500InternalServerError()
        {
            _dogsServiceMock.Setup(r =>
                r.GetDogsAsync(
                    It.IsAny<PagingParamsDto>(),
                    It.IsAny<SortingParamsDto>()))
                .Throws(new Exception());

            ActionResult<List<DogDto>> result = await _dogsController.GetDogs(new(), new());

            result.Should().NotBeNull();
            result.Result.As<ObjectResult>().StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task AddDog_Returns200Ok()
        {
            _dogsServiceMock.Setup(r =>
                r.AddDogAsync(It.IsAny<DogDto>()))
                .Returns(Task.CompletedTask);

            IActionResult result = await _dogsController.AddDog(_dogDto);

            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task AddDog_Returns400BadRequest()
        {
            _dogsServiceMock.Setup(r =>
                r.AddDogAsync(It.IsAny<DogDto>()))
                .Throws(new ArgumentException());

            IActionResult result = await _dogsController.AddDog(_dogDto);

            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }
        
        [Fact]
        public async Task AddDog_Returns409Conflict()
        {
            _dogsServiceMock.Setup(r =>
                r.AddDogAsync(It.IsAny<DogDto>()))
                .Throws(new InvalidOperationException());

            IActionResult result = await _dogsController.AddDog(_dogDto);

            result.Should().NotBeNull();
            result.Should().BeOfType<ConflictResult>();
        }
        
        [Fact]
        public async Task AddDog_Returns500InternalServerError()
        {
            _dogsServiceMock.Setup(r =>
                r.AddDogAsync(It.IsAny<DogDto>()))
                .Throws(new Exception());

            IActionResult result = await _dogsController.AddDog(_dogDto);

            result.Should().NotBeNull();
            result.As<ObjectResult>().StatusCode.Should().Be(500);
        }
    }
}
