using System.Collections.Generic;
using System.Threading.Tasks;
using Mang.Application.Contract.System;
using Mang.Application.Contract.System.Logs;
using Mang.Public.Dto;
using Mang.Web.Extension.BaseController;
using Mang.Web.Extension.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mang.Web.Controllers.System
{
    public class LogController : BaseController
    {
        private readonly ILogService _service;

        public LogController(ILogService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<JsonResultModel<LogDto>> GetAsync(int id)
        {
            var result = await _service.GetAsync(id);
            return result.ToSuccess();
        }

        [HttpGet]
        public async Task<JsonResultModel<PageResultDto<LogListDto>>> GetAsync([FromQuery] GetLogListDto input)
        {
            var result = await _service.GetAsync(input);
            return result.ToSuccess();
        }

        [HttpGet]
        [Route("level/dic")]
        public async Task<JsonResultModel<List<DictionaryDto>>> GetLevelDictionaryAsync()
        {
            var result = await _service.GetLevelDictionaryAsync();
            return result.ToSuccess();
        }
    }
}