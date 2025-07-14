using System.Text.Json;
using FluentValidation;
using Hub.Monetrik.Domain.Notifications;
using Hub.Monetrik.Mediator.Interfaces;
using Hub.Monetrik.Mediator.Interfaces.Mediator;
using Hub.MoneTrik.Infrastructure.Enums.Notifications;
using static Hub.Monetrik.Mediator.Interfaces.IPipelineBehavior;

namespace Hub.Monetrik.Domain.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly IMediator _mediator;

        public ValidationBehavior(
            IEnumerable<IValidator<TRequest>> validators,
            IMediator mediator)
        {
            _validators = validators;
            _mediator = mediator;
        }
        public async Task<TResponse> Handle(TRequest request, Func<Task<TResponse>> next)
        {
            if (!_validators.Any()) return await next();

            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context)));

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count > 0)
            {
                // Publica apenas a mensagem de erro sem formatação adicional
                foreach (var error in failures)
                {
                    await _mediator.Publish(new Notification(
                        error.ErrorMessage,
                        ENotificationType.Error
                    ));
                }

                throw new ValidationException(failures);
            }


            return await next();
        }
    }
}