namespace Bmp.Application.DTOs;

public record AuthResponse(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    string Token
);
