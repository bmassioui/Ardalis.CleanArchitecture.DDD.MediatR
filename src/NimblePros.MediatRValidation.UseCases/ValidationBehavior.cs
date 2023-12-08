using FluentValidation;
using MediatR;

namespace NimblePros.MediatRValidation.UseCases;

/// <summary>
/// This behavior assumes validators have been registered with the container.
/// Example:
/// builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
///        .Where(t => t.IsClosedTypeOf(typeof(IValidator&lt;&gt;)))
///        .AsImplementedInterfaces();
///        
/// You'll also need to register this behavior:
/// Example:
/// builder.RegisterGeneric(typeof(ValidationBehavior&lt;,&gt;))
///     .As(typeof(IPipelineBehavior&lt;,&gt;));
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class ValidationBehavior<TRequest, TResponse> : 
  IPipelineBehavior<TRequest, TResponse>
  where TRequest : IRequest<TResponse>
{
  private readonly IEnumerable<IValidator<TRequest>> _validators;

  public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
  {
    _validators = validators;
  }

  public async Task<TResponse> Handle(TRequest request, 
    RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
  {
    if (_validators.Any())
    {
      var context = new ValidationContext<TRequest>(request);

      var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
      var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

      if (failures.Count != 0)
        throw new ValidationException(failures);
    }

    return await next();
  }
}
