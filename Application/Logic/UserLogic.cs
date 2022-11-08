using System.Runtime.ConstrainedExecution;
using Application.DaoIInterfaces;
using Application.ILogic_interfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class UserLogic:IUsersLogic
{ 
    
    private readonly IUserDao _userDao;

    public UserLogic(IUserDao userDao)
    {
        _userDao = userDao;
    }

    public async Task<User> CreateAsync(CreateUserDto createUserDto)
    {
        User? exist =await _userDao.GetUserByUsernameAsync(createUserDto.UserName);
        if (exist != null)
        {
            throw new Exception("User is exist");
        }

        //ValedateUserDTO(createUserDto);
        User newUser = await _userDao.CreateAsync(createUserDto);

        return newUser;
    }

    public async Task<User?> GetUserById(int id)
    {
        User? user = await _userDao.GetUserByIdAsync(id);
        if (user == null)
        {
            throw new Exception($" User With id = {id}, not exist!! ");
        }

        return user;
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        User? user = await  _userDao.GetUserByUsernameAsync(username);
        if (user == null)
        {
            throw new Exception("User not exist");
        }

        return user;
    }

    private void ValedateUserDTO(CreateUserDto createUserDto)
    {
        // need to add some validate 
    }
}