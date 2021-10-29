using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeSql;
using Mang.Domain.System;
using Mang.Application.Contract.System.Roles;
using Mang.ServiceBase.Impl;
using Mang.Infrastructure.AOP.Transactional;
using Mang.Infrastructure.Repository;
using Mang.Public.Dto;
using Mang.Public.Extension;

namespace Mang.Application.System
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public class RoleService :
        CrudService<Role, RoleDto, RoleListDto, GetRoleListDto, CreateUpdateRoleDto, CreateUpdateRoleDto>,
        IRoleService
    {
        private readonly RoleManager _roleManager;
        private readonly IRepository<RolePermission> _rolePermissionRepository;
        private readonly IRepository<Permission> _permissionRepository;

        public RoleService(RoleManager roleManager, IRepository<RolePermission> rolePermissionRepository,
            IRepository<Permission> permissionRepository,
            IRepository<Role> repository) : base(repository)
        {
            _roleManager = roleManager;
            _rolePermissionRepository = rolePermissionRepository;
            _permissionRepository = permissionRepository;
        }

        /// <summary>
        /// 列表查询构造器
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override ISelect<Role> CreateFilteredQuery(GetRoleListDto input)
        {
            return Repository.Include(a => a.CreateUser)
                .WhereIf(!input.Name.IsNullOrEmpty(), a => a.Name.Contains(input.Name));
        }

        /// <summary>
        /// 请求排序
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override ISelect<Role> ApplySorting(ISelect<Role> query, GetRoleListDto input)
        {
            if (input.Sort.ToInitialUpper() == nameof(RoleListDto.CreateUserName))
            {
                query.OrderByIf(true, a => a.CreateUser.Name, input.Asc);
                return query;
            }

            return base.ApplySorting(query, input);
        }

        /// <summary>
        /// 列表查询结果映射
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override async Task<RoleListDto> MapToListDtoAsync(Role entity)
        {
            var roleDto = await base.MapToListDtoAsync(entity);
            return roleDto;
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="input"></param>
        public override async Task CreateAsync(CreateUpdateRoleDto input)
        {
            var role = await _roleManager.CreateAsync(input.Name, input.Description, input.Sort);
            await Repository.InsertAsync(role);
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        public override async Task UpdateAsync(int id, CreateUpdateRoleDto input)
        {
            var role = await Repository.GetAsync(id);
            ThrowIf(role == null, new RoleNotExistsException(id.ToString()));

            await _roleManager.ChangeNameAsync(role, input.Name);
            role.Description = input.Description;
            role.Sort = input.Sort;

            await Repository.UpdateAsync(role);
        }

        /// <summary>
        /// 获取角色字典
        /// </summary>
        /// <returns></returns>
        public async Task<List<DictionaryDto>> GetDictionaryAsync()
        {
            return await Repository.ToListAsync(a => new DictionaryDto { Key = a.Id, Value = a.Name });
        }

        public async Task<List<int>> GetPermissionAsync(int id)
        {
            var rolePermissions = await _rolePermissionRepository
                .Where(a => a.RoleId == id)
                .ToListAsync(a => a.PermissionId);

            return await _permissionRepository
                .Where(a => rolePermissions.Contains(a.Id))
                .ToListAsync(a => a.Id);
        }

        [Transaction]
        public async Task AssignPermissionAsync(AssignPermissionDto input)
        {
            await _rolePermissionRepository.DeleteAsync(a => a.RoleId == input.Id);

            var entities = input.PermissionIds.Select(
                a => new RolePermission
                {
                    RoleId = input.Id,
                    PermissionId = a
                }
            );

            await _rolePermissionRepository.InsertAsync(entities);

            /*
             * 角色重新分配权限，对应的用户的所有权限都产生改变
             * 在这里把用户权限缓存清除，以保证角色分配权限实时生效
             */
            await Cache.RemoveByKeyPrefixAsync(SystemCacheKeyPrefixDefinition.UserPermission);
        }

        /// <summary>
        /// 设置角色状态
        /// </summary>
        /// <param name="input"></param>
        public async Task SetIsEnableAsync(SetIsEnableDto input)
        {
            var role = await Repository.GetAsync(input.Id);
            ThrowIf(role == null, new RoleNotExistsException(input.Id.ToString()));
            role.IsEnable = input.IsEnable;
            await Repository.UpdateAsync(role);
        }
    }
}