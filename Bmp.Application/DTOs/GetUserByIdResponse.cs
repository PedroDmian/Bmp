namespace Bmp.Application.DTOs;

public record GetUserByIdResponse(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    string? Image
);
