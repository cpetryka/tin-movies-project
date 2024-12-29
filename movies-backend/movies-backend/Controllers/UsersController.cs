using Microsoft.AspNetCore.Mvc;
using movies_backend.DTOs;
using movies_backend.Repositories;

namespace movies_backend.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("add-new-user-role")]
    public async Task<IActionResult> AddNewUserRole([FromBody] AddNewUserRoleDto addNewUserRoleDto)
    {
        var id = await _userRepository.AddNewUserRole(addNewUserRoleDto.Name);

        return Created("", new
        {
            message = "User role added successfully",
            addedUserRoleId = id
        });
    }

    [HttpDelete("delete-user-role")]
    public async Task<IActionResult> DeleteUserRole([FromQuery] int id)
    {
        var deletedStatus = await _userRepository.DeleteUserRole(id);

        return Ok(new
        {
            message = deletedStatus ? "User role deleted successfully" : "User role not found"
        });
    }

    [HttpPost("add-new-user")]
    public async Task<IActionResult> AddNewUser([FromBody] AddNewUserDto addNewUserDto)
    {
        var id = await _userRepository.AddNewUser(addNewUserDto.Name, addNewUserDto.Email, addNewUserDto.Password, addNewUserDto.UserRoleId);

        return Created("", new
        {
            message = "User added successfully",
            addedUserId = id
        });
    }

    [HttpGet("get-user-by-email")]
    public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
    {
        var user = await _userRepository.GetUserByEmail(email);

        if (user == null)
        {
            return NotFound(new
            {
                message = "User not found"
            });
        }

        return Ok(user);
    }

    [HttpGet("get-user-by-id")]
    public async Task<IActionResult> GetUserById([FromQuery] int id)
    {
        var user = await _userRepository.GetUserById(id);

        if (user == null)
        {
            return NotFound(new
            {
                message = "User not found"
            });
        }

        return Ok(user);
    }

    [HttpGet("get-all-users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsers();

        return Ok(users);
    }

    [HttpDelete("delete-user")]
    public async Task<IActionResult> DeleteUser([FromQuery] int id)
    {
        var deletedStatus = await _userRepository.DeleteUser(id);

        return Ok(new
        {
            message = deletedStatus ? "User deleted successfully" : "User not found"
        });
    }
}