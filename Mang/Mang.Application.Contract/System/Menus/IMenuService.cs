using System.Collections.Generic;
using System.Threading.Tasks;
using Mang.ServiceBase.Interface;
using Mang.Public.Dto;

namespace Mang.Application.Contract.System.Menus
{
    /// <summary>
    /// 菜单服务接口
    /// </summary>
    public interface IMenuService : IBaseService
    {
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        Task<List<MenuListDto>> GetAsync();

        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MenuDto> GetAsync(int id);

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(CreateUpdateMenuDto input);

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(int id, CreateUpdateMenuDto input);

        /// <summary>
        /// 设置启用/禁用状态
        /// </summary>
        /// <returns></returns>
        Task SetIsEnableAsync(SetIsEnableDto input);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}