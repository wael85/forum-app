using Application.ILogic_interfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;
[ApiController]
[Authorize]
[Route("api/v1/posts")]
public class PostController : ControllerBase
{
    private readonly IPostLogic _postLogic;

    public PostController(IPostLogic postLogic)
    {
        _postLogic = postLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<Post>> CreateAsync([FromBody] PostCreationDTO dto)
    {
        try
        {
            Post post = await _postLogic.CreateAsync(dto);
            return Created($"api/v1/posts/{post.Id}",post);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetPostsAsync([FromQuery] int? userId)
    {
        try
        {
            if (userId.HasValue)
            {
                IEnumerable<Post> userPosts = await _postLogic.GetByUserIdAsync((int)userId);
                return Ok(userPosts);
            }
            IEnumerable<Post> posts = await _postLogic.GetAllAsync();
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Post?>> GetByIdAsync([FromRoute] int id)
    {
        try
        { 
            Post? posts = await _postLogic.GetByIdAsync(id);
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
   
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute]int id)
    {
        try
        { 
             await _postLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}