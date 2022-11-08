using Application.DaoIInterfaces;
using Application.ILogic_interfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDAO _postDao;
    private readonly IUserDao _userDao;

    public PostLogic(IPostDAO postDao, IUserDao userDao)
    {
        _postDao = postDao;
        _userDao = userDao;
    }

    public async Task<Post> CreateAsync(PostCreationDTO dto)
    {
        User? user = await _userDao.GetUserByIdAsync(dto.UserId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.UserId} not exist");
        }
        ValidatPost(dto);
        Post created = await _postDao.CreateAsync(dto);
        return created;
    }

    private void ValidatPost(PostCreationDTO dto)
    {
        //--
    }

    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        return await _postDao.GetAllAsync();
    }

    public async Task<IEnumerable<Post>> GetByUserIdAsync(int userId)
    {
        User? user = await _userDao.GetUserByIdAsync(userId);
        if (user == null)
        {
            throw new Exception($"User with id {userId} not exist");
        }

        return await _postDao.GetByUserIdAsync(userId);
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        return await _postDao.GetByIdAsync(id);
    }

    public async Task DeleteAsync(int id)
    {
        Post? exist = await _postDao.GetByIdAsync(id);
        if (exist == null)
            throw new Exception($"Post with the id: {id} not found..");
        else
            await _postDao.DeleteAsync(id);
    }
}