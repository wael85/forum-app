using Domain.DTOs;
using Domain.Models;

namespace Application.DaoIInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(CreateUserDto dto);
    Task<User?> GetUserByUsernameAsync(string userName);
    Task<User?> GetUserByIdAsync(int Id);
}