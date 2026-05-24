namespace PawClinic.Application.Features.Owners.Queries.GetAllOwners
{
    public class PagedOwnerListVm
    {
        public List<OwnerListVm> Owners { get; set; } = new();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
