using Domain.DTOs;
using Domain.Models;

namespace Application.DaoIInterfaces;

public interface IPostDAO
{
    Task<Post> CreateAsync(PostCreationDTO dto);
    Task<IEnumerable<Post>> GetAllAsync();
    Task<IEnumerable<Post>> GetByUserIdAsync(int userId);
    Task<Post?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}