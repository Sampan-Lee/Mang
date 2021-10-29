using System.Threading.Tasks;
using Mang.ServiceBase;
using JetBrains.Annotations;
using Mang.Infrastructure.Repository;
using Mang.Public.Util;

namespace Mang.Domain.System
{
    public class RoleManager : DomainService
    {
        private readonly IRepository<Role> _repository;

        public RoleManager(IRepository<Role> repository)
        {
            _repository = repository;
        }

        public async Task<Role> CreateAsync([NotNull] string name, [CanBeNull] string description,
            [CanBeNull] int? sort)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            var nameExist = await _repository.Where(a => a.Name == name).AnyAsync();
            ThrowIf(nameExist, new RoleAlreadyExistsException(name));

            return new Role
            {
                Name = name,
                Description = description,
                Sort = sort ?? 0,
                IsEnable = true
            };
        }

        public async Task ChangeNameAsync([NotNull] Role role, [NotNull] string name)
        {
            Check.NotNull(role, nameof(role));
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingRole = await _repository.Where(a => a.Name == name).FirstAsync();
            if (existingRole != null && existingRole.Id != role.Id)
            {
                throw new RoleAlreadyExistsException(name);
            }

            role.ChangeName(name);
        }
    }
}