using System.Collections.Generic;
using System.Threading.Tasks;
using Mang.ServiceBase.Interface;
using Mang.Domain.System;

namespace Mang.Application.Contract.System.Permissions
{
    /// <summary>
    /// 权限服务接口
    /// </summary>
    public interface IPermissionService : IBaseService
    {
        /// <summary>
        /// 获取权限数据
        /// </summary>
        /// <returns></returns>
        Task<List<PermissionModuleDto>> GetAsync();

        /// <summary>
        /// 获取菜单类型权限
        /// </summary>
        /// <returns></returns>
        Task<List<MenuPermissionDto>> GetMenuPermissionAsync();

        /// <summary>
        /// 根据用户ID获取权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<Permission>> GetAsync(int userId);
    }
}