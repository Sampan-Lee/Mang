using AutoMapper;
using Mang.Application.Contract.System;
using Mang.Application.Contract.System.Accounts;
using Mang.Application.Contract.System.Files;
using Mang.Application.Contract.System.Logs;
using Mang.Application.Contract.System.Menus;
using Mang.Application.Contract.System.Permissions;
using Mang.Application.Contract.System.Roles;
using Mang.Application.Contract.System.Admins;
using Mang.Domain.System;

namespace Mang.Application.System
{
    public class SystemAutoMapperProfile : Profile
    {
        public SystemAutoMapperProfile()
        {
            #region 登录用户

            CreateMap<Domain.System.Admin, AccountDto>();
            CreateMap<Menu, AccountMenuDto>().ForMember(dest => dest.Title,
                opt => opt.MapFrom(src => src.Name));

            #endregion

            #region 用户

            CreateMap<Admin, AdminDto>();
            CreateMap<Admin, AdminListDto>();
            CreateMap<CreateUpdateAdminDto, Admin>();
            CreateMap<CreateUpdateAdminDto, Admin>();

            #endregion

            #region 角色

            CreateMap<Role, RoleDto>();
            CreateMap<Role, RoleListDto>();
            CreateMap<CreateUpdateRoleDto, Role>();
            CreateMap<Permission, RolePermissionDto>();

            #endregion

            #region 菜单

            CreateMap<Menu, MenuListDto>().ForMember(dest => dest.PermissionName,
                opt => opt.MapFrom(src => src.Permission.DisplayName)
            );
            CreateMap<Menu, MenuDto>();
            CreateMap<CreateUpdateMenuDto, Menu>();

            #endregion

            #region 权限

            CreateMap<Permission, PermissionDto>();

            #endregion

            #region 日志

            CreateMap<Log, LogDto>();
            CreateMap<GetLogListDto, Log>();
            CreateMap<Log, LogListDto>();

            #endregion

            #region 文件

            CreateMap<File, FileDto>();

            #endregion
        }
    }
}