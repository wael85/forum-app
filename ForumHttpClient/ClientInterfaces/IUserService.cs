using Domain.DTOs;
using Domain.Models;

namespace ForumHttpClient.ClientInterfaces;

public interface IUserService
{
    Task<User> CreatAsync(CreateUserDto dto);
    Task<User> GetUserByUsernameAsync(string username);
    Task<User> GetUserByIdAsync(int id);
}