using Domain.Entities;

namespace Application.DTOs.Accounts;

public class UserDto
{
    public string Id { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string PhoneKey { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public Domain.Enums.Gender Gender { get; set; }

    public static UserDto FromEntit(ApplicationUser user)
    {
        return new UserDto()
        {
            Id = user.Id,
            UserName = user.UserName ?? "",
            Email = user.Email ?? "",
            Name = user.Name ?? "",
            PhoneKey = user.PhoneKey ?? "",
            PhoneNumber = user.PhoneNumber ?? "",
            ImageUrl = user.ImageUrl ?? "",
            BirthDate = user.BirthDate,
            Gender = user.Gender ?? Domain.Enums.Gender.Male,
        };
    }
}
