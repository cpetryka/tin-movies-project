namespace movies_backend.DTOs;

public class GetUserDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string UserRoleName { get; set; } = null!;
}