namespace Bmp.Application.DTOs;

public record AuthRequest(
    string Email,
    string Password
);