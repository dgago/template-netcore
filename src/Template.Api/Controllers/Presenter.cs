using System;
using System.Linq;

using Application.Models.Result;

using FluentValidation.Results;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Template.Api.Controllers
{
    public class Presenter
    {
        private const string NOT_FOUND    = "NotFound";
        private const string BUSINESS     = "Business";
        private const string UNAUTHORIZED = "Unauthorized";
        private const string BAD_REQUEST  = "BadRequest";

        public IActionResult GetCreatedResult<T>(EntityResult<T> result)
            where T : class
        {
            UriBuilder urlBuilder = new UriBuilder {Path = "~/api/values", Query = null};

            string url = urlBuilder.ToString();

            return !result.IsValid
                ? CreateErrorResult(result)
                : new CreatedResult(url, result.Item);
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
            ActionResult         actionResult;
            ModelStateDictionary modelState = GetErrors(result);
            switch (result.Notifications.First().Errors.First().ErrorCode)
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
                case BAD_REQUEST:
                default:
                    actionResult = new BadRequestObjectResult(modelState);
                    break;
            }

            return actionResult;
        }

        private static ModelStateDictionary GetErrors(Result result)
        {
            ModelStateDictionary modelState = new ModelStateDictionary();

            foreach (ValidationResult notif in result.Notifications)
            {
                foreach (ValidationFailure error in notif.Errors)
                {
                    modelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return modelState;
        }

        public IActionResult GetNoContentResult(Result result)
        {
            return !result.IsValid
                ? CreateErrorResult(result)
                : new NoContentResult();
        }
    }
}