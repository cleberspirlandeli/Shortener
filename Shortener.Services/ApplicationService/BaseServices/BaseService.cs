using FluentValidation;
using FluentValidation.Results;
using Shortener.Domain;
using Shortener.Services.Notifications;

using System;

namespace Shortener.Services.ApplicationService.BaseServices
{
    public abstract class BaseService
    {
        private readonly INotification _notification;

        protected BaseService(INotification notification)
        {
            _notification = notification;
        }

        protected void NotifyError(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                NotifyError(error.ErrorMessage);
            }
        }

        protected void NotifyError(string mensagem)
        {
            _notification.Handle(new Notifier(mensagem));
        }

        protected bool OperationIsValid()
        {
            return !_notification.HasNotification();
        }

        protected bool ExecuteValidations<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            NotifyError(validator);

            return false;
        }

        protected void VerifyExists(object objectVerify, string customMessage = "")
        {
            if (objectVerify == null && !String.IsNullOrEmpty(customMessage))
                throw new Exception(customMessage);
        }
    }
}
