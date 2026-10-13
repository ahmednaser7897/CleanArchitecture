namespace Application.DTOs.Accounts;

public class TokenResponseDto
{
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
    public required DateTime ExpiresAt { get; set; }
}

