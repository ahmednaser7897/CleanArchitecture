using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs.Accounts;

public class LoginResponseDto
{
    public required string Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Name { get; set; }
    public required string PhoneKey { get; set; }
    public required string PhoneNumber { get; set; }
    public required string ImageUrl { get; set; }
    public required DateTime? BirthDate { get; set; }
    public required Gender Gender { get; set; }
    public required IEnumerable<string> Roles { get; set; }
    public required TokenResponseDto TokenResponse { get; set; }
}

