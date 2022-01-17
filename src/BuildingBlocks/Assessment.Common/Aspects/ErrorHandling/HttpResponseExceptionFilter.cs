using Assessment.Common.Infrastructure.ErrorHandling;
using Assessment.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace Assessment.Common.Aspects.ErrorHandling
{
    /// <summary>
    /// Uygulamanın herhangi bir yerinde hata alınması durumunda,
    /// GenericResult nesene modelini response olarak döndürür
    /// </summary>
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null)
                return;

            GenericResult result = new GenericResult();
            result.ErrorMessage = context.Exception.Message;
            result.ErrorCode = HttpStatusCode.InternalServerError.GetHashCode().ToString(); // eğer hata tipleri ile uyuşmazsa

            switch (context.Exception)
            {
                case LoginIncorrectException:
                case TokenException:
                    result.ErrorCode = HttpStatusCode.Unauthorized.GetHashCode().ToString();
                    break;
                case RecordExistException:
                    result.ErrorCode = HttpStatusCode.NotFound.GetHashCode().ToString();
                    break;
                case ValidationException:
                case ArgumentNullException:
                case RecordNotFoundException:
                    result.ErrorCode = HttpStatusCode.BadRequest.GetHashCode().ToString();
                    break;
                case DatabaseException:
                    result.ErrorCode = HttpStatusCode.InternalServerError.GetHashCode().ToString();
                    break;
                default:
                    result.ErrorCode = HttpStatusCode.InternalServerError.GetHashCode().ToString();
                    break;
            }

            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = Convert.ToInt32(result.ErrorCode);

            context.HttpContext.Response.ContentType = "application/problem+json";
            context.Result = new JsonResult(result);

        }
    }
}
