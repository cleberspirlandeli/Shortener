using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shortener.Services.Notifications;
using System.Linq;

namespace Alpha.API.Controllers.Common
{
    //[ApiController]
    public abstract class BaseController : ControllerBase
    {
        private readonly INotification _notifier;

        protected BaseController(INotification notifier)
        {
            _notifier = notifier;
        }

        // Validação de notificações de erro
        protected bool IsValid() => !_notifier.HasNotification();

        protected ActionResult CustomResponse(object result = null)
        {
            if (IsValid())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifier.GetNotifications().Select(x => x.Message)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotifyErrorModelInvalid(modelState);
            return CustomResponse();
        }

        // Validation ModelState
        protected void NotifyErrorModelInvalid(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                var errorMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(errorMessage);
            }
        }

        protected void NotifyError(string errorMessage) => _notifier.Handle(new Notifier(errorMessage));

    }
}
