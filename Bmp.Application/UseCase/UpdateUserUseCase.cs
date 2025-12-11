using Bmp.Domain.Repositories;
using Bmp.Domain.Entities;
using Bmp.Domain.Exceptions;
using Bmp.Application.DTOs;

namespace Bmp.Application.UseCase;

public class UpdateUserUseCase
{
    private readonly IUserRepository _userRepository;

    public UpdateUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UpdateUserResponse> Execute(UpdateUserRequest updateUserRequest, Guid id)
    {
        var existingUser = await _userRepository.GetByIdAsync(id);

        if (existingUser == null)
        {
            throw new UserNotFoundException(id);
        }

        var users = await _userRepository.UpdateAsync(new User
        {
            Id = id,
            FirstName = updateUserRequest.FirstName,
            LastName = updateUserRequest.LastName,
            Image = updateUserRequest.Image
        });

        return new UpdateUserResponse(
            users.Id,
            users.Email,
            users.FirstName,
            users.LastName,
            users.Image
        );
    }
}