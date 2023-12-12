namespace Entities.Models;

public sealed class AppUserRole
{
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; }

    public Guid AppRoleId { get; set; }
    public AppRole AppRole { get; set; }
}