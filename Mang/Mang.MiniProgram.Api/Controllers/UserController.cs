using Mang.Application.Contract.Users;
using Mang.Web.Extension.BaseController;

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
    }
}