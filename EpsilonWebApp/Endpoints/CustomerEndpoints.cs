using EpsilonWebApp.Core.Features.Customers.DeleteCustomer;

namespace EpsilonWebApp.Endpoints;

public static class CustomerEndpoints
{
    public static IEndpointRouteBuilder RegisterCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("api/v1/customers")
            .WithTags("Customers")
            .DisableAntiforgery();

        group.MapDelete("/{id:guid}", async (Guid id, IDeleteCustomer deleteCustomer, CancellationToken cancellationToken) =>
        {
            var result = await deleteCustomer.InvokeAsync(id, cancellationToken).ConfigureAwait(false);
            return result.ToResult();
        });

        return app;
    }
}