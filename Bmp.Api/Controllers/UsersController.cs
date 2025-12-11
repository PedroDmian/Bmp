using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Bmp.Api.Responses;
using Bmp.Application.UseCase;
using Bmp.Application.DTOs;
using Bmp.Domain.Exceptions;

namespace Bmp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UsersController : ControllerBase
    {
        private readonly GetUsersUseCase _getUsersUseCase;
        private readonly GetUserByIdUseCase _getUserByIdUseCase;
        private readonly UpdateUserUseCase _updateUserUseCase;
        private readonly DeleteUserUseCase _deleteUserUseCase;

        public UsersController(
            GetUsersUseCase getUsersUseCase,
            GetUserByIdUseCase getUserByIdUseCase,
            UpdateUserUseCase updateUserUseCase,
            DeleteUserUseCase deleteUserUseCase
        ) {
            _getUsersUseCase = getUsersUseCase;
            _getUserByIdUseCase = getUserByIdUseCase;
            _updateUserUseCase = updateUserUseCase;
            _deleteUserUseCase = deleteUserUseCase;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var response = await _getUserByIdUseCase.Execute(id);
                
                return Ok(ApiResponse<GetUserByIdResponse>.SuccessResponse(
                    response,
                    message: "User retrieved successfully"
                ));   
            } 
            catch(UserNotFoundException ex)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(
                    ex.Message,
                    errorCode: 1206
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest updateUserRequest, Guid id)
        {
            try
            {
                var response = await _updateUserUseCase.Execute(updateUserRequest, id);

                return Ok(ApiResponse<UpdateUserResponse>.SuccessResponse(
                    response,
                    message: "User updated successfully"
                ));   
            } 
            catch(UserNotFoundException ex)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(
                    ex.Message,
                    errorCode: 1206
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _deleteUserUseCase.Execute(id);

                return Ok(ApiResponse<object>.SuccessResponse(
                    null,
                    message: "User deleted successfully"
                ));
            } 
            catch(UserNotFoundException ex)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(
                    ex.Message,
                    errorCode: 1206
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
