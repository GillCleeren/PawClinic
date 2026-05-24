using Microsoft.EntityFrameworkCore;
using PawClinic.Application.Contracts;
using PawClinic.Domain.Common;
using PawClinic.Domain.Entities;
using PawClinic.Domain.Enums;

namespace PawClinic.Persistence
{
    public class PawClinicDbContext : DbContext
    {
        private readonly ILoggedInUserService? _loggedInUserService;

        public PawClinicDbContext(DbContextOptions<PawClinicDbContext> options, ILoggedInUserService loggedInUserService)
            : base(options)
        {
            _loggedInUserService = loggedInUserService;
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Vet> Vets { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PawClinicDbContext).Assembly);

            // --- Vet GUIDs ---
            var vetPatelId = Guid.Parse("A1000000-0000-0000-0000-000000000001");
            var vetOkaforId = Guid.Parse("A2000000-0000-0000-0000-000000000002");
            var vetLinId = Guid.Parse("A3000000-0000-0000-0000-000000000003");

            // --- Owner GUIDs ---
            var ownerJohnsonId = Guid.Parse("B1000000-0000-0000-0000-000000000001");
            var ownerDuboisId = Guid.Parse("B2000000-0000-0000-0000-000000000002");
            var ownerReyesId = Guid.Parse("B3000000-0000-0000-0000-000000000003");

            // --- Pet GUIDs ---
            var petBellaId = Guid.Parse("C1000000-0000-0000-0000-000000000001");
            var petOscarId = Guid.Parse("C2000000-0000-0000-0000-000000000002");
            var petPipId = Guid.Parse("C3000000-0000-0000-0000-000000000003");
            var petLunaId = Guid.Parse("C4000000-0000-0000-0000-000000000004");

            // --- Appointment GUIDs ---
            var appt1Id = Guid.Parse("D1000000-0000-0000-0000-000000000001");
            var appt2Id = Guid.Parse("D2000000-0000-0000-0000-000000000002");
            var appt3Id = Guid.Parse("D3000000-0000-0000-0000-000000000003");
            var appt4Id = Guid.Parse("D4000000-0000-0000-0000-000000000004");
            var appt5Id = Guid.Parse("D5000000-0000-0000-0000-000000000005");

            // Seed Vets
            modelBuilder.Entity<Vet>().HasData(
                new Vet { VetId = vetPatelId, Name = "Dr. Sarah Patel", Specialisation = VetSpecialisation.GeneralPractice },
                new Vet { VetId = vetOkaforId, Name = "Dr. James Okafor", Specialisation = VetSpecialisation.Surgery },
                new Vet { VetId = vetLinId, Name = "Dr. Mei Lin", Specialisation = VetSpecialisation.Dentistry }
            );

            // Seed Owners
            modelBuilder.Entity<Owner>().HasData(
                new Owner { OwnerId = ownerJohnsonId, Name = "Mrs. Emily Johnson", Email = "emily.johnson@example.com", PhoneNumber = "555-0101", Address = "14 Elm Street, Springfield" },
                new Owner { OwnerId = ownerDuboisId, Name = "Mr. Pierre Dubois", Email = "pierre.dubois@example.com", PhoneNumber = "555-0102", Address = "7 Maple Avenue, Shelbyville" },
                new Owner { OwnerId = ownerReyesId, Name = "Ms. Carmen Reyes", Email = "carmen.reyes@example.com", PhoneNumber = "555-0103", Address = "23 Oak Lane, Capital City" }
            );

            // Seed Pets
            modelBuilder.Entity<Pet>().HasData(
                new Pet { PetId = petBellaId, Name = "Bella", Species = Species.Dog, Breed = "Labrador Retriever", DateOfBirth = new DateTime(2022, 3, 15), OwnerId = ownerJohnsonId, IsArchived = false },
                new Pet { PetId = petOscarId, Name = "Oscar", Species = Species.Cat, Breed = "Maine Coon", DateOfBirth = new DateTime(2020, 7, 4), OwnerId = ownerDuboisId, IsArchived = false },
                new Pet { PetId = petPipId, Name = "Pip", Species = Species.Rabbit, Breed = "Holland Lop", DateOfBirth = new DateTime(2023, 1, 20), OwnerId = ownerDuboisId, IsArchived = false },
                new Pet { PetId = petLunaId, Name = "Luna", Species = Species.Dog, Breed = "Border Collie", DateOfBirth = new DateTime(2024, 5, 10), OwnerId = ownerReyesId, IsArchived = false }
            );

            // Seed Appointments
            // appt1 — Completed: Bella's routine vaccination with Dr. Patel (past)
            modelBuilder.Entity<Appointment>().HasData(new Appointment
            {
                AppointmentId = appt1Id,
                PetId = petBellaId,
                VetId = vetPatelId,
                ScheduledDateTime = new DateTime(2025, 4, 10, 10, 0, 0, DateTimeKind.Utc),
                ReasonForVisit = "Annual vaccination",
                Status = AppointmentStatus.Completed,
                Notes = "Routine vaccination administered. All clear. Next visit in 12 months."
            });

            // appt2 — Completed: Oscar's dental check with Dr. Lin (past)
            modelBuilder.Entity<Appointment>().HasData(new Appointment
            {
                AppointmentId = appt2Id,
                PetId = petOscarId,
                VetId = vetLinId,
                ScheduledDateTime = new DateTime(2025, 5, 3, 14, 0, 0, DateTimeKind.Utc),
                ReasonForVisit = "Dental check-up",
                Status = AppointmentStatus.Completed,
                Notes = "Mild tartar build-up noted. Dental clean recommended in 6 months."
            });

            // appt3 — Cancelled: Pip's follow-up (cancelled before it happened)
            modelBuilder.Entity<Appointment>().HasData(new Appointment
            {
                AppointmentId = appt3Id,
                PetId = petPipId,
                VetId = vetPatelId,
                ScheduledDateTime = new DateTime(2025, 5, 20, 11, 0, 0, DateTimeKind.Utc),
                ReasonForVisit = "Weight check",
                Status = AppointmentStatus.Cancelled,
                Notes = "Owner cancelled — will reschedule."
            });

            // appt4 — Scheduled: Bella's follow-up with Dr. Patel (future)
            modelBuilder.Entity<Appointment>().HasData(new Appointment
            {
                AppointmentId = appt4Id,
                PetId = petBellaId,
                VetId = vetPatelId,
                ScheduledDateTime = new DateTime(2027, 6, 15, 10, 0, 0, DateTimeKind.Utc),
                ReasonForVisit = "Annual check-up",
                Status = AppointmentStatus.Scheduled,
                Notes = null
            });

            // appt5 — Scheduled: Luna's first visit with Dr. Okafor (future)
            modelBuilder.Entity<Appointment>().HasData(new Appointment
            {
                AppointmentId = appt5Id,
                PetId = petLunaId,
                VetId = vetOkaforId,
                ScheduledDateTime = new DateTime(2027, 6, 20, 9, 0, 0, DateTimeKind.Utc),
                ReasonForVisit = "New patient consultation",
                Status = AppointmentStatus.Scheduled,
                Notes = null
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        entry.Entity.CreatedBy = _loggedInUserService?.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = _loggedInUserService?.UserId;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
