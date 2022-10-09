using System.Runtime.ConstrainedExecution;
using Application.DaoIInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public class UserDao:IUserDao
{
    private readonly FileContext _fileData;

    public UserDao(FileContext fileData)
    {
        _fileData = fileData;
    }

    public  Task<User> CreateAsync(CreateUserDto dto)
    {
        int userId = 1;
        if (_fileData.Users.Any())
        {
            userId = _fileData.Users.Max(u => u.Id);
            userId++;
        }

        User toCreate = new User(userId, dto.UserName, dto.Password, dto.Email);
        _fileData.Users.Add(toCreate);
        _fileData.SaveChanges();
        return  Task.FromResult(toCreate);
    }

    public Task<User?> GetUserByUsernameAsync(string userName)
    {
        User? user = _fileData.Users.FirstOrDefault(u => u.UserName.Equals(userName,StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(user);
    }

    public Task<User?> GetUserByIdAsync(int id)
    {
        User? user = _fileData.Users.FirstOrDefault(u => u.Id == id);
        return Task.FromResult(user);
    }
}