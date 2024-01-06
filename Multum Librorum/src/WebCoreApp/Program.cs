using CQRS.Communication.Abstract;
using CQRS.Communication.Options;
using CQRS.Communication.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebCoreApp;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

var restDispatcherOptions = new RestDispatcherOptions();
builder.Configuration.GetSection(nameof(RestDispatcherOptions)).Bind(restDispatcherOptions);
builder.Services.Configure<RestDispatcherOptions>(options =>
{
    options.EndpointRoutes = restDispatcherOptions.EndpointRoutes;
});

builder.Services.AddHttpClient();
        
builder.Services.AddScoped<IRestDispatcher, RestDispatcher>();

await builder.Build().RunAsync();