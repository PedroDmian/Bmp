using Bmp.Domain.Repositories;
using Bmp.Domain.Entities;
using Bmp.Application.DTOs;

namespace Bmp.Application.UseCase;

public class CreateUserUseCase
{
    private readonly IUserRepository _userRepository;

    public CreateUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<CreateUserResponse> Execute(CreateUserRequest userRequest)
    {
        var existing = await _userRepository.GetByEmailAsync(userRequest.Email);

        if (existing != null)
        {
            throw new InvalidOperationException("A user with this email already exists.");
        }

        var user = new User
        {
            Email = userRequest.Email,
            Password = userRequest.Password,
            FirstName = userRequest.FirstName,
            LastName = userRequest.LastName
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return new CreateUserResponse(
            user.Id,
            user.Email,
            user.FirstName,
            user.LastName
        );
    }
}