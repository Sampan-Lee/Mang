using System.Collections.Generic;
using System.Threading.Tasks;
using Mang.ServiceBase.Interface;
using Mang.Public.Dto;

namespace Mang.Application.Contract.System.Roles
{
    /// <summary>
    /// 角色服务接口
    /// </summary>
    public interface IRoleService : ICrudService<RoleDto, RoleListDto, GetRoleListDto, CreateUpdateRoleDto,
        CreateUpdateRoleDto>
    {
        /// <summary>
        /// 获取角色字典
        /// </summary>
        /// <returns></returns>
        Task<List<DictionaryDto>> GetDictionaryAsync();

        /// <summary>
        /// 获取角色权限ID
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns></returns>
        Task<List<int>> GetPermissionAsync(int id);

        /// <summary>
        /// 分配角色权限
        /// </summary>
        /// <param name="input"></param>
        Task AssignPermissionAsync(AssignPermissionDto input);

        /// <summary>
        /// 设置用户状态
        /// </summary>
        /// <returns></returns>
        Task SetIsEnableAsync(SetIsEnableDto input);
    }
}