using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using ForumHttpClient.ClientInterfaces;
using ForumHttpClient.Services;

namespace ForumHttpClient.ClientImp;

public class UserClientImp : IUserService
{
    private HttpClient _httpClient;

    public UserClientImp(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<User> CreatAsync(CreateUserDto dto)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",JwtAuthService.Jwt);
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/v1/users", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        User user = JsonSerializer.Deserialize<User>(result,new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;
    }

    public async Task<User> GetUserByUsernameAsync(string? username)
    {
        string uri = "api/v1/users";
        if (!string.IsNullOrEmpty(username))
        {
            uri += $"?username={username}";
        }
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",JwtAuthService.Jwt);
        HttpResponseMessage response = await _httpClient.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        User user = JsonSerializer.Deserialize<User>(result,new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",JwtAuthService.Jwt);

        HttpResponseMessage response = await _httpClient.GetAsync($"api/v1/users/{id}");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        User user = JsonSerializer.Deserialize<User>(result,new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;
    }
}