using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NimblePros.MediatRValidation.UseCases;
using NimblePros.MediatRValidation.UseCases.Contributors.Create;
using NimblePros.MediatRValidation.UseCases.Contributors.Delete;
using Xunit;

namespace NimblePros.MediatRValidation.IntegrationTests.UseCases;
public class DeleteContributorCommandHandler
{
  [Theory]
  [InlineData(0)]
  [InlineData(-1)]
  public async Task ThrowsValidationExceptionGivenInvalidName(int invalidId)
  {
    // Setup DI container and services
    var services = new ServiceCollection();

    // Add MediatR
    services.AddMediatR(cfg =>
    {
      cfg.RegisterServicesFromAssemblies(typeof(DeleteContributorCommand).Assembly);
    });

    // Setup Autofac
    var builder = new ContainerBuilder();
    builder.Populate(services);

    // Register Validators
    builder.RegisterAssemblyTypes(typeof(CreateContributorCommandValidator).Assembly)
           .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
           .AsImplementedInterfaces();

    builder.RegisterGeneric(typeof(ValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));

    var container = builder.Build();

    using var scope = container.BeginLifetimeScope();
    var mediator = scope.Resolve<IMediator>();

    var command = new DeleteContributorCommand(invalidId);

    // Act & Assert
    await Assert.ThrowsAsync<ValidationException>(() => mediator.Send(command));
  }
}
