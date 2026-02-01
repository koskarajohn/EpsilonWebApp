using EpsilonWebApp.Core.Features.Customers.DeleteCustomer;
using EpsilonWebApp.Core.Features.Customers.GetCustomers;

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

        group.MapDelete("/{id:guid}", async (Guid id, IDeleteCustomer deleteCustomer, CancellationToken cancellationToken) =>
        {
            var result = await deleteCustomer.InvokeAsync(id, cancellationToken).ConfigureAwait(false);
            return result.ToResult();
        });

        return app;
    }
}