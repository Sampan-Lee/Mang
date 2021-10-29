using System;
using System.Collections.Generic;
using NLog.Config;
using Game.Common.Log;

namespace Mang.Web.Extension.Dependency
{
    public static class NlogDependency
    {
        private static Dictionary<string, Type> LayoutRenderers => new Dictionary<string, Type>()
        {
            {"traceId", typeof(TraceIdLayoutRenderer)},
            {"clientip", typeof(ClientIPLayoutRenderer)},
            {"requesturl", typeof(RequestUrlLayoutRenderer)},
            {"userid", typeof(UserIdLayoutRenderer)},
        };

        /// <summary>
        /// 注册模版页
        /// </summary>
        public static void AddNloglayout()
        {
            foreach (var item in LayoutRenderers)
            {
                ConfigurationItemFactory
                    .Default
                    .LayoutRenderers
                    .RegisterDefinition(item.Key, item.Value);
            }
        }
    }
}