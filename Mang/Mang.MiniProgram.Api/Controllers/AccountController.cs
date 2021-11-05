using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Mang.Application.Contract.Users;
using Mang.Public.Extension;
using Mang.Web.Extension.Authentication;
using Mang.Web.Extension.BaseController;
using Mang.Web.Extension.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mang.MiniProgram.Api.Controllers
{
    /// <summary>
    /// 账号服务
    /// </summary>
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userService"></param>
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<JsonResultModel<string>> LoginAsync([FromBody] LoginDto input)
        {
            var loginUser = await _userService.LoginAsync(input);
            string token = string.Empty;
            if (loginUser is { Id: > 0 })
            {
                List<Claim> claims = new List<Claim>
                {
                    new(ClaimTypes.Sid, loginUser.Id.ToString()),
                    new(ClaimTypes.Name, loginUser.NickName),
                    new(ClaimTypes.Actor, loginUser.AvatarUrl),
                    new(ClaimTypes.MobilePhone, loginUser.Phone),
                    new("IsFinishRegister", loginUser.IsVip.ToString())
                };
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

        /// <summary>
        /// 完成用户注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("complete")]
        public async Task<JsonResultModel<string>> CompleteAsync([FromBody] CompleteUserDto input)
        {
            var loginUser = await _userService.CompleteAsync(input);
            string token = string.Empty;
            if (loginUser is { Id: > 0 })
            {
                List<Claim> claims = new List<Claim>
                {
                    new(ClaimTypes.Sid, loginUser.Id.ToString()),
                    new(ClaimTypes.Name, loginUser.NickName),
                    new(ClaimTypes.Actor, loginUser.AvatarUrl),
                    new(ClaimTypes.MobilePhone, loginUser.Phone),
                    new("IsFinishRegister", loginUser.IsVip.ToString())
                };
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
    }
}