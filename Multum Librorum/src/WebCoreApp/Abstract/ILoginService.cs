using Client.Messages.Models;

namespace WebCoreApp.Abstract;

public interface ILoginService
{
     Task<bool> Login(string email, string password);
     Task<bool> LoginEmployee(string email, string password);
     Task Logout();
     Task LogoutEmployee();
     Task CheckToken();
     Task CheckEmployeeToken();
     Task<TokenWithUserInfo> IsUserLogin();
     Task<TokenWithUserInfo> IsEmployeeLogin();
}