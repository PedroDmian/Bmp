namespace Bmp.Application.DTOs;

public record CreateUserResponse(
    Guid Id,
    string Email,
    string FirstName,
    string LastName
);
