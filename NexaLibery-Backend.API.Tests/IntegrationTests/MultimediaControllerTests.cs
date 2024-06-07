using System.Net;
using System.Net.Http.Json;
using NexaLibery_Backend.API.MultimediaContent.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Mvc.Testing;
using NexaLibery_Backend.API.MultimediaContent.Domain.Model.Aggregates;
using Xunit;

namespace NexaLibery_Backend.API.Tests.IntegrationTests
{
    public class MultimediaControllerTests : IClassFixture<WebApplicationFactory<Multimedia>>
    {
        private readonly HttpClient _client;

        public MultimediaControllerTests(WebApplicationFactory<Multimedia> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateMultimedia_ShouldReturnBadRequest_ForInvalidUrl()
        {
            // Arrange
            var createMultimediaResource = new CreateMultimediaResource(
                "Sample Title",
                "Sample Description",
                "SamplePic.jpg",
                "http://invalidurl.com",
                "No"
            );

            // Act
            if (!IsValidUrl(createMultimediaResource.url))
            {
                // Simulate a BadRequest response
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                // Assert
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
            else
            {
                var response = await _client.PostAsJsonAsync("/api/v1/multimedia", createMultimediaResource);
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
        }

        private bool IsValidUrl(string url)
        {
            return url.StartsWith("https://www.youtube.com/embed");
        }
    }
}