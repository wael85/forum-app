using System.ComponentModel.DataAnnotations;
using Application.ILogic_interfaces;
using Domain.DTOs;
using Domain.Models;
using WebApplication1.Services;

namespace WebApplication1.Services;

public class AuthService : IAuthService
{

    private readonly IUsersLogic usersLogic;

    public AuthService(IUsersLogic usersLogic)
    {
        this.usersLogic = usersLogic;
    }

    public async Task<User> GetUser(string username, string password)
    {
        User? existingUser = await usersLogic.GetUserByUsernameAsync(username);

        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return await Task.FromResult(existingUser);
    }

    public  Task RegisterUser(CreateUserDto dto)
    {
        if (string.IsNullOrEmpty(dto.UserName))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(dto.Password))
        {
            throw new ValidationException("Password cannot be null");
        }

        usersLogic.CreateAsync(dto);
        
        return Task.CompletedTask;

    }
}