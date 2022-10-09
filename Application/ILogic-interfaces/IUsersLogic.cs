using Domain.DTOs;
using Domain.Models;

namespace Application.ILogic_interfaces;

public interface IUsersLogic
{
     Task<User> CreateAsync(CreateUserDto createUserDto);
     Task<User?> GetUserById(int id);
     Task<User?> GetUserByUsernameAsync(string username);
}

