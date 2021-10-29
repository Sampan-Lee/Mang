using System.Threading.Tasks;
using Mang.Application.Contract.System.Admins;
using Mang.Public.Dto;
using Mang.Web.Extension.BaseController;
using Mang.Web.Extension.Model;
using Microsoft.AspNetCore.Mvc;

namespace Mang.Web.Controllers.System
{
    public class AdminController : BaseController
    {
        private readonly IAdminService _service;

        public AdminController(IAdminService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<JsonResultModel<AdminDto>> GetAsync([FromRoute] int id)
        {
            var result = await _service.GetAsync(id);
            return result.ToSuccess();
        }

        [HttpGet]
        public async Task<JsonResultModel<PageResultDto<AdminListDto>>> GetAsync(
            [FromQuery] GetAdminListDto input)
        {
            var result = await _service.GetAsync(input);
            return result.ToSuccess();
        }

        [HttpPost]
        public async Task<JsonResultModel<bool>> CreateAsync([FromBody] CreateUpdateAdminDto input)
        {
            await _service.CreateAsync(input);
            return true.ToSuccess();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<JsonResultModel<bool>> UpdateAsync([FromRoute] int id, [FromBody] CreateUpdateAdminDto input)
        {
            await _service.UpdateAsync(id, input);
            return true.ToSuccess();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<JsonResultModel<bool>> DeleteAsync([FromRoute] int id)
        {
            await _service.DeleteAsync(id);
            return true.ToSuccess();
        }

        [HttpPost]
        [Route("password:reset")]
        public async Task<JsonResultModel<bool>> ResetPasswordAsync([FromBody] ResetPasswordDto input)
        {
            await _service.ResetPasswordAsync(input);
            return true.ToSuccess();
        }

        [HttpPost]
        [Route("role:assign")]
        public async Task<JsonResultModel<bool>> AssignRoleAsync([FromBody] AssignRoleDto input)
        {
            await _service.AssignRoleAsync(input);
            return true.ToSuccess();
        }


        [HttpPost]
        [Route("IsEnable:set")]
        public async Task<JsonResultModel<bool>> SetIsEnableAsync([FromBody] SetIsEnableDto input)
        {
            await _service.SetIsEnableAsync(input);
            return true.ToSuccess();
        }
    }
}