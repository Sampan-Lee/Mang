using System.Linq;
using System.Threading.Tasks;
using FreeSql;
using Longbow.Security.Cryptography;
using Mang.Domain.System;
using Mang.Application.Contract.System.Admins;
using Mang.ServiceBase.Impl;
using Mang.Infrastructure.AOP.Transactional;
using Mang.Infrastructure.Repository;
using Mang.Public.Dto;
using Mang.Public.Extension;

namespace Mang.Application.System
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public class AdminService : CrudService<Admin, AdminDto, AdminListDto, GetAdminListDto,
            CreateUpdateAdminDto, CreateUpdateAdminDto>,
        IAdminService
    {
        private readonly UserManager _userManager;
        private readonly IRepository<AdminRole> _userRoleRepository;

        public AdminService(UserManager userManager,
            IRepository<AdminRole> userRoleRepository,
            IRepository<Admin> repository) :
            base(repository)
        {
            _userManager = userManager;
            _userRoleRepository = userRoleRepository;
        }

        /// <summary>
        /// 列表查询条件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override ISelect<Admin> CreateFilteredQuery(GetAdminListDto input)
        {
            return Repository.Include(a => a.CreateUser)
                .IncludeMany(a => a.Roles)
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), a => a.Name.Contains(input.Name))
                .WhereIf(!input.Phone.IsNullOrWhiteSpace(), a => a.Phone.Contains(input.Phone))
                .WhereIf(input.IsEnable.HasValue, a => a.IsEnable == input.IsEnable.Value);
        }

        /// <summary>
        /// 请求排序
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override ISelect<Admin> ApplySorting(ISelect<Admin> query, GetAdminListDto input)
        {
            if (input.Sort.ToInitialUpper() == nameof(AdminListDto.CreateUserName))
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
        protected override async Task<AdminListDto> MapToListDtoAsync(Admin entity)
        {
            var userListDto = await base.MapToListDtoAsync(entity);

            userListDto.Roles = entity.Roles.Select(a => a.Name).ToList();
            return userListDto;
        }

        /// <summary>
        /// 获取单个用户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<AdminDto> GetAsync(int id)
        {
            var adminUser = await Repository
                .IncludeMany(a => a.Roles)
                .Where(a => a.Id == id)
                .FirstAsync();

            var adminUserDto = Mapper.Map<AdminDto>(adminUser);
            adminUserDto.RoleIds = adminUser.Roles.Select(a => a.Id).ToList();

            return adminUserDto;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Transaction]
        public override async Task CreateAsync(CreateUpdateAdminDto input)
        {
            var user = await _userManager.CreateAsync(
                input.Name,
                input.Phone,
                input.Email,
                input.Sort
            );
            await Repository.InsertAsync(user);

            var userRole = input.RoleIds.Select(a => new AdminRole
            {
                AdminId = user.Id,
                RoleId = a
            });
            await _userRoleRepository.InsertAsync(userRole);
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        [Transaction]
        public override async Task UpdateAsync(int id, CreateUpdateAdminDto input)
        {
            var user = await Repository.GetAsync(id);
            ThrowIf(user == null, new AdminNotExistsException(id.ToString()));
            await _userManager.ChangeNameAsync(user, input.Name);
            await _userManager.ChangePhoneAsync(user, input.Phone);
            await _userManager.ChangeEmailAsync(user, input.Email);
            user.Sort = input.Sort;
            await Repository.UpdateAsync(user);

            var assignRoleDto = new AssignRoleDto
            {
                Id = id,
                RoleIds = input.RoleIds
            };
            await AssignRoleAsync(assignRoleDto);
        }

        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Transaction]
        public async Task AssignRoleAsync(AssignRoleDto input)
        {
            await _userRoleRepository.Where(a => a.AdminId == input.Id)
                .ToDelete()
                .ExecuteAffrowsAsync();

            var entities = input.RoleIds.Select(a => new AdminRole
            {
                AdminId = input.Id,
                RoleId = a
            });

            await Cache.RemoveAsync(SystemCacheKeyPrefixDefinition.UserPermission + input.Id);

            await _userRoleRepository.InsertAsync(entities);
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ResetPasswordAsync(ResetPasswordDto input)
        {
            var user = await Repository.GetAsync(input.Id);
            ThrowIf(user == null, new AdminNotExistsException(input.Id.ToString()));

            user.PasswordSalt = LgbCryptography.GenerateSalt(); //生成密码盐
            user.Password = LgbCryptography.ComputeHash(input.Password, user.PasswordSalt);

            await Repository.UpdateAsync(user);
        }

        /// <summary>
        /// 设置用户状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task SetIsEnableAsync(SetIsEnableDto input)
        {
            var user = await Repository.GetAsync(input.Id);
            ThrowIf(user == null, new AdminNotExistsException(input.Id.ToString()));
            user.IsEnable = input.IsEnable;
            await Repository.UpdateAsync(user);
        }
    }
}