using Ardalis.Result;
using Ardalis.SharedKernel;

namespace NimblePros.MediatRValidation.UseCases.Contributors.Delete;

public record DeleteContributorCommand(int ContributorId) : ICommand<Result>;
