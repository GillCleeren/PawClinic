namespace PawClinic.Application.Features.Owners.Queries.GetAllOwners
{
    public class OwnerListVm
    {
        public Guid OwnerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
