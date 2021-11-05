using System;
using System.Threading.Tasks;
using Mang.Application.Contract.Users;
using Mang.Domain.Users;
using Mang.Infrastructure.MiniProgram.Auths;
using Mang.Infrastructure.Repository;
using Mang.ServiceBase.Impl;

namespace Mang.Application.Users
{
    public class UserService : BaseService, IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly IRepository<UserPermission> _userPermissionRepository;
        private readonly IMiniProgramAuth _miniProgramAuth;

        public UserService(IRepository<User> repository,
            IRepository<UserPermission> userPermissionRepository,
            IMiniProgramAuth miniProgramAuth)
        {
            _repository = repository;
            _userPermissionRepository = userPermissionRepository;
            _miniProgramAuth = miniProgramAuth;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<UserLoginDto> LoginAsync(LoginDto input)
        {
            var session = await _miniProgramAuth.CodeToSession(input.Code);

            var user = await _repository.Where(a => a.OpenId == session.openid).ToOneAsync();
            user.NickName = input.NickName;
            user.AvatarUrl = input.AvatarUrl;
            user.CreateTime = DateTime.Now;
            user.UpdateTime = DateTime.Now;
            user.LastLoginTime = DateTime.Now;

            await _repository.InsertOrUpdateAsync(user);
            var userLoginDto = new UserLoginDto
            {
                Id = user.Id,
                NickName = user.NickName,
                AvatarUrl = user.AvatarUrl,
                Phone = user.Phone,
                IsFinishRegister = user.IsFinishRegister
            };

            return userLoginDto;
        }

        /// <summary>
        /// 补全用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<UserLoginDto> CompleteAsync(CompleteUserDto input)
        {
            var user = await _repository.GetAsync(CurrentUser.Id);

            user.WechatNum = input.WechatNum;
            user.Gender = input.Gender;
            user.AgeBracketId = input.AgeBracketId;
            user.Hobby = input.Hobby;
            user.IsFinishRegister = true;
            user.UpdateTime = DateTime.Now;
            user.LastLoginTime = DateTime.Now;

            await _repository.UpdateAsync(user);

            var userLoginDto = new UserLoginDto
            {
                Id = user.Id,
                NickName = user.NickName,
                AvatarUrl = user.AvatarUrl,
                Phone = user.Phone,
                IsFinishRegister = user.IsFinishRegister
            };

            return userLoginDto;
        }
    }
}