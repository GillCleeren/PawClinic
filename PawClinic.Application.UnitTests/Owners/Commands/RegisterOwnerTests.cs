using AutoMapper;
using PawClinic.Application.Features.Owners.Commands.RegisterOwner;
using PawClinic.Application.Profiles;
using PawClinic.Application.UnitTests.Mocks;
using Microsoft.Extensions.Logging.Abstractions;

namespace PawClinic.Application.UnitTests.Owners.Commands
{
    public class RegisterOwnerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<Application.Contracts.Persistence.IOwnerRepository> _mockOwnerRepository;

        public RegisterOwnerTests()
        {
            _mockOwnerRepository = RepositoryMocks.GetOwnerRepository();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>(), new NullLoggerFactory());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidOwner_AddedToRepository()
        {
            var handler = new RegisterOwnerCommandHandler(_mockOwnerRepository.Object, _mapper);

            var result = await handler.Handle(new RegisterOwnerCommand
            {
                Name = "New Owner",
                Email = "new.owner@example.com",
                PhoneNumber = "555-9999",
                Address = "99 New Street"
            }, CancellationToken.None);

            result.Success.ShouldBeTrue();
            var allOwners = await _mockOwnerRepository.Object.ListAllAsync();
            allOwners.Count.ShouldBe(4);
        }

        [Fact]
        public async Task Handle_DuplicateEmail_ReturnsFailure()
        {
            var handler = new RegisterOwnerCommandHandler(_mockOwnerRepository.Object, _mapper);

            var result = await handler.Handle(new RegisterOwnerCommand
            {
                Name = "Duplicate",
                Email = "emily.johnson@example.com",  // already in mock
                PhoneNumber = "555-0000",
                Address = "1 Test Road"
            }, CancellationToken.None);

            result.Success.ShouldBeFalse();
            result.ValidationErrors.ShouldNotBeEmpty();
        }
    }
}
