using PawClinic.Application.Exceptions;
using PawClinic.Application.Features.Appointments.Commands.CancelAppointment;
using PawClinic.Application.UnitTests.Mocks;

namespace PawClinic.Application.UnitTests.Appointments.Commands
{
    public class CancelAppointmentTests
    {
        [Fact]
        public async Task Handle_CompletedAppointment_ThrowsBadRequestException()
        {
            var mockRepo = RepositoryMocks.GetAppointmentRepository();
            // The mock has one appointment with Id D1 and Status Completed
            var completedId = Guid.Parse("D1000000-0000-0000-0000-000000000001");

            var handler = new CancelAppointmentCommandHandler(mockRepo.Object);

            await Should.ThrowAsync<BadRequestException>(async () =>
                await handler.Handle(new CancelAppointmentCommand { AppointmentId = completedId }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_NonExistentAppointment_ThrowsNotFoundException()
        {
            var mockRepo = RepositoryMocks.GetAppointmentRepository();
            var handler = new CancelAppointmentCommandHandler(mockRepo.Object);

            await Should.ThrowAsync<NotFoundException>(async () =>
                await handler.Handle(new CancelAppointmentCommand { AppointmentId = Guid.NewGuid() }, CancellationToken.None));
        }
    }
}
