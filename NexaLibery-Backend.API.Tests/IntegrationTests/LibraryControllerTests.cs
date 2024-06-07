using Microsoft.AspNetCore.Mvc;
using Moq;
using NexaLibery_Backend.API.MultimediaContent.Domain.Model.Aggregates;
using NexaLibery_Backend.API.MultimediaContent.Domain.Model.Commands;
using NexaLibery_Backend.API.MultimediaContent.Domain.Model.Queries;
using NexaLibery_Backend.API.MultimediaContent.Domain.Repositories;
using NexaLibery_Backend.API.MultimediaContent.Domain.Services;
using NexaLibery_Backend.API.MultimediaContent.Interfaces.REST;
using NexaLibery_Backend.API.MultimediaContent.Interfaces.REST.Resources;
using NexaLibery_Backend.API.Shared.Domain.Repositories;

namespace NexaLibery_Backend.API.Tests.IntegrationTests
{
    public class LibraryControllerTests
    {
        private readonly LibraryController _controller;
        private readonly Mock<ILibraryCommandService> _mockLibraryCommandService;
        private readonly Mock<ILibraryQueryService> _mockLibraryQueryService;

        public LibraryControllerTests()
        {
            // Mock para ILibraryRepository y IUnitOfWork
            var mockLibraryRepository = new Mock<ILibraryRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            
            _mockLibraryCommandService = new Mock<ILibraryCommandService>();
            _mockLibraryCommandService.Setup(x => x.Handle(It.IsAny<CreateLibraryCommand>())).ReturnsAsync(new Library());

            _mockLibraryQueryService = new Mock<ILibraryQueryService>();
            _mockLibraryQueryService.Setup(x => x.Handle(It.IsAny<GetAllLibraryQuery>())).ReturnsAsync(new List<Library>());

            
            _controller = new LibraryController(_mockLibraryCommandService.Object, _mockLibraryQueryService.Object);
        }

        [Fact]
        public async Task CreateLibrary_ValidInput_ReturnsCreatedAtAction()
        {
            // Arrange
            var createLibraryResource = new CreateLibraryResource(
                title: "Sample Title",
                description: "Sample Description",
                pic: "SamplePic.jpg",
                url: "http://sampleurl.com",
                premium: "No"
            );

            // Act
            var result = await _controller.CreateLibrary(createLibraryResource) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
            Assert.IsType<LibraryResource>(result.Value);
        }

        [Fact]
        public async Task GetAllLibrary_ReturnsOkResult()
        {
            // Act
            var result = await _controller.GetAllLibrary() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<LibraryResource>>(result.Value);
        }
    }
}