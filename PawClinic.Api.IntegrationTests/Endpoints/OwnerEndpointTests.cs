using PawClinic.Api.IntegrationTests.Base;

namespace PawClinic.Api.IntegrationTests.Endpoints
{
    public class OwnerEndpointTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public OwnerEndpointTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllOwners_ReturnsSuccess()
        {
            var client = _factory.GetAnonymousClient();
            var response = await client.GetAsync("/api/owners");
            response.EnsureSuccessStatusCode();
        }
    }
}
