using Application.DTOs.Response;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.Filters;

public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(m => (m.Value?.Errors.Count ?? default) > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => (kvp.Value?.Errors ?? []).Select(e => e.ErrorMessage).AsEnumerable()
                );

            var errorResponse = new ValidationErrorResponse(errors);
            context.Result = new OkObjectResult(errorResponse);
        }
    }
}

