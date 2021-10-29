using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Mang.Application.Contract.System.Files
{
    /// <summary>
    /// 文件服务接口
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        Task<List<FileDto>> UploadAsync(IFormFileCollection files);
    }
}