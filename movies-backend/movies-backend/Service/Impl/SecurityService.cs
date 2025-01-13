using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using movies_backend.Data;
using movies_backend.DTOs;
using movies_backend.Model.User;

namespace movies_backend.Repositories.Impl;

public class SecurityService : ISecurityService
{
    private readonly ApplicationContext _context;
    private readonly IConfiguration _configuration;

    public SecurityService(ApplicationContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<AuthorizationResultDto> RegisterAsync(RegisterUserDto registerUserDto)
    {
        // If user with the same username already exists, return an error
        if (await _context.Users.AnyAsync(u => u.Email == registerUserDto.Email))
        {
            return new AuthorizationResultDto { Success = false, Errors = new[] { "User already exists" } };
        }

        // Create a new user
        var user = new User
        {
            Name = registerUserDto.Name,
            Email = registerUserDto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password),
            UserRole = await _context.UserRoles.SingleAsync(ur => ur.Name == "User"),
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7)
        };

        // Generate tokens and save the user to the database
        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();

        user.RefreshToken = refreshToken;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new AuthorizationResultDto { Success = true, AccessToken = accessToken, RefreshToken = refreshToken };
    }

    public async Task<AuthorizationResultDto> LoginAsync(LoginUserDto loginUserDto)
    {
        // Find a user with the given username
        var user = await _context.Users
            .Include(u => u.UserRole)
            .SingleOrDefaultAsync(u => u.Email == loginUserDto.Email);

        // If user with the given username doesn't exist or the password is incorrect, return an error
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginUserDto.Password, user.Password))
        {
            return new AuthorizationResultDto { Success = false, Errors = new[] { "Invalid username or password" } };
        }

        // Generate a new access token and refresh token
        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _context.SaveChangesAsync();

        return new AuthorizationResultDto { Success = true, AccessToken = accessToken, RefreshToken = refreshToken };
    }

    public async Task<AuthorizationResultDto> RefreshTokenAsync(string refreshToken)
    {
        // Find a user with the given refresh token
        var user = await _context.Users
            .Include(u => u.UserRole)
            .SingleOrDefaultAsync(u => u.RefreshToken == refreshToken);

        // If user with the given refresh token doesn't exist or the refresh token is expired, return an error
        if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return new AuthorizationResultDto { Success = false, Errors = new[] { "Invalid or expired refresh token" } };
        }

        // Generate a new access token and refresh token
        var newAccessToken = GenerateAccessToken(user);
        var newRefreshToken = GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _context.SaveChangesAsync();

        return new AuthorizationResultDto { Success = true, AccessToken = newAccessToken, RefreshToken = newRefreshToken };
    }

    // --------------------------------------------------------------------------------------------------------------

    private string GenerateAccessToken(User user)
    {
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var jwtKey = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserRole.Name)
            }),
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(jwtKey),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
        return jwtSecurityTokenHandler.WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        return Guid
            .NewGuid()
            .ToString()
            .Replace("-", "");
    }
}