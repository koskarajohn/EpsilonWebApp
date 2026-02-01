using EpsilonWebApp.Client.Pages;
using EpsilonWebApp.Components;
using EpsilonWebApp.Core.Features.Customers.DeleteCustomer;
using EpsilonWebApp.Core.Features.Customers.GetCustomers;
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
    .Filter.ByExcluding(
        Matching.FromSource("Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware"))
    .Enrich.WithProperty("MachineName", System.Environment.MachineName)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddScoped<IDeleteCustomer, DeleteCustomer>();
builder.Services.AddScoped<IGetCustomers, GetCustomers>();

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

