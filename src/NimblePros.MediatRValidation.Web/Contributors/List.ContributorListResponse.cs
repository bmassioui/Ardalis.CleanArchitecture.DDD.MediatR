using NimblePros.MediatRValidation.Web.ContributorEndpoints;

namespace NimblePros.MediatRValidation.Web.Endpoints.ContributorEndpoints;

public class ContributorListResponse
{
  public List<ContributorRecord> Contributors { get; set; } = new();
}
