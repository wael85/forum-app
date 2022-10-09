using System.Text.Json;
using Domain.Models;

namespace FileData;

public class FileContext
{
    private const string filePath = "data.json";
    private DataContainer? _container;


    public ICollection<User> Users
    {
        get
        {
            LoadData();
            return _container!.Users;
        }
    }
    public ICollection<Post> Posts
    {
        get
        {
            LoadData();
            return _container!.Posts;
        }
    }
    public ICollection<Comment> Comments
    {
        get
        {
            LoadData();
            return _container!.Comments;
        }
    }

    private void LoadData()
    {
        if(_container != null) return;
        if (!File.Exists(filePath))
        {
            _container = new DataContainer()
            {
                Users = new List<User>(),
                Posts = new List<Post>(),
                Comments = new List<Comment>()
            };
            return;
        }

        string content = File.ReadAllText(filePath);
        _container = JsonSerializer.Deserialize<DataContainer>(content);
    }

    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize<DataContainer>(_container, new JsonSerializerOptions()
        {
            WriteIndented = true
        });
        File.WriteAllText(filePath, serialized);
        _container = null;
    }
}