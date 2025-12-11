namespace Bmp.Application.DTOs;

public record GetUsersResponse(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    string? Image
);
