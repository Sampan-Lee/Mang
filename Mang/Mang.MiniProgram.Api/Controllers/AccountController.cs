using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Mang.Application.Contract;
using Mang.Application.Contract.Users;
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
        public async Task<string> LoginAsync([FromBody] LoginDto input)
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
                    new(MiniProgramPermissionDefinition.FinishRegister, loginUser.FinishRegister.ToString())
                };
                //查询出用户对应的权限角色  
                token = JWTService.GetToken(claims);
            }

            return token;
        }

        /// <summary>
        /// 完成用户注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("complete")]
        public async Task<string> CompleteAsync([FromBody] FinishRegisterDto input)
        {
            var loginUser = await _userService.FinishRegisterAsync(input);
            string token = string.Empty;
            if (loginUser is { Id: > 0 })
            {
                List<Claim> claims = new List<Claim>
                {
                    new(ClaimTypes.Sid, loginUser.Id.ToString()),
                    new(ClaimTypes.Name, loginUser.NickName),
                    new(ClaimTypes.Actor, loginUser.AvatarUrl),
                    new(ClaimTypes.MobilePhone, loginUser.Phone),
                    new(MiniProgramPermissionDefinition.FinishRegister, loginUser.FinishRegister.ToString())
                };
                //查询出用户对应的权限角色  
                token = JWTService.GetToken(claims);
            }

            return token;
        }
    }
}