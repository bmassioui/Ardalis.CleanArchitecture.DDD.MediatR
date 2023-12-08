using FluentValidation;
using NimblePros.MediatRValidation.Core;

namespace NimblePros.MediatRValidation.UseCases.Contributors.Create;

public class CreateContributorCommandValidator : AbstractValidator<CreateContributorCommand>
{
  public CreateContributorCommandValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty()
      .WithMessage("Name is required.")
      .MinimumLength(2)
      .MaximumLength(DomainModelConstants.DEFAULT_NAME_LENGTH);
  }
}
