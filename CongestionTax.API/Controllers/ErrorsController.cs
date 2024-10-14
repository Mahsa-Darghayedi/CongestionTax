using CongestionTaxCalculator.Service.Extensions.CustomExceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTax.API.Controllers;


public class ErrorsController : ControllerBase
{
    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception == null)
            return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "An error has occurred.");

        var (statusCode, message) = exception switch
        {
            ServiceException serviceException => ((int)serviceException.StatusCode, serviceException.Message),
            _ => (StatusCodes.Status500InternalServerError, "An error has occurred.")
        };
        return Problem(statusCode: statusCode, title: message);

    }
}
