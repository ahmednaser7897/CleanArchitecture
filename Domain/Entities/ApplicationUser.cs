
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;


public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = string.Empty;
    public string? Bio { get; set; }

    public string PhoneKey { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public DateTime? BirthDate { get; set; } = null;

}
