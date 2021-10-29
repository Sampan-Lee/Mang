using System.Collections.Generic;
using System.Threading.Tasks;
using Mang.Application.Contract.System.Permissions;
using Mang.Web.Extension.BaseController;
using Mang.Web.Extension.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mang.Web.Controllers.System
{
    [AllowAnonymous]
    public class PermissionController : BaseController
    {
        private readonly IPermissionService _service;

        public PermissionController(IPermissionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<JsonResultModel<List<PermissionModuleDto>>> GetAsync()
        {
            var result = await _service.GetAsync();
            return result.ToSuccess();
        }

        [HttpGet]
        [Route("menu-permission")]
        public async Task<JsonResultModel<List<MenuPermissionDto>>> GetMenuPermissionAsync()
        {
            var result = await _service.GetMenuPermissionAsync();
            return result.ToSuccess();
        }
    }
}