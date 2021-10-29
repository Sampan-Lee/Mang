using System.Collections.Generic;
using System.Threading.Tasks;
using Mang.Application.Contract.System.Files;
using Mang.Web.Extension.BaseController;
using Mang.Web.Extension.Model;
using Microsoft.AspNetCore.Mvc;

namespace Mang.Web.Controllers.System
{
    public class FileController : BaseController
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<JsonResultModel<List<FileDto>>> OnPostUploadAsync()
        {
            var result = await _fileService.UploadAsync(Request?.Form.Files);
            return result.ToSuccess();
        }
    }
}