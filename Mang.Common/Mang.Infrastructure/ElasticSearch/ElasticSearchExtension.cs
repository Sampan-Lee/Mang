using System;
using Nest;

namespace Mang.Infrastructure.ElasticSearch
{
    public static class ElasticSearchExtension
    {
        /// <summary>
        /// 统计新、老用户脚本
        /// </summary>
        /// <param name="userCategory">用户类型（NewUser:新用户；OldUser老用户）</param>
        /// <param name="field">DateTime字段，与用户创建时间比较的业务时间字段</param>
        /// <returns></returns>
        public static Func<QueryContainerDescriptor<object>, QueryContainer> QueryByUserCategoryScript(
            StatUserCategory userCategory,
            string field)
        {
            var userQueryFunc = userCategory == StatUserCategory.New ? "equals" : "isAfter";

            QueryContainer UserQuery(QueryContainerDescriptor<object> u) =>
                u.Script(d => d.Script(a => a.Source($@"
                                    ZonedDateTime compareTime =
                                        ZonedDateTime.of(doc['{field}'].value.year,
                                            doc['{field}'].value.monthOfYear, doc['{field}'].value.dayOfMonth, 0, 0, 0, 0,
                                            ZoneId.of('Z'));
                                    ZonedDateTime registerTime =
                                        ZonedDateTime.of(doc['data.registerTime'].value.year,
                                            doc['data.registerTime'].value.monthOfYear, doc['data.registerTime'].value.dayOfMonth,
                                            0, 0, 0, 0, ZoneId.of('Z'));
                                    return compareTime.{userQueryFunc}(registerTime);
                                    ")
                    )
                );

            return UserQuery;
        }
    }
}