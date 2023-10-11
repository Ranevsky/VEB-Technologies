namespace Identity.Presentation.Models;

public class ChangeUserPasswordDataModel
{
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public bool NeedLogoutAccounts { get; set; }
}