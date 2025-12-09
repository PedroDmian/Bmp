using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System.Text;

using Bmp.Application.DTOs;
using Bmp.Application.UseCase;
using Bmp.Api.Responses;
using Microsoft.AspNetCore.Authorization;

namespace Bmp.API.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly CreateUserUseCase _createUserUseCase;
    private readonly AuthTokenUseCase _authTokenUseCase;

    public AuthController(CreateUserUseCase createUserUseCase, AuthTokenUseCase authTokenUseCase)
    {
        _createUserUseCase = createUserUseCase;
        _authTokenUseCase = authTokenUseCase;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserRequest userRequest)
    {
        try
        {
            var hash = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: userRequest.Password,
                    salt: Encoding.UTF8.GetBytes("SALT_KEY123"),
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8
                )
            );
            userRequest = userRequest with { Password = hash };

            var response = await _createUserUseCase.execute(userRequest);
            
            return Ok(ApiResponse<CreateUserResponse>.SuccessResponse(
                response,
                message: "User created successfully"
            ));
        } 
        catch(InvalidOperationException ex)
        {
            return BadRequest(ApiResponse<object>.ErrorResponse(
                ex.Message,
                errorCode: 1309
            ));
        }
        catch(Exception ex)
        {
            return StatusCode(500, ApiResponse<object>.ErrorResponse(
                ex.Message,
                errorCode: 1000
            ));
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthRequest authRequest)
    {
        try
        {
            var hash = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: authRequest.Password,
                    salt: Encoding.UTF8.GetBytes("SALT_KEY123"),
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8
                )
            );
            authRequest = authRequest with { Password = hash };

            var response = await _authTokenUseCase.execute(authRequest);
            
            return Ok(ApiResponse<AuthResponse>.SuccessResponse(
                response,
                message: "User logged in successfully"
            ));
        } 
        catch(UnauthorizedAccessException ex)
        {
            return Unauthorized(ApiResponse<object>.ErrorResponse(
                ex.Message,
                errorCode: 1104
            ));
        }
        catch(Exception ex)
        {
            return StatusCode(500, ApiResponse<object>.ErrorResponse(
                ex.Message,
                errorCode: 1000
            ));
        }
    }
}