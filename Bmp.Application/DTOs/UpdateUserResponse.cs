namespace Bmp.Application.DTOs;

public record UpdateUserResponse(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    string? Image
);
