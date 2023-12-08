using Ardalis.Result;
using Ardalis.SharedKernel;

namespace NimblePros.MediatRValidation.UseCases.Contributors.Get;

public record GetContributorQuery(int ContributorId) : IQuery<Result<ContributorDTO>>;
