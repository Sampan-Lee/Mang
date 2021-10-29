using System.Threading.Tasks;
using Mang.ServiceBase.Interface;
using Mang.Public.Dto;

namespace Mang.Application.Contract.System.Admins
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    public interface IAdminService : ICrudService<AdminDto, AdminListDto, GetAdminListDto,
        CreateUpdateAdminDto, CreateUpdateAdminDto>
    {
        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AssignRoleAsync(AssignRoleDto input);

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <returns></returns>
        Task ResetPasswordAsync(ResetPasswordDto input);

        /// <summary>
        /// 设置用户状态
        /// </summary>
        /// <returns></returns>
        Task SetIsEnableAsync(SetIsEnableDto input);
    }
}