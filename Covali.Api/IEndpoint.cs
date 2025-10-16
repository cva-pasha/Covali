using Microsoft.AspNetCore.Routing;

namespace Covali.Api;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}