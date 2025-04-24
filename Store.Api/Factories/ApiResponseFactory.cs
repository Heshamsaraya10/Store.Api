using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using System.Net;

namespace Store.Api.Factories
{
    public class ApiResponseFactory
    {
        public static ActionResult CustomValidationErrorResponse(ActionContext context)
        {
            var errors = context.ModelState.Where(error => error.Value.Errors.Any())
                .Select(error => new ValidationError
                {
                    Key = error.Key,
                    Errors = error.Value.Errors.Select(e => e.ErrorMessage)
                });

            var validationResponce = new ValidationErrorResponce
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Errors = errors,
                ErrorMessage = "Validation Failed"
            };

            return new BadRequestObjectResult(validationResponce);
        }
    }
}
