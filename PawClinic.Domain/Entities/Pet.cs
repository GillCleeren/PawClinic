using PawClinic.Domain.Common;
using PawClinic.Domain.Enums;

namespace PawClinic.Domain.Entities
{
    public class Pet : AuditableEntity
    {
        public Guid PetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Species Species { get; set; }
        public string? Breed { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsArchived { get; set; }
        public Guid OwnerId { get; set; }
        public Owner Owner { get; set; } = default!;
    }
}
