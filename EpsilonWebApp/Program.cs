using EpsilonWebApp;
using EpsilonWebApp.Client.Pages;
using EpsilonWebApp.Components;
using EpsilonWebApp.Core.Features.Customers.CreateCustomer;
using EpsilonWebApp.Core.Features.Customers.DeleteCustomer;
using EpsilonWebApp.Core.Features.Customers.GetCustomer;
using EpsilonWebApp.Core.Features.Customers.GetCustomers;
using EpsilonWebApp.Core.Features.Customers.UpdateCustomer;
using EpsilonWebApp.Endpoints;
using EpsilonWebApp.SQLServer;
using EpsilonWebApp.SQLServer.Registration;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Serilog;
using Serilog.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddMudServices();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.WithProperty("MachineName", System.Environment.MachineName)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddScoped<IDeleteCustomer, DeleteCustomer>();
builder.Services.AddScoped<IGetCustomers, GetCustomers>();
builder.Services.AddScoped<IUpdateCustomer, UpdateCustomer>();
builder.Services.AddScoped<IGetCustomer, GetCustomer>();
builder.Services.AddScoped<ICreateCustomer, CreateCustomer>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();
app.Logger.LogInformation("Application starting {EnvironmentName}", app.Environment.EnvironmentName);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Use GlobalExceptionHandler for all requests (returns JSON for API, can be customized for pages)
app.UseExceptionHandler();

app.UseHttpsRedirection();
app.UseAntiforgery();
app.RegisterCustomerEndpoints();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(EpsilonWebApp.Client._Imports).Assembly);

if (app.Environment.IsDevelopment())
{
    //await MigrateDatabaseAsync(app);
}

app.Run();

async Task MigrateDatabaseAsync(WebApplication webApplication)
{
    using (var scope = webApplication.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            await context.Database.EnsureCreatedAsync();
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex.ToString());
        }
    }
}

