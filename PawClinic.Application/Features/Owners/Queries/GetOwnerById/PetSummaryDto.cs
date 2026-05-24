using PawClinic.Domain.Enums;

namespace PawClinic.Application.Features.Owners.Queries.GetOwnerById
{
    public class PetSummaryDto
    {
        public Guid PetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Species Species { get; set; }
        public string? Breed { get; set; }
        public bool IsArchived { get; set; }
    }
}
