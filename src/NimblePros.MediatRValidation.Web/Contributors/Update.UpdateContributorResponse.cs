using NimblePros.MediatRValidation.Web.ContributorEndpoints;

namespace NimblePros.MediatRValidation.Web.Endpoints.ContributorEndpoints;

public class UpdateContributorResponse
{
  public UpdateContributorResponse(ContributorRecord contributor)
  {
    Contributor = contributor;
  }
  public ContributorRecord Contributor { get; set; }
}
