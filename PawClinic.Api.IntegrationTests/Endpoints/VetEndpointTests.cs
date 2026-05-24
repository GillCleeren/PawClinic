using PawClinic.Api.IntegrationTests.Base;

namespace PawClinic.Api.IntegrationTests.Endpoints
{
    public class VetEndpointTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public VetEndpointTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllVets_ReturnsSuccess()
        {
            var client = _factory.GetAnonymousClient();
            var response = await client.GetAsync("/api/vets");
            response.EnsureSuccessStatusCode();
        }
    }
}
