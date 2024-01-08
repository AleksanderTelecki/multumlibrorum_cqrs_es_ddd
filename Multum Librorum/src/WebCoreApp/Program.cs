using Blazored.LocalStorage;
using CQRS.Communication.Abstract;
using CQRS.Communication.Options;
using CQRS.Communication.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using WebCoreApp;
using MudBlazor.Services;
using WebCoreApp.Abstract;
using WebCoreApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

var restDispatcherOptions = new RestDispatcherOptions();
builder.Configuration.GetSection(nameof(RestDispatcherOptions)).Bind(restDispatcherOptions);
builder.Services.Configure<RestDispatcherOptions>(options =>
{
    options.EndpointRoutes = restDispatcherOptions.EndpointRoutes;
});

builder.Services.AddHttpClient();
builder.Services.AddBlazoredLocalStorage();        

builder.Services.AddScoped<IRestDispatcher, RestDispatcher>();
builder.Services.AddScoped<IMicroDispatcher, MicroDispatcher>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddSingleton<ErrorHandlerService>();
builder.Services.AddSingleton<SuccessHandlerService>();

await builder.Build().RunAsync();