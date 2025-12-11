namespace Bmp.Application.DTOs;

public record UpdateUserRequest(
    string LastName,
    string FirstName,
    string Image
);