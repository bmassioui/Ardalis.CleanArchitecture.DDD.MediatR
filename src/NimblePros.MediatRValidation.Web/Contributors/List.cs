using FastEndpoints;
using MediatR;
using NimblePros.MediatRValidation.Web.Endpoints.ContributorEndpoints;
using NimblePros.MediatRValidation.UseCases.Contributors.List;

namespace NimblePros.MediatRValidation.Web.ContributorEndpoints;

/// <summary>
/// List all Contributors
/// </summary>
/// <remarks>
/// List all contributors - returns a ContributorListResponse containing the Contributors.
/// </remarks>
public class List(IMediator _mediator) : EndpointWithoutRequest<ContributorListResponse>
{
  public override void Configure()
  {
    Get("/Contributors");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new ListContributorsQuery(null, null));

    if (result.IsSuccess)
    {
      Response = new ContributorListResponse
      {
        Contributors = result.Value.Select(c => new ContributorRecord(c.Id, c.Name)).ToList()
      };
    }
  }
}
