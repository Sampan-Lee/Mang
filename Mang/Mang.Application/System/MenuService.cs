using System.Collections.Generic;
using System.Threading.Tasks;
using Mang.Domain.System;
using Mang.Application.Contract.System.Menus;
using Mang.ServiceBase.Impl;
using Mang.Infrastructure.Repository;
using Mang.Public.Dto;

namespace Mang.Application.System
{
    /// <summary>
    /// 菜单服务
    /// </summary>
    public class MenuService : BaseService, IMenuService
    {
        private readonly MenuManager _menuManager;
        private readonly IRepository<Menu> _repository;

        public MenuService(MenuManager menuManager,
            IRepository<Menu> repository)

        {
            _menuManager = menuManager;
            _repository = repository;
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<MenuListDto>> GetAsync()
        {
            var menus = await _repository
                .Include(a => a.CreateUser)
                .Include(a => a.Permission)
                .OrderBy(a => a.Sort)
                .ToTreeListAsync();
            var menuDtos = Mapper.Map<List<MenuListDto>>(menus);
            return menuDtos;
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MenuDto> GetAsync(int id)
        {
            var menu = await _repository.GetAsync(id);
            return Mapper.Map<MenuDto>(menu);
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="input"></param>
        public async Task CreateAsync(CreateUpdateMenuDto input)
        {
            var menu = await _menuManager.CreateAsync(input.Name,
                input.Path,
                input.Component,
                input.Icon,
                input.ParentId,
                input.PermissionId,
                input.Sort);

            await _repository.InsertAsync(menu);

            await Cache.RemoveByKeyPrefixAsync(SystemCacheKeyPrefixDefinition.UserMenu);
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        public async Task UpdateAsync(int id, CreateUpdateMenuDto input)
        {
            var menu = await _repository.GetAsync(id);
            await _menuManager.ChangeNameAsync(menu, input.Name);
            Mapper.Map(input, menu);
            await _repository.UpdateAsync(menu);

            await Cache.RemoveByKeyPrefixAsync(SystemCacheKeyPrefixDefinition.UserMenu);
        }

        /// <summary>
        /// 设置启用/禁用状态
        /// </summary>
        /// <param name="input"></param>
        public async Task SetIsEnableAsync(SetIsEnableDto input)
        {
            var menu = await _repository.GetAsync(input.Id);
            menu.IsEnable = input.IsEnable;

            await _repository.UpdateAsync(menu);

            await Cache.RemoveByKeyPrefixAsync(SystemCacheKeyPrefixDefinition.UserMenu);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);

            await Cache.RemoveByKeyPrefixAsync(SystemCacheKeyPrefixDefinition.UserMenu);
        }
    }
}