using PawClinic.Domain.Common;

namespace PawClinic.Domain.Entities
{
    public class Owner : AuditableEntity
    {
        public Guid OwnerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public ICollection<Pet>? Pets { get; set; }
    }
}
