using System;
using Microsoft.EntityFrameworkCore;
using AskAQuestion.Api.Data;
using AskAQuestion.Api.Entities;

namespace AskAQuestion.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AskAQuestionDbContext _context;

    public UserRepository(AskAQuestionDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllUsers()
    {
        var users = await _context.Users
            .AsNoTracking()
            .ToListAsync();

        return users;
    }

    public async Task<User> GetUserByEmail(string email)
    {
        var user = await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email);

        return user;
    }

    public async Task<User> RegisterUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task UpdateUser(User user)
    {
        var userExists = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUser(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UserExists(Guid id)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null) return false;
        return true;
    }

    public async Task<User> GetUserById(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task CreateUserRole(UserRole userRole)
    {
        await _context.UserRoles.AddAsync(userRole);
        await _context.SaveChangesAsync();
    }
}

