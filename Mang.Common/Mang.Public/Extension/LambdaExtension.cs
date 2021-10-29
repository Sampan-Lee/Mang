using System;
using System.Collections.Generic;
using System.Linq;
using Mang.Public.Entity;

namespace Mang.Public.Extension
{
    public static class LambdaExtension
    {
        /// <summary>
        /// 将泛型集合转换为树状结构
        /// </summary>
        /// <param name="list"></param>
        /// <param name="rootId"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> ToTreeList<T>(this List<T> list, int? rootId = null) where T : class, ITreeEntity<T>
        {
            List<T> BindMenuTree(int? currentId,int level)
            {
                List<T> result = new List<T>();
                var items = list.Where(a => a.ParentId == currentId).ToList();
                if (!items.IsNullOrEmpty())
                {
                    foreach (var menu in items)
                    {
                        var nextTree = BindMenuTree(menu.Id, level + 1);
                        menu.Children = nextTree;
                        result.Add(menu);
                    }
                }

                return result;
            }
            List<T> returnList = new List<T>();
            if (list != null && list.Count(a => a.ParentId == null)==0)
            {
                var firstItem = list.OrderBy(a => a.ParentId).FirstOrDefault();
                if (firstItem != null)
                {
                    var firstList = list.Where(a => a.ParentId == firstItem.ParentId).ToList();
                    foreach (var item in firstList)
                    {
                        rootId = item?.ParentId;
                        var treeList = BindMenuTree(rootId, 1);
                        returnList.AddRange(treeList);
                    }
                }
            }
            else
            {
                returnList= BindMenuTree(rootId, 1);
            }
            return returnList;
        }

        /// <summary>
        /// 递归对树形结构数据处理---条件查询
        /// 警告⚠️： 在全部结果集里处理树形条件查询，数据量较大时不可取
        /// </summary>
        /// <param name="list"></param> 
        /// <param name="condition"></param>
        /// <param name="where"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> TreeWhereIf<T>(this List<T> list, bool condition, Func<T, bool> where)
            where T : class, ITreeEntity<T>
        {
            List<T> newarr = new List<T>();
            list.ForEach(element =>
            {
                if (condition)
                {
                    if (where(element))
                    {
                        if (!element.Children.IsNullOrEmpty())
                        {
                            var children = element.Children.TreeWhereIf(true, where);
                            element.Children = children;
                        }

                        newarr.Add(element);
                    }
                    else
                    {
                        if (!element.Children.IsNullOrEmpty())
                        {
                            var children = element.Children.TreeWhereIf(true, where);
                            element.Children = children;
                            if (!children.IsNullOrEmpty())
                            {
                                newarr.Add(element);
                            }
                        }
                    }
                }
                else
                {
                    newarr.Add(element);
                }
            });
            return newarr;
        }
    }

}