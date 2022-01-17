using Assessment.Common.Security.Token;
using Assessment.Dto;
using Assessment.Enum.Stock.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace Assessment.Common.Security
{
    /// <summary>
    /// Uygulamanın Authorize işlemlerini yapar.
    /// Gelen her istekte çalışır.
    /// Gelen Requestten apiCode parametresini alarak, Rediste apiCode var mı yok mu kontrolü yapar.
    /// Eğer apiCode yoksa, Response olarak GenericResult tipinde nesne döndürür.
    /// </summary>
    public class AssessmentAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // TODO: apiCode neden header dan alınmıyor? alınsa daha iyi olabilir
            var request = context.HttpContext.Request;
            request.EnableBuffering();

            TokenDto tokenDto = new TokenDto();
            if (request.Method == "GET")
            {
                tokenDto.ApiCode = request.Query["apiCode"];

            } 
            else if(request.Method == "POST")
            {
                var reader = new StreamReader(request.Body);
                string body = reader.ReadToEndAsync().Result;
                request.Body.Position = 0;
                tokenDto = JsonConvert.DeserializeObject<TokenDto>(body);
            }

            if (tokenDto == null || string.IsNullOrWhiteSpace(tokenDto.ApiCode))
            {
                context.Result = new UnauthorizedObjectResult(new GenericResult
                {
                    ErrorMessage = "ApiCode boş olamaz",
                    ErrorCode = HttpStatusCode.Unauthorized.GetHashCode().ToString()
                });
            }

            var tokenManager = (ITokenService)context.HttpContext.RequestServices.GetService(typeof(ITokenService));
            var userInfo = tokenManager.GetUserByToken(tokenDto.ApiCode);
            if (userInfo != null)
            {
               tokenManager.RefreshTokenAsync(userInfo.ApiCode);
            }
            else
            {
                string jsonString = JsonConvert.SerializeObject(new GenericResult
                {
                    ErrorMessage = "ApiCode hatalı",
                    ErrorCode = HttpStatusCode.Unauthorized.GetHashCode().ToString()
                });

                context.Result = new UnauthorizedObjectResult(jsonString);
            }

        }
    }
}
