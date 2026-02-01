using EpsilonWebApp.Core.Contracts;
using EpsilonWebApp.SQLServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EpsilonWebApp.SQLServer.Registration;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString, sqlOptions => 
            {
                
            }));

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}