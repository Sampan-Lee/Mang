using System.Threading.Tasks;
using Game.Domain.Shared;
using Mang.Application.Contract.Notes;
using Mang.Application.Contract.Notes.Dtos;
using Mang.Web.Extension.BaseController;
using Mang.Web.Extension.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mang.MiniProgram.Api.Controllers
{
    /// <summary>
    /// 纸条服务
    /// </summary>
    [Authorize("FinishRegister")]
    public class NoteController : BaseController
    {
        private readonly INoteService _service;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        public NoteController(INoteService service)
        {
            _service = service;
        }

        /// <summary>
        /// 投放一张纸条
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResultModel<bool>> CreateAsync([FromBody] CreateNoteDto input)
        {
            var result = await _service.CreateAsync(input);
            return result.ToSuccess();
        }

        /// <summary>
        /// 获取一张纸条
        /// </summary>
        /// <param name="gender">性别</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResultModel<NoteDto>> GetAsync([FromQuery] Gender gender)
        {
            var result = await _service.GetAsync(gender);
            return result.ToSuccess();
        }
    }
}