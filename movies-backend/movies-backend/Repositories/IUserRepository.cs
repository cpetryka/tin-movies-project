using movies_backend.DTOs;
using movies_backend.Model.User;

namespace movies_backend.Repositories;

public interface IUserRepository
{
    /*************************************************************************************************
     * USER ROLES MANAGEMENT
     *************************************************************************************************/
    Task<bool> DoesUserRoleExist(int userRoleId);
    Task<int> AddNewUserRole(string newRoleName);
    Task<bool> DeleteUserRole(int id);

    /*************************************************************************************************
     * USERS MANAGEMENT
     *************************************************************************************************/
    Task<bool> DoesUserExist(int userId);
    Task<GetUserDto?> GetUserById(int id);
    Task<GetUserDto?> GetUserByEmail(string email);
    Task<ICollection<GetUserDto>> GetAllUsers();
    Task<int> AddNewUser(string name, string email, string password, int userRoleId);
    Task<bool> DeleteUser(int id);
}