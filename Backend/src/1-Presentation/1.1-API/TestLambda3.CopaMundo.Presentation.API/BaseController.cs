using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TestLambda3.WorldCup.Application.Interface;

namespace TestLambda3.WorldCup.Presentation.API
{
    public class BaseController : ControllerBase
    {
        private readonly IBaseNotification _baseNotification;
        public ProblemDetailsFactory ProblemDetails => HttpContext?.RequestServices?.GetRequiredService<ProblemDetailsFactory>();

        public BaseController(IBaseNotification baseNotification)
        {
            _baseNotification = baseNotification;
        }

        protected IActionResult OKOrBadRequest(object? result)
        {
            if (_baseNotification.IsValid) return Ok(result);

            return BadRequestBase();
        }

        protected IActionResult CreatedOrBadRequest(object? result)
        {
            if (_baseNotification.IsValid) return Created(string.Empty, result);

            return BadRequestBase();
        }

        protected IActionResult BadRequestBase()
        {
            var problemDetails = ProblemDetails.CreateProblemDetails(HttpContext, (int)HttpStatusCode.BadRequest, "Bad request");
            var notifications = _baseNotification.Notifications.Select(notification => new { name = notification.Key, reason = notification.Message });

            problemDetails.Extensions.Add("invalid-params", notifications);
            return BadRequest(problemDetails);
        }

    }
}


