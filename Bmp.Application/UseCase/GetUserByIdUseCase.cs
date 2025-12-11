using Bmp.Domain.Repositories;
using Bmp.Domain.Exceptions;
using Bmp.Application.DTOs;

namespace Bmp.Application.UseCase;

public class GetUserByIdUseCase
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUserByIdResponse> Execute(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
        {
            throw new UserNotFoundException(id);
        }

        return new GetUserByIdResponse(
            user.Id,
            user.Email,
            user.FirstName,
            user.LastName,
            user.Image
        );
    }
}