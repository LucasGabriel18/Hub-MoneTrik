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
        private readonly NotificationHandler _notifications;

        public ValidationBehavior(
            IEnumerable<IValidator<TRequest>> validators,
            NotificationHandler notifications)
        {
            _validators = validators;
            _notifications = notifications;
        }

        public async Task<TResponse> Handle(TRequest request, Func<Task<TResponse>> next)
        {
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context)));

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count > 0)
            {
                foreach (var error in failures)
                {
                    await _notifications.Handle(new Notification(
                        message: error.ErrorMessage,
                        type: ENotificationType.Error
                    ));
                }
                
                return default;
            }
            
            return await next();
        }
    }
}