namespace Bmp.Application.DTOs;

public record CreateUserRequest(
    string LastName,
    string FirstName,
    string Email,
    string Password
);