using Autofac;
using NimblePros.MediatRValidation.Core.Interfaces;
using NimblePros.MediatRValidation.Core.Services;

namespace NimblePros.MediatRValidation.Core;

/// <summary>
/// An Autofac module that is responsible for wiring up services defined in the Core project.
/// </summary>
public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<DeleteContributorService>()
        .As<IDeleteContributorService>().InstancePerLifetimeScope();
  }
}
