using Application.DaoIInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class UserEfcDAO : IUserDao
{
    private readonly ForumContext context;

    public UserEfcDAO(ForumContext context)
    {
        this.context = context;
    }
    public async Task<User> CreateAsync(CreateUserDto dto)
    {
        User userToCreate = new User();
        userToCreate.Email = dto.Email;
        userToCreate.Password = dto.Password;
        userToCreate.UserName = dto.UserName;
        
        EntityEntry<User> newUser = await context.Users.AddAsync(userToCreate);
        await context.SaveChangesAsync();
        
        return newUser.Entity;    }

    public async  Task<User?> GetUserByUsernameAsync(string userName)
    {
        User? user = await context.Users.FirstOrDefaultAsync(x => x.UserName.Equals(userName));
        return user;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        User? user =await context.Users.FindAsync(id);
        return user;
    }
}