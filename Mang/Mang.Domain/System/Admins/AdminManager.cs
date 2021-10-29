using System.Threading.Tasks;
using Castle.Core.Internal;
using JetBrains.Annotations;
using Longbow.Security.Cryptography;
using Mang.ServiceBase;
using Mang.Infrastructure.Repository;
using Mang.Public.Util;

namespace Mang.Domain.System
{
    public class UserManager : DomainService
    {
        private readonly IRepository<Admin> _repository;

        public UserManager(IRepository<Admin> repository)
        {
            _repository = repository;
        }

        public async Task<Admin> CreateAsync(
            [NotNull] string name,
            [NotNull] string phone,
            [NotNull] string email,
            [CanBeNull] int? sort
        )
        {
            Check.NotNullOrWhiteSpace(phone, nameof(phone));
            var phoneExist = await _repository.Where(a => a.Phone == phone).AnyAsync();
            ThrowIf(phoneExist, new AdminAlreadyExistsException(phone));

            Check.NotNullOrWhiteSpace(phone, nameof(email));
            var emailExist = await _repository.Where(a => a.Email == email).AnyAsync();
            ThrowIf(emailExist, new AdminAlreadyExistsSameEmailException(email));

            var passwordSalt = LgbCryptography.GenerateSalt(); //生成密码盐

            return new Admin
            {
                PasswordSalt = passwordSalt,
                Password = LgbCryptography.ComputeHash("123456", passwordSalt), //默认密码123456
                Name = name,
                Phone = phone,
                Email = email,
                Sort = sort ?? 0,
                IsEnable = true
            };
        }

        public async Task ChangeNameAsync([NotNull] Admin user, [NotNull] string name)
        {
            Check.NotNull(user, nameof(user));
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var exists = await _repository.Where(a => a.Name == name)
                .Where(a => a.Id != user.Id)
                .AnyAsync();
            if (exists)
            {
                throw new AdminAlreadyExistsException(name);
            }

            user.ChangeName(name);
        }

        public async Task ChangePhoneAsync([NotNull] Admin user, [NotNull] string newPhone)
        {
            Check.NotNull(user, nameof(user));
            Check.NotNullOrWhiteSpace(newPhone, nameof(newPhone));

            var existingUser = await _repository.Where(a => a.Phone == newPhone).FirstAsync();
            if (existingUser != null && existingUser.Id != user.Id)
            {
                throw new AdminAlreadyExistsSamePhoneException(newPhone);
            }

            user.ChangePhone(newPhone);
        }

        public async Task ChangeEmailAsync(Admin user, string newEmail)
        {
            Check.NotNull(user, nameof(user));
            if (newEmail.IsNullOrEmpty()) return;

            var existingUser = await _repository.Where(a => a.Email == newEmail).FirstAsync();
            if (existingUser != null && existingUser.Id != user.Id)
            {
                throw new AdminAlreadyExistsSameEmailException(newEmail);
            }

            user.ChangeEmail(newEmail);
        }
    }
}