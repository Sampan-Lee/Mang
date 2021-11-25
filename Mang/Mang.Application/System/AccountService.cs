using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Longbow.Security.Cryptography;
using Mang.Domain.System;
using Mang.Application.Contract.System.Accounts;
using Mang.Application.Contract.System.Permissions;
using Mang.Application.Contract.System.Admins;
using Mang.Infrastructure.Repository;
using Mang.Infrastructure.Sms;
using Mang.Public.Extension;
using Mang.Public.Util;
using Mang.ServiceBase.Impl;

namespace Mang.Application.System
{
    /// <summary>
    /// 管理员用户授权认证
    /// </summary>
    public class AccountService : BaseService, IAccountService
    {
        private const string AdminLoginCaptchaCacheKey = "admin_login_captcha_";
        private readonly IRepository<Admin> _userRepository;
        private readonly IRepository<Menu> _menuRepository;
        private readonly IAdminService _userService;
        private readonly IPermissionService _permissionService;
        private readonly ISmsService _smsService;

        public AccountService(IRepository<Admin> userRepository,
            IRepository<Menu> menuRepository,
            IAdminService userService,
            IPermissionService permissionService,
            ISmsService smsService)
        {
            _userRepository = userRepository;
            _permissionService = permissionService;
            _menuRepository = menuRepository;
            _userService = userService;
            _smsService = smsService;
        }

        /// <summary>
        /// 发送登录验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task<bool> SendCaptchaAsync(string phone)
        {
            var cacheKey = AdminLoginCaptchaCacheKey + phone;
            var exist = await Cache.ExistAsync(cacheKey);
            ThrowIf(exist, new BusinessException("验证码有效期5分钟，请勿重复发送"));

            var smsType = (SmsType)Appsettings.app("AppSettings", "Sms", "SmsType").ToInt();
            var captcha = smsType == SmsType.DevSms ? "666666" : new Random().Next(100000, 999999).ToString();
            await Cache.SetAsync(cacheKey, captcha, TimeSpan.FromMinutes(5));

            var send = await _smsService.SendAsync(phone, captcha);
            return send;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AccountDto> LoginAsync(LoginDto input)
        {
            Admin user;
            if (input.Type == LoginType.Account)
            {
                user = await _userRepository
                    .Where(a => a.Phone == input.Identifer)
                    .ToOneAsync();

                ThrowIf(user == null, new AdminNotExistsException(input.Identifer));
                var password = LgbCryptography.ComputeHash(input.Credential, user.PasswordSalt);
                ThrowIf(user.Password != password, new PasswordWrongException());
            }
            else
            {
                user = await _userRepository
                    .Where(a => a.Phone == input.Identifer)
                    .FirstAsync();
                ThrowIf(user == null, new AdminNotExistsException(input.Identifer));

                var cacheKey = SystemCacheKeyPrefixDefinition.LoginCaptcha + input.Identifer;
                var captcha = await Cache.GetAsync(cacheKey);
                ThrowIf(string.IsNullOrWhiteSpace(captcha), new UnGetCaptchaException());
                ThrowIf(captcha != input.Credential, new CaptchaWrongException());
            }

            return Mapper.Map<AccountDto>(user);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<AdminDto> GetUserAsync()
        {
            return await _userService.GetAsync(CurrentUser.Id);
        }

        /// <summary>
        /// 获取用户菜单,只支持三级
        /// </summary>
        /// <returns></returns>
        public async Task<List<AccountMenuDto>> GetMenuAsync()
        {
            var cacheKey = SystemCacheKeyPrefixDefinition.UserMenu + CurrentUser.Id;
            var result = await Cache.GetAsync<List<AccountMenuDto>>(cacheKey);

            if (result == null)
            {
                var permission = await _permissionService.GetAsync(CurrentUser.Id);

                if (permission.IsNullOrEmpty()) return null;

                var permissionIds = permission.Where(a => !a.ParentId.HasValue)
                    .Select(a => a.Id)
                    .ToList();

                // var menus = await _menuRepository
                //     .Where(a => permissionIds.Contains(a.PermissionId.Value)
                //                 || (!a.PermissionId.HasValue && a.Id == 1)
                //                 || a.Children.AsSelect().Any(b =>
                //                     !b.PermissionId.HasValue || permissionIds.Contains(b.PermissionId.Value))
                //                 || a.Children.AsSelect().Any(c =>
                //                     c.Children.AsSelect().Any(d =>
                //                         !d.PermissionId.HasValue || permissionIds.Contains(d.PermissionId.Value)))
                //     )
                //     .OrderBy(a => a.Sort)
                //     .ToTreeListAsync();

                var menus = await _menuRepository
                    .Where(a => !a.ParentId.HasValue && !a.PermissionId.HasValue && !a.Children.AsSelect().Any()
                                || !a.PermissionId.HasValue && a.Children.AsSelect().Any(b =>
                                    !b.PermissionId.HasValue || permissionIds.Contains(b.PermissionId.Value))
                                || permissionIds.Contains(a.PermissionId.Value) ||
                                a.ParentId.HasValue && !a.PermissionId.HasValue
                    )
                    .Where(a => a.IsEnable)
                    .OrderBy(a => a.Sort)
                    .ToListAsync();

                result = Mapper.Map<List<AccountMenuDto>>(menus.ToTreeList());

                await Cache.SetAsync(cacheKey, result);
            }

            return result;
        }

        /// <summary>
        /// 获取用户操作权限
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetPermissionAsync()
        {
            var permission = await _permissionService.GetAsync(CurrentUser.Id);
            return permission.Select(a => a.Name).ToList();
        }

        /// <summary>
        /// 校验账号权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        public async Task<bool> CheckPermissionAsync(int userId, string permission)
        {
            var permissions = await _permissionService.GetAsync(userId);
            var result = permissions?.Any(a => a.Name == permission) ?? false;
            return result;
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        public async Task LogoutAsync()
        {
            await Cache.RemoveAsync(SystemCacheKeyPrefixDefinition.UserPermission + CurrentUser.Id);
            await Cache.RemoveAsync(SystemCacheKeyPrefixDefinition.UserMenu + CurrentUser.Id);
        }
    }
}