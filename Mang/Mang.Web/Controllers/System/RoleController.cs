using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mang.Application.Contract.System.Roles;
using Mang.Public.Dto;
using Mang.Web.Extension.BaseController;
using Mang.Web.Extension.Model;

namespace Mang.Web.Controllers.System
{
    public class RoleController : BaseController
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<JsonResultModel<RoleDto>> GetAsync([FromRoute] int id)
        {
            var result = await _service.GetAsync(id);
            return result.ToSuccess();
        }

        [HttpGet]
        public async Task<JsonResultModel<PageResultDto<RoleListDto>>> GetAsync([FromQuery] GetRoleListDto input)
        {
            var result = await _service.GetAsync(input);
            return result.ToSuccess();
        }

        [HttpGet]
        [Route("dic")]
        public async Task<JsonResultModel<List<DictionaryDto>>> GetDictionaryAsync()
        {
            var result = await _service.GetDictionaryAsync();
            return result.ToSuccess();
        }


        [HttpGet]
        [Route("{id}/permission")]
        public async Task<JsonResultModel<List<int>>> GetPermissionAsync([FromRoute] int id)
        {
            var result = await _service.GetPermissionAsync(id);
            return result.ToSuccess();
        }

        [HttpPost]
        [Route("permission:assign")]
        public async Task<JsonResultModel<bool>> AssignPermissionAsync([FromBody] AssignPermissionDto input)
        {
            await _service.AssignPermissionAsync(input);
            return true.ToSuccess();
        }

        [HttpPost]
        [Route("isEnable:set")]
        public async Task<JsonResultModel<bool>> AssignPermissionAsync([FromBody] SetIsEnableDto input)
        {
            await _service.SetIsEnableAsync(input);
            return true.ToSuccess();
        }

        [HttpPost]
        public async Task<JsonResultModel<object>> CreateAsync([FromBody] CreateUpdateRoleDto input)
        {
            await _service.CreateAsync(input);
            return new JsonResultModel<object>();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<JsonResultModel<object>> UpdateAsync([FromRoute] int id, [FromBody] CreateUpdateRoleDto input)
        {
            await _service.UpdateAsync(id, input);
            return new JsonResultModel<object>();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<JsonResultModel<object>> DeleteAsync([FromRoute] int id)
        {
            await _service.DeleteAsync(id);
            return new JsonResultModel<object>();
        }
    }
}