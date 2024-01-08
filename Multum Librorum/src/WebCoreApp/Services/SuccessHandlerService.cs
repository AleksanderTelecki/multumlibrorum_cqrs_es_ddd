namespace WebCoreApp.Services;

public class SuccessHandlerService
{
    public event Action<string> OnSuccess;

    public void ShowSuccess(string message)
    {
        OnSuccess?.Invoke(message);
    }
}