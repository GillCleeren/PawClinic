using PawClinic.Domain.Common;
using PawClinic.Domain.Enums;

namespace PawClinic.Domain.Entities
{
    public class Vet : AuditableEntity
    {
        public Guid VetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public VetSpecialisation Specialisation { get; set; }
    }
}
