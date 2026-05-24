using PawClinic.Application.Contracts.Persistence;
using PawClinic.Domain.Entities;
using PawClinic.Domain.Enums;

namespace PawClinic.Application.UnitTests.Mocks
{
    public static class RepositoryMocks
    {
        public static Mock<IOwnerRepository> GetOwnerRepository()
        {
            var ownerJohnsonId = Guid.Parse("B1000000-0000-0000-0000-000000000001");
            var ownerDuboisId = Guid.Parse("B2000000-0000-0000-0000-000000000002");
            var ownerReyesId = Guid.Parse("B3000000-0000-0000-0000-000000000003");

            var owners = new List<Owner>
            {
                new Owner { OwnerId = ownerJohnsonId, Name = "Mrs. Emily Johnson", Email = "emily.johnson@example.com", PhoneNumber = "555-0101", Address = "14 Elm Street" },
                new Owner { OwnerId = ownerDuboisId, Name = "Mr. Pierre Dubois", Email = "pierre.dubois@example.com", PhoneNumber = "555-0102", Address = "7 Maple Avenue" },
                new Owner { OwnerId = ownerReyesId, Name = "Ms. Carmen Reyes", Email = "carmen.reyes@example.com", PhoneNumber = "555-0103", Address = "23 Oak Lane" }
            };

            var mockRepo = new Mock<IOwnerRepository>();
            mockRepo.Setup(r => r.ListAllAsync()).ReturnsAsync(owners);
            mockRepo.Setup(r => r.IsEmailUniqueAsync(It.IsAny<string>()))
                .ReturnsAsync((string email) => !owners.Any(o => o.Email == email));
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => owners.FirstOrDefault(o => o.OwnerId == id));
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Owner>()))
                .ReturnsAsync((Owner owner) =>
                {
                    owner.OwnerId = Guid.NewGuid();
                    owners.Add(owner);
                    return owner;
                });

            return mockRepo;
        }

        public static Mock<IAppointmentRepository> GetAppointmentRepository()
        {
            var petBellaId = Guid.Parse("C1000000-0000-0000-0000-000000000001");
            var vetPatelId = Guid.Parse("A1000000-0000-0000-0000-000000000001");

            var appointments = new List<Appointment>
            {
                new Appointment
                {
                    AppointmentId = Guid.Parse("D1000000-0000-0000-0000-000000000001"),
                    PetId = petBellaId,
                    VetId = vetPatelId,
                    ScheduledDateTime = new DateTime(2025, 4, 10, 10, 0, 0, DateTimeKind.Utc),
                    ReasonForVisit = "Annual vaccination",
                    Status = AppointmentStatus.Completed,
                    Notes = "All clear."
                }
            };

            var mockRepo = new Mock<IAppointmentRepository>();
            mockRepo.Setup(r => r.ListAllAsync()).ReturnsAsync(appointments);
            mockRepo.Setup(r => r.IsVetDoubleBookedAsync(It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<Guid?>()))
                .ReturnsAsync(false);
            mockRepo.Setup(r => r.IsPetDoubleBookedAsync(It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<Guid?>()))
                .ReturnsAsync(false);
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Appointment>()))
                .ReturnsAsync((Appointment appt) =>
                {
                    appt.AppointmentId = Guid.NewGuid();
                    appointments.Add(appt);
                    return appt;
                });
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => appointments.FirstOrDefault(a => a.AppointmentId == id));

            return mockRepo;
        }
    }
}
