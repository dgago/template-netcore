using System;
using System.Linq;

using FluentValidation.Results;

using Kit.Application.Models.Responses;
using Kit.Domain.Validation;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Kit.Presentation
{
    public abstract class Presenter
    {
        public IActionResult GetCreatedResult<T>(EntityResult<T> result,
            HttpRequest request,
            string path)
            where T : class
        {
            UriBuilder uriBuilder = new UriBuilder
            {
                Host = request.Host.Host,
                Port = request.Host.Port ?? 80,
                Path = path,
                Scheme = request.Scheme
            };

            return !result.IsValid
                ? CreateErrorResult(result)
                : new CreatedResult(uriBuilder.ToString(), result.Item);
        }

        public IActionResult GetOkResult(Result result)
        {
            return !result.IsValid ? CreateErrorResult(result) : new OkResult();
        }

        public IActionResult GetOkObjectResult<T>(EntityResult<T> result)
            where T : class
        {
            return !result.IsValid
                ? CreateErrorResult(result)
                : new OkObjectResult(result.Item);
        }

        public IActionResult GetListResult<T>(HttpResponse response,
            QueryResult<T> result)
            where T : class
        {
            response.Headers.Add("x-count", result.Count.ToString());
            return !result.IsValid
                ? CreateErrorResult(result)
                : new OkObjectResult(result.Items);
        }

        private IActionResult CreateErrorResult(Result result)
        {
            ActionResult actionResult;
            ModelStateDictionary modelState = GetErrors(result);

            ValidationResult res = result.Notifications.FirstOrDefault(x => !x.IsValid);
            if (res == null || res.Errors.Count == 0)
            {
                return new BadRequestObjectResult(modelState);
            }

            switch (res.Errors.First().ErrorCode)
            {
                case nameof(ErrorType.NotFound):
                    actionResult = new NotFoundObjectResult(modelState);
                    break;
                case nameof(ErrorType.BadRequest):
                    actionResult = new UnprocessableEntityObjectResult(modelState);
                    break;
                case nameof(ErrorType.Unauthorized):
                    actionResult = new UnauthorizedResult();
                    break;
                default:
                    actionResult = new BadRequestObjectResult(modelState);
                    break;
            }

            return actionResult;
        }

        private ModelStateDictionary GetErrors(Result result)
        {
            ModelStateDictionary modelState = new ModelStateDictionary();

            foreach (ValidationResult notification in result.Notifications)
            {
                if (notification.IsValid)
                {
                    continue;
                }

                foreach (ValidationFailure error in notification.Errors)
                {
                    modelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return modelState;
        }

        public IActionResult GetNoContentResult(Result result)
        {
            return !result.IsValid ? CreateErrorResult(result) : new NoContentResult();
        }
    }
}