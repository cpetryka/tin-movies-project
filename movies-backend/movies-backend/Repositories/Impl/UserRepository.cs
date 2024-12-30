using Microsoft.EntityFrameworkCore;
using movies_backend.Data;
using movies_backend.DTOs;
using movies_backend.Model.User;

namespace movies_backend.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<bool> DoesUserRoleExist(int userRoleId)
    {
        return await _context.UserRoles.AnyAsync(ur => ur.Id == userRoleId);
    }

    public async Task<int> AddNewUserRole(string newRoleName)
    {
        var userRole = new UserRole { Name = newRoleName };
        await _context.AddAsync(userRole);
        await _context.SaveChangesAsync();

        return userRole.Id;
    }

    public async Task<bool> DeleteUserRole(int id)
    {
        if (!DoesUserRoleExist(id).Result)
        {
            return false;
        }

        _context.UserRoles.Remove(new UserRole { Id = id });
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DoesUserExist(int userId)
    {
        return await _context.Users.AnyAsync(u => u.Id == userId);
    }

    public async Task<int> AddNewUser(string name, string email, string password, int userRoleId)
    {
        var user = new User
        {
            Name = name,
            Email = email,
            Password = password,
            UserRoleId = userRoleId
        };

        await _context.AddAsync(user);
        await _context.SaveChangesAsync();

        return user.Id;
    }

    public async Task<GetUserDto?> GetUserByEmail(string email)
    {
        var user = await _context.Users
            .Include(u => u.UserRole)
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
        {
            return null;
        }

        return new GetUserDto
        {
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            UserRoleName = user.UserRole.Name
        };
    }

    public async Task<GetUserDto?> GetUserById(int id)
    {
        var user = await _context.Users
            .Include(u => u.UserRole)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return null;
        }

        return new GetUserDto
        {
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            UserRoleName = user.UserRole.Name
        };

    }

    public async Task<ICollection<GetUserDto>> GetAllUsers()
    {
        var users = await _context.Users
            .Include(u => u.UserRole)
            .ToListAsync();

        Console.Write(users);

        return users.Select(user => new GetUserDto
        {
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            UserRoleName = user.UserRole.Name
        }).ToList();
    }

    public async Task<bool> DeleteUser(int id)
    {
        if (!DoesUserExist(id).Result)
        {
            return false;
        }

        _context.Users.Remove(new User { Id = id });
        await _context.SaveChangesAsync();
        return true;
    }
}