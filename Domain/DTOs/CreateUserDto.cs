namespace Domain.DTOs;

public class CreateUserDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }

    public CreateUserDto(string userName, string password, string email)
    {
        UserName = userName;
        Password = password;
        Email = email;
    }
}