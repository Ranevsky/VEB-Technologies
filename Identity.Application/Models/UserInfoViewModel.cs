namespace Identity.Application.Models;

public class UserInfoViewModel
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int Age { get; set; }
    public string[] Roles { get; set; } = null!;
}