
using Application.ILogic_interfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;
[ApiController]
[Route("/api/v1/users")]
public class UserController: ControllerBase
{
    private readonly IUsersLogic _usersLogic;

    public UserController(IUsersLogic usersLogic)
    {
        _usersLogic = usersLogic;
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateAsync(CreateUserDto dto)
    {
        try
        {
            User user = await _usersLogic.CreateAsync(dto);
            return Created($"api/v/users/{user.Id}",user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    [Route("/{id}")]
    public async Task<ActionResult<User?>> GetUserById([FromRoute] int id)
    {
        try
        {
            User? user = await _usersLogic.GetUserById(id);
            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}