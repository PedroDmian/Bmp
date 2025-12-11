using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Bmp.Application.UseCase;
using Bmp.Api.Responses;
using Bmp.Application.DTOs;

namespace Bmp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UsersController : ControllerBase
    {
        private readonly GetUsersUseCase _getUsersUseCase;

        public UsersController(GetUsersUseCase getUsersUseCase)
        {
            _getUsersUseCase = getUsersUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _getUsersUseCase.Execute();
                
                return Ok(ApiResponse<List<GetUsersResponse>>.SuccessResponse(
                    response,
                    message: "Users retrieved successfully"
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
}
