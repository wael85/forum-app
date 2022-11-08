using Domain.DTOs;
using Domain.Models;

namespace ForumHttpClient.ClientInterfaces;

public interface IPostService
{
    Task<Post> CreateAsync(PostCreationDTO dto);
    Task<IEnumerable<Post>> GetAllAsync();
    Task<IEnumerable<Post>> GetByUserIdAsync(int userId);
    Task<Post?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}