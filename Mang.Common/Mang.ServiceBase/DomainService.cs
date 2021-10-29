using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Mang.ServiceBase
{
    public abstract class DomainService
    {
        public IServiceProvider ServiceProvider { get; set; }
        protected readonly object ServiceProviderLock = new();

        protected TService LazyGetRequiredService<TService>(ref TService reference)
        {
            if (reference == null)
            {
                lock (ServiceProviderLock)
                {
                    if (reference == null)
                    {
                        reference = ServiceProvider.GetRequiredService<TService>();
                    }
                }
            }

            return reference;
        }

        protected ILoggerFactory LoggerFactory => LazyGetRequiredService(ref _loggerFactory);
        private ILoggerFactory _loggerFactory;

        protected ILogger Logger => _lazyLogger.Value;

        private Lazy<ILogger> _lazyLogger =>
            new Lazy<ILogger>(() => LoggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance, true);

        protected void ThrowIf(bool condition, Exception exception)
        {
            if (condition) throw exception;
        }
    }
}