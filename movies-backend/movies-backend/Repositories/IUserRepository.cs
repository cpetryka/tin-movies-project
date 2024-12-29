using movies_backend.DTOs;
using movies_backend.Model.User;

namespace movies_backend.Repositories;

public interface IUserRepository
{
    Task<bool> DoesUserRoleExist(int userRoleId);
    Task<int> AddNewUserRole(string newRoleName);
    Task<bool> DeleteUserRole(int id);

    Task<bool> DoesUserExist(int userId);
    Task<int> AddNewUser(string name, string email, string password, int userRoleId);
    Task<GetUserDto?> GetUserByEmail(string email);
    Task<GetUserDto?> GetUserById(int id);
    Task<ICollection<GetUserDto>> GetAllUsers();
    Task<bool> DeleteUser(int id);
}