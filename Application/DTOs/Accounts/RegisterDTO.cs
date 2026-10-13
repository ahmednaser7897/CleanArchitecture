using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Accounts;

public class RegisterDTO
{
    [Required]
    public required string UserName { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [Required]
    public required string Address { get; set; }
}

