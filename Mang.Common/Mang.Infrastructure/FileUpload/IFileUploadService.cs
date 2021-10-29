using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Mang.Infrastructure.File
{
    public interface IFileUploadService
    {
        Task<List<string>> UploadAsync(IFormFileCollection files);
    }
}