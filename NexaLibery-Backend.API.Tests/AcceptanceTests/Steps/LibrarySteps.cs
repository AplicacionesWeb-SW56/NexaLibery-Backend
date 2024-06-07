using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NexaLibery_Backend.API.MultimediaContent.Domain.Model.Aggregates;
using NexaLibery_Backend.API.MultimediaContent.Interfaces.REST.Resources;
using TechTalk.SpecFlow;
using Xunit;

namespace NexaLibery_Backend.API.Tests.AcceptanceTests.Steps
{
    [Binding]
    public class LibrarySteps
    {
        private readonly WebApplicationFactory<Library> _factory;
        private readonly HttpClient _client;
        private HttpResponseMessage _response;
        private CreateLibraryResource _libraryResource;

        public LibrarySteps(WebApplicationFactory<Library> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Given(@"I have a library resource with title ""(.*)"" and description ""(.*)""")]
        public void GivenIHaveALibraryResourceWithTitleAndDescription(string title, string description)
        {
            _libraryResource = new CreateLibraryResource(
                title,
                description,
                "SamplePic.jpg",
                "http://sampleurl.com",
                "No"
            );
        }

        [When(@"I send a POST request to ""(.*)"" with the library resource")]
        public async Task WhenISendAPOSTRequestToWithTheLibraryResource(string url)
        {
            var jsonContent = JsonConvert.SerializeObject(_libraryResource);
            _response = await _client.PostAsync(url, new StringContent(jsonContent, Encoding.UTF8, "application/json"));
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int statusCode)
        {
            Assert.Equal((HttpStatusCode)statusCode, _response.StatusCode);
        }

        [Then(@"the response should contain the created library with title ""(.*)""")]
        public async Task ThenTheResponseShouldContainTheCreatedLibraryWithTitle(string title)
        {
            var content = await _response.Content.ReadAsStringAsync();
            var createdLibrary = JsonConvert.DeserializeObject<LibraryResource>(content);
            Assert.Equal(title, createdLibrary.title);
        }
    }
}
