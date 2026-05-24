using AutoMapper;
using PawClinic.Application.Features.Appointments.Commands.ScheduleAppointment;
using PawClinic.Application.Profiles;
using PawClinic.Application.UnitTests.Mocks;
using Microsoft.Extensions.Logging.Abstractions;

namespace PawClinic.Application.UnitTests.Appointments.Commands
{
    public class ScheduleAppointmentTests
    {
        private readonly IMapper _mapper;

        public ScheduleAppointmentTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>(), new NullLoggerFactory());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Handle_PastDate_ThrowsValidationException()
        {
            var mockRepo = RepositoryMocks.GetAppointmentRepository();
            var handler = new ScheduleAppointmentCommandHandler(mockRepo.Object, _mapper);

            await Should.ThrowAsync<PawClinic.Application.Exceptions.ValidationException>(async () =>
                await handler.Handle(new ScheduleAppointmentCommand
                {
                    PetId = Guid.NewGuid(),
                    VetId = Guid.NewGuid(),
                    ScheduledDateTime = DateTime.UtcNow.AddDays(-1),
                    ReasonForVisit = "Test"
                }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_VetDoubleBooked_ThrowsValidationException()
        {
            var mockRepo = RepositoryMocks.GetAppointmentRepository();
            // Override double-booking to return true for vet
            mockRepo.Setup(r => r.IsVetDoubleBookedAsync(It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<Guid?>()))
                .ReturnsAsync(true);

            var handler = new ScheduleAppointmentCommandHandler(mockRepo.Object, _mapper);

            await Should.ThrowAsync<PawClinic.Application.Exceptions.ValidationException>(async () =>
                await handler.Handle(new ScheduleAppointmentCommand
                {
                    PetId = Guid.NewGuid(),
                    VetId = Guid.NewGuid(),
                    ScheduledDateTime = DateTime.UtcNow.AddDays(7),
                    ReasonForVisit = "Test"
                }, CancellationToken.None));
        }
    }
}
