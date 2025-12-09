using Bmp.Domain.Entities;

namespace Bmp.Application.Interfaces;

public interface IJwtTokenService
{
    /// <summary>
    /// Genera un token JWT para el usuario especificado.
    /// </summary>
    /// <param name="user">Usuario para el que se generará el token.</param>
    /// <returns>Token JWT como string.</returns>
    string GenerateToken(User user);

    /// <summary>
    /// Genera un token de acceso y un token de refresco.
    /// </summary>
    /// <param name="user">Usuario para el que se generarán los tokens.</param>
    /// <returns>Objeto con AccessToken y RefreshToken.</returns>
    (string AccessToken, string RefreshToken) GenerateTokenWithRefresh(User user);

    /// <summary>
    /// Valida si un token JWT es válido y no ha expirado.
    /// </summary>
    /// <param name="token">Token JWT a validar.</param>
    /// <returns>True si es válido, false si no.</returns>
    bool ValidateToken(string token);

    /// <summary>
    /// Obtiene el Id del usuario desde un token JWT válido.
    /// </summary>
    /// <param name="token">Token JWT.</param>
    /// <returns>Id del usuario.</returns>
    Guid GetUserIdFromToken(string token);
}