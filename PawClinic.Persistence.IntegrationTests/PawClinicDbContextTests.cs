using Microsoft.EntityFrameworkCore;
using PawClinic.Application.Contracts;
using PawClinic.Domain.Entities;
using PawClinic.Persistence;

namespace PawClinic.Persistence.IntegrationTests
{
    public class PawClinicDbContextTests
    {
        private readonly PawClinicDbContext _dbContext;
        private readonly Mock<ILoggedInUserService> _loggedInUserServiceMock;
        private readonly string _loggedInUserId;

        public PawClinicDbContextTests()
        {
            var options = new DbContextOptionsBuilder<PawClinicDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _loggedInUserId = "00000000-0000-0000-0000-000000000000";
            _loggedInUserServiceMock = new Mock<ILoggedInUserService>();
            _loggedInUserServiceMock.Setup(m => m.UserId).Returns(_loggedInUserId);

            _dbContext = new PawClinicDbContext(options, _loggedInUserServiceMock.Object);
        }

        [Fact]
        public async Task Save_SetCreatedByProperty()
        {
            var owner = new Owner
            {
                OwnerId = Guid.NewGuid(),
                Name = "Test Owner",
                Email = "test@example.com",
                PhoneNumber = "555-0000",
                Address = "1 Test Road"
            };

            _dbContext.Owners.Add(owner);
            await _dbContext.SaveChangesAsync();

            owner.CreatedBy.ShouldBe(_loggedInUserId);
        }
    }
}
