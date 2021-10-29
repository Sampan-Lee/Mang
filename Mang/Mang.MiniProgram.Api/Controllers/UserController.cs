using System.Threading.Tasks;
using Mang.Application.Contract.Users;
using Mang.Web.Extension.BaseController;
using Mang.Web.Extension.Model;
using Microsoft.AspNetCore.Mvc;

namespace Mang.MiniProgram.Api.Controllers
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public class UserController : BaseController
    {
        private readonly IUserService _service;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        public UserController(IUserService service)
        {
            _service = service;
        }

        /// <summary>
        /// 完成用户注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<JsonResultModel<bool>> RegisterAsync([FromBody] RegisterUserDto input)
        {
            var result = await _service.RegisterAsync(input);
            return result.ToSuccess();
        }
    }
}