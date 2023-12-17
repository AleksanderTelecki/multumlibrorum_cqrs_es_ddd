using CQRS.Communication.Enums;

namespace CQRS.Communication.Options;

public class RestDispatcherOptions
{
    public Dictionary<EndpointEnum, string> EndpointRoutes { get; set; }

    public RestDispatcherOptions()
    {
        
    }
}