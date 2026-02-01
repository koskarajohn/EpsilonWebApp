using EpsilonWebApp.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress + "api/v1/") });
builder.Services.AddScoped<APIClient>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
