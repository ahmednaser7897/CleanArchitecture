using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Accounts;

public class LoginRequestDto
{
    [Required, DataType(DataType.EmailAddress)]
    public required string Email { get; set; } = "admin@test.com";

    [Required, DataType(DataType.Password)]
    public required string Password { get; set; } = "Admin@123";

}

