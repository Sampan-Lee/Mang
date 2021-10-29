using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Mang.Public.Dto;
using MongoDB.Driver;
using MongoDB.Driver.Core.Misc;
using MongoDB.Driver.Linq;

namespace Mang.Public.Extension
{
    public static class IMongoQueryableExtension
    {
        public static IMongoQueryable<T> WhereIf<T>(this IMongoQueryable<T> queryable, bool condition,
            Expression<Func<T, bool>> predicate)
        {
            if (queryable == null)
                throw new ArgumentNullException(nameof(queryable));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            return condition ? queryable.Where(predicate) : queryable;
        }

        public static IMongoQueryable<T> Page<T>(this IMongoQueryable<T> queryable,
            int index, int size)
        {
            return queryable.Skip((index - 1) * size).Take(size);
        }

        public static IAggregateFluent<TResult> MatchIf<TResult>(
            this IAggregateFluent<TResult> aggregate,
            bool condition,
            Expression<Func<TResult, bool>> filter)
        {
            Ensure.IsNotNull<IAggregateFluent<TResult>>(aggregate, nameof(aggregate));
            return condition
                ? aggregate.AppendStage<TResult>(PipelineStageDefinitionBuilder.Match<TResult>(filter))
                : aggregate;
        }

        public static async Task<PageResultDto<TResult>> ToPageListAsync<TResult>(
            this IAggregateFluent<TResult> aggregate,
            GetPageDto input) where TResult : class
        {
            Ensure.IsNotNull(aggregate, nameof(aggregate));

            var total = aggregate.Count().FirstOrDefault()?.Count;

            var result = new PageResultDto<TResult> { Total = total ?? 0 };

            if (result.Total > 0)
            {
                result.Data = await aggregate.Skip((input.Index - 1) * input.Size).Limit(input.Size).ToListAsync();
            }

            return result;
        }
    }
}