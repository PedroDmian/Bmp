using Bmp.Domain.Repositories;
using Bmp.Domain.Exceptions;

namespace Bmp.Application.UseCase;

public class DeleteUserUseCase
{
    private readonly IUserRepository _userRepository;

    public DeleteUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Execute(Guid id)
    {
        var existingUser = await _userRepository.GetByIdAsync(id);

        if (existingUser == null)
        {
            throw new UserNotFoundException(id);
        }

        await _userRepository.DeleteAsync(id);
    }
}