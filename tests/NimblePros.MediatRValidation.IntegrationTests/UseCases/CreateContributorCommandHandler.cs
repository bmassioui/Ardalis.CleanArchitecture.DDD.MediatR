using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NimblePros.MediatRValidation.UseCases;
using NimblePros.MediatRValidation.UseCases.Contributors.Create;
using Xunit;

namespace NimblePros.MediatRValidation.IntegrationTests.UseCases;
public class CreateContributorCommandHandler
{
  [Theory]
  [InlineData(null)]
  [InlineData("")]
  [InlineData("A")]
  [InlineData("0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789A")]
  public async Task ThrowsValidationExceptionGivenInvalidName(string? invalidName)
  {
    // Setup DI container and services
    var services = new ServiceCollection();

    // Add MediatR
    services.AddMediatR(cfg =>
    {
      cfg.RegisterServicesFromAssemblies(typeof(CreateContributorCommand).Assembly);
    });

    // Setup Autofac
    var builder = new ContainerBuilder();
    builder.Populate(services);

    // Register Validators
    builder.RegisterAssemblyTypes(typeof(CreateContributorCommandValidator).Assembly)
           .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
           .AsImplementedInterfaces();

    // Register Pipeline Behavior
    builder.RegisterGeneric(typeof(ValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));

    var container = builder.Build();

    // Create a scope to resolve services
    using var scope = container.BeginLifetimeScope();
    var mediator = scope.Resolve<IMediator>();

    // Create an invalid command
#nullable disable
    var command = new CreateContributorCommand(invalidName);

    // Act & Assert
    await Assert.ThrowsAsync<ValidationException>(() => mediator.Send(command));

  }
}
