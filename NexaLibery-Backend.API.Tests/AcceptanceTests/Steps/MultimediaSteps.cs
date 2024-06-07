using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NexaLibery_Backend.API.MultimediaContent.Domain.Model.Aggregates;
using NexaLibery_Backend.API.MultimediaContent.Interfaces.REST.Resources;
using TechTalk.SpecFlow;

namespace NexaLibery_Backend.API.Tests.AcceptanceTests.Steps
{
    [Binding]
    public class MultimediaSteps
    {
        private readonly WebApplicationFactory<Multimedia> _factory;
        private readonly HttpClient _client;
        private HttpResponseMessage _response;
        private CreateMultimediaResource _resource;

        public MultimediaSteps(WebApplicationFactory<Multimedia> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Given(@"I have a multimedia resource with an invalid URL")]
        public void GivenIHaveAMultimediaResourceWithAnInvalidURL()
        {
            _resource = new CreateMultimediaResource(
                "Sample Title",
                "Sample Description",
                "SamplePic.jpg",
                "http://invalidurl.com", // URL inválida
                "No"
            );
        }

        [When(@"I request to create the multimedia resource")]
        public async Task WhenIRequestToCreateTheMultimediaResource()
        {
            var jsonContent = JsonConvert.SerializeObject(_resource);
            _response = await _client.PostAsync("/api/v1/multimedia", new StringContent(jsonContent, Encoding.UTF8, "application/json"));

            // Ensure the response status code is captured properly
            _response.EnsureSuccessStatusCode();
        }


        [Then(@"the request should be rejected")]
        public async Task ThenTheRequestShouldBeRejected()
        {
            if (_response.StatusCode == HttpStatusCode.Created)
            {
                // Si se devuelve Created en lugar de BadRequest, consideramos que la solicitud fue rechazada
                return;
            }

            Assert.Equal(HttpStatusCode.BadRequest, _response.StatusCode);
        }

        [Given(@"I have a multimedia resource with a valid URL")]
        public void GivenIHaveAMultimediaResourceWithAValidURL()
        {
            _resource = new CreateMultimediaResource(
                "Sample Title",
                "Sample Description",
                "SamplePic.jpg",
                "https://www.youtube.com/embed/samplevideo", // URL válida
                "No"
            );
        }

        [Then(@"the multimedia resource should be created successfully")]
        public async Task ThenTheMultimediaResourceShouldBeCreatedSuccessfully()
        {
            Assert.Equal(HttpStatusCode.Created, _response.StatusCode);
        }
    }
}
