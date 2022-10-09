namespace Domain.Models;

public class Comment
{
    public int Id { get; }
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public Comment(int id,int postId ,string title, string content)
    {
        this.Id = id;
        this.PostId = postId;
        this.Title = title;
        this.Content = content;
    }
}