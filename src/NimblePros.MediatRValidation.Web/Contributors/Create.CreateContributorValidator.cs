using FastEndpoints;
using FluentValidation;
using NimblePros.MediatRValidation.Core;

namespace NimblePros.MediatRValidation.Web.Endpoints.ContributorEndpoints;

/// <summary>
/// See: https://fast-endpoints.com/docs/validation
/// </summary>
public class CreateContributorValidator : Validator<CreateContributorRequest>
{
  public CreateContributorValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty()
      .WithMessage("Name is required.")
      .MinimumLength(2)
      .MaximumLength(DomainModelConstants.DEFAULT_NAME_LENGTH);
  }
}
