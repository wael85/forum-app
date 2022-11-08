namespace Domain.DTOs;

public class PostCreationDTO
{
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public PostCreationDTO(int userId, string title, string content)
    {
        UserId = userId;
        Title = title;
        Content = content;
    }
}