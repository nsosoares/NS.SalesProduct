using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NS.SalesProduct.Business.Interfaces;
using NS.SalesProduct.Services.API.ViewModels;

namespace NS.SalesProduct.Services.API.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected readonly INotificationHandler _notificationHandler;

        protected BaseController(INotificationHandler notificationHandler)
        {
            _notificationHandler = notificationHandler;
        }

        protected ActionResult<ResponseApiViewModel> CustomResponse(ModelStateDictionary modelState, object data = null)
        {
            ValidateModelState(modelState);
            if (!OperationIsValid())
                return BadRequest(new ResponseApiViewModel
                {
                    Success = false,
                    Data = data,
                    Notifications = _notificationHandler.GetNotifications().ToList()
                });
            return Ok(new ResponseApiViewModel
            {
                Success = true,
                Data = data,
                Notifications = _notificationHandler.GetNotifications().ToList()
            });
        }

        protected void ValidateModelState(ModelStateDictionary modelState)
        {
            var errorsInModelState = modelState.Values.SelectMany(value => value.Errors);
            foreach (var error in errorsInModelState)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected ActionResult<ResponseApiViewModel> CustomResponse(object data = null)
        {
            if (!OperationIsValid())
                return BadRequest(new ResponseApiViewModel 
                { 
                    Success = false,
                    Data = data,
                    Notifications = _notificationHandler.GetNotifications().ToList()
                });
            return Ok(new ResponseApiViewModel
            {
                Success = true,
                Data = data,
                Notifications = _notificationHandler.GetNotifications().ToList()
            });
        }

        protected bool OperationIsValid() => !_notificationHandler.HasNotifications();

        protected void Notify(string message)
            => _notificationHandler.Handle(message);
    }
}
