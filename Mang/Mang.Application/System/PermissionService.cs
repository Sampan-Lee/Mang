using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mang.Domain.System;
using Mang.Application.Contract.System.Permissions;
using Mang.ServiceBase.Impl;
using Mang.Infrastructure.Repository;
using Mang.Public.Dto;
using Mang.Public.Extension;

namespace Mang.Application.System
{
    /// <summary>
    /// 权限服务
    /// </summary>
    public class PermissionService : BaseService, IPermissionService
    {
        private readonly IRepository<Permission> _repository;
        private readonly IRepository<Admin> _userRepository;
        private readonly IRepository<Role> _roleRepository;

        public PermissionService(IRepository<Permission> repository, IRepository<Admin> userRepository,
            IRepository<Role> roleRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 获取全部权限
        /// </summary>
        /// <returns></returns>
        public async Task<List<PermissionModuleDto>> GetAsync()
        {
            var permissions = await _repository.ToTreeListAsync();
            var permissionModuleDtos = permissions.GroupBy(a => a.Module).Select(a => new PermissionModuleDto
            {
                Module = a.Key,
                Permissions = Mapper.Map<List<PermissionDto>>(a)
            }).ToList();
            return permissionModuleDtos;
        }

        /// <summary>
        /// 获取菜单级别的权限
        /// </summary>
        /// <returns></returns>
        public async Task<List<MenuPermissionDto>> GetMenuPermissionAsync()
        {
            var permissions = await _repository
                .Where(a => !a.ParentId.HasValue)
                .ToListAsync();

            var menuPermissionDtos = permissions.GroupBy(a => a.Module).Select(a => new MenuPermissionDto
            {
                Module = a.Key,
                Permissions = a.Select(b => new DictionaryDto
                {
                    Key = b.Id,
                    Value = b.DisplayName
                }).ToList()
            }).ToList();
            return menuPermissionDtos;
        }

        /// <summary>
        /// 根据用户ID获取权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Permission>> GetAsync(int userId)
        {
            var cacheKey = SystemCacheKeyPrefixDefinition.UserPermission + userId;
            var userPermission = await Cache.GetAsync<List<Permission>>(cacheKey);

            if (userPermission == null)
            {
                var user = await _userRepository.Select
                    .IncludeMany(a => a.Roles)
                    .Where(a => a.Id == userId)
                    .FirstAsync();

                var roleIds = user.Roles.Select(b => b.Id).ToList();
                if (roleIds.IsNullOrEmpty()) return null;

                var roles = await _roleRepository.Select
                    .IncludeMany(a => a.Permissions)
                    .Where(a => roleIds.Contains(a.Id))
                    .ToListAsync();

                userPermission = roles.Select(a => a.Permissions)
                    .SelectMany(a => a)
                    .ToList();
                if (userPermission.IsNullOrEmpty()) return null;

                /*
                 * 用户权限校验伴随着每一次接口请求，并发高，IO频繁，存入缓存提升效率
                 * 在用户分配角色和角色分配权限时，更新缓存
                 */
                await Cache.SetAsync(cacheKey, userPermission);
            }

            return userPermission;
        }
    }
}