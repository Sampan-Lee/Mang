using System.Collections.Generic;
using System.Threading.Tasks;
using Mang.Application.Contract.System.Menus;
using Mang.Public.Dto;
using Mang.Web.Extension.BaseController;
using Mang.Web.Extension.Model;
using Microsoft.AspNetCore.Mvc;

namespace Mang.Web.Controllers.System
{
    public class MenuController : BaseController
    {
        private readonly IMenuService _service;

        public MenuController(IMenuService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<JsonResultModel<List<MenuListDto>>> GetAsync()
        {
            var result = await _service.GetAsync();
            return result.ToSuccess();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<JsonResultModel<MenuDto>> GetAsync([FromRoute] int id)
        {
            var result = await _service.GetAsync(id);
            return result.ToSuccess();
        }

        [HttpPost]
        public async Task<JsonResultModel<bool>> CreateAsync([FromBody] CreateUpdateMenuDto input)
        {
            await _service.CreateAsync(input);
            return true.ToSuccess();
        }

        [HttpPost]
        [Route("IsEnable:set")]
        public async Task<JsonResultModel<bool>> SetIsEnableAsync([FromBody] SetIsEnableDto input)
        {
            await _service.SetIsEnableAsync(input);
            return true.ToSuccess();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<JsonResultModel<bool>> UpdateAsync([FromRoute] int id, [FromBody] CreateUpdateMenuDto input)
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
    }
}