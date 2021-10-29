using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mang.Domain.System;
using Mang.Application.Contract.System.Files;
using Mang.Infrastructure.File;
using Mang.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;

namespace Mang.Application.System
{
    /// <summary>
    /// 文件服务
    /// </summary>
    public class FileService : IFileService
    {
        private readonly IRepository<File> _repository;
        private readonly IMapper _mapper;
        private readonly IFileUploadService _fileUploadService;

        public FileService(IRepository<File> repository, IFileUploadService fileUploadService, IMapper mapper)
        {
            _repository = repository;
            _fileUploadService = fileUploadService;
            _mapper = mapper;
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="fileCollection"></param>
        /// <returns></returns>
        public async Task<List<FileDto>> UploadAsync(IFormFileCollection fileCollection)
        {
            var paths = await _fileUploadService.UploadAsync(fileCollection);

            var code = Guid.NewGuid();
            var files = paths.Select(a => new File
            {
                Code = code,
                Path = a
            });

            files = await _repository.InsertAsync(files);

            var fileDtos = _mapper.Map<List<FileDto>>(files);

            return fileDtos;
        }
    }
}