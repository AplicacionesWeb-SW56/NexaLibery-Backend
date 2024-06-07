using NexaLibery_Backend.API.MultimediaContent.Domain.Model.Aggregates;
using NexaLibery_Backend.API.MultimediaContent.Domain.Model.Commands;

namespace NexaLibery_Backend.API.Tests.UnitTests
{
    public class LibraryTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var command = new CreateLibraryCommand(
                title: "Sample Title",
                description: "Sample Description",
                pic: "SamplePic.jpg",
                url: "http://sampleurl.com",
                premium: "No"
            );

            // Act
            var library = new Library(command);

            // Assert
            Assert.Equal("Sample Title", library.title);
            Assert.Equal("Sample Description", library.description);
            Assert.Equal("SamplePic.jpg", library.pic);
            Assert.Equal("http://sampleurl.com", library.url);
            Assert.Equal("No", library.premium);
        }
    }
}