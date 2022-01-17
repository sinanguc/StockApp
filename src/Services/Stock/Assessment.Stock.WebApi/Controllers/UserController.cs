using Assessment.Common.Security;
using Assessment.Dto;
using Assessment.Dto.Stock.Authentication;
using Assessment.Enum.Stock.Messages;
using Assessment.Stock.Business.Abstract.AuthenticationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Stock.WebApi.Controllers
{
    public class UserController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public UserController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("dologin")]
        public async Task<ActionResult<GenericResult>> Login(LoginRequestDto loginRequestDto)
        {
            result.Response = await _authenticationService.LoginAsync(loginRequestDto);
            result.Message = StockMessages.AccessTokenBasariylaOlusturuldu;
            return Ok(result);
        }

        [HttpGet("dologout")]
        public async Task<ActionResult<GenericResult>> Logout(string apiCode)
        {
            await _authenticationService.LogoutAsync(apiCode);
            result.Response = true;
            result.Message = StockMessages.AccessTokenBasariylaOlusturuldu;
            return Ok(result);
        }
    }
}
