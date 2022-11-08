using Application.DaoIInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public class PostDAO : IPostDAO
{
    private readonly FileContext _fileContext;

    public PostDAO(FileContext fileContext)
    {
        _fileContext = fileContext;
    }

    public  Task<Post> CreateAsync(PostCreationDTO dto)
    {
        int id = 1;
        if (_fileContext.Posts.Any())
        {
            id = _fileContext.Posts.Max(x => x.Id);
            id++;
        }

        Post post = new Post(id, dto.UserId, dto.Title, dto.Content);
         _fileContext.Posts.Add(post);
          _fileContext.SaveChanges();
          return Task.FromResult(post);

    }

    public Task<IEnumerable<Post>> GetAllAsync()
    {
        IEnumerable<Post> posts = _fileContext.Posts;
        return Task.FromResult(posts);
    }

    public Task<IEnumerable<Post>> GetByUserIdAsync(int userId)
    {
        IEnumerable<Post> posts = _fileContext.Posts;
        posts = posts.Where(post => post.UserId == userId);
        return Task.FromResult(posts);
    }

    public Task<Post?> GetByIdAsync(int id)
    {
        Post? post = _fileContext.Posts.FirstOrDefault(post => post.Id == id);
        return Task.FromResult(post);
    }

    public Task DeleteAsync(int id)
    {
        Post? existed = _fileContext.Posts.FirstOrDefault(post => post.Id == id);
        if (existed == null)
        {
            throw new Exception("Post not existed..");
        }

        _fileContext.Posts.Remove(existed);
        _fileContext.SaveChanges();
        return Task.CompletedTask;
    }
}