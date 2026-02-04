using EpsilonWebApp;
using EpsilonWebApp.Client.Pages;
using EpsilonWebApp.Components;
using EpsilonWebApp.Core.Contracts;
using EpsilonWebApp.Core.Entities;
using EpsilonWebApp.Core.Features.Authentication;
using EpsilonWebApp.Core.Features.Authentication.Login;
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
using Login = EpsilonWebApp.Core.Features.Authentication.Login.Login;

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
builder.Services.AddScoped<IJWTService, JWTService>();
builder.Services.AddScoped<ILogin, Login>();

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

app.UseExceptionHandler();

app.UseHttpsRedirection();
app.UseAntiforgery();
//app.UseMiddleware<AuthenticationMiddleware>();

app.RegisterCustomerEndpoints();
app.RegisterAuthenticationEndpoints();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(EpsilonWebApp.Client._Imports).Assembly);

if (app.Environment.IsDevelopment())
{
    await MigrateDatabaseAsync(app);
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
            await context.Database.MigrateAsync();
            
            var userRepo = services.GetRequiredService<IUserRepository>();
            var dummyUser = await userRepo.GetUserByEmailAsync("kvkarag@gmail.com", CancellationToken.None);
            if (dummyUser is null)
            {
                dummyUser = new User("kvkarag@gmail.com", "StrongPassword!@#");
                await userRepo.AddAsync(dummyUser, CancellationToken.None);
                
            }
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex.ToString());
        }
    }
}



