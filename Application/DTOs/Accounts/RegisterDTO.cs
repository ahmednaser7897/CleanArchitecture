using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Accounts;

public class RegisterDTO
{
    [Required]
    public required string Name { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [Required]
    public required string Address { get; set; }
    [Required]
    public required string Phone { get; set; }
    [Required]
    public required string PhoneKey { get; set; }
    public string? Bio { get; set; }
    public string? ImageUrl { get; set; }
    [Required]
    public DateTime BirthDate { get; set; } = DateTime.Now;
}

