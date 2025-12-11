using Bmp.Application.DTOs;
using Bmp.Application.Interfaces;
using Bmp.Domain.Repositories;

namespace Bmp.Application.UseCase;

public class AuthTokenUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtService;

    public AuthTokenUseCase(IUserRepository userRepository, IJwtTokenService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<AuthResponse> Execute(AuthRequest authRequest)
    {
        var find = await _userRepository.GetByEmailAsync(authRequest.Email);

        if (find == null)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        if(find.Password != authRequest.Password)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        string token = _jwtService.GenerateToken(find);
        
        return new AuthResponse(
            find.Id,
            find.Email,
            find.FirstName,
            find.LastName,
            token
        );
    }
}