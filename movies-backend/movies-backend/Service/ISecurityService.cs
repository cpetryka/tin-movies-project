using movies_backend.DTOs;

namespace movies_backend.Repositories;

public interface ISecurityService
{
    Task<AuthorizationResultDto> RegisterAsync(RegisterUserDto registerUserDto);
    Task<AuthorizationResultDto> LoginAsync(LoginUserDto loginUserDto);
    Task<AuthorizationResultDto> RefreshTokenAsync(string refreshToken);
}