using Bmp.Domain.Repositories;
using Bmp.Domain.Entities;
using Bmp.Application.DTOs;

namespace Bmp.Application.UseCase;

public class GetUsersUseCase
{
    private readonly IUserRepository _userRepository;

    public GetUsersUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<GetUsersResponse>> Execute()
    {
        var users = await _userRepository.GetAllAsync();

        return users.Select(user => new GetUsersResponse(
            Id: user.Id,
            Email: user.Email,
            FirstName: user.FirstName,
            LastName: user.LastName,
            Image: user.Image
        )).ToList();
    }
}