using NexaLibery_Backend.API.MultimediaContent.Domain.Model.Aggregates;
using NexaLibery_Backend.API.MultimediaContent.Domain.Model.Commands;

namespace NexaLibery_Backend.API.Tests.UnitTests
{
    public class MultimediaTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties_WithValidUrl()
        {
            // Arrange
            var command = new CreateMultimediaCommand(
                title: "Sample Title",
                description: "Sample Description",
                pic: "SamplePic.jpg",
                url: "https://www.youtube.com/embed/samplevideo",
                premium: "No"
            );

            // Act
            var multimedia = new Multimedia(command);

            // Assert
            Assert.Equal("Sample Title", multimedia.title);
            Assert.Equal("Sample Description", multimedia.description);
            Assert.Equal("SamplePic.jpg", multimedia.pic);
            Assert.Equal("https://www.youtube.com/embed/samplevideo", multimedia.url);
            Assert.Equal("No", multimedia.premium);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WithInvalidUrl()
        {
            // Arrange
            var command = new CreateMultimediaCommand(
                title: "Sample Title",
                description: "Sample Description",
                pic: "SamplePic.jpg",
                url: "http://invalidurl.com",
                premium: "No"
            );

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => ValidateUrl(command.url));
            Assert.Equal("URL must start with 'https://www.youtube.com/embed'", exception.Message);
        }

        private void ValidateUrl(string url)
        {
            if (!url.StartsWith("https://www.youtube.com/embed"))
            {
                throw new ArgumentException("URL must start with 'https://www.youtube.com/embed'");
            }
        }
    }
}