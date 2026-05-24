namespace PawClinic.App.ViewModels;

public class PetViewModel
{
    public Guid PetId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Species { get; set; } = string.Empty;
    public string? Breed { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsArchived { get; set; }
    public Guid OwnerId { get; set; }
}
