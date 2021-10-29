using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Mang.Public.Extension;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Mang.Infrastructure.File
{
    public class LocalFileUploadService : IFileUploadService
    {
        private readonly IHostingEnvironment _env;

        public LocalFileUploadService(IHostingEnvironment env)
        {
            _env = env;
        }

        public async Task<List<string>> UploadAsync(IFormFileCollection files)
        {
            if (files.IsNullOrEmpty()) throw new Exception("请选择上传的文件!");
            List<string> result = new List<string>();
            string day = DateTime.Now.ToString("yyyyMMdd");
            string urlPath = $"Upload/{day}/";
            string filePath = Path.Combine(_env.ContentRootPath, urlPath).ReplacePath();
            if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);

            foreach (var file in files)
            {
                var ext = Path.GetExtension(file.FileName);
                string fileName = Guid.NewGuid() + ext;
                var path = Path.Combine(filePath, fileName);
                //不得大于10M
                if (file.Length <= 1024 * 1024 * 10)
                {
                    using (var stream = new FileStream(path, FileMode.CreateNew, FileAccess.ReadWrite))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                else
                {
                    throw new Exception("上传文件过大!");
                }

                //更新数据库的url
                result.Add($"{urlPath}{fileName}");
            }

            return result;
        }
    }
}