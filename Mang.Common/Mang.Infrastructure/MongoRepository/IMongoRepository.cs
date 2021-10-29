using MongoDB.Driver;

namespace Mang.Infrastructure.MongoRepository
{
    public interface IMongoRepository<TEntity>
    {
        IMongoCollection<TEntity> Collection { get; }
    }
}