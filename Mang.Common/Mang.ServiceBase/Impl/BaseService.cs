using System;
using AutoMapper;
using FreeSql;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using Mang.Infrastructure.DistributedCache;
using Mang.ServiceBase.Interface;
using Mang.Public.CurrentUser;
using Mang.Public.Util;
using Nest;

namespace Mang.ServiceBase.Impl
{
    /// <summary>
    /// 基础服务
    /// </summary>
    public abstract class BaseService : IBaseService
    {
        public IServiceProvider ServiceProvider { get; set; }

        protected readonly object ServiceProviderLock = new object();

        protected TService LazyGetRequiredService<TService>(ref TService reference)
            => LazyGetRequiredService(typeof(TService), ref reference);

        protected TRef LazyGetRequiredService<TRef>(Type serviceType, ref TRef reference)
        {
            if (reference == null)
            {
                lock (ServiceProviderLock)
                {
                    if (reference == null)
                    {
                        reference = (TRef)ServiceProvider.GetRequiredService(serviceType);
                    }
                }
            }

            return reference;
        }

        private ICurrentUser _currentUser;
        public ICurrentUser CurrentUser => LazyGetRequiredService(ref _currentUser);

        private IMapper _mapper;
        public IMapper Mapper => LazyGetRequiredService(ref _mapper);

        private UnitOfWorkManager _unitOfWorkManager;
        public UnitOfWorkManager UnitOfWorkManager => LazyGetRequiredService(ref _unitOfWorkManager);

        private ILogger _logger;
        public ILogger Logger => LazyGetRequiredService(ref _logger);

        public IAuthorizationService AuthorizationService => LazyGetRequiredService(ref _authorizationService);
        private IAuthorizationService _authorizationService;

        private IDistributedCache _cache;
        public IDistributedCache Cache => LazyGetRequiredService(ref _cache);

        private IElasticClient _elasticClient;
        public IElasticClient ElasticClient => LazyGetRequiredService(ref _elasticClient);

        protected void ThrowIf(bool condition, BusinessException exception)
        {
            ExceptionUtil.ThrowIf(condition, exception);
        }
    }
}