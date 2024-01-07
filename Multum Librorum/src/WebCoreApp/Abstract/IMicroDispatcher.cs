using CQRS.Communication.Enums;

namespace WebCoreApp.Abstract;

public interface IMicroDispatcher
{
    public HttpClient GetClient(EndpointEnum endpoint);
}