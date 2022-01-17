using Assessment.Dto;
using Assessment.Enum.Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Assessment.Common.Aspects.Validation
{
    /// <summary>
    /// Cllient tarafından gönderilen parametrelerin,
    /// Uygulamada zorun parametrelerle karşılaştırmasını yapar.
    /// Eğer Model valid olmazsa, GenericResult tipinde nesneyi response olarak döndürür.
    /// </summary>
    public class ValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;

            GenericResult errorReponse = new GenericResult();

            #region TODO: Dokümanda istenmemeiş, fakat bunun gibi bir validation model Response da döndürülmeli!!
            //var errorsInModelState = context.ModelState
            //    .Where(x => x.Value.Errors.Count > 0)
            //    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

            //foreach (var error in errorsInModelState)
            //{
            //    foreach (var subError in error.Value)
            //    {
            //        ValidationErrorModel errorModel = new ValidationErrorModel
            //        {
            //            FieldName = error.Key,
            //            Message = subError
            //        };
            //    }
            //}
            #endregion

            errorReponse.Message = GenericMessages.LutfenTumGerekliAlanlariDoldurunuz;
            errorReponse.ErrorCode = HttpStatusCode.BadRequest.GetHashCode().ToString();
            context.HttpContext.Response.StatusCode = HttpStatusCode.BadRequest.GetHashCode();
            context.Result = new JsonResult(errorReponse) { StatusCode = HttpStatusCode.BadRequest.GetHashCode() };
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid)
                await next();

            GenericResult errorReponse = new GenericResult();

            #region TODO: Dokümanda istenmemeiş, fakat bunun gibi bir validation model Response da döndürülmeli!!
            //var errorsInModelState = context.ModelState
            //    .Where(x => x.Value.Errors.Count > 0)
            //    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

            //foreach (var error in errorsInModelState)
            //{
            //    foreach (var subError in error.Value)
            //    {
            //        ValidationErrorModel errorModel = new ValidationErrorModel
            //        {
            //            FieldName = error.Key,
            //            Message = subError
            //        };
            //    }
            //}
            #endregion

            errorReponse.Message = GenericMessages.LutfenTumGerekliAlanlariDoldurunuz;
            errorReponse.ErrorCode = HttpStatusCode.BadRequest.GetHashCode().ToString();
            context.HttpContext.Response.StatusCode = HttpStatusCode.BadRequest.GetHashCode();
            context.Result = new JsonResult(errorReponse) { StatusCode = HttpStatusCode.BadRequest.GetHashCode() };
        }
    }
}
