using Domain.DTOs;
using Domain.Models;

namespace WebApplication1.Services;

public interface IAuthService
{
    Task<User> GetUser(string username, string password);
    Task RegisterUser(CreateUserDto dto);
}