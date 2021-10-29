using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mang.Application.Contract.System.Accounts;
using Mang.Application.Contract.System.Admins;
using Mang.Public.CurrentUser;
using Mang.Public.Extension;
using Mang.Web.Extension.Authentication;
using Mang.Web.Extension.BaseController;
using Mang.Web.Extension.Model;

namespace Mang.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("captcha")]
        [AllowAnonymous]
        public async Task<JsonResultModel<bool>> GetCaptchaAsync([FromQuery] string phone)
        {
            var result = await _service.SendCaptchaAsync(phone);
            return result.ToSuccess();
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<JsonResultModel<String>> LoginAsync([FromBody] LoginDto input)
        {
            var loginUser = await _service.LoginAsync(input);
            string token = string.Empty;
            if (loginUser is { Id: > 0 })
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Sid, loginUser.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, loginUser.Name));
                claims.Add(new Claim(GameClaimTypes.IsAdmin, true.ToString()));
                claims.Add(new Claim(GameClaimTypes.IsSuperAdmin, (loginUser.Id == 1).ToString()));
                //查询出用户对应的权限角色  
                token = JWTService.GetToken(claims);
            }

            return new JsonResultModel<string>
            {
                status = true,
                code = token.IsNullOrWhiteSpace() ? HttpStatusCode.LoginFail : HttpStatusCode.OK,
                data = token
            };
        }

        [HttpGet]
        [Route("info")]
        public async Task<JsonResultModel<AdminDto>> GetAsync(string token)
        {
            var userDto = await _service.GetUserAsync();
            return userDto.ToSuccess();
        }

        [HttpGet]
        [Route("menu")]
        public async Task<JsonResultModel<List<AccountMenuDto>>> GetMenuAsync()
        {
            var result = await _service.GetMenuAsync();
            return result.ToSuccess();
        }

        [HttpGet]
        [Route("permission")]
        public async Task<JsonResultModel<List<string>>> GetPermissionAsync()
        {
            var result = await _service.GetPermissionAsync();
            return result.ToSuccess();
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult LogoutAsync()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}