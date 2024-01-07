namespace WebCoreApp.Services;

public class ErrorHandlerService
{
    public event Action<string> OnError;

    public void ShowError(string message)
    {
        OnError?.Invoke(message);
    }
}