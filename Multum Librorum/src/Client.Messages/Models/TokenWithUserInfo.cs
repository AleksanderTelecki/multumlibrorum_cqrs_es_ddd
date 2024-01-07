namespace Client.Messages.Models;

public class TokenWithUserInfo
{
    public string Token { get; set; }
    public Guid UserId { get; set; }
    public string UserEmail { get; set; }
    public string UserName { get; set; }
}