namespace PawClinic.Application.Features.Owners.Queries.GetOwnerById
{
    public class OwnerDetailVm
    {
        public Guid OwnerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<PetSummaryDto> Pets { get; set; } = new();
    }
}
