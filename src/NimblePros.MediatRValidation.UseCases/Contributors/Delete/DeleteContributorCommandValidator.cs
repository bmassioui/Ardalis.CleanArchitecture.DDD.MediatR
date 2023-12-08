using FluentValidation;

namespace NimblePros.MediatRValidation.UseCases.Contributors.Delete;

public class DeleteContributorCommandValidator : 
  AbstractValidator<DeleteContributorCommand>
{
  public DeleteContributorCommandValidator()
  {
    RuleFor(x => x.ContributorId)
      .GreaterThan(0);
  }
}
