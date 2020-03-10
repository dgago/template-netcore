using System;
using System.Linq;

using Application.Models.Result;

using FluentValidation.Results;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api
{
    public abstract class Presenter
    {
        private const string NOT_FOUND = "NotFound";
        private const string BUSINESS = "Business";

        private const string UNAUTHORIZED = "Unauthorized";
//        private const string BAD_REQUEST = "BadRequest";

        public IActionResult GetCreatedResult<T>(EntityResult<T> result,
            HttpRequest request, string path)
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
                case NOT_FOUND:
                    actionResult = new NotFoundObjectResult(modelState);
                    break;
                case BUSINESS:
                    actionResult = new UnprocessableEntityObjectResult(modelState);
                    break;
                case UNAUTHORIZED:
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

            foreach (ValidationResult notif in result.Notifications)
            {
                if (notif.IsValid)
                {
                    continue;
                }

                foreach (ValidationFailure error in notif.Errors)
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