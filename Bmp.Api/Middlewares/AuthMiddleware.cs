using Bmp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Bmp.Api.Middlewares;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IJwtTokenService jwtService)
    {
        var endpoint = context.GetEndpoint();

        if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
        {
            await _next(context);
            return;
        }

        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (!string.IsNullOrEmpty(token))
        {
            try
            {
                var userId = jwtService.ValidateToken(token);

                context.Items["UserId"] = userId;
            }
            catch
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new
                {
                    success = false,
                    message = "Invalid token",
                    error_code = 1204,
                    data = new { }
                });
                return;
            }
        }
        else
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsJsonAsync(new
            {
                success = false,
                message = "Authorization header missing",
                error_code = 1001,
                data = new { }
            });
            return;
        }

        await _next(context);
    }
}