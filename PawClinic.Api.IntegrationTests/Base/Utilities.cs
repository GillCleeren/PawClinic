using PawClinic.Domain.Entities;
using PawClinic.Domain.Enums;
using PawClinic.Persistence;

namespace PawClinic.Api.IntegrationTests.Base
{
    public static class Utilities
    {
        public static void InitializeDbForTests(PawClinicDbContext context)
        {
            var vetPatelId = Guid.Parse("A1000000-0000-0000-0000-000000000001");
            var ownerJohnsonId = Guid.Parse("B1000000-0000-0000-0000-000000000001");
            var petBellaId = Guid.Parse("C1000000-0000-0000-0000-000000000001");

            context.Vets.Add(new Vet
            {
                VetId = vetPatelId,
                Name = "Dr. Sarah Patel",
                Specialisation = VetSpecialisation.GeneralPractice
            });

            context.Owners.Add(new Owner
            {
                OwnerId = ownerJohnsonId,
                Name = "Mrs. Emily Johnson",
                Email = "emily.johnson@example.com",
                PhoneNumber = "555-0101",
                Address = "14 Elm Street"
            });

            context.Pets.Add(new Pet
            {
                PetId = petBellaId,
                Name = "Bella",
                Species = Species.Dog,
                Breed = "Labrador Retriever",
                DateOfBirth = new DateTime(2022, 3, 15),
                OwnerId = ownerJohnsonId,
                IsArchived = false
            });

            context.SaveChanges();
        }
    }
}
