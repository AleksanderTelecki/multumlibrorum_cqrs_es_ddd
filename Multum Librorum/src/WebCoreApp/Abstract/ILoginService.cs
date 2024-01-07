using Client.Messages.Models;

namespace WebCoreApp.Abstract;

public interface ILoginService
{
     Task<bool> Login(string email, string password);
     Task Logout();
     Task CheckToken();
     Task<TokenWithUserInfo> IsUserLogin();
}