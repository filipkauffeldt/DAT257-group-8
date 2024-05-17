using Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using Fluxor;
using API;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddRadzenComponents();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IApiHandler, ApiRequestHandler>();
builder.Services.AddScoped<IGeoLocator, GeoLocatorHandler>();

var currentAssembly = typeof(Program).Assembly;
builder.Services.AddFluxor(options => 
    {
        options.ScanAssemblies(currentAssembly);
        options.UseReduxDevTools();
    });

await builder.Build().RunAsync();
