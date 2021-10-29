using System.Threading.Tasks;
using JetBrains.Annotations;
using Mang.ServiceBase;
using Mang.Infrastructure.Repository;
using Mang.Public.Util;

namespace Mang.Domain.System
{
    public class MenuManager : DomainService
    {
        private readonly IRepository<Menu> _repository;

        public MenuManager(IRepository<Menu> repository)
        {
            _repository = repository;
        }

        public async Task<Menu> CreateAsync(
            [NotNull] string name,
            [NotNull] string path,
            [NotNull] string component,
            [CanBeNull] string icon,
            [CanBeNull] int? parentId,
            [CanBeNull] int? permissionId,
            [CanBeNull] int? sort)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            var exists = await _repository
                .Where(a => a.ParentId == parentId)
                .Where(a => a.Name == name)
                .AnyAsync();

            ThrowIf(exists, new MenuAlreadyExsitsException(name));

            Check.NotNullOrWhiteSpace(path, nameof(path));
            Check.NotNullOrWhiteSpace(component, nameof(component));

            return new Menu
            {
                ParentId = parentId,
                PermissionId = permissionId,
                Name = name,
                Component = component,
                Icon = icon,
                Path = path,
                IsEnable = true,
                Sort = sort ?? 0
            };
        }

        public async Task ChangeNameAsync([NotNull] Menu menu, [NotNull] string name)
        {
            Check.NotNull(menu, nameof(menu));
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var exists = await _repository
                .Where(a => a.Id != menu.Id)
                .Where(a => a.Name == name)
                .Where(a => a.ParentId == menu.ParentId)
                .AnyAsync();
            
            if (exists)
            {
                throw new MenuAlreadyExsitsException(name);
            }

            menu.ChangeName(name);
        }
    }
}