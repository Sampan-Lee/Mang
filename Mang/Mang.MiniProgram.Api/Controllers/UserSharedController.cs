using System.Threading.Tasks;
using Mang.Application.Contract.UserShareds;
using Mang.Web.Extension.BaseController;
using Mang.Web.Extension.Model;
using Microsoft.AspNetCore.Mvc;

namespace Mang.MiniProgram.Api.Controllers
{
    /// <summary>
    /// 用户分享服务
    /// </summary>
    public class UserSharedController : BaseController
    {
        private readonly IUserSharedService _service;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        public UserSharedController(IUserSharedService service)
        {
            _service = service;
        }

        /// <summary>
        /// 点击分享链接回调
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResultModel<bool>> CreateAsync([FromBody] CreateUserSharedDto input)
        {
            var result = await _service.CreateAsync(input);
            return result.ToSuccess();
        }
    }
}