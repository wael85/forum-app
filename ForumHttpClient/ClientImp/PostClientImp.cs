using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using ForumHttpClient.ClientInterfaces;
using ForumHttpClient.Services;

namespace ForumHttpClient.ClientImp;

public class PostClientImp: IPostService
{
    private readonly HttpClient _httpClient;

    public PostClientImp(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Post> CreateAsync(PostCreationDTO dto)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",JwtAuthService.Jwt);

        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/v1/posts", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Post post = JsonSerializer.Deserialize<Post>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return post;
    }

    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",JwtAuthService.Jwt);

        HttpResponseMessage response = await _httpClient.GetAsync("api/v1/posts");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable<Post> posts = JsonSerializer.Deserialize<IEnumerable<Post>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return posts;
    }

    public async Task<IEnumerable<Post>> GetByUserIdAsync(int userId)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",JwtAuthService.Jwt);

        HttpResponseMessage response = await _httpClient.GetAsync($"api/v1/posts?userId={userId}");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable<Post> posts = JsonSerializer.Deserialize<IEnumerable<Post>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return posts;
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",JwtAuthService.Jwt);

        HttpResponseMessage response = await _httpClient.GetAsync($"api/v1/posts/{id}");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Post? post = JsonSerializer.Deserialize<Post>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return post;
    }

    public async Task DeleteAsync(int id)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",JwtAuthService.Jwt);

        HttpResponseMessage response = await _httpClient.DeleteAsync($"api/v1/posts/{id}");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        
    }
}