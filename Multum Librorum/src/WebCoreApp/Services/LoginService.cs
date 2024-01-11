using System.Net.Http.Json;
using Blazored.LocalStorage;
using Client.Messages.Models;
using CQRS.Communication.Enums;
using Employee.Messages.Models;
using WebCoreApp.Abstract;

namespace WebCoreApp.Services;

public class LoginService: ILoginService
{
    private readonly IMicroDispatcher _microDispatcher;
    private readonly ILocalStorageService _localStorage;
    private readonly ErrorHandlerService _errorHandlerService;

    public LoginService(IMicroDispatcher microDispatcher, ILocalStorageService localStorage, ErrorHandlerService errorHandlerService)
    {
        _microDispatcher = microDispatcher;
        _localStorage = localStorage;
        _errorHandlerService = errorHandlerService;
    }

    public async Task<bool> Login(string email, string password)
    {
        try
        {
            var response = await _microDispatcher
                .GetClient(EndpointEnum.ClientEndpoint)
                .PostAsJsonAsync("api/v1/Token", new ClientCredentials { Email = email, Password = password});

            response.EnsureSuccessStatusCode();
        
            var token = await response.Content.ReadFromJsonAsync<TokenWithUserInfo>();
            await _localStorage.SetItemAsync("tokenWithUserData", token);

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _errorHandlerService.ShowError("Unsuccesful Login");
            return false;
        }
    }
    
    public async Task<bool> LoginEmployee(string email, string password)
    {
        try
        {
            var response = await _microDispatcher
                .GetClient(EndpointEnum.EmployeeEndpoint)
                .PostAsJsonAsync("api/v1/Token", new EmployeeCredentials { Email = email, Password = password});

            response.EnsureSuccessStatusCode();
        
            var token = await response.Content.ReadFromJsonAsync<TokenWithUserInfo>();
            await _localStorage.SetItemAsync("tokenWithEmployeeData", token);

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _errorHandlerService.ShowError("Unsuccesful Login");
            return false;
        }
    }
    
    public async Task CheckEmployeeToken()
    {
        try
        {
            var token = await _localStorage.GetItemAsync<TokenWithUserInfo>("tokenWithUserData");

            var httpClient = _microDispatcher.GetClient(EndpointEnum.EmployeeEndpoint);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Token);

            var response = await httpClient.GetAsync("api/v1/Token/Check");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("tokenWithUserData");
    }
    
    public async Task LogoutEmployee()
    {
        await _localStorage.RemoveItemAsync("tokenWithEmployeeData");
    }
    
    public async Task CheckToken()
    {
        try
        {
            var token = await _localStorage.GetItemAsync<TokenWithUserInfo>("tokenWithUserData");

            var httpClient = _microDispatcher.GetClient(EndpointEnum.ClientEndpoint);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Token);

            var response = await httpClient.GetAsync("api/v1/Token/Check");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public async Task<TokenWithUserInfo> IsUserLogin()
    {
        var token = await _localStorage.GetItemAsync<TokenWithUserInfo>("tokenWithUserData");
        return token;
    }
    
    public async Task<TokenWithUserInfo> IsEmployeeLogin()
    {
        var token = await _localStorage.GetItemAsync<TokenWithUserInfo>("tokenWithEmployeeData");
        return token;
    }
}