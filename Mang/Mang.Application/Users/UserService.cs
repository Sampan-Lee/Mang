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
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<UserLoginDto> LoginAsync(string code)
        {
            var userLoginDto = new UserLoginDto();

            var session = await _miniProgramAuth.CodeToSession(code);
            var user = await _repository.Where(a => a.OpenId == session.openid).ToOneAsync();

            if (user == null)
            {
                user = new User
                {
                    OpenId = session.openid,
                    IsFinishRegister = false,
                    LastLoginTime = DateTime.Now
                };
                user = await _repository.InsertAsync(user);

                userLoginDto.Id = user.Id;
                userLoginDto.IsFinishRegister = false;
            }
            else
            {
                var userPermission = await _userPermissionRepository.Where(a => a.UserId == user.Id).ToOneAsync();
                userLoginDto.Id = user.Id;
                userLoginDto.NickName = user.NickName;
                userLoginDto.AvatarUrl = user.AvatarUrl;
                userLoginDto.Phone = user.Phone;
                userLoginDto.IsVip = userPermission.IsVip;
                userLoginDto.IsFinishRegister = user.IsFinishRegister;
            }

            return userLoginDto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> RegisterAsync(RegisterUserDto input)
        {
            throw new NotImplementedException();
        }
    }
}