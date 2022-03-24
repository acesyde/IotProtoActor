using FastEndpoints;

namespace IotProto.Host.Endpoints.Hello;

public class HelloEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/hello");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await this.SendAsync(new { }, StatusCodes.Status200OK, ct);
    }
}