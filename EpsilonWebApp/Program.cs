using EpsilonWebApp.Client.Pages;
using EpsilonWebApp.Components;
using Serilog;
using Serilog.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Filter.ByExcluding(
        Matching.FromSource("Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware"))
    .Enrich.WithProperty("MachineName", System.Environment.MachineName)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var app = builder.Build();
app.Logger.LogInformation("Application starting {EnvironmentName}", app.Environment.EnvironmentName);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(EpsilonWebApp.Client._Imports).Assembly);

app.Run();
