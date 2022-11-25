using Application.DaoIInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class PostEfcDAO : IPostDAO{

   private readonly ForumContext context;


   public PostEfcDAO(ForumContext context)
   {
       this.context = context;
   }

   public async Task<Post> CreateAsync(PostCreationDTO dto)
    {
        Post post = new Post();
        post.Title = dto.Title;
        post.Content = dto.Content;
        post.UserId = dto.UserId;
        EntityEntry<Post> returnedPost = await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
        return returnedPost.Entity;

    }

    public async  Task<IEnumerable<Post>> GetAllAsync()
    {
        List<Post> posts = await context.Posts.ToListAsync();
        return posts;
    }

    public async Task<IEnumerable<Post>> GetByUserIdAsync(int userId)
    {
        IQueryable<Post> query = context.Posts.AsQueryable();
        query = query.Where(post => post.UserId == userId);
        List<Post> posts =await query.ToListAsync();
        return posts;
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        Post? post = await context.Posts.FindAsync(id);
        return post;

    }

    public async Task DeleteAsync(int id)
    {
        Post? post = await GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception("Post not find");
        }
        context.Posts.Remove(post);
        await context.SaveChangesAsync();
    }
}