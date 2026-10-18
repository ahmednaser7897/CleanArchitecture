using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Accounts;

public class RegisterRequestDto
{
    [Required]
    public required string Name { get; set; }
    [Required, DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    [Required, DataType(DataType.Password)]
    public required string Password { get; set; }

    [Required]
    public required string Address { get; set; }
    [Required, DataType(DataType.PhoneNumber)]
    public required string Phone { get; set; }
    [Required]
    [RegularExpression(@"^\+[1-9]\d{0,2}$", ErrorMessage = "Phone key must be a valid-format country calling code, e.g. +20 or +966.")]
    public required string PhoneKey { get; set; }
    public string? Bio { get; set; }
    public string? ImageUrl { get; set; }
    [Required, DataType(DataType.Date)]
    public DateTime BirthDate { get; set; } = DateTime.UtcNow;
}

