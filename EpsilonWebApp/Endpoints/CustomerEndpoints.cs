using EpsilonWebApp.Core.Features.Customers.DeleteCustomer;
using EpsilonWebApp.Core.Features.Customers.GetCustomers;
using EpsilonWebApp.Core.Features.Customers.UpdateCustomer;
using EpsilonWebApp.Shared.DTO;

namespace EpsilonWebApp.Endpoints;

public static class CustomerEndpoints
{
    public static IEndpointRouteBuilder RegisterCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("api/v1/customers")
            .WithTags("Customers")
            .DisableAntiforgery();

        group.MapGet("", async (IGetCustomers getCustomers, CancellationToken cancellationToken) =>
        {
            var customers = await getCustomers.InvokeAsync(cancellationToken);
            return customers.ToResult();
        });
        
        group.MapPut("/{id:guid}", async (Guid id, UpsertCustomerDTO customer, IUpdateCustomer updateCustomer, CancellationToken cancellationToken) =>
        {
            
            customer.Id = id;
            var result = await updateCustomer.InvokeAsync(customer, cancellationToken).ConfigureAwait(false);
            return result.ToResult();
        });

        group.MapDelete("/{id:guid}", async (Guid id, IDeleteCustomer deleteCustomer, CancellationToken cancellationToken) =>
        {
            var result = await deleteCustomer.InvokeAsync(id, cancellationToken).ConfigureAwait(false);
            return result.ToResult();
        });

        return app;
    }
}